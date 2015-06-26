using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class ItemDatabase : ScriptableObject {
	public List<BaseItem> items;

	void OnEnable(){
		if (items==null)
			items = new List<BaseItem>();
	}

	public BaseItem GetItem(string name){
		return items.Find(x=>x != null && x.itemName==name);
	}

	public static ItemDatabase Load(){
		UnityEngine.Object[] files = Resources.LoadAll("ItemDatabase", typeof(ItemDatabase));
		if (files != null && files.Length > 0)
		{
			if (files.Length > 1)
			{
				Debug.LogWarning("There are more than one ItemDatabase files in 'Resources' folder. Check your project to keep only one.");
			}
			return (ItemDatabase)files[0];
		}
		Debug.LogWarning ("ItemDatabase asset not found! Please create one using the Project Editor or the create context menu.");
		return null;
	}
}
