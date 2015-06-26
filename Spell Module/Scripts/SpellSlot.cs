using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SpellSlot : UsableSlot {
	public Text spellName;
	public Text description;

	public override BaseItem Replace (BaseItem item)
	{
		return (container != null && (item == null || item is Spell )) ? container.Replace(id, item) : item;
	}
	
	public override void OnBeginDrag ()
	{
		if (observedItem != null) {
			draggedItem = observedItem;
		} 
	}

	public override void OnEndDrag ()
	{
		draggedItem = null;
	}

	public override void OnDrop ()
	{
	}

	public override void OnItemUpdate ()
	{
		base.OnItemUpdate ();
		Spell spell = observedItem as Spell;
		if (spellName != null) {
			spellName.text=UITools.ColorString(spell.itemName,spell.color);
		}

		if (description != null) {
			description.text=spell.description;
		}
	}
}
