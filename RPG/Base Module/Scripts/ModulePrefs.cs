using UnityEngine;
using UnityEngine.Events;
using System.Collections;

/// <summary>
/// Class containing Data, that is shared between all modules.
/// </summary>
public static class ModulePrefs  {
	private static bool debug = true;
	private static string usernameKey="830af030-7224-4b74-b45f-9e48eb4bc582";
	private static string passwordKey="830af030-7224-4b74-b45f-9e48eb4bc582";
	private static string playerNameKey="336654f4-cac5-49d6-941d-2fb81cbce185";
    private static string playerLevelKey = "f1f68b5b-d5f7-44b3-846d-c4602fed5859";
    private static string playerCustomKey = "ae6e3c16-ddc4-411d-8597-105622350fc3";
    private static string playerPositionKey = "336654f4-cac5-49d6-941d-2fb81cbce185";

	private static User user;
	public static User User{
		get{
			if(user == null){
				user= new User("LocalUser");
			}
			return user;
		}
		set{
			user=value;
		}
	}

	public static void SaveUser(){
		if (debug) {
			PlayerPrefs.SetString (usernameKey, user.name);
			PlayerPrefs.SetString (playerNameKey, user.player.name);
			PlayerPrefs.SetInt (playerLevelKey, user.player.level);
			PlayerPrefs.SetString (playerCustomKey, user.player.custom);
		}
	}

	public static void LoadUser(){
		if (debug) {
			string username = PlayerPrefs.GetString (usernameKey);
			string password = PlayerPrefs.GetString (passwordKey);
		
			user = new User (username);
			user.password = password;
		
			string playerName = PlayerPrefs.GetString (playerNameKey);
			int playerLevel = PlayerPrefs.GetInt (playerLevelKey, 1);
			string playerCustomData = PlayerPrefs.GetString (playerCustomKey);
			user.player.name = playerName;
			user.player.level = playerLevel;
			user.player.custom = playerCustomData;
		}
	}
}
