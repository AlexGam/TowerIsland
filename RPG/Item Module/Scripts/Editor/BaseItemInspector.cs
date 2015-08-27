using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditorInternal;
using System.Linq;
using ICode;
using ICode.FSMEditor;
using StateMachine = ICode.StateMachine;
using ReorderableList = UnityEditorInternal.ReorderableList;
using ArrayUtility=ICode.ArrayUtility;

[CustomEditor(typeof(BaseItem),true)]
public class BaseItemInspector : Editor {
	protected ItemDatabase database;
	private ReorderableList customDataList;
	private string[] parameterTypeNames; 

	protected virtual void OnEnable(){
		database = ItemDatabase.Load ();

		parameterTypeNames = TypeUtility.GetSubTypeNames (typeof(FsmVariable));
		parameterTypeNames = ArrayUtility.Insert<string> (parameterTypeNames, "None", 0);

		customDataList = new ReorderableList(serializedObject, 
		                                     serializedObject.FindProperty("customData"), 
		                                     true, true, true, true);
		customDataList.elementHeight = EditorGUIUtility.singleLineHeight * 3+10;
		customDataList.onRemoveCallback = (ReorderableList list) => {
			list.serializedProperty.serializedObject.Update();
			DestroyImmediate(list.serializedProperty.GetArrayElementAtIndex(list.index).objectReferenceValue,true);
			AssetDatabase.SaveAssets();
			list.serializedProperty.DeleteArrayElementAtIndex(list.index);
			list.serializedProperty.serializedObject.ApplyModifiedProperties();
		};

		customDataList.drawElementCallback =  (Rect rect, int index, bool isActive, bool isFocused) => {
			var element = customDataList.serializedProperty.GetArrayElementAtIndex(index);
			FsmVariable variable = element.objectReferenceValue as FsmVariable;
			rect.y+=2;
			int m = parameterTypeNames.ToList ().FindIndex (x => x == (variable!= null?variable.GetType ().Name:""));
			m = Mathf.Clamp (m, 0, int.MaxValue);
			rect.height=EditorGUIUtility.singleLineHeight;
			m = EditorGUI.Popup (rect,"Parameter Type", m, parameterTypeNames);
			string typeName=parameterTypeNames [m];
			string variableTypeName = (variable == null ? "None" : variable.GetType ().Name);

			if(typeName != variableTypeName){
				DestroyImmediate(element.objectReferenceValue,true);
				if(typeName != "None"){
					variable = ScriptableObject.CreateInstance (TypeUtility.GetTypeByName(typeName)[0]) as FsmVariable;
					variable.hideFlags = HideFlags.HideInHierarchy;
					
					if (EditorUtility.IsPersistent (element.serializedObject.targetObject)) {
						AssetDatabase.AddObjectToAsset (variable, element.serializedObject.targetObject);
						AssetDatabase.SaveAssets ();
					}
					
					element.serializedObject.Update();
					element.objectReferenceValue = variable;
					element.serializedObject.ApplyModifiedProperties ();
				}
			}
			if(variable != null){
				SerializedObject mVariable=new SerializedObject(variable);
				mVariable.Update();
				rect.y+=EditorGUIUtility.singleLineHeight+2;
				EditorGUI.PropertyField(rect,mVariable.FindProperty("name"));
				rect.y+=EditorGUIUtility.singleLineHeight+2;
				EditorGUI.PropertyField(rect,mVariable.FindProperty("value"));
				mVariable.ApplyModifiedProperties();
			}
		};
		customDataList.drawHeaderCallback = (Rect rect) => {  
			EditorGUI.LabelField(rect, "Custom Data");
		};
	}

	public override void OnInspectorGUI ()
	{
		serializedObject.Update ();
		DrawProperties ("m_Script","itemName","description","icon","color","grabSound","placeSound");
		GUILayout.Space (5);
		GUILayout.BeginVertical(EditorStyles.inspectorFullWidthMargins);
		customDataList.DoLayoutList ();
		GUILayout.EndVertical ();
		GUILayout.Space (5);
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
