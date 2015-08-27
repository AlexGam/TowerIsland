using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(AttributeSettings),true)]
public class AttrobuteSettingsInspector : ModuleSettingsInspector {
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		serializedObject.Update ();
		SerializedProperty save = DrawProperty ("save");
		if (save.boolValue) {
			SerializedProperty saveLocal=DrawProperty("saveLocal");
			if(!saveLocal.boolValue){
				DrawProperties("serverAddress","saveScript","saveInterval","loadScript");
			}
			
		}
		serializedObject.ApplyModifiedProperties ();
	}
}
