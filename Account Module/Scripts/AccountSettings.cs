using UnityEngine;
using System.Collections;

[System.Serializable]
public class AccountSettings : ModuleSettings {
	public string serverAddress = "localhost";
	public string createAccount = "createAccount.php";
	public string login = "login.php";
	public string resetPassword = "resetPassword.php";
}
