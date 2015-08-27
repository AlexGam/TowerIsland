using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryContainer : UIContainer {
	public override bool Add (BaseItem item)
	{
		for (int i=0; i< slots.Length; i++) {
			InventoryItem inventoryItem=Items[i] as InventoryItem;
			if(inventoryItem != null && inventoryItem.itemName== item.itemName && (inventoryItem.stack+(item as InventoryItem).stack) <= inventoryItem.maxStack){
				inventoryItem.stack+=((InventoryItem)item).stack;
				return true;
			}
		}
		
		return base.Add (item);
	}

	public virtual int GetItemStack(string itemName){
		int stack = 0;
		for (int i=0; i< Items.Count; i++) {
			InventoryItem mItem=Items[i] as InventoryItem;
			if(mItem != null && mItem.itemName == itemName){
				stack+=mItem.stack;
			}
		}
		return stack;
	}

	public virtual bool ReduceItemStack(string itemName, int amount){
		int mStack = amount;
		for (int i=0; i< Items.Count; i++) {
			InventoryItem mItem=Items[i] as InventoryItem;
			if(mItem != null && mItem.itemName == itemName){
				int itemStack=mItem.stack;
				mItem.stack-=mStack;
				if(mItem.stack>=0){
					return true;
				}else{
					mStack-=itemStack;
				}
			}
		}
		return false;
	}
}
