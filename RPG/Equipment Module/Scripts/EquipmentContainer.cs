using UnityEngine;
using System.Collections;
using ICode;

public class EquipmentContainer : UIContainer {
	public StateMachine equip;
	public StateMachine unEquip;

	public override BaseItem Replace (int id, BaseItem item)
	{
		if (id < slots.Length){
			BaseItem prev = Items[id];
			if(prev != null && unEquip != null){
				//Un equip
				GameObject go=new GameObject("UnEquip");
				ICodeBehaviour behaviour=go.AddBehaviour(unEquip);
				behaviour.stateMachine.SetVariable("Item",prev);
			}
			if(item != null && equip != null){
				//Equip
				GameObject go=new GameObject("Equip");
				ICodeBehaviour behaviour=go.AddBehaviour(equip);
				behaviour.stateMachine.SetVariable("Item",item);

			}
			Items[id] = item;
			return prev;
		}
		return item;
	}
	
	/*public override void Deserialize (string data)
	{
		ItemDatabase database = ItemDatabase.Load ();
		string[] split = data.Split ('/');
		foreach (string itemSplit in split) {
			if(!string.IsNullOrEmpty(itemSplit)){
				string[] itemDataSplit=itemSplit.Split(';');
				if(itemDataSplit.Length>1){
					int slot=System.Convert.ToInt32(itemDataSplit[0]);
					string itemName=itemDataSplit[1];
					
					EquipmentItem item=(EquipmentItem)database.GetItem(itemName.Trim());
					if(item != null){
						EquipmentItem mItem=(EquipmentItem)ScriptableObject.Instantiate(item);
						mItem.Deserialize(itemDataSplit);
						Replace(slot,mItem);
					}
				}
			}		
		}
	}*/
}
