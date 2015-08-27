using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using ICode.FSMEditor;

[CustomEditor(typeof(AttributeHandler),true)]
public class AttributeHandlerInspector : Editor {

	protected SerializedProperty list;
	protected ReorderableList mList;
	
	protected virtual void OnEnable(){
		if ((target as AttributeHandler).transform.root.GetComponent<AttributeHandler> ()== null) {
			(target as AttributeHandler).transform.root.gameObject.AddComponent<AttributeHandler>();
		}
		list = serializedObject.FindProperty ("attributes");
		if ((target as AttributeHandler).attributes == null) {
			(target as AttributeHandler).attributes= new List<ObjectAttribute>();
		}
		mList = new ReorderableList ((target as AttributeHandler).attributes, "Attributes", false, true){
			drawElementCallback = new ReorderableList.ElementCallbackDelegate (this.OnDrawElement),
			onAddCallback= new ReorderableList.AddCallbackDelegate(this.AddElement)
		};
	}
	
	protected virtual void AddElement(){
		serializedObject.Update ();
		list.InsertArrayElementAtIndex (list.arraySize);
		
		serializedObject.ApplyModifiedProperties ();
		OnEnable ();
	}
	
	protected virtual void OnDrawElement(int index, bool selected){
		SerializedProperty property=list.GetArrayElementAtIndex(index);
		
		SerializedProperty attributeName=property.FindPropertyRelative("attributeName");
		
		SerializedProperty maxValue=property.FindPropertyRelative("maxValue");
		SerializedProperty value=property.FindPropertyRelative("value");
		SerializedProperty referenced = property.FindPropertyRelative ("referenced");
		
		GUILayout.BeginVertical("box");
		bool state = EditorPrefs.GetBool ("AttributesEditor" + index);
		EditorGUI.indentLevel += 1;
		GUILayout.BeginHorizontal ();
		state = EditorGUILayout.Foldout (state, attributeName.stringValue);
	
		if (GUILayout.Button (EditorGUIUtility.FindTexture ("Toolbar Minus"), "label", GUILayout.Width (20))) {
			list.DeleteArrayElementAtIndex (index);
			serializedObject.ApplyModifiedProperties ();
			OnEnable ();
			return;
		}
		GUILayout.EndHorizontal ();

		EditorGUI.indentLevel -= 1;
		EditorPrefs.SetBool ("AttributesEditor" + index, state);
		if (state) {
		
			attributeName.stringValue = EditorGUILayout.TextField ("Name", attributeName.stringValue);
			
			EditorGUILayout.PropertyField (referenced);
			EditorGUILayout.PropertyField (property.FindPropertyRelative ("startValue"));
			if (!referenced.boolValue) {
				EditorGUILayout.PropertyField (maxValue);
			} else {
				SerializedProperty referenceName = property.FindPropertyRelative ("referenceName");
				SerializedProperty reference = property.FindPropertyRelative ("reference");
				string[] names = (target as AttributeHandler).attributes.Select (x => x.AttributeName).ToArray ();
				int k = names.ToList ().FindIndex (x => x == referenceName.stringValue);
				k = EditorGUILayout.Popup ("Reference", k, names);
				SerializedProperty multiplier = property.FindPropertyRelative ("multiplier");
				EditorGUILayout.PropertyField (multiplier);
				EditorGUILayout.PropertyField (reference);
				if (k >= 0 && k < names.Length) {
					referenceName.stringValue = names [k];
					ObjectAttribute referencedAttribute = (target as AttributeHandler).GetAttribute (referenceName.stringValue);
					ObjectAttribute thisAttribute = (target as AttributeHandler).GetAttribute (attributeName.stringValue);
					maxValue.intValue = (int)(thisAttribute.reference.Evaluate ((float)referencedAttribute.Value / (float)referencedAttribute.MaxValue) * 100 * multiplier.floatValue);
				}
			}
			value.intValue = (int)EditorGUILayout.IntSlider ((int)value.intValue, 0, (int)maxValue.intValue);
			
			Rect rect = GUILayoutUtility.GetRect (100, 20);
			EditorGUI.ProgressBar (rect, ((float)value.intValue / (float)(maxValue.intValue <= 0 ? 1 : maxValue.intValue)), value.intValue + "/" + maxValue.intValue);
			if((target as AttributeHandler).transform.root == (target as AttributeHandler).transform){
				EditorGUILayout.PropertyField (property.FindPropertyRelative("onChange"));
				EditorGUILayout.PropertyField (property.FindPropertyRelative("onIncrease"));
				EditorGUILayout.PropertyField (property.FindPropertyRelative("onDecrease"));
				EditorGUILayout.PropertyField (property.FindPropertyRelative("onMaximumReach"));
			}
		}


		GUILayout.EndVertical();
	}


	public override void OnInspectorGUI ()
	{
		GUILayout.Space (5);
		serializedObject.Update ();
		if ((target as AttributeHandler).transform.root == (target as AttributeHandler).transform) {
			EditorGUILayout.PropertyField (serializedObject.FindProperty ("freePoints"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("updateUI"));
			SerializedProperty settings = serializedObject.FindProperty ("settings");
			EditorGUILayout.PropertyField (settings);
		} else {
			EditorGUILayout.HelpBox("Attributes on root transform will be overriden.",MessageType.Info);
		}
		if (mList != null) {
			mList.DoList ();
		}
		serializedObject.ApplyModifiedProperties ();

	}

	private void DrawProperties(SerializedObject obj,params string[] properties){
		for (int i=0; i<properties.Length; i++) {
			EditorGUILayout.PropertyField(obj.FindProperty(properties[i]));
		}
	}
}
