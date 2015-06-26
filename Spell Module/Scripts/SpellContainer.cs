using UnityEngine;
using System.Collections;
using ICode;

public class SpellContainer : UIContainer {
	public SpellSlot slot;

	public override void Start ()
	{
		slot.gameObject.SetActive(false);
		base.Start ();
	}

	public override bool Add (BaseItem item)
	{
		if (item != null) {
			slot.gameObject.SetActive(true);
			GameObject go = Instantiate(slot.gameObject) as GameObject;
			go.transform.SetParent(transform,false);
			SpellSlot mSlot=go.GetComponent<SpellSlot>();
			slots=ArrayUtility.Add<UISlot>(slots,mSlot);
			mSlot.Initialize(slots.Length-1,this);
			Replace(mSlot.id,item);
			slot.gameObject.SetActive(false);
			return true;
		}
		return false;
	}

	public override bool HasFreeSlots ()
	{
		return true;
	}
}
