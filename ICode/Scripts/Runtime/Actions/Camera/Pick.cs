using UnityEngine;
using System.Collections;

namespace ICode.Actions.UnityCamera{
	[Category(Category.Camera)]   
	[System.Serializable]
	public  class Pick : StateAction {
		[DefaultValue(30.0f)]
		[Tooltip("Maximum distance to pick up.")]
		public FsmFloat maxDistance;
		[Tag]
		public FsmString tag;
		[Shared]
		[Tooltip("Store the game object under mouse.")]
		public FsmGameObject gameObject;

		public override void OnEnter (){
			DoPick ();
		}

		public override void OnUpdate ()
		{
			DoPick ();
		}

		private void DoPick(){
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray.origin, ray.direction,out hit, maxDistance.Value)) 
			{
				if(hit.collider.tag == tag.Value)
				{
					GameObject go = hit.collider.gameObject;
					gameObject.Value=go;
				}
			}
		}
	}
}