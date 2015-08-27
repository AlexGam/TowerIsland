using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SellSlot : UISlot {
	public Text amount;

	public override void OnItemUpdate ()
	{
		base.OnItemUpdate ();
		InventoryItem item = observedItem as InventoryItem;
		if (amount != null) {
			amount.text=(item != null)?item.stack.ToString():"";
		}
		(container as SellContainer).UpdateTotalPrice ();
	}

}
