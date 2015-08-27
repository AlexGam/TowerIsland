using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(PlayerSettings))]
public class PlayerSettingsInspector : ModuleSettingsInspector {
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		serializedObject.Update ();
		SerializedProperty saveLocal=DrawProperty("saveLocal");
		if(!saveLocal.boolValue){
			DrawProperties("serverAddress","createPlayer","loadPlayers","deletePlayer");
		}
		serializedObject.ApplyModifiedProperties ();
	}
}
