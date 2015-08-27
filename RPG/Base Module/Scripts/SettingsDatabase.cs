using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SettingsDatabase : ScriptableObject {
	public List<ModuleSettings> items;
	
	void OnEnable(){
		if (items==null)
			items = new List<ModuleSettings>();
	}
	
	public ModuleSettings GetItem(string name){
		return items.Find(x=>x != null && x.settingsName==name);
	}
	
	public static SettingsDatabase Load(){
		UnityEngine.Object[] files = Resources.LoadAll("SettingsDatabase", typeof(SettingsDatabase));
		if (files != null && files.Length > 0)
		{
			if (files.Length > 1)
			{
				Debug.LogWarning("There are more than one SettingsDatabase files in 'Resources' folder. Check your project to keep only one.");
			}
			return (SettingsDatabase)files[0];
		}
		Debug.LogWarning ("SettingsDatabase asset not found! Please create one using the Project Editor or the create context menu.");
		return null;
	}
}
