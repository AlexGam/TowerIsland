﻿using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

[CustomModule("Items")]
[System.Serializable]
public class ItemEditor:ListEditor {
	[SerializeField]
	private ItemDatabase database;
	[SerializeField]
	private SerializedObject itemObject;
	[SerializeField]
	private Vector2 scroll;
	[SerializeField]
	private Vector2 valueScroll;
	[SerializeField]
	private Editor editor;
	[SerializeField]
	private int index;
	[SerializeField]
	private BaseItem selected;

	public override void OnEnable ()
	{
		database = LoadOrCreate ();
		database.items.RemoveAll (x => x == null);
		if (itemObject== null && database.items.Count > 0) {
			itemObject=new SerializedObject(selected!= null?selected:database.items[0]);	
			editor = Editor.CreateEditor (itemObject.targetObject);
		}
	}

	public override void OnListGUI ()
	{
		SelectItem ();
	}

	public override void OnValueGUI ()
	{
		DrawItem ();
	}

	private void SelectItem(){
		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace ();
		if (GUILayout.Button ("",ModuleEditor.Styles.Plus)) {
			ICode.FSMEditor.FsmGUIUtility.SubclassMenu<UsableItem>(AddItem);
		}
		GUILayout.EndHorizontal ();
		ModuleEditor.Styles.DrawLine ("Items");
		List<string> typeNames = new List<string> (){"All"};
		typeNames.AddRange(ICode.TypeUtility.GetSubTypeNames (typeof(UsableItem)));
		index=EditorGUILayout.Popup(index,typeNames.ToArray());
		List<BaseItem> items = new List<BaseItem> (); 
		if (index == 0) {
			items = database.items;
		} else {
			items=database.items.FindAll(x=>x.GetType().Name == typeNames[index]);
		}

		scroll = GUILayout.BeginScrollView (scroll);
		for(int i=0; i< items.Count; i++) {
			GUILayout.BeginHorizontal();
			if(GUILayout.Button(items[i].itemName)){
				GUI.FocusControl("");
				itemObject=new SerializedObject(items[i]);
				selected=items[i];
			}	

			if(GUILayout.Button("",ModuleEditor.Styles.Minus,GUILayout.Width(18))){
				GameObject.DestroyImmediate(items[i],true);
				AssetDatabase.SaveAssets();
				database.items.RemoveAll(x=>x==null);
				EditorUtility.SetDirty(database);
			}
			GUILayout.EndHorizontal();
		}
		GUILayout.EndScrollView ();
	}
	
	private void AddItem(Type type){
		BaseItem item = (BaseItem)ScriptableObject.CreateInstance (type);
		item.hideFlags = HideFlags.HideInHierarchy;
		database.items.Add (item);
		AssetDatabase.AddObjectToAsset (item, database);
		AssetDatabase.SaveAssets ();
		EditorUtility.SetDirty (database);
		GUI.FocusControl("");
		itemObject=new SerializedObject(item);	
	}

	private void DrawItem(){
		GUILayout.Label ("");
		ModuleEditor.Styles.DrawLine ("Item Values");
		valueScroll = GUILayout.BeginScrollView (valueScroll);
		if (itemObject != null && itemObject.targetObject != null) {
			if(editor== null || editor.target != itemObject.targetObject){
				editor= Editor.CreateEditor(itemObject.targetObject);
			}
			editor.OnInspectorGUI();
		}
		GUILayout.EndScrollView ();
	}


	private ItemDatabase LoadOrCreate(){
		UnityEngine.Object[] files = Resources.LoadAll("ItemDatabase", typeof(ItemDatabase));
		if (files != null && files.Length > 0)
		{
			if (files.Length > 1)
			{
				Debug.LogWarning("There are more than one ItemDatabase files in 'Resources' folder. Check your project to keep only one.");
			}
			return (ItemDatabase)files[0];
		}

		if (!System.IO.Directory.Exists(Application.dataPath + "/RPG/Item Module/Resources")) {
			AssetDatabase.CreateFolder("Assets/RPG/Item Module", "Resources");
		}	
		ItemDatabase database = ICode.FSMEditor.AssetCreator.CreateAsset<ItemDatabase>("Assets/RPG/Item Module/Resources/ItemDatabase.asset");

		EditorUtility.DisplayDialog("Created ItemDatabase!","The ItemDatabase asset must be located in any Resources folder.", "Ok");
		return database;
	}
}
