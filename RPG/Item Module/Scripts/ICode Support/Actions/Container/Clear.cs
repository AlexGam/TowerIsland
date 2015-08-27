using UnityEngine;
using System.Collections;

namespace ICode.Actions.ItemSystem{
	[Category("RPG/Modules/Item/Container")]
	[Tooltip("Clear the container.")]
	[System.Serializable]
	public class Clear : StateAction {
		[SharedPersistent]
		[Tooltip("GameObject that has an UIContainer component.")]
		public FsmGameObject gameObject;

		public override void OnEnter ()
		{
			UIContainer container = gameObject.Value.GetComponent<UIContainer> ();
			if (container != null) {
				container.Clear();
			}
			Finish ();
		}
	}
}
