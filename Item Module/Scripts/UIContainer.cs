using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(GridLayoutGroup))]
public class UIContainer: MonoBehaviour{
	public int id;
	[SettingsSelect]
	public ContainerSettings settings;
	protected UISlot[] slots;
	private bool skipSaving;

	private List<BaseItem> items = new List<BaseItem> ();
	public List<BaseItem> Items { 
		get { 
			while (items.Count < slots.Length){ 
				items.Add(null); 
			}
			return items; 
		} 
	}

	public virtual void Start(){
		slots = GetComponentsInChildren<UISlot> (false);
		for (int i=0; i< slots.Length; i++) {
			UISlot mSlot=slots[i];
			mSlot.Initialize(i,this);
		}
		if (settings.save) {
			Invoke("Load",1);
			InvokeRepeating("Save",settings.saveInterval,settings.saveInterval);
		}
	}

	public virtual BaseItem Replace (int id, BaseItem item)
	{
		if (id < slots.Length){
			BaseItem prev = Items[id];
			Items[id] = item;
			return prev;
		}
		return item;
	}

	public virtual bool Add(BaseItem item){
		for (int i=0; i < slots.Length; i++) {
			if(Items[i] == null){
				Replace(i,item);
				return true;
			}
		}
		return false;
	}

	public virtual bool HasFreeSlots(){
		for (int i=0; i < slots.Length; i++) {
			if(Items[i] == null){
				return true;
			}
		}
		return false;
	}

	public BaseItem GetItem (int id) { 
		return (id < Items.Count) ? Items[id] : null; 
	}
	
	public BaseItem GetItem(string name){
		return Items.Find(x=>x != null && x.itemName == name);
	}

	public UISlot GetSlot(BaseItem item){
		for (int i=0; i<slots.Length; i++) {
			if(slots[i].observedItem == item){
				return slots[i];
			}
		}
		return null;
	}

	public BaseItem[] GetItems(string name){
		return Items.FindAll (x => x != null && x.itemName == name).ToArray();
	}

	public virtual void Clear(){
		for (int i=0; i< slots.Length; i++) {
			Replace(i,null);
		}
	}

	public virtual string Serialize(){
		string itemData = string.Empty;
		for (int i=0; i<Items.Count; i++) {
			BaseItem item=Items[i];
			if(item != null){
				itemData+= i.ToString()+";"+item.Serialize()+"/";
			}
		}
		return itemData;
	}
	
	public virtual void Deserialize(string data){
		string[] split = data.Split ('/');
		ItemDatabase database = ItemDatabase.Load ();
		foreach (string itemSplit in split) {
			if(!string.IsNullOrEmpty(itemSplit)){
				string[] itemDataSplit=itemSplit.Split(';');
				int slot=System.Convert.ToInt32(itemDataSplit[0]);
				string itemName=itemDataSplit[1];

				BaseItem item=database.GetItem(itemName.Trim());
				if(item != null){
					BaseItem mItem=(BaseItem)ScriptableObject.Instantiate(item);
					mItem.Deserialize(itemDataSplit);
					Replace(slot,mItem);
				}
			}		
		}
	}

	public virtual void Save(){
		if (!string.IsNullOrEmpty (ModulePrefs.User.player.name)) {
			if(settings.saveLocal){
				SaveInternalPrefs(ModulePrefs.User.player.name);
			}else{
				StartCoroutine (SaveInternal (ModulePrefs.User.player.name));
			}
		}
	}

	private void SaveInternalPrefs(string playerName){
		PlayerPrefs.SetString ("Container" + id + playerName, Serialize ());
	}

	private IEnumerator SaveInternal(string playerName){
		if (skipSaving) {
			yield break;
		}
		WWWForm newForm = new WWWForm ();
		newForm.AddField ("name", playerName);
		newForm.AddField ("id", id);
		newForm.AddField ("itemData", Serialize());
		
		WWW w = new WWW (settings.serverAddress + "/"+settings.saveScript, newForm);
		while (!w.isDone) {
			yield return new WaitForEndOfFrame();
		}
		
		if (w.error != null) {
			Debug.LogError (w.error);
		}
	}

	public virtual void Load(){
		if (!string.IsNullOrEmpty (ModulePrefs.User.player.name)) {
			if(settings.saveLocal){
				LoadInternalPrefs (ModulePrefs.User.player.name);
			}else{
				StartCoroutine (LoadInternal (ModulePrefs.User.player.name));
			}
		}
	}

	
	private void LoadInternalPrefs(string playerName){
		string res=PlayerPrefs.GetString ("Container" + id + playerName);
		Deserialize (res);
	}

	private IEnumerator LoadInternal(string playerName){
		skipSaving = true;
		WWWForm newForm = new WWWForm ();
		newForm.AddField ("name", playerName);
		newForm.AddField ("id", id);
		WWW w = new WWW (settings.serverAddress + "/"+settings.loadScript, newForm);
		while (!w.isDone) {
			yield return new WaitForEndOfFrame();
		}
		
		if (w.error != null) {
			Debug.LogError (w.error);
		}
		
		string res = w.text.Trim ();
		Deserialize (res);
		skipSaving = false;
	}
}
