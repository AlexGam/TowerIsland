using UnityEngine;
using System.Collections;

namespace ICode.Actions.ItemSystem{
	[Category("RPG/Modules/Item/Slot")]
	[Tooltip("Cool down the slot/container")]
	[System.Serializable]
	public class CoolDown : StateAction {
		[SharedPersistent]
		[Tooltip("GameObject that has an UsableSlot component.")]
		public FsmGameObject gameObject;
		public FsmFloat coolDown;
		public FsmFloat globalCoolDown;

		public override void OnEnter ()
		{
			UsableSlot slot = gameObject.Value.GetComponent<UsableSlot> ();
			if (slot != null) {
				slot.CoolDown(coolDown.Value,globalCoolDown.Value);
			}
			Finish ();
		}
	}
}
