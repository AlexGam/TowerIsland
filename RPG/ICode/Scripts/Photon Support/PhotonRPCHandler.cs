using UnityEngine;
using System.Collections;

public class PhotonRPCHandler : MonoBehaviour {
	private Animator animator;

	private void Start(){
		animator = GetComponent<Animator> ();
	}

	[RPC]
	private void SetAnimatorBool(string parameter, bool value){
		if (animator != null && !string.IsNullOrEmpty(parameter)) {
			animator.SetBool(parameter,value);
		}
	}

	[RPC]
	private void SetAnimatorFloat(string parameter, float value){
		if (animator != null && !string.IsNullOrEmpty(parameter)) {
			animator.SetFloat(parameter,value);
		}
	}

	[RPC]
	private void SetAnimatorInt(string parameter, int value){
		if (animator != null && !string.IsNullOrEmpty(parameter)) {
			animator.SetInteger(parameter,value);
		}
	}

	[RPC]
	private void SetTrigger(string trigger){
		if (animator != null && !string.IsNullOrEmpty(trigger)) {
			animator.SetTrigger(trigger);
		}
	}

	[RPC]
	private void CrossFade(string state){
		if (animator != null && !string.IsNullOrEmpty(state)) {
			animator.CrossFade(state,0f);
		}
	}

	[RPC]
	private void LoadLevel(string level){
		if (!string.IsNullOrEmpty (level)) {
			PhotonNetwork.LoadLevel(level);
		}
	}

	[RPC]
	private void SetPosition(Vector3 position){
		this.transform.position = position;
	}


}

