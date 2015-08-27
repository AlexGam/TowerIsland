using UnityEngine;
using System.Collections;

namespace ICode.Actions.LoginModule{
	[Category("Modulos/Account Module") ] 
	[System.Serializable]
	public class Login : StateAction {
		[Tooltip("The registered username.")]
		public FsmString username;
		[Tooltip("User password.")]
		public FsmString password;
		[Tooltip("Event sended on success.")]
		[DefaultValue("OnLogin")]
		public FsmString successEvent;
		[Tooltip("Event sended on fail.")]
		[DefaultValue("OnLoginFail")]
		public FsmString failEvent;
		
		public override void OnEnter ()
		{
			AccountSystem.current.LoginAccount (username.Value, password.Value, OnLogin);
			Finish ();
		}

		private void OnLogin(bool success){
			if (success) {
				this.Root.Owner.SendEvent (successEvent.Value, null);			
			} else {
				this.Root.Owner.SendEvent (failEvent.Value, null);
			}
		}
	}
}