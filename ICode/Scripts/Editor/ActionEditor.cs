using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Reflection;
using System.Linq;
using ICode;
using ICode.Actions;
using ArrayUtility=ICode.ArrayUtility;

namespace ICode.FSMEditor{
	public class ActionEditor {
		private StateInspector host;
		private StateAction[] actions;
		private State state;
		private ReorderableList actionList;

		public ActionEditor(State state, StateInspector host)
		{
			this.host = host;
			this.state = state;
		}
		
		public void OnEnable()
		{
			this.ResetActionList ();
		}
		
		public void OnInspectorGUI()
		{
			actionList.DoList ();
		}
		
		private void ResetActionList()
		{
			this.actions = this.state.Actions;
			this.actionList = new ReorderableList (this.actions, "Action", true, true)
			{
				drawElementCallback = new ReorderableList.ElementCallbackDelegate(this.OnActionElement),
				onReorderCallback = new ReorderableList.ReorderCallbackDelegate(this.OnReorderList),
				onAddCallback = new ReorderableList.AddCallbackDelegate(delegate(){
					FsmGUIUtility.SubclassMenu<StateAction> (CreateAction);		
				}),
				onContextClick=new ReorderableList.ContextCallbackDelegate(delegate(int index){
					FsmGUIUtility.ExecutableContextMenu(actions[index],state).ShowAsContext();
				}),
			};
			this.host.Repaint();
			if(FsmEditor.instance != null)
				FsmEditor.instance.Repaint ();
		}

		private void OnReorderList(IList list){
			list.CopyTo(state.Actions,0);	
		}
	
		private void CreateAction(Type type){
			StateAction action = (StateAction)ScriptableObject.CreateInstance (type);
			action.name = type.GetCategory () + "." + type.Name;
			action.hideFlags = HideFlags.HideInHierarchy;
			state.Actions = ArrayUtility.Add<StateAction> (state.Actions, action);

			if (EditorUtility.IsPersistent (state)) {
				AssetDatabase.AddObjectToAsset (action, state);
				AssetDatabase.SaveAssets ();
			}
			this.ResetActionList ();
		}
		
		private void OnActionElement(int index, bool selected){
			StateAction action = actions [index];
			bool enabled = action.IsEnabled;
			action.IsOpen = GUIDrawer.ObjectTitlebar (action, action.IsOpen,ref enabled, FsmGUIUtility.ExecutableContextMenu(action,state));
			action.IsEnabled = enabled;
			if (action.IsOpen) {
				GUIDrawer.OnGUI(action);
			}
		}
	}
}