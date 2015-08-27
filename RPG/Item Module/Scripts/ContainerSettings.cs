using UnityEngine;
using System.Collections;

[System.Serializable]
public class ContainerSettings : ModuleSettings {
	public bool save;
	public bool saveLocal;
	public string serverAddress="localhost";
	public string saveScript="saveContainer.php";
	public float saveInterval=5;
	public string loadScript="loadContainer.php";

}
