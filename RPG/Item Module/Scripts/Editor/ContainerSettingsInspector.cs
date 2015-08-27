using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ContainerSettings),true)]
public class ContainerSettingsInspector : ModuleSettingsInspector {
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
