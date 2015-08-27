using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class PlayerSystem : BaseModule {
	[SettingsSelect]
	public PlayerSettings settings;
	private static string playersKey="1f2f7d35-78e6-4029-8a86-c80debf30e31";
	public static PlayerSystem current;
	public override string[] Callbacks {
		get {
			return new string[]{
				"OnCreatePlayer",
				"OnLoadPlayer",
				"OnDeletePlayer"
			};
		}
	}
	
	private void OnEnable(){
		if (PlayerSystem.current != null)
		{
			Debug.LogWarning("Multiple PlayerSystems in scene... this is not supported");
		}
		else
		{
			PlayerSystem.current = this;
		}
	}
	
	private void OnDisable(){
		if (PlayerSystem.current == this)
		{
			PlayerSystem.current = null;
		}
	}

	public void CreatePlayer(string username,string playerName, string custom){
		if (settings.saveLocal) {
			CreatePlayerInternalPrefs (playerName, custom, null);
		} else {
			CreatePlayer (username, playerName, custom, null);
		}
	}

	public void CreatePlayer(string username,string playerName, string custom, UnityAction<bool> callback){
		if (settings.saveLocal) {
			CreatePlayerInternalPrefs (playerName, custom, callback);
		} else {
			StartCoroutine (CreatePlayerInternal (username, playerName, custom, callback));
		}
	}

	private void CreatePlayerInternalPrefs(string playerName, string custom, UnityAction<bool> callback){
		string newPlayer=playerName+","+custom+",1";

		string allPlayers = PlayerPrefs.GetString (playersKey);
		string[] arr = allPlayers.Split('/');
		if (arr.Length > 0)
		{
			for (int i = 0; i < arr.Length; i++)
			{
				string[] data = arr[i].Split(',');
				if(data.Length > 2){
					string mName=data[0];
					if(playerName == mName){
						if(callback != null){
							callback.Invoke(false);
						}
						return;
					}
				}
			}
		}
		if (callback != null) {
			callback.Invoke (true);
		}
		PlayerPrefs.SetString (playersKey,allPlayers + "/" + newPlayer);
		PlayerEventData eventData = new PlayerEventData ();
		eventData.playerName = playerName;
		eventData.level = 1;
		eventData.custom = custom;
		Execute("OnCreatePlayer",eventData);
	}

	private IEnumerator CreatePlayerInternal(string username,string playerName, string custom, UnityAction<bool> callback){
		WWWForm newForm = new WWWForm ();
		newForm.AddField ("account", username);
		newForm.AddField ("name", playerName);
		newForm.AddField ("custom", custom);
		newForm.AddField ("level", 1);
		
		WWW w = new WWW (settings.serverAddress + "/"+settings.createPlayer, newForm);
		
		while (!w.isDone) {
			yield return new WaitForEndOfFrame();
		}
		
		if (w.error != null) {
			Debug.LogError (w.error);
		}
		
		bool res = w.text.Trim ().Equals("true");
		if (callback != null) {
			callback.Invoke(res);		
		}
		PlayerEventData eventData = new PlayerEventData ();
		eventData.playerName = playerName;
		eventData.level = 1;
		eventData.custom = custom;
		Execute("OnCreatePlayer",eventData);
	}

	public void LoadPlayers(string username)
	{
		if (settings.saveLocal) {
			LoadPlayersInternalPrefs(null);
		} else {
			LoadPlayers (username, null);
		}
	}

	public void LoadPlayers(string username,UnityAction<PlayerEventData> callback)
	{
		if (settings.saveLocal) {
			LoadPlayersInternalPrefs(callback);
		} else {
			StartCoroutine (LoadPlayersInternal (username, callback));
		}
	}

	private void LoadPlayersInternalPrefs(UnityAction<PlayerEventData> callback){
		string res = PlayerPrefs.GetString (playersKey);
		string[] arr = res.Split('/');
		if (arr.Length > 0)
		{
			for (int i = 0; i < arr.Length; i++)
			{
				string[] data = arr[i].Split(',');
				if(data.Length > 2){
					PlayerEventData eventData = new PlayerEventData ();
					eventData.playerName = data[0];
					Debug.Log(data[2]);
					eventData.level = int.Parse(data[2]);
					eventData.custom = data[1];
					Execute("OnLoadPlayer",eventData);
					if (callback != null) {
						callback.Invoke(eventData);
					}
				}
			}
		}
	}
	
	private IEnumerator LoadPlayersInternal(string username, UnityAction<PlayerEventData> callback)
	{
		WWWForm newForm = new WWWForm();
		newForm.AddField("account", username);
		
		WWW w = new WWW(settings.serverAddress + "/"+settings.loadPlayers, newForm);
		
		while (!w.isDone)
		{
			yield return new WaitForEndOfFrame();
		}
		
		if (w.error != null)
		{
			Debug.LogError(w.error);
		}
		
		string res = w.text.Trim();
        Debug.Log(res);
		string[] arr = res.Split('/');
		if (arr.Length > 0)
		{
			for (int i = 0; i < arr.Length; i++)
			{
				string[] data = arr[i].Split(',');
				if(data.Length > 2){
					PlayerEventData eventData = new PlayerEventData ();
					eventData.playerName = data[0];
					Debug.Log(data[2]);
					eventData.level = int.Parse(data[2]);
					eventData.custom = data[1];
					Execute("OnLoadPlayer",eventData);
					if (callback != null) {
						callback.Invoke(eventData);
					}
				}
			}
		}
	}


	public void DeletePlayer(string playerName) {
		if (settings.saveLocal) {
			DeletePlayerInternalPrefs(playerName,null);
		} else {
			DeletePlayer (playerName, null);
		}
	}

	public void DeletePlayer(string playerName, UnityAction callback)
	{
		if (settings.saveLocal) {
			DeletePlayerInternalPrefs(playerName,callback);
		} else {
			StartCoroutine (DeletePlayerInternal (playerName, callback));
		}
	}

	public void DeletePlayerInternalPrefs(string playerName,UnityAction callback){
		string res = PlayerPrefs.GetString (playersKey);
		List<string> players = new List<string> ();
		string[] arr = res.Split('/');
		if (arr.Length > 0)
		{
			for (int i = 0; i < arr.Length; i++)
			{
				string[] data = arr[i].Split(',');
				if(data.Length > 2){
					string mName = data[0];
					string custom = data[1];
					string level = data[2];
					if(mName != playerName){
						players.Add(mName+","+custom+","+level);
					}
				}
			}
		}
		string allPlayers = string.Empty;
		foreach (string player in players) {
			allPlayers+=player+"/";
		}
		PlayerPrefs.SetString (playersKey, allPlayers);
		if (callback != null) {
			callback.Invoke();		
		}
		Execute("OnDeletePlayer",new PlayerEventData());
	}

	private IEnumerator DeletePlayerInternal(string playerName,UnityAction callback)
	{
		WWWForm newForm = new WWWForm();
		newForm.AddField("name", playerName);
		
		WWW w = new WWW(settings.serverAddress + "/" + settings.deletePlayer , newForm);
		
		while (!w.isDone)
		{
			yield return new WaitForEndOfFrame();
		}
		
		if (w.error != null)
		{
			Debug.LogError(w.error);
		}
		if (callback != null) {
			callback.Invoke();		
		}
		Execute("OnDeletePlayer",new PlayerEventData());
	}

}
