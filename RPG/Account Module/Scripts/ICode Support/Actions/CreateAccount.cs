using UnityEngine;
using System.Collections;

namespace ICode.Actions.LoginModule{
	[Category("RPG/Modules/Login") ] 
	[System.Serializable]
	public class CreateAccount : StateAction {
		[Tooltip("The registered username.")]
		public FsmString username;
		[Tooltip("User password.")]
		public FsmString password;
		[Tooltip("User email.")]
		public FsmString email;
		[Tooltip("Event sended on success.")]
		[DefaultValue("OnCreateAccount")]
		public FsmString successEvent;
		[Tooltip("Event sended on fail.")]
		[DefaultValue("OnCreateAccountFail")]
		public FsmString failEvent;

		public override void OnEnter ()
		{
			AccountSystem.current.CreateAccount (username.Value, password.Value,email.Value,OnCreateAccount);
			Finish ();
		}

		private void OnCreateAccount(bool success){
			if (success) {
				this.Root.Owner.SendEvent (successEvent.Value, null);			
			} else {
				this.Root.Owner.SendEvent (failEvent.Value, null);
			}
		}
	}
}