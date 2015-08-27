using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class RoomSlot : MonoBehaviour, IPointerClickHandler {
	public Text roomName;
	public Text playerCount;
	public Image selectionFrame;

	public SelectRoomEvent onSelectRoom;
	
	public void OnPointerClick(PointerEventData eventData){
		transform.root.BroadcastMessage ("DisableSelectionFrame", SendMessageOptions.DontRequireReceiver);
		selectionFrame.enabled = true;
		onSelectRoom.Invoke (this);
	}
	
	private void DisableSelectionFrame(){
		selectionFrame.enabled = false;
	}
	
	[System.Serializable]
	public class SelectRoomEvent:UnityEvent<RoomSlot>{
		
	}
}
