using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectPlayerHandler : MonoBehaviour {
	[SerializeField]
	private string playLevel;
	[SerializeField]
	private string createLevel;
	[SerializeField]
	private GameObject layoutGroup;
	[SerializeField]
	private GameObject slot;
	private PlayerSlot selectedSlot;

	private void Start(){
		PlayerSystem.current.LoadPlayers (ModulePrefs.User.name, OnLoadPlayer);
	}

	private void OnLoadPlayer(PlayerEventData eventData){
		GameObject go = (GameObject)Instantiate (slot);
		go.SetActive (true);
		PlayerSlot mSlot = go.GetComponent<PlayerSlot> ();
		mSlot.playerName.text = eventData.playerName;
		mSlot.playerClass.text = eventData.custom;
		Debug.Log (eventData.level);
		mSlot.playerLevel.text = "Lvl."+eventData.level.ToString ();
		go.transform.SetParent( layoutGroup.transform,false);
	}

	public void SelectPlayer(PlayerSlot playerSlot){
		selectedSlot = playerSlot;
		if (ModulePrefs.User == null) {
			ModulePrefs.User= new User("LocalUser");
		}
		ModulePrefs.User.player.name = playerSlot.playerName.text;
		ModulePrefs.User.player.level=int.Parse(System.Text.RegularExpressions.Regex.Match(playerSlot.playerLevel.text, @"\d+").Value);
		ModulePrefs.User.player.custom = playerSlot.playerClass.text;
	}

	public void DeleteSelectedPlayer(){
		PlayerSystem.current.DeletePlayer (ModulePrefs.User.player.name,OnDeletePlayer);
	}

	private void OnDeletePlayer(){
		Destroy (selectedSlot.gameObject);
		ModulePrefs.User.player.name = string.Empty;
	}

	public void Play(){
		if (string.IsNullOrEmpty (ModulePrefs.User.player.name)) {
			Debug.LogWarning("Loading play level failed! You should only enable the play button when a player is selected.");	
		}else if(string.IsNullOrEmpty(playLevel)){
			Debug.LogWarning("Loading play level failed! Level name not set in the inspector of SelectPlayerHandler.");
		}else{
			ModulePrefs.SaveUser();
			Application.LoadLevel (playLevel);
		}
	}

	public void CreatePlayer(){
		if (string.IsNullOrEmpty (createLevel)) {
			Debug.LogWarning("Loading create level failed! Level name not set in the inspector of SelectPlayerHandler.");
		} else {
			Application.LoadLevel (createLevel);
		}
	}
}
