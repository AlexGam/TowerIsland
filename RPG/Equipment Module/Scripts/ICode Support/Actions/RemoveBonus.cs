using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ICode.Actions.Attributes{
	[Category("RPG/Modules/Item/Equipment")]
	[Tooltip("Apply bonus.")]
	[System.Serializable]
	public class RemoveBonus : StateAction {
		[SharedPersistent]
		[Tooltip("GameObject to use.")]
		public FsmGameObject gameObject;
		[Shared]
		[Tooltip("Item to use.")]
		public FsmObject item;
	
		public override void OnEnter ()
		{
			List<BonusAttribute> bonus = (item.Value as EquipmentItem).bonus;
			for (int i=0; i< bonus.Count; i++) {
				gameObject.Value.SendMessage("ApplyTemporaryDamage",new object[]{bonus[i].name,bonus[i].curValue},SendMessageOptions.DontRequireReceiver);
			}
			Finish ();
		}
	}
}