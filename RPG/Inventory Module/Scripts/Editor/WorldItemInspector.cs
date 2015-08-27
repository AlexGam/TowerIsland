using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;
using System.Linq;
using System.Collections;

[CustomEditor(typeof(WorldItem))]
public class WorldItemInspector : Editor {

	private SerializedProperty itemProperty;
	private ReorderableList containerList;


	private void OnEnable(){
		itemProperty = serializedObject.FindProperty ("item");

		containerList = new ReorderableList(serializedObject, 
		                           serializedObject.FindProperty("containerIds"), 
		                           true, true, true, true);
		containerList.drawElementCallback =  (Rect rect, int index, bool isActive, bool isFocused) => {
			var element = containerList.serializedProperty.GetArrayElementAtIndex(index);
			rect.y += 2;
			rect.height=EditorGUIUtility.singleLineHeight;
			EditorGUI.PropertyField(rect,element, GUIContent.none);

		};
		containerList.drawHeaderCallback = (Rect rect) => {  
			EditorGUI.LabelField(rect, "Possible Containers");
		};

	}


	public override void OnInspectorGUI ()
	{
		GUILayout.Space (5);
		serializedObject.Update ();
		EditorGUIUtility.labelWidth = 80;
		EditorGUILayout.PropertyField (itemProperty);
		containerList.DoLayoutList ();
		serializedObject.ApplyModifiedProperties ();
	}
}
