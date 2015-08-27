using UnityEngine;
using System.Collections;

[System.Serializable]
public class AttributeSettings : ModuleSettings {
	public bool save;
	public bool saveLocal;
    public string serverAddress = "190.142.201.124";
	public float saveInterval=10;
	public string saveScript="saveAttributes.php";
	public string loadScript="loadAttributes.php";
}
