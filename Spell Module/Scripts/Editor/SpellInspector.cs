using UnityEngine;
using UnityEditor;
using System.Collections;
using ICode;
using StateMachine = ICode.StateMachine;

//[CustomEditor(typeof(Spell),true)]
public class SpellInspector : BaseItemInspector {
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		serializedObject.Update ();
		DrawProperties ("onUse", "coolDown", "containerCoolDown");
		serializedObject.ApplyModifiedProperties ();
	}
}