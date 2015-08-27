using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(AccountHandler))]
public class LoginHandlerInspector : Editor {
	private bool loginReference;
	private bool registrationReference;
	private bool messageReference;
	public int menuIndex = 0;
	public string[] menuItems = new string[] {"Login", "Registration","Recover", "Message"};

	private void OnEnable(){
		loginReference = EditorPrefs.GetBool ("loginReference", false);
		registrationReference = EditorPrefs.GetBool ("registrationReference", false);
		messageReference = EditorPrefs.GetBool ("messageReference", false);
	}

	private void OnDisable(){
		EditorPrefs.SetBool ("loginReference",loginReference);
		EditorPrefs.SetBool ("registrationReference",registrationReference);
		EditorPrefs.SetBool ("messageReference",messageReference);
	}

	public override void OnInspectorGUI ()
	{
		serializedObject.Update ();
		GUILayout.Space (5);

		menuIndex = GUILayout.SelectionGrid(menuIndex, menuItems, menuItems.Length,EditorStyles.toolbarButton);

		GUILayout.BeginVertical ((GUIStyle)"Tooltip");
		switch (menuIndex) {
		case 0:
			DrawProperty("loginFailed","Login Failed");
			DrawProperty("loadLevelOnLogin","Success Level");
			EditorGUI.indentLevel += 1;
			loginReference = EditorGUILayout.Foldout (loginReference, "Login GUI");
			if (loginReference) {
				DrawProperty("loginWindow","Window");
				DrawProperty("username");
				DrawProperty("password");
				DrawProperty("rememberMe");
			}
			EditorGUI.indentLevel -= 1;
			break;
		case 1:
			DrawProperty("emptyField");
			DrawProperty("passwordMatch");
			DrawProperty("invalidEmail");
			DrawProperty("userExists");
			DrawProperty("accountCreated");
			EditorGUI.indentLevel += 1;
			registrationReference = EditorGUILayout.Foldout (registrationReference, "Registration GUI");
			if (registrationReference) {
				DrawProperty("registrationWindow","Window");
				DrawProperty("registrationUsername","Username");
				DrawProperty("registrationPassword","Password");
				DrawProperty("registrationConfirmPassword","Confirm Password");
				DrawProperty("registrationEmail","Email");
			}
			EditorGUI.indentLevel -= 1;
			break;
		case 3:
			EditorGUI.indentLevel += 1;
			messageReference = EditorGUILayout.Foldout (messageReference, "Message GUI");
			if (messageReference) {
				DrawProperty("messageWindow","Window");
				DrawProperty("title");
				DrawProperty("message");
				DrawProperty("button");
			}
			EditorGUI.indentLevel -= 1;
			break;
		}

		GUILayout.EndVertical ();
		serializedObject.ApplyModifiedProperties ();
	}

	private SerializedProperty DrawProperty(string propertyPath, string label,params GUILayoutOption[] options){
		SerializedProperty property=serializedObject.FindProperty(propertyPath);
		EditorGUILayout.PropertyField(property,new GUIContent(label),options);
		return property;
	}

	private SerializedProperty DrawProperty(string propertyPath, string label){
		SerializedProperty property=serializedObject.FindProperty(propertyPath);
		EditorGUILayout.PropertyField(property,new GUIContent(label));
		return property;
	}

	private SerializedProperty DrawProperty(string propertyPath){
		SerializedProperty property=serializedObject.FindProperty(propertyPath);
		EditorGUILayout.PropertyField(property);
		return property;
	}
}
