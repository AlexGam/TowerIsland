using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class QuitOnClick : MonoBehaviour,IPointerClickHandler {
	public void OnPointerClick(PointerEventData ev){
		Application.Quit ();
	}
}
