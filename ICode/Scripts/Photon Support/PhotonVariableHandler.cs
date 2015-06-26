using UnityEngine;
using System.Collections;

namespace ICode{
	public class PhotonVariableHandler : MonoBehaviour {
		private ICodeBehaviour[] behaviours;
		
		private void OnEnable(){
			behaviours = GetComponents<ICodeBehaviour> ();
		}

		[RPC]
		private void SetFsmString(string variable,string value){
			SetFsmVariable (variable, value);
		}

		[RPC]
		private void SetFsmInt(string variable,int value){
			SetFsmVariable (variable, value);
		}

		[RPC]
		private void SetFsmBool(string variable,bool value){
			SetFsmVariable (variable, value);
		}

		[RPC]
		private void SetFsmFloat(string variable,float value){
			SetFsmVariable (variable, value);
		}

		[RPC]
		private void SetFsmVector3(string variable,Vector3 value){
			SetFsmVariable (variable, value);
		}

		private void SetFsmVariable(string variable,object value){
			foreach (ICodeBehaviour behaviour in behaviours) {
				behaviour.stateMachine.SetVariable(variable,value);			
			}
		}
	}
}