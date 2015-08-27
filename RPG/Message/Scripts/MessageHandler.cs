using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageHandler : MonoBehaviour {
	public int id;
	public Text prefab; 

	public void Add(string message){
		Add (message, Color.white);
	}

	public void Add(string message, Color color){
		GameObject go = (GameObject)Instantiate (prefab.gameObject);
		go.SetActive (true);
		go.transform.SetParent (transform, false);
		go.transform.SetAsFirstSibling ();
		Text text = go.GetComponent<Text> ();
		text.text = UITools.ColorString (message, color);
	}

	public static MessageHandler Get(int id){
		MessageHandler[] handlers=FindObjectsOfType<MessageHandler>();
		for (int i=0; i< handlers.Length; i++) {
			MessageHandler handler=handlers[i];
			if(handler.id == id){
				return handler;
			}
		}
		return null;
	}
}
