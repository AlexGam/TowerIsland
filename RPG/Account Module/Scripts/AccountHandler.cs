using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AccountHandler : MonoBehaviour {
	#region Login GUI Reference
	[SerializeField]
	private string loadLevelOnLogin;
	[SerializeField]
	private string loginFailed="Username or password wrong! You may need to activate your account.";
	[SerializeField]
	private GameObject loginWindow;
	[SerializeField]
	private InputField username;
	[SerializeField]
	private InputField password;
	[SerializeField]
	private Toggle rememberMe;
	#endregion
	#region Registration GUI Reference
	[SerializeField]
	private string emptyField="You need to complete all fields!";
	[SerializeField]
	private string passwordMatch="Password does not match confirm password!";
	[SerializeField]
	private string invalidEmail="Please enter a valid email!";
	[SerializeField]
	private string accountCreated="Your account was created and an activation link was sended to your email address.After activation you may login into your account.";
	[SerializeField]
	private string userExists="Username already exists!";
	[SerializeField]
	private GameObject registrationWindow;
	[SerializeField]
	private InputField registrationUsername;
	[SerializeField]
	private InputField registrationPassword;
	[SerializeField]
	private InputField registrationConfirmPassword;
	[SerializeField]
	private InputField registrationEmail;
	#endregion
	#region Message GUI Reference
	[SerializeField]
	private GameObject messageWindow;
	[SerializeField]
	private Text title;
	[SerializeField]
	private Text message;
	[SerializeField]
	private Button button;
	#endregion

	private void Start(){
		username.text = PlayerPrefs.GetString ("username", string.Empty);
		username.enabled = false;
		username.enabled = true;
		password.text = PlayerPrefs.GetString ("password", string.Empty);
		password.enabled = false;
		password.enabled = true;
		rememberMe.isOn = string.IsNullOrEmpty (username.text) ? false : true;
	}

	public void LoginUsingFields(){
		Debug.Log ("Tying to login using Username: "+username.text+" and Password: "+password.text);
		AccountSystem.current.LoginAccount (username.text, password.text,OnLogin);
	}

	private void OnLogin(bool success){
		if (success) {
			if (rememberMe.isOn) {
				PlayerPrefs.SetString ("username", username.text);
				PlayerPrefs.SetString ("password", password.text);
			} else {
				PlayerPrefs.DeleteKey ("username");
				PlayerPrefs.DeleteKey ("password");
			}
            Application.LoadLevel("Select Player");
		} else {
			ShowMessage("Login Failed",loginFailed,loginWindow,loginWindow);
		}
	}

	public void CreateAccountUsingFields(){
		if (string.IsNullOrEmpty (registrationEmail.text) || 
		    string.IsNullOrEmpty (registrationPassword.text) ||
		    string.IsNullOrEmpty (registrationConfirmPassword.text) ||
		    string.IsNullOrEmpty (registrationEmail.text)) {
			ShowMessage("Registration Failed",emptyField,registrationWindow,registrationWindow);
			return;
		}
		
		if (registrationPassword.text != registrationConfirmPassword.text) {
			ShowMessage("Registration Failed",passwordMatch,registrationWindow,registrationWindow);
			return;
		}

        if (!AccountSystem.ValidateEmail(registrationEmail.text))
        {
            ShowMessage("Registration Failed", invalidEmail, registrationWindow, registrationWindow);
            return;
        }
		
		AccountSystem.current.CreateAccount (registrationUsername.text, registrationPassword.text, registrationEmail.text, OnCreateAccount);
	}

	private void OnCreateAccount(bool success){
		if (success) {
			ShowMessage("Account Created",accountCreated,registrationWindow,loginWindow);
			username.text=registrationUsername.text;
			password.text=registrationPassword.text;
		} else {
			ShowMessage("Registration Failed",userExists,registrationWindow, registrationWindow);
		}
	}

	private void ShowMessage(string title,string message,GameObject closeWindow, GameObject openWindow){
		this.title.text=title;
		this.message.text=message;
		messageWindow.SetActive(true);
		closeWindow.SetActive(false);
		button.onClick.RemoveAllListeners();
		button.onClick.AddListener(delegate {
			openWindow.SetActive(true);
			messageWindow.SetActive(false);
		});
	}
}
