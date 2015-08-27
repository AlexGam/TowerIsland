using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class AttributeHandler : MonoBehaviour {
	[SettingsSelect]
	public AttributeSettings settings;
	public List<ObjectAttribute> attributes;
	[SerializeField]
	private bool updateUI;
	public bool UpdateUI{
		get{
			return updateUI;
		}
		set{
			updateUI=value;
		}
	}
	public int freePoints=2;

    public Vector3 position;

	private bool skipSaving;
	private UIAttribute[] uiAttributes;

	private void Awake(){
		if (transform.root != transform) {
			AttributeHandler handler=transform.root.GetComponent<AttributeHandler>();
			for(int i=0;i< attributes.Count;i++){
				ObjectAttribute other=handler.GetAttribute(attributes[i].AttributeName);
				if(other!= null){
					other.CopyFrom(attributes[i]);
				}else{
					handler.attributes.Add(attributes[i]);
				}
			}
			Destroy(this);
		} 
	}

	private void Start(){
		if (updateUI) {
			uiAttributes = FindObjectsOfType<UIAttribute> ();
		
			for (int i=0; i<uiAttributes.Length; i++) {
				UIAttribute ui = uiAttributes [i];
				ui.Initialize (this);
				ObjectAttribute attribute = GetAttribute (ui.attribute);
				if (attribute != null) {
					attribute.OnChange.AddListener (ui.OnAttributeChange);
				}
			}
		}
		for (int i=0; i<attributes.Count; i++) {
			ObjectAttribute attribute = attributes [i];
			attribute.CurrentValue = attribute.startValue;
		}
		if (settings.save && PhotonView.Get(gameObject).isMine) {
			Invoke ("Load", 0);
			InvokeRepeating ("Save", settings.saveInterval, settings.saveInterval);
		}
		
	}

	public ObjectAttribute GetAttribute(string attributeName){
		for (int i=0; i< attributes.Count; i++) {
			if (attributes [i].AttributeName == attributeName) {
				return attributes[i];
			}
		}
		return null;
	}

	public bool IncreaseAttribute(string attributeName){
		if (freePoints <= 0) {
			return false;
		}
		ObjectAttribute attribute = GetAttribute (attributeName);
		if (attribute != null) {
			attribute.Value += 1;
			freePoints -= 1;
			return true;
		}
		return false;
	}

	public void IncreaseFreePoints(int amount){
		freePoints += amount;
	}

	public void IncreaseReference(ObjectAttribute attribute){
		ObjectAttribute attr = GetAttribute (attribute.referenceName);
		if (attr != null) {
			attr.Value+=1;
		}
	}

	public void DecreaseReference(ObjectAttribute attribute){
		ObjectAttribute attr = GetAttribute (attribute.referenceName);
		if (attr != null) {
			attr.Value-=1;
		}
	}

	public void ApplyRawEmpty(ObjectAttribute attribute){
		if (attribute != null) {
			attribute.Value=0;
		}
	}

	public void Refresh(ObjectAttribute attribute){
		attribute.Refresh ();
	}

	[RPC]
	public void ApplyRawDamage(string name, int damage){
		ObjectAttribute attribute = attributes.Find (x => x.AttributeName == name);
		if (attribute != null) {
			int mValue=attribute.Value;
			mValue-=damage;
			if(mValue <0){
				mValue=0;
			}
			if(mValue>attribute.MaxValue+attribute.TemporaryValue){
				mValue=attribute.MaxValue+attribute.TemporaryValue;
			}
			attribute.Value=mValue;
			Debug.Log("Apply Raw Damage: "+gameObject.name+" "+attribute.Value);
		}else{
			Debug.LogWarning("The attribute "+name+" could not be found.");
		}
	}

	[RPC]
	public void ApplyDamage(string name, int damage){

		ObjectAttribute attribute = attributes.Find (x => x.AttributeName == name);
		if (attribute != null) {
			int mValue=attribute.CurrentValue;
			mValue-=damage;
			if(mValue <0){
				mValue=0;
			}
			if(mValue>attribute.Value+attribute.TemporaryValue){
				mValue=attribute.Value+attribute.TemporaryValue;
			}
			attribute.CurrentValue=mValue;
			//Debug.Log("Apply Damage: "+gameObject.name+" "+attribute.AttributeName +" "+attribute.CurrentValue);
		}else{
			Debug.LogWarning("The attribute "+name+" could not be found.");
		}
	}

	public void ApplyTemporaryDamage(object[] data){
		string name = (string)data [0];
		int damage = (int)data [1];
		ObjectAttribute attribute = attributes.Find (x => x.AttributeName == name);
		if (attribute != null) {
			int mValue=attribute.TemporaryValue;
			mValue-=damage;
			if(mValue <0){
				mValue=0;
			}
			attribute.TemporaryValue=mValue;
			//Debug.Log("Apply Temporary Damage: "+gameObject.name+" "+attribute.AttributeName +" "+attribute.TemporaryValue);
		}else{
			Debug.LogWarning("The attribute "+name+" could not be found.");
		}
	}

	[RPC]
	public void ApplyTemporaryDamage(string name, int damage){
		ObjectAttribute attribute = attributes.Find (x => x.AttributeName == name);
		if (attribute != null) {
			int mValue=attribute.TemporaryValue;
			mValue-=damage;
			if(mValue <0){
				mValue=0;
			}
			attribute.TemporaryValue=mValue;
			//Debug.Log("Apply Temporary Damage: "+gameObject.name+" "+attribute.AttributeName +" "+attribute.TemporaryValue);
		}else{
			Debug.LogWarning("The attribute "+name+" could not be found.");
		}
	}

	public void Load(){
		/*if (ModulePrefs.User == null) {
			ModulePrefs.LoadUser();
		}*/
		if (!string.IsNullOrEmpty (ModulePrefs.User.player.name)) {
			if(settings.saveLocal){
				LoadInternalPrefs(ModulePrefs.User.player.name);
			}else{
				StartCoroutine (LoadInternal (ModulePrefs.User.player.name));
			}
		}
	}

	public void Save(){
		/*if (ModulePrefs.User == null) {
			ModulePrefs.LoadUser();
		}*/

		if (!string.IsNullOrEmpty (ModulePrefs.User.player.name)) {
			if(settings.saveLocal){
				SaveInternalPrefs(ModulePrefs.User.player.name);
			}
            
            else{
				StartCoroutine (SaveInternal (ModulePrefs.User.player.name));
			}
		}
	}

	private void LoadInternalPrefs(string playerName){
		string res = PlayerPrefs.GetString("Attributes"+playerName);

    //  string res2 = PlayerPrefs.GetString("Position" + playerName);
    //
    //
    //  Vector3 position = getVector3(res2);
    //
    //  Instantiate(gameObject, position, Quaternion.identity);


		if (string.IsNullOrEmpty (res)) {
			return;
		}

     //   if (string.IsNullOrEmpty(res2))
     //   {
     //       return;
     //   }

		string[] p = res.Split ('#');
		freePoints = int.Parse (p[0].Trim());
		
		string[] split = p[1].Split ('/');
		
		foreach (string itemSplit in split) {
			if(!string.IsNullOrEmpty(itemSplit)){
				string[] itemDataSplit=itemSplit.Split(';');
				if(itemDataSplit.Length>3){
					string name=itemDataSplit[0];
					int value=System.Convert.ToInt32(itemDataSplit[1]);
					int maxValue=System.Convert.ToInt32(itemDataSplit[2]);
					int curValue=System.Convert.ToInt32(itemDataSplit[3]);
					
					ObjectAttribute attribute=GetAttribute(name);
					if(attribute != null){
						attribute.SetRaw(curValue,value,maxValue);
					}
				}
			}		
		}
	}

	private IEnumerator LoadInternal(string playerName){
		skipSaving = true;
		WWWForm newForm = new WWWForm ();
		newForm.AddField ("name", playerName);
		
		WWW w = new WWW (settings.serverAddress + "/" + settings.loadScript, newForm);
		
		while (!w.isDone) {
			yield return new WaitForEndOfFrame();
		}
		
		if (w.error != null) {
			Debug.LogError (w.error);
		}

		string res = w.text.Trim ();

		if (string.IsNullOrEmpty (res)) {
			skipSaving=false;
			yield break;
		}

    //    string[] a = res.Split('|');

      //  position = getVector3(a[1]);


  //      Instantiate(gameObject, position, Quaternion.identity);

 //       Debug.Log(position);

		string[] p = res.Split ('#');
		freePoints = int.Parse (p[0].Trim());

		string[] split = p[1].Split ('/');
		
		foreach (string itemSplit in split) {
			if(!string.IsNullOrEmpty(itemSplit)){
				string[] itemDataSplit=itemSplit.Split(';');
				if(itemDataSplit.Length>3){
					string name=itemDataSplit[0];
                    int value = System.Convert.ToInt32(itemDataSplit[1]);
                    int maxValue = System.Convert.ToInt32(itemDataSplit[2]);
                    int curValue = System.Convert.ToInt32(itemDataSplit[3]);

					ObjectAttribute attribute=GetAttribute(name);
					if(attribute != null){
						attribute.SetRaw(curValue,value,maxValue);
					}
				}
			}		
		}

		skipSaving = false;
	}

public Vector3 getVector3(string rString){
    string[] temp = rString.Substring(1,rString.Length-2).Split(',');
    float x = float.Parse(temp[0]);
    float y = float.Parse(temp[1]);
    float z = float.Parse(temp[2]);
    Vector3 rValue = new Vector3(x,y,z);
    return rValue;
}

private void SaveInternalPrefs(string playerName){

   // string position = transform.position.ToString();

	string itemData = freePoints.ToString()+"#";
		
	for (int i=0; i<attributes.Count; i++) {
		ObjectAttribute item=attributes[i];
		if(item != null){
			itemData+= item.AttributeName+";"+item.Value+";"+item.MaxValue+";"+item.CurrentValue+"/";
		}
	}
	PlayerPrefs.SetString ("Attributes"+playerName,itemData);
   // PlayerPrefs.SetString("Position" + playerName, position);
}

private IEnumerator SaveInternal(string playerName){
	if (skipSaving) {
		yield break;
	}


    Debug.Log("Saving attributes for player: " + playerName + ", Level: " + attributes[2].CurrentValue);

   // string position = transform.position.ToString();

	WWWForm newForm = new WWWForm ();
	newForm.AddField ("name", playerName);
    newForm.AddField("level", attributes[2].CurrentValue);
  //  newForm.AddField("position", position);
		
	string itemData = freePoints.ToString()+"#";
		
	for (int i=0; i<attributes.Count; i++) {
		ObjectAttribute item=attributes[i];
		if(item != null){
			itemData+= item.AttributeName+";"+item.Value+";"+item.MaxValue+";"+item.CurrentValue+"/";
		}
	}
	newForm.AddField ("itemData", itemData);
		
	WWW w = new WWW (settings.serverAddress + "/"+settings.saveScript, newForm);
		
	while (!w.isDone) {
		yield return new WaitForEndOfFrame();
	}
		
	if (w.error != null) {
		Debug.LogError (w.error);
	}
}
}
