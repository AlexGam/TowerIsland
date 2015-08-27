using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ICode;

public class TeleporterSlot : UsableSlot {
	public Text teleporterName;

	public override BaseItem Replace (BaseItem item)
	{
		return (container != null && (item == null || item is Teleporter )) ? container.Replace(id, item) : item;
	}

	public override void OnClick ()
	{
		OnDoubleClick ();
	}

	public override void OnDoubleClick ()
	{
		if (!IsCoolDown && observedItem != null) {
			Teleporter teleporter=observedItem as Teleporter;
			if (teleporter.onUse != null) {
				GameObject go = new GameObject ("UseItem");
				ICodeBehaviour behaviour = go.AddBehaviour (teleporter.onUse);
			
				behaviour.stateMachine.SetVariable ("Item", teleporter);
				behaviour.stateMachine.SetVariable ("Slot", gameObject);
				CoolDown (teleporter.coolDown, teleporter.containerCoolDown);
			}
		}
	}

	public override void OnBeginDrag (){}
	
	public override void OnEndDrag (){}
	
	public override void OnDrop (){}
	
	public override void OnItemUpdate ()
	{
		base.OnItemUpdate ();
		Teleporter teleporter = observedItem as Teleporter;
		if (teleporterName != null && teleporter != null) {
			teleporterName.text=UITools.ColorString(teleporter.itemName,teleporter.color);
		}
	}
}
