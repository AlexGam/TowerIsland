using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ICode;

public class CartContainer : UIContainer {
	public Text totalPrice;
	public StateMachine processPurchase;

	public override BaseItem Replace (int id, BaseItem item)
	{
		if (id < slots.Length){
			BaseItem prev = Items[id];
			InventoryItem clone=item != null?(InventoryItem)Instantiate( item):null;
			Items[id] = clone;
			if(item != null){
				clone.dirty=true;
			}
			return prev;
		}
		return item;
	}

	public override bool Add (BaseItem item)
	{
		for (int i=0; i< slots.Length; i++) {
			InventoryItem inventoryItem=Items[i] as InventoryItem;
			if(inventoryItem != null && inventoryItem.itemName== item.itemName && (inventoryItem.stack+(item as InventoryItem).stack) <= inventoryItem.maxStack){
				inventoryItem.stack+=((InventoryItem)item).stack;
				(slots[i] as CartSlot).amount.text=inventoryItem.stack.ToString();
				UpdateTotalPrice();
				return true;
			}
		}
		
		return base.Add (item);
	}

	public void UpdateTotalPrice(){
		totalPrice.text = GetTotalPrice().ToString ();
	}

	private int GetTotalPrice(){
		int total = 0;
		for (int i=0; i< Items.Count; i++) {
			InventoryItem mItem=Items[i] as InventoryItem;
			if(mItem != null){
				total+=mItem.buyPrice*mItem.stack;
			}
		}
		return total;
	}

	public void ProcessPurchase(){
		int total = GetTotalPrice ();
		GameObject go = new GameObject ("ProcessPurchase");
		ICodeBehaviour behaviour = go.AddBehaviour(processPurchase);
		behaviour.stateMachine.SetVariable ("Price", total);
		behaviour.stateMachine.SetVariable ("Container", this.gameObject);
		behaviour.stateMachine.SetVariable ("Items", Items.FindAll(x=>x!= null).ToArray());
	}
}
