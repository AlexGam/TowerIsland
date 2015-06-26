using UnityEngine;
using UnityEditor;
using System.Collections;
using ICode;
using StateMachine = ICode.StateMachine;

[CustomEditor(typeof(UsableItem),true)]
public class UsableItemInspector : BaseItemInspector {
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		serializedObject.Update ();
		SerializedProperty onUse= serializedObject.FindProperty("onUse");
		if (onUse.objectReferenceValue != null) {
			StateMachine stateMachine=onUse.objectReferenceValue as StateMachine;
			FsmVariable k= stateMachine.GetVariable("Item");
			if(k == null || !(k is FsmObject)){
				EditorGUILayout.HelpBox("On Use state machine should contain a FsmObject variable with the name \"Item\"",MessageType.Error);
			}
			FsmVariable s= stateMachine.GetVariable("Slot");
			if(s == null || !(s is FsmGameObject)){
				EditorGUILayout.HelpBox("On Use state machine should contain a FsmGameObject variable with the name \"Slot\"",MessageType.Error);
			}
		}
		DrawProperties ("onUse", "coolDown", "containerCoolDown");
		serializedObject.ApplyModifiedProperties ();
	}
}
