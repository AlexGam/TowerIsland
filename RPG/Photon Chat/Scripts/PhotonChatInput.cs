using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PhotonChatInput : MonoBehaviour {
	public Text chatText;
	InputField mInput;
	private PhotonView photonView;
	private string playerName;
	public Image blinkOnReceive;

	void Start ()
	{
		mInput = GetComponent<InputField>();
		photonView=PhotonView.Get (gameObject);

	}
	
	public void OnSubmit (string text)
	{
		if (!string.IsNullOrEmpty(text))
		{
			
			text=ModulePrefs.User.player.name+": "+text;
			photonView.RPC("OnSubmitRPC",PhotonTargets.All,text);
			mInput.text = "";
		}
		
	}
	
	[RPC]
	private void OnSubmitRPC(string text){
		if (chatText != null)
		{
			if (!string.IsNullOrEmpty(text))
			{
				chatText.text+="\n"+text;
				if(!blinking){
					StartCoroutine(Blink());
				}
			}
		}
	}
	private bool blinking;
	private IEnumerator Blink(){
		blinking = true;
		for (int i=0; i< 2; i++) {
			blinkOnReceive.enabled=false;
			yield return new WaitForSeconds(0.2f);
			blinkOnReceive.enabled=true;
			yield return new WaitForSeconds(0.2f);
		}
		blinking = false;
	}
}
