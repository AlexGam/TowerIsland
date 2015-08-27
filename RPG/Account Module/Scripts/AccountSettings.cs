using UnityEngine;
using System.Collections;

[System.Serializable]
public class AccountSettings : ModuleSettings {
    public string serverAddress = "190.142.201.124";
	public string createAccount = "createAccount.php";
	public string login = "login.php";
	public string resetPassword = "resetPassword.php";
}