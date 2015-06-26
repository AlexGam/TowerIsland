using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(SettingsSelectAttribute),true)]
public class SettingsSelectDrawer : PropertyDrawer {
	private SettingsDatabase database;
	private Rect position;
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		this.position = position;
		if (database == null) {
			database=SettingsDatabase.Load();
		}
		List<string> settings = database.items.Where (x => x.GetType () == this.fieldInfo.FieldType).Select(y=>y.settingsName).ToList();
		if (settings.Count > 0) {
			int index = 0; 
			if (property.objectReferenceValue != null) {
				ModuleSettings moduleSettings = property.objectReferenceValue as ModuleSettings;
				index = settings.FindIndex (x => x == moduleSettings.settingsName);
			}
			index = EditorGUI.Popup (position, "Settings", index, settings.ToArray ());
			property.objectReferenceValue = database.GetItem (settings [index]);
		} else {
			EditorGUI.HelpBox(position,fieldInfo.FieldType.Name+" not found, please create one using the ProjectEditor.",MessageType.Error);
		}
	}

	public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
	{
		if (database == null) {
			database=SettingsDatabase.Load();
		}
		List<string> settings = database.items.Where (x => x.GetType () == this.fieldInfo.FieldType).Select(y=>y.settingsName).ToList();
		float height=((GUIStyle)"HelpBox").CalcHeight(new GUIContent(fieldInfo.FieldType.Name+ " not found, please create one using the ProjectEditor."),position.width);
		return settings.Count>0?base.GetPropertyHeight (property, label):Mathf.Clamp(height,height+17,1000);
	}
}
