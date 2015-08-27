using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CrossFadeAlpha : MonoBehaviour {
	public float alpha;
	public float duration;
	public float delay;
	public bool ignoreTimeScale;

	private void Start(){
		StartCoroutine (DelayCrossFade ());
	}

	private IEnumerator DelayCrossFade(){
		yield return new WaitForSeconds(delay);
		Graphic graphic = GetComponent<Graphic> ();
		graphic.CrossFadeAlpha(alpha,duration,ignoreTimeScale);
	}
}
