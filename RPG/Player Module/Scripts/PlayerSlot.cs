using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerSlot : MonoBehaviour, IPointerClickHandler {
	public Text playerName;
	public Text playerClass;
	public Text playerLevel;
	public Image selectionFrame;

	public SelectPlayerEvent onSelectPlayer;

	public void OnPointerClick(PointerEventData eventData){
		transform.root.BroadcastMessage ("DisableSelectionFrame", SendMessageOptions.DontRequireReceiver);
		selectionFrame.enabled = true;
		onSelectPlayer.Invoke (this);
	}

	private void DisableSelectionFrame(){
		selectionFrame.enabled = false;
	}

	[System.Serializable]
	public class SelectPlayerEvent:UnityEvent<PlayerSlot>{

	}
}
