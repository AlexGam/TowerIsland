using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoomHandler : MonoBehaviour {
	[SerializeField]
	private bool connectOnStart=true;
	[SerializeField]
	private GameObject group;
	[SerializeField]
	private GameObject slot;
	private RoomSlot selectedSlot;
	
	[SerializeField]
	private InputField roomNameInput;
	[SerializeField]
	private Toggle roomVisible;
	[SerializeField]
	private Toggle roomOpen;
	[SerializeField]
	private Text message;
	[SerializeField]
	private string emptyRoomName="Please enter a room name!";
	[SerializeField]
	private string joinedLevel;

	private void Start(){
		if (connectOnStart) {
			ConnectUsingSettings ();
		}
	}

	public void ConnectUsingSettings(){
		PhotonNetwork.ConnectUsingSettings (RoomSystem.current.settings.version);
		PrefabCache.Initialize ();
	}

	public void SelectRoom(RoomSlot roomSlot){
		selectedSlot = roomSlot;
	}

	public void JoinRoomUsingFields(){
		if(selectedSlot != null){
			PhotonNetwork.JoinRoom(selectedSlot.roomName.text);
		}
	}

	public void OnReceivedRoomListUpdate(){
		DestroyActiveChildren (group.transform);
		RoomInfo[] rooms= PhotonNetwork.GetRoomList ();
		foreach (RoomInfo info in rooms) {
			if(info.visible){
				GameObject go=(GameObject)Instantiate(slot);
				go.SetActive(true);
				RoomSlot mSlot=go.GetComponent<RoomSlot>();
				mSlot.roomName.text=info.name;
				mSlot.playerCount.text=info.playerCount.ToString();
				go.transform.SetParent(group.transform,false);
			}
		}
	}

	private void DestroyActiveChildren(Transform root){
		Transform[] children = root.GetComponentsInChildren<Transform> ();
		foreach (Transform mTransform in children) {
			if(mTransform != root && mTransform.gameObject.activeSelf){
				Destroy(mTransform.gameObject);
			}	
		}
	}

	public void CreateRoomUsingFields(){
		if(string.IsNullOrEmpty(roomNameInput.text)){
			message.text=emptyRoomName;
		}else{
			RoomOptions options= new RoomOptions();
			options.isVisible=roomVisible.isOn;
			options.isOpen=roomOpen.isOn;
			options.maxPlayers= RoomSystem.current.settings.maxPlayers;
			PhotonNetwork.JoinOrCreateRoom (roomNameInput.text,options,TypedLobby.Default);
		}
	}

	private void OnJoinedRoom(){
		if (string.IsNullOrEmpty (joinedLevel)) {
			Debug.LogWarning("Loading level failed! Please enter a level in the RoomHandler inspector.");
		} else {
			PhotonNetwork.LoadLevel (joinedLevel);
		}
	}
}
