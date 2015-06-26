using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlayerSettings : ModuleSettings {
	public bool saveLocal;
	public string serverAddress = "localhost";
	public string createPlayer = "createPlayer.php";
	public string loadPlayers = "loadPlayers.php";
	public string deletePlayer = "deletePlayer.php";
}
