using UnityEngine;
using System.Collections;

namespace ICode.Actions.Attributes{
	[Category("RPG/Modules/Attributes")]
	[Tooltip("Apply damage.")]
	[System.Serializable]
	public class ApplyDamage : StateAction {
		[SharedPersistent]
		[Tooltip("GameObject to use.")]
		public FsmGameObject gameObject;
		[Tooltip("Attribute name.")]
		public FsmString attribute;
		[Tooltip("Damage to apply.")]
		public FsmInt damage;

		public override void OnEnter ()
		{
			AttributeHandler handler = gameObject.Value.GetComponent<AttributeHandler> ();
			if (handler != null) {
				handler.ApplyDamage(attribute.Value,damage.Value);
			}
			Finish ();
		}
	}
}
