using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using ICode;
using StateMachine = ICode.StateMachine;

[CustomEditor(typeof(AddItemsHandler),true)]
public class AddItemsHandlerInspector : Editor {
	private ItemDatabase database;
	private ReorderableList itemList;
	private ReorderableList containerList;

	private void OnEnable(){
		database = ItemDatabase.Load ();
		itemList = new ReorderableList(serializedObject, 
		                                    serializedObject.FindProperty("items"), 
		                                    true, true, true, true);
		itemList.drawElementCallback =  (Rect rect, int index, bool isActive, bool isFocused) => {
			var element = itemList.serializedProperty.GetArrayElementAtIndex(index);
			rect.y += 2;
			rect.height=EditorGUIUtility.singleLineHeight;
			string itemName=element.objectReferenceValue!= null?(element.objectReferenceValue as UsableItem).itemName:string.Empty;
			Color color = GUI.backgroundColor;
			GUI.backgroundColor = element.objectReferenceValue == null ? Color.red : color;
			itemName=EditorGUI.TextField(rect,itemName);
			GUI.backgroundColor = color;
			BaseItem item=database.GetItem(itemName);
			element.objectReferenceValue = item;
		};
		itemList.drawHeaderCallback = (Rect rect) => {  
			EditorGUI.LabelField(rect, "Items");
		};

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
		SerializedProperty trigger = serializedObject.FindProperty ("trigger");
		EditorGUILayout.PropertyField (trigger);

		SerializedProperty execute = serializedObject.FindProperty ("execute");
		if (execute.objectReferenceValue != null) {
			StateMachine stateMachine = execute.objectReferenceValue as StateMachine;
			FsmVariable items = stateMachine.GetVariable ("Items");
			if (items == null || !(items is FsmArray)) {
				EditorGUILayout.HelpBox (stateMachine.Name + " state machine should contain a FsmArray variable with the name \"Items\"", MessageType.Error);
			}
			FsmVariable container = stateMachine.GetVariable ("Container");
			if (container == null || !(container is FsmGameObject)) {
				EditorGUILayout.HelpBox (stateMachine.Name + " state machine should contain a FsmGameObject variable with the name \"Container\"", MessageType.Error);
			}
		}
		EditorGUILayout.PropertyField (execute);
		EditorGUILayout.PropertyField (serializedObject.FindProperty("openContainer"));
		EditorGUILayout.PropertyField (serializedObject.FindProperty("clearContainer"));
		if (trigger.enumValueIndex == 1) {
			EditorGUILayout.PropertyField (serializedObject.FindProperty ("closeContainer"));
			SerializedProperty triggerTag = serializedObject.FindProperty ("triggerTag");
			triggerTag.stringValue = EditorGUILayout.TagField (triggerTag.stringValue);
		}

		EditorGUILayout.PropertyField (serializedObject.FindProperty("generateRandomData"));
		if (itemList.count == 0) {
			EditorGUILayout.HelpBox("Please add at least one item.",MessageType.Error);
		}
		itemList.DoLayoutList();
		if (containerList.count == 0) {
			EditorGUILayout.HelpBox("Please add at least one possible container id.",MessageType.Error);
		}
		containerList.DoLayoutList ();
		serializedObject.ApplyModifiedProperties ();
	}
}
