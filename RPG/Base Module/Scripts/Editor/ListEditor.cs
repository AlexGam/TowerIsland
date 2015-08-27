using UnityEngine;
using UnityEditor;
using System.Collections;

[System.Serializable]
public class ListEditor : ModuleEditor {
	[SerializeField]
	private Rect listRect= new Rect(0,0,200,1000);
	public override void OnGUI ()
	{
		listRect.height = position.height - listRect.y;
		GUILayout.BeginArea(listRect,"","box");
		OnListGUI ();
		GUILayout.EndArea ();
		Rect rect = new Rect (listRect.width, listRect.y, position.width - listRect.width, position.height - listRect.y);
		GUILayout.BeginArea(rect,"","hostview");
		OnValueGUI ();
		GUILayout.EndArea ();
		ResizeMenuHorizontal ();
	}

	public virtual void OnListGUI(){

	}

	public virtual void OnValueGUI(){}

	private void ResizeMenuHorizontal(){
		Rect rect = new Rect (listRect.width - 5, listRect.y, 10, listRect.height);
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
				listRect.width=ev.mousePosition.x;
				listRect.width=Mathf.Clamp(listRect.width,200,400);
				ev.Use();
			}
			break;
		}
	}
}
