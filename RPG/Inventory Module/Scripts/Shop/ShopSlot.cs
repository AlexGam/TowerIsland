using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ShopSlot : UISlot {
	public GameObject itemValues;
	public Text price;

	public override void OnBeginDrag()
	{
		if (observedItem != null) {
			draggedItem = observedItem;
		} 
	}

	public override void OnEndDrag()
	{
		draggedItem = null;
	}

	public override void OnDrop()
	{

	}

	public override void OnDoubleClick ()
	{
		if (observedItem != null) {
			(container as ShopContainer).cart.Add(observedItem);
		}
	}

	public override void OnItemUpdate ()
	{
		base.OnItemUpdate ();
		InventoryItem item = observedItem as InventoryItem;
		if (item != null) {
			price.text = item.buyPrice.ToString ();
			item.dirty = true;
			itemValues.SetActive (true);
		} else {
			itemValues.SetActive(false);
		}

	}
}
