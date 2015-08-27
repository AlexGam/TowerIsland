using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreatePlayerHandler : MonoBehaviour {
	[SerializeField]
	private string successLevel;
	[SerializeField]
	private string nameInUse="Name already in use!";
	[SerializeField]
	private string nameEmpty="Please enter a name!";
	[SerializeField]
	private string defaultClass="Warlock";
	[SerializeField]
	private Text message;

	private string playerName;
	private string playerClass;

	private void Start(){
		playerClass = defaultClass;
	}

	public void SelectPlayerClass(string playerClass){
		this.playerClass = playerClass;
	}
	
	public void OnEnterPlayerName(string playerName){
		this.playerName = playerName;
	}

	public void CreatePlayer(){
		if (!string.IsNullOrEmpty (playerName)) {
			string username = ModulePrefs.User.name;
			PlayerSystem.current.CreatePlayer (username, playerName, playerClass, OnCreatePlayer);
		} else {
			message.text=nameEmpty;
		}
	}

    public void CancelCreate() {
        Application.LoadLevel(successLevel);
    }

	private void OnCreatePlayer(bool succcess){
		Debug.Log ("Player Created: "+succcess);
		if (succcess) {
			Application.LoadLevel(successLevel);
		} else {
			message.text=nameInUse;	
		}
	}
}
