using UnityEngine;
using UnityEditor;
using System.Collections;

[System.Serializable]
public class ModuleEditor:ScriptableObject {
	public Rect position;

	public virtual void OnEnable(){}

	public virtual void OnGUI(){}

	public virtual void Update(){}
	
	public static class Styles{
		private static GUIStyle menu;
		public static GUIStyle Menu{
			get{
				if(menu == null){
					menu = new GUIStyle ((GUIStyle)"ProfilerLeftPane");
					menu.padding= new RectOffset(0,2,0,0);
				}
				return menu;
			}
		}
		private static GUIStyle plus;
		public static GUIStyle Plus{
			get{
				if(plus == null){
					plus= new GUIStyle("OL Plus");
				}
				return plus;
			}
		}

		private static GUIStyle minus;
		public static GUIStyle Minus{
			get{
				if(minus == null){
					minus= new GUIStyle("OL Minus");
					minus.margin= new RectOffset(0,0,5,0);
				}
				return minus;
			}
		}

		private static GUIStyle title;
		public static GUIStyle Title{
			get{
				if(title == null){
					title= new GUIStyle("label");
					title.fontSize=14;
				}
				return title;
			}
		}

		private static GUIStyle line;
		public static GUIStyle Line{
			get{
				if(line == null){
					line= new GUIStyle("ShurikenLine");
					line.fontSize=14;
					line.normal.textColor=((GUIStyle)"label").normal.textColor;
					line.contentOffset=new Vector2(3,-2);
				}
				return line;
			}
		}

		public static void DrawLine(){
			GUILayout.Label ("", Line);
		}

		public static void DrawLine(string label){
			GUILayout.Label (label, Line);
		}
	}
}
