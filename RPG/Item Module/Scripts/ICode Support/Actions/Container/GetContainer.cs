using UnityEngine;
using System.Collections;

namespace ICode.Actions.ItemSystem{
	[Category("RPG/Modules/Item/Container")]
	[Tooltip("Find the container by id.")]
	[System.Serializable]
	public class GetContainer : StateAction {
		[Tooltip("Id of the container.")]
		public FsmInt id;
		[Shared]
		[Tooltip("Store the container game object.")]
		public FsmGameObject store;
		
		
		public override void OnEnter ()
		{
			UIContainer container = ItemUtility.GetContainer (id.Value);
			if(container != null){
				store.Value= container.gameObject;
			}
			Finish ();
		}
	}
}
