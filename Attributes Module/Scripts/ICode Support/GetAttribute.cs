using UnityEngine;
using System.Collections;

namespace ICode.Actions.Attributes{
	[Category("RPG/Modules/Attributes")]
	[Tooltip("Apply damage.")]
	[System.Serializable]
	public class GetAttribute : StateAction {
		[SharedPersistent]
		[Tooltip("GameObject to use.")]
		public FsmGameObject gameObject;
		[Tooltip("Attribute name.")]
		public FsmString attribute;
		[Shared]
		[NotRequired]
		[Tooltip("Store max value.")]
		public FsmInt maxValue;
		[Shared]
		[NotRequired]
		[InspectorLabel("Value")]
		[Tooltip("Store value.")]
		public FsmInt _value;
		[Shared]
		[NotRequired]
		[Tooltip("Store current value.")]
		public FsmInt curValue;
		[Shared]
		[NotRequired]
		[Tooltip("Store temp value.")]
		public FsmInt tempValue;
		[Tooltip("Stop to update the variable on exit of the state.")]
		public FsmBool stopOnExit;

		private AttributeHandler handler;
		private ObjectAttribute attr;
		public override void OnEnter ()
		{
			if (gameObject.Value == null) {
				Debug.Log (this.Root.Name);
			}
			handler = gameObject.Value.GetComponent<AttributeHandler> ();
			attr=handler.GetAttribute(attribute.Value);
			if (attr != null) {
				attr.OnChange.AddListener(DoGetAttribute);
				DoGetAttribute (attr);
			}

			Finish ();
	
		}

		public override void OnExit ()
		{
			if (attr != null && stopOnExit.Value) {
				attr.OnChange.RemoveListener(DoGetAttribute);
			}
		}

		private void DoGetAttribute(ObjectAttribute mAttribute){
			if(mAttribute != null){
				maxValue.Value=mAttribute.MaxValue;
				_value.Value=mAttribute.Value;
				curValue.Value=mAttribute.CurrentValue;
				tempValue.Value=mAttribute.TemporaryValue;
			}
		}
	}
}
