using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

public class ProjectEditor : EditorWindow {
	[MenuItem ("Window/Zerano Assets/RPG/Project Editor")]
	public static ProjectEditor ShowWindow ()
	{
		ProjectEditor window = EditorWindow.GetWindow<ProjectEditor>("Project Editor");
		window.minSize=new Vector2(600,400);
		return window;
	}

	[SerializeField]
	private Rect menuRect= new Rect(0,0,200,1000);
	private float minMenuWidth=150;
	private float maxMenuWidth=400;
	[SerializeField]
	private int menuIndex = 0;
	[SerializeField]
	private string[] menuItems;
	[SerializeField]
	private List<ModuleMap> editorMap;

	private void OnEnable(){
		RebuildEditorMap ();
		menuItems = editorMap.OrderBy(y=>y.order).Select (x => x.menu).ToArray ();
	}
	
	private void OnGUI(){
		menuRect.height = position.height - menuRect.y;
		
		GUILayout.BeginArea(menuRect,ModuleEditor.Styles.Menu);
		menuIndex = GUILayout.SelectionGrid(menuIndex, menuItems, 1,EditorStyles.toolbarButton);
		GUILayout.EndArea();

		Rect rect = new Rect (menuRect.width, menuRect.y, position.width - menuRect.width, position.height - menuRect.y);
		GUILayout.BeginArea (rect);
		foreach (ModuleMap map in editorMap) {
			if(map.menu == menuItems[menuIndex] && map.enabled){
				map.editor.position=rect;
				map.editor.OnGUI();
			}
		}
		GUILayout.EndArea ();
		ResizeMenuHorizontal ();
	}

	private void Update(){
		foreach (ModuleMap map in editorMap) {
			if(map.menu == menuItems[menuIndex]){
				if(!map.enabled){
					map.editor.OnEnable();
					map.enabled=true;
				}
				map.editor.Update();
			}
		}
	}
	
	private void ResizeMenuHorizontal(){
		Rect rect = new Rect (menuRect.width - 5, menuRect.y, 10, menuRect.height);
		EditorGUIUtility.AddCursorRect(rect, MouseCursor.ResizeHorizontal);
		int controlID = GUIUtility.GetControlID(FocusType.Passive);
		Event ev = Event.current;
		switch (ev.rawType) {
		case EventType.MouseDown:
			if(rect.Contains(ev.mousePosition)){
				GUIUtility.hotControl = controlID;
				ev.Use();
			}
			break;
		case EventType.MouseUp:
			if (GUIUtility.hotControl == controlID)
			{
				GUIUtility.hotControl = 0;
				ev.Use();
			}
			break;
		case EventType.MouseDrag:
			if (GUIUtility.hotControl == controlID)
			{
				menuRect.width=ev.mousePosition.x;
				menuRect.width=Mathf.Clamp(menuRect.width,minMenuWidth,maxMenuWidth);
				ev.Use();
			}
			break;
		}
	}

	private void RebuildEditorMap(){
		if (editorMap == null) {
			editorMap = new List<ModuleMap> ();
		}
		editorMap.RemoveAll(x=>x.editor== null);
		Type[] types = GetSubTypes (typeof(ModuleEditor));
		for (int i=0; i<types.Length; i++) {
			Type type=types[i];
			object[] attributes=type.GetCustomAttributes(true);
			for (int j = 0; j < attributes.Length; j++)
			{
				CustomModuleAttribute moduleAttribute = attributes[j] as CustomModuleAttribute;

				if (moduleAttribute != null && editorMap.Find(x=>x.editor.GetType() == type)== null )
				{
					ModuleMap map = new ModuleMap();
					map.menu=moduleAttribute.MenuItem;
					map.editor=(ModuleEditor)ScriptableObject.CreateInstance(type);
					map.editor.hideFlags=HideFlags.HideAndDontSave;
					editorMap.Add(map);
				}
			}
		}
	}

	public static Type[] GetSubTypes(Type baseType){
		IEnumerable<Type> types= AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()) .Where(type => type.IsSubclassOf(baseType));
		return types.ToArray();
	}

	[System.Serializable]
	public class ModuleMap{
		public string menu;
		public int order;
		public ModuleEditor editor;
		public bool enabled;
	}
}
