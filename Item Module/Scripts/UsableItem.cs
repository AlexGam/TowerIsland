using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class UsableItem : BaseItem {
	public ICode.StateMachine onUse;
	public float coolDown;
	public float containerCoolDown;
}
