using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class AccountSystem : BaseModule {
	[SettingsSelect]
	public AccountSettings settings;

	public static AccountSystem current;
	public override string[] Callbacks {
		get {
			return new string[]{
				"OnLogin",
				"OnCreateAccount",
				"OnResetPassword"
			};
		}
	}

	private void OnEnable(){
		if (AccountSystem.current != null)
		{
			Debug.LogWarning("Multiple AccountSystems in scene... this is not supported");
		}
		else
		{
			AccountSystem.current = this;
		}
	}

	private void OnDisable(){
		if (AccountSystem.current == this)
		{
			AccountSystem.current = null;
		}
	}

	public void CreateAccount(string username, string password, string email){
		CreateAccount(username, password, email,null);
	}
	
	public void CreateAccount(string username, string password, string email, UnityAction<bool> callback){
		StartCoroutine (CreateAccountInternal (username, password, email,callback));
	}
	
	private IEnumerator CreateAccountInternal (string username, string password, string email, UnityAction<bool> callback)
	{
		if (settings == null) {
			yield break;		
		}
		WWWForm newForm = new WWWForm ();
		newForm.AddField ("name", username);
		newForm.AddField ("password", password);
		newForm.AddField ("email", email);

        WWW w = new WWW(settings.serverAddress + "/" + settings.createAccount, newForm);
		
		while (!w.isDone) {
			yield return new WaitForEndOfFrame();
		}
		
		if (w.error != null) {
			Debug.LogError (w.error);
		}
		
		bool res = w.text.Trim ().Equals("true");
		if (callback != null) {
			callback.Invoke (res);
		}
		AccountEventData eventData = new AccountEventData ();
        eventData.serverAdress = settings.serverAddress;
		User user = new User (username);
		user.password = password;
		eventData.user = user;
		eventData.result = res;
		Execute("OnCreateAccount",eventData);
	}
	
	public void LoginAccount(string username, string password)
	{
		LoginAccount(username, password,null);
	}
	
	public void LoginAccount(string username, string password, UnityAction<bool> callback)
	{
		StartCoroutine (LoginAccountInternal (username, password,callback));
	}
    public static bool ValidateEmail(string email)
    {
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        System.Text.RegularExpressions.Match match = regex.Match(email);
        return match.Success;
    }

	private IEnumerator LoginAccountInternal (string username, string password, UnityAction<bool> callback)
	{
		if (settings == null) {
			yield break;		
		}
		WWWForm newForm = new WWWForm ();
		newForm.AddField ("name", username);
		newForm.AddField ("password", password);

        WWW w = new WWW(settings.serverAddress + "/"+ settings.login, newForm);
		
		while (!w.isDone) {
			yield return new WaitForEndOfFrame();
		}
		
		if (w.error != null) {
			Debug.LogError (w.error);
		}
		
		bool res = w.text.Trim ().Equals("true");
		User user = new User (username);
		user.password = password;
		ModulePrefs.User = user;

		if (callback != null) {
			callback.Invoke (res);
		}
		AccountEventData eventData = new AccountEventData ();
        eventData.serverAdress = settings.login;
		eventData.user = user;
		eventData.result = res;
		Execute("OnLogin",eventData);
	}


}

