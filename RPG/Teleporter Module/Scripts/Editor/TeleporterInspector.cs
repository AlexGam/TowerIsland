using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Teleporter),true)]
public class TeleporterInspector : UsableItemInspector {
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		serializedObject.Update ();
		DrawProperties ("scene","position");
		serializedObject.ApplyModifiedProperties ();
	}
}
