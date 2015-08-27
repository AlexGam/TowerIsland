using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using ICode;

[System.Serializable]
public class BaseItem : ScriptableObject {
	//Item name
	public string itemName="New Item";
	//Item description
	[Multiline(4)]
	public string description;
	//Sprite to use
	public Sprite icon;
	//Color to use on the name
	public Color color=Color.white;
	//Sound to play when the item is grabed
	public AudioClip grabSound;
	//Sound to play when the item is placed
	public AudioClip placeSound;
	[System.NonSerialized]
	public bool dirty;

	public List<FsmVariable> customData;

	public virtual void GenerateRandomData(){

	}

	public virtual string GetTooltip(){
		string t = "<b>"+UITools.ColorString(itemName,color)+"</b>";
		if (!string.IsNullOrEmpty(description)){
			t += "\n" + description;
		}
		return t;
	}

	
	public virtual string Serialize(){
		return itemName;
	}
	
	public virtual void Deserialize(string[] data){
		
	}
}
