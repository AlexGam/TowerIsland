using UnityEngine;
using System.Collections;

public class ShopContainer : UIContainer {
	public CartContainer cart;

	public override bool Add (BaseItem item)
	{
		cart.Clear ();
		return base.Add (item);
	}
}
