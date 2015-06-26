using UnityEngine;
using System.Collections;

namespace ICode.Actions.ItemSystem{
	[Category("RPG/Modules/Item/Container")]
	[Tooltip("Removes an item from container.")]
	[System.Serializable]
	public class Remove : StateAction {
		[NotRequired]
		[SharedPersistent]
		[Tooltip("GameObject that has an UIContainer component.")]
		public FsmGameObject container;
		[NotRequired]
		[SharedPersistent]
		[Tooltip("GameObject that has an UISlot component.")]
		public FsmGameObject slot;

		[NotRequired]
		[Shared]
		[Tooltip("Item to add.")]
		public FsmObject item;
		
		public override void OnEnter ()
		{
			UISlot mSlot = null;
			if (slot.Value != null) {
				mSlot = slot.Value.GetComponent<UISlot> ();
			} else {
				UIContainer mContainer = container.Value.GetComponent<UIContainer> ();
				mSlot=mContainer.GetSlot((BaseItem)item.Value);
			}
			
			if(mSlot!= null){
				mSlot.Replace(null);
			}
			Finish ();
		}
	}
}
