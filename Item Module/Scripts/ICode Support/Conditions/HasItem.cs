using UnityEngine;
using System.Collections;

namespace ICode.Conditions.ItemSystem{
	[Category("RPG/Modules/Item/Container")]
	[Tooltip("Checks if the container has an item.")]
	public class HasItem : Condition {
		[SharedPersistent]
		[Tooltip("GameObject with UIContainer component.")]
		public FsmGameObject gameObject;
		[Tooltip("Name of the item.")]
		public FsmString itemName;
		[Tooltip("Does the result equals this condition.")]
		public FsmBool equals;
		
		private UIContainer container;
		public override void OnEnter ()
		{
			base.OnEnter ();
			container = gameObject.Value.GetComponent<UIContainer> ();
		}
		
		public override bool Validate ()
		{
			return (container.GetItem(itemName) != null) == equals.Value;
		}
	}
}