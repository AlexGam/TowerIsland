using UnityEngine;
using System.Collections;

namespace ICode.Actions.UnityPhysics{
	[Category(Category.Physics)]
	[Tooltip("Get information when the ray intersects any collider.")]
	[HelpUrl("http://docs.unity3d.com/Documentation/ScriptReference/Physics.Raycast.html")]
	[System.Serializable]
	public class Raycast : StateAction {
		[NotRequired]
		[SharedPersistent]
		[Tooltip("Use a target instead of origin position.")]
		public FsmGameObject target;
		[NotRequired]
		[Tooltip("The starting point of the ray in world coordinates.")]
		public FsmVector3 origin;
		[Tooltip("The direction of the ray.")]
		public FsmVector3 direction;
		[Tooltip("The length of the ray.")]
		public FsmFloat distance;
		[Tooltip("Layer masks can be used selectively filter game objects for example when casting rays.")]
		public LayerMask layerMask;

		[Shared]
		[NotRequired]
		[Tooltip("The distance from the ray's origin to the impact point.")]
		public FsmFloat hitDistance;
		[Shared]
		[NotRequired]
		[Tooltip("The normal of the surface the ray hit.")]
		public FsmVector3 hitNormal;
		[Shared]
		[NotRequired]
		[Tooltip("The impact point in world space where the ray hit the collider.")]
		public FsmVector3 hitPoint;
		[Shared]
		[NotRequired]
		[Tooltip("The GameObject of the rigidbody or collider that was hit.")]
		public FsmGameObject hitGameObject;

		[Tooltip("Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			DoRaycast ();
			if (!everyFrame) {
				Finish();			
			}
		}

		public override void OnUpdate ()
		{	
			DoRaycast ();
		}

		private void DoRaycast(){
			RaycastHit hit;
			if (Physics.Raycast (FsmUtility.GetPosition(target,origin), direction.Value,out hit, distance.Value, layerMask)) {
				if(!hitDistance.IsNone)
					hitDistance.Value=hit.distance;
				if(!hitNormal.IsNone)
					hitNormal.Value=hit.normal;
				if(!hitPoint.IsNone)
					hitPoint.Value=hit.point;
				if(!hitGameObject.IsNone)
					hitGameObject.Value=hit.transform.gameObject;
			}
		}
		
	}
}