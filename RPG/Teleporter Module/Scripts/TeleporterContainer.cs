using UnityEngine;
using System.Collections;
using ICode;

public class TeleporterContainer : UIContainer {
	public TeleporterSlot slot;
	
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
			TeleporterSlot mSlot=go.GetComponent<TeleporterSlot>();
			slots=ArrayUtility.Add<UISlot>(slots,mSlot);
			mSlot.Initialize(slots.Length-1,this);
			Replace(mSlot.id,item);
			slot.gameObject.SetActive(false);
			return true;
		}
		return false;
	}

	public override void Clear ()
	{
		for (int i=0; i<this.slots.Length; i++) {
			slots[i].gameObject.SetActive(false);
		}
	}

	public override bool HasFreeSlots ()
	{
		return true;
	}
}
