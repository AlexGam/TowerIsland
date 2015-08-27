using UnityEngine;
using System.Collections;
using ICode;

namespace ICode.FSMEditor{
	public static class ShortcutEditor  {
		public static void DoGUI(Rect position){
			if (PreferencesEditor.GetBool (Preference.ShowShortcuts)) {
				GUILayout.BeginArea (position);
				GUILayout.FlexibleSpace();
				GUILayout.BeginVertical((GUIStyle)"U2D.createRect");
				ShortcutGUI ("Show Help", "F1");
				ShortcutGUI ("Select All", "F3");
				ShortcutGUI ("Create State", "Ctrl+Click Canvas");
				ShortcutGUI ("Center View", "Tab");
				ShortcutGUI ("Action Browser", "Ctrl+A");
				ShortcutGUI ("Condition Browser", "Ctrl+C");
				GUILayout.EndVertical();
				GUILayout.EndArea ();
			}
		}
		
		
		private static void ShortcutGUI(string title,string shortcut){
			GUILayout.BeginHorizontal ();
			GUILayout.Label(title,FsmEditorStyles.shortcutLabel,GUILayout.Width(130));
			GUILayout.Label(shortcut,FsmEditorStyles.shortcutLabel);
			GUILayout.EndHorizontal ();
		}
		
		public static void HandleKeyEvents(){
			if (FsmEditor.instance == null) {
				return;			
			}
			Event ev = Event.current;
			switch (ev.type) {
			case EventType.KeyDown:
				if (ev.keyCode == KeyCode.F1) {
					PreferencesEditor.ToggleBool(Preference.ShowShortcuts);
					ev.Use();
				}
				if(ev.keyCode== KeyCode.Tab){
					FsmEditor.instance.CenterView();
				}
				
				if(ev.keyCode== KeyCode.F2){
					PreferencesEditor.ToggleBool(Preference.ShowInfo);
					ev.Use();
				}
				
				if(ev.keyCode== KeyCode.F3){
					FsmEditor.instance.ToggleSelection();
					ev.Use();
				}
				break;
			case EventType.KeyUp:

				if(ev.control){
					if(ev.keyCode== KeyCode.A){
						ActionBrowser.ShowWindow();
						ev.Use();
					}
					if(ev.keyCode== KeyCode.C){
						ConditionBrowser.ShowWindow();
						ev.Use();
					}
				}
				break;
			case EventType.MouseUp:
				if(ev.control && FsmEditor.instance != null){
					FsmEditorUtility.AddNode<State>(ev.mousePosition,FsmEditor.Active);
					if(FsmEditor.instance != null){
						FsmEditor.instance.Repaint();
					}
					ev.Use();
				}
				break;
			}
			
		}
	}
}