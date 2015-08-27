using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditorInternal;
using ICode;
using StateMachine = ICode.StateMachine;

[CustomEditor(typeof(EquipmentItem),true)]
public class EquipmentItemInspector : InventoryItemInspector {
	private ReorderableList bonusList;

	protected override void OnEnable ()
	{
		base.OnEnable ();
		bonusList = new ReorderableList(serializedObject, 
		                                     serializedObject.FindProperty("bonus"), 
		                                     true, true, true, true);
		bonusList.drawElementCallback =  (Rect rect, int index, bool isActive, bool isFocused) => {
			var element = bonusList.serializedProperty.GetArrayElementAtIndex(index);
			SerializedProperty nameProperty=element.FindPropertyRelative("name");
			SerializedProperty minProperty=element.FindPropertyRelative("minValue");
			SerializedProperty maxProperty=element.FindPropertyRelative("maxValue");
			rect.y += 2;
			rect.height=EditorGUIUtility.singleLineHeight;
			rect.width-=110f;
			EditorGUI.PropertyField(rect,nameProperty,GUIContent.none);
			rect.x+=rect.width+5;
			rect.width=50f;
			EditorGUI.PropertyField(rect,minProperty,GUIContent.none);
			rect.x+=rect.width+5;
			rect.width=50f;
			EditorGUI.PropertyField(rect,maxProperty,GUIContent.none);



		};
		bonusList.drawHeaderCallback = (Rect rect) => {  
			EditorGUI.LabelField(rect, "Attribute Bonus");
		};
	}

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		serializedObject.Update();
		DrawProperty ("equipmentRegion");
		GUILayout.Space (5f);
		EditorGUILayout.BeginVertical (EditorStyles.inspectorFullWidthMargins);
		bonusList.DoLayoutList ();
		EditorGUILayout.EndVertical ();
		GUILayout.Space (5f);
		DrawProperty ("defaultAttack");
		serializedObject.ApplyModifiedProperties ();
	}
}
