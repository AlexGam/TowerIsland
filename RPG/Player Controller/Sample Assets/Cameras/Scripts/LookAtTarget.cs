using UnityEngine;
using System.Collections;

public class LookAtTarget : MonoBehaviour {
	[SerializeField]
	private Transform target;
	private Transform mTransform;

	void Start () {
		mTransform = transform;
	}

	void Update () {
		if (target != null) {
			mTransform.LookAt (target.position);
		}
	}
}
