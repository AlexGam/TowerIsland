using UnityEngine;
using System.Collections;

namespace ICode.Conditions.ItemSystem{
	[Category("RPG/Modules/Item/Slot")]
	[Tooltip("Checks if the slot is cooling down.")]
	public class IsCoolDown : Condition {
		[SharedPersistent]
		[Tooltip("GameObject with UsableSlot component.")]
		public FsmGameObject gameObject;
		[Tooltip("Does the result equals this condition.")]
		public FsmBool equals;
		
		private UsableSlot slot;
		public override void OnEnter ()
		{
			base.OnEnter ();
			slot = gameObject.Value.GetComponent<UsableSlot> ();
		}
		
		public override bool Validate ()
		{
			return slot.IsCoolDown == equals.Value;
		}
	}
}