using UnityEngine;
using System.Collections;

namespace ICode.Actions.ItemSystem{
	[Category("RPG/Modules/Item/Container")]
	[Tooltip("Get an item from container.")]
	[System.Serializable]
	public class GetItem : StateAction {
		[SharedPersistent]
		[Tooltip("GameObject that has an UIContainer component.")]
		public FsmGameObject gameObject;
		[Tooltip("Item to get.")]
		public FsmString itemName;
		[Shared]
		[Tooltip("Store the item.")]
		public FsmObject store;


		public override void OnEnter ()
		{
			UIContainer container = gameObject.Value.GetComponent<UIContainer> ();

			if (container != null) {
				store.Value=container.GetItem(itemName.Value);
			}
			Finish ();
		}
	}
}
