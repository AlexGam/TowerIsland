﻿using UnityEngine;
using System.Collections;

namespace ICode.Actions.Photon{
	[Category("Photon/PhotonNetwork")]   
	[Tooltip("Gets the PhotonRoom information.")]
	[System.Serializable]
	public class GetRoom : StateAction {
		[Shared]
		[Tooltip( "The room to use.")]
		public FsmRoom room;
		[NotRequired]
		[Shared]
		[InspectorLabel("Name")]
		[Tooltip( "Store the room name.")]
		public FsmString _name;
		[NotRequired]
		[Shared]
		[Tooltip( "Store the room name.")]
		public FsmInt playerCount;

		[Tooltip("Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			RoomInfo roomInfo = room.Value;
			if (roomInfo != null) {
				_name.Value = roomInfo.name;
				playerCount.Value=roomInfo.playerCount;

			}
			if (!everyFrame) {
				Finish ();
			}
		}
		
		public override void OnUpdate ()
		{
			RoomInfo roomInfo = room.Value;
			if (roomInfo != null) {
				_name.Value = roomInfo.name;
				playerCount.Value=roomInfo.playerCount;
			}
		}
	}
}