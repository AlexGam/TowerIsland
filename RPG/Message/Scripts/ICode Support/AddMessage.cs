using UnityEngine;
using System.Collections;

namespace ICode.Actions{
	[Category("RPG/Modules/Message")]
	[Tooltip("Adds a new message text to a MessageHandler.")]
	[System.Serializable]
	public class AddMessage : StateAction {
		[Tooltip("Id of the MessageHandler.")]
		public FsmInt id;
		[Tooltip("Message to print.")]
		public FsmString message;
		[Tooltip("Color of the text.")]
		public FsmColor color;
		
		public override void OnEnter ()
		{
			MessageHandler handler = MessageHandler.Get (id.Value);
			if (handler != null) {
				handler.Add(message.Value,color.Value);
			}
			Finish ();
		}
	}
}
