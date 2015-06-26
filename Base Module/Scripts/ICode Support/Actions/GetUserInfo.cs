using UnityEngine;
using System.Collections;

namespace ICode.Actions.BaseModule{
	[Category("RPG/Modules")]
	[Tooltip("Get the information of the user stored in ModulePrefs.")]
	[System.Serializable]
	public class GetUserInfo : StateAction {
		[NotRequired]
		[Shared]
		[Tooltip("Store the name of the user.(Account)")]
		public FsmString username;
		[NotRequired]
		[Shared]
		[Tooltip("Store the name of the player.")]
		public FsmString playerName;
		[NotRequired]
		[Shared]
		[Tooltip("Store the level of the player.")]
		public FsmInt playerLevel;
		[NotRequired]
		[Shared]
		[Tooltip("Store the custom data of the player.")]
		public FsmString custom;

		public override void OnEnter ()
		{
			if (ModulePrefs.User == null) {
				ModulePrefs.LoadUser();
			}
			username.Value = ModulePrefs.User.name;
			playerName.Value = ModulePrefs.User.player.name;
			playerLevel.Value = ModulePrefs.User.player.level;
			custom.Value = ModulePrefs.User.player.custom;

			Finish ();
		}
	}
}