using UnityEngine;
using System.Collections;

namespace ICode.Actions.Attributes{
	[Category("RPG/Modules/Item/Inventory")]
	[Tooltip("Apply damage.")]
	[System.Serializable]
	public class ReduceStack : StateAction {
		[Shared]
		[Tooltip("Item to use.")]
		public FsmObject item;
		[DefaultValue(1)]
		[Tooltip("Stack amount to reduce.")]
		public FsmInt amount;

		public override void OnEnter ()
		{
			if (item.Value is InventoryItem) {
				(item.Value as InventoryItem).stack-=amount.Value;
			}
			Finish ();
		}
	}
}
