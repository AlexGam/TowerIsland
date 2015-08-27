using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class CartSlot : UISlot {
	public Text amount;

	public override void OnEndDrag()
	{
		draggedItem = null;
	}

	public override void OnDrop()
	{
		if (draggedItem != null && draggedItem.dirty) {
			InventoryItem mItem=(InventoryItem)container.GetItem(id);
			if(mItem != null && mItem.itemName == draggedItem.itemName){
				mItem.stack+=(draggedItem as InventoryItem).stack;
			}else{
				BaseItem item = Replace (draggedItem);
				draggedItem = item;
			}
			amount.text=(observedItem as InventoryItem).stack.ToString();
			(container as CartContainer).UpdateTotalPrice ();
		}
	}

	public override void OnItemUpdate ()
	{
		base.OnItemUpdate ();
		InventoryItem item = observedItem as InventoryItem;
		if (amount != null) {
			amount.text=(item != null)?item.stack.ToString():"";
		}
		(container as CartContainer).UpdateTotalPrice ();
	}
}
