using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ActionbarSlot : UsableSlot {
	public KeyCode key;

	public override void OnUpdate ()
	{
		base.OnUpdate ();
		if (Input.GetKeyDown (key)) {
			OnDoubleClick();
		}
	}

	public override void OnClick ()
	{
		OnDoubleClick ();
	}
}
