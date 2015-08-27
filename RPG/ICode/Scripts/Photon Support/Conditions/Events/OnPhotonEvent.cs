using UnityEngine;
using System.Collections;

namespace ICode.Conditions.Photon{
	[Category("Photon")]
	[System.Serializable]
	public class OnPhotonEvent : Condition {
		private bool raised;
		public PhotonNetworkingMessage type;

		public override void OnEnter ()
		{
			base.OnEnter ();
			NetworkingMessageHandler.current.RegisterListener(type.ToString(),OnRaiseEvent);
		}
		
		public override void OnExit ()
		{
			if (raised) {
				NetworkingMessageHandler.current.RemoveListener(type.ToString(),OnRaiseEvent);
			}
			raised = false;
		}
		
		private void OnRaiseEvent(ModuleEventData eventData){
			raised = true;
		}
		
		public override bool Validate ()
		{
			return raised;
		}
	}
}