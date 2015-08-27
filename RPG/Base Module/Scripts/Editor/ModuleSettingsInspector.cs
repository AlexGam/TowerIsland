using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ModuleSettings))]
public class ModuleSettingsInspector : Editor {
	public override void OnInspectorGUI ()
	{
		serializedObject.Update ();
		DrawProperties ("m_Script","version","settingsName");
		serializedObject.ApplyModifiedProperties ();
	}

	protected void DrawProperties(params string[] properties){
		DrawProperties (serializedObject, properties);
	}
	
	protected void DrawProperties(SerializedObject obj,params string[] properties){
		for (int i=0; i<properties.Length; i++) {
			SerializedProperty property=obj.FindProperty(properties[i]);
			if(property != null){
				EditorGUILayout.PropertyField(obj.FindProperty(properties[i]));
			}
		}
	}
	
	protected SerializedProperty DrawProperty(string propertyPath){
		return DrawProperty (serializedObject, propertyPath);
	}
	
	protected SerializedProperty DrawProperty(SerializedObject obj,string propertyPath){
		SerializedProperty property = obj.FindProperty (propertyPath);
		if (property != null) {
			EditorGUILayout.PropertyField (property);
		}
		return property;
	}
}
