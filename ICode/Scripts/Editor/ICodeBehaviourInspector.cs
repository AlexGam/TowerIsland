using UnityEngine;
using UnityEditor;
using System.Collections;

namespace ICode.FSMEditor{
	[CustomEditor(typeof(ICodeBehaviour))]
	public class ICodeBehaviourInspector : Editor {
		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI ();
			SerializedProperty property=serializedObject.FindProperty("stateMachine");
			bool flag = GUI.enabled;
			GUI.enabled=!(property.objectReferenceValue ==null || !EditorUtility.IsPersistent(property.objectReferenceValue));
			if (GUILayout.Button ("Bind to GameObject")) {
				serializedObject.Update ();
				StateMachine stateMachine=(StateMachine)FsmUtility.Copy((StateMachine)property.objectReferenceValue);
				property.objectReferenceValue=stateMachine;
				serializedObject.ApplyModifiedProperties();
				if(FsmEditor.instance != null){
					FsmEditor.SelectStateMachine(stateMachine);
				}
			}
			GUI.enabled = flag;
			if (GUILayout.Button ("Open in Editor")) {
				FsmEditor.ShowWindow ();
				if(FsmEditor.instance != null){
					FsmEditor.SelectStateMachine((StateMachine)property.objectReferenceValue);
				}
			}
		}
	}
}