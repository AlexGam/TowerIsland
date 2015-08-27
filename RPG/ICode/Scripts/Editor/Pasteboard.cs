using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;
using ICode;
using ArrayUtility=ICode.ArrayUtility;

namespace ICode.FSMEditor{
	public static class Pasteboard  {
		private static Node node;

		public static void Copy(Node node){
			Pasteboard.node = node;
		}

		public static void Paste(Vector2 position, StateMachine stateMachine){
			Node mNode = (Node)FsmUtility.Copy(node);
			mNode.Parent = stateMachine;
			mNode.hideFlags = HideFlags.HideInHierarchy;
			mNode.Name = FsmEditorUtility.GenerateUniqueNodeName(mNode.GetType(),stateMachine);
			stateMachine.Nodes = ArrayUtility.Add<Node> (stateMachine.Nodes, mNode);
			mNode.position = new Rect(position.x,position.y,FsmEditorStyles.StateWidth,FsmEditorStyles.StateHeight);
			FsmEditorUtility.ParentChilds(stateMachine);

			foreach (Transition origTransition in mNode.Transitions) {			
				if(!stateMachine.Nodes.Contains(origTransition.ToNode)){
					FsmEditorUtility.DestroyImmediate(origTransition);
					mNode.Transitions=ArrayUtility.Remove(mNode.Transitions,origTransition);
				}
			}

			if (mNode.GetType () == typeof(StateMachine)) {
				mNode.position.width = FsmEditorStyles.StateMachineWidth;
				mNode.position.height = FsmEditorStyles.StateMachineHeight;

			}
			FsmEditorUtility.UpdateNodeColor (mNode);
		}

		public static bool CanPaste(){
			return Pasteboard.node != null;
		}
	}
}	