using UnityEngine;
using System.Collections;

public class NetworkingMessageHandler : BaseModule {
	private static NetworkingMessageHandler instance;
	public static NetworkingMessageHandler current{
		get{
			if(instance== null){
				GameObject go= new GameObject("NetworkingMessageHandler");
				go.AddComponent<NetworkingMessageHandler>();
				return current;
			}
			return instance;
		}
	}

	public override string[] Callbacks {
		get {
			return new string[]{
				"OnConnectedToPhoton",
				"OnFailedToConnectToPhoton",
				"OnDisconnectedFromPhoton",
				"OnConnectionFail",
				"OnJoinedLobby",
				"OnLeftLobby",
				"OnLeftRoom",
				"OnPhotonCreateRoomFailed",
				"OnPhotonJoinRoomFailed",
				"OnCreatedRoom",
				"OnReceivedRoomListUpdate",
				"OnJoinedRoom",

				"OnMasterClientSwitched",
				"OnPhotonPlayerConnected",
				"OnPhotonPlayerDisconnected",
				"OnPhotonRandomJoinFailed",
				"OnConnectedToMaster",
				"OnPhotonMaxCccuReached",
			};
		}
	}

	private void Awake(){
		instance = this;
	}

	private void OnMasterClientSwitched(PhotonPlayer newMasterClient){
		Execute ("OnMasterClientSwitched", new ModuleEventData ());
	}

	private void OnPhotonPlayerConnected(PhotonPlayer newPlayer){
		Execute ("OnPhotonPlayerConnected", new ModuleEventData ());
	}

	private void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer){
		Execute ("OnPhotonPlayerDisconnected", new ModuleEventData ());
	}

	private void OnPhotonRandomJoinFailed(){
		Execute ("OnPhotonRandomJoinFailed", new ModuleEventData ());
	}

	private void OnConnectedToMaster() {
		Execute ("OnConnectedToMaster", new ModuleEventData ());
	}

	private void OnPhotonMaxCccuReached(){
		Execute ("OnPhotonMaxCccuReached", new ModuleEventData ());
	}

	private void OnConnectedToPhoton(){
		Execute ("OnConnectedToPhoton", new ModuleEventData ());
	}
	
	private void OnFailedToConnectToPhoton(){
		Execute ("OnFailedToConnectToPhoton", new ModuleEventData ());
	}
	
	private void OnDisconnectedFromPhoton(){
		Execute ("OnDisconnectedFromPhoton", new ModuleEventData ());
	}
	
	private void OnConnectionFail(){
		Execute ("OnConnectionFail", new ModuleEventData ());
	}
	
	
	private void OnJoinedLobby(){
		Execute ("OnJoinedLobby", new ModuleEventData ());
	}
	
	private void OnLeftLobby(){
		Execute ("OnLeftLobby", new ModuleEventData ());
	}
	
	private void OnLeftRoom(){
		Execute ("OnLeftRoom", new ModuleEventData ());
	}
	
	private void OnPhotonCreateRoomFailed(){
		Execute ("OnPhotonCreateRoomFailed", new ModuleEventData ());
	}
	
	private void OnPhotonJoinRoomFailed(){
		Execute ("OnPhotonJoinRoomFailed", new ModuleEventData ());
	}
	
	private void OnCreatedRoom(){
		Execute ("OnCreatedRoom", new ModuleEventData ());
	}
	
	private void OnReceivedRoomListUpdate(){
		Execute ("OnReceivedRoomListUpdate", new ModuleEventData ());
	}
	
	private void OnJoinedRoom(){
		Execute ("OnJoinedRoom", new ModuleEventData ());
	}

}
