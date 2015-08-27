using UnityEngine;
using System.Collections;

public class LookAtMainCamera : MonoBehaviour {
	private Transform target;
	private Transform mTransform;
	
	void Start () {
		if (Camera.main != null) {
			target = Camera.main.transform;
		} else {
			StartCoroutine(SearchCamera());
		}
		mTransform = transform;
	}
	
	void Update () {
		if (target != null) {
			mTransform.LookAt (target.position);
		}
	}

	private IEnumerator SearchCamera(){
		while (target == null) {
			if (Camera.main != null) {
				target = Camera.main.transform;
			}
			yield return new WaitForSeconds(2.0f);
		}
	}
}
