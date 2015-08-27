using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class LoadLevelOnClick : MonoBehaviour,IPointerClickHandler {
	public string level;
	public void OnPointerClick(PointerEventData ev){
		if (!string.IsNullOrEmpty (level)) {
			Application.LoadLevel(level);
		}
	}
}
