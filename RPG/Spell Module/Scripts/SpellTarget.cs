using UnityEngine;
using System.Collections;

public class SpellTarget : MonoBehaviour {
	public float maxDistance;
	public GameObject player;

	private void Start(){
		DontDestroyOnLoad (gameObject);
		Renderer renderer = GetComponent<Renderer> ();
		renderer.enabled = false;
	}
	
	// Update is called once per frame
	private void Update () {
		if (player == null) {
			return;
		}
		RaycastHit hit;
		Vector3 pos = transform.position;
		if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, Mathf.Infinity)) {
			pos = hit.point+ Vector3.up*0.2f;
		}
		Vector3 diff = pos - player.transform.position;
		float distance = diff.magnitude;
		if (distance > (maxDistance - (transform.localScale.x / 2))) {
			transform.position = player.transform.position + (diff / distance) * maxDistance;
		} else {
			transform.position = pos;
		}
	}
}
