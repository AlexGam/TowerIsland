using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class CanvasGroupActivator : MonoBehaviour {
	public CanvasGroup canvasGroup;
	public KeyCode key;

	private void Start(){
		if (canvasGroup == null) {
			canvasGroup = GetComponent<CanvasGroup> ();
		}
	}

	private void Update(){
		if (Input.GetKeyDown (key) && !UITools.IsInputFieldSelected()) {
			Toggle();
		}
	}

	public void Activate(){
		canvasGroup.alpha=1.0f;		
		canvasGroup.interactable=true;
		canvasGroup.blocksRaycasts=true;
	}

	public void Deactivate(){
		canvasGroup.alpha = 0f;		
		canvasGroup.interactable=false;
		canvasGroup.blocksRaycasts=false;
	}

	public void Toggle(){
		if (canvasGroup.alpha > 0.9) {
			canvasGroup.alpha = 0f;		
			canvasGroup.interactable=false;
			canvasGroup.blocksRaycasts=false;
		} else {
			canvasGroup.alpha=1.0f;		
			canvasGroup.interactable=true;
			canvasGroup.blocksRaycasts=true;
		}
	}
}
