using UnityEngine;
using System.Collections;

public class EquipmentSlot : UsableSlot {
	public EquipmentRegion region;

	public override BaseItem Replace (BaseItem item)
	{
		return (container != null && (item == null || (item is EquipmentItem && (item as EquipmentItem).equipmentRegion==region))) ? container.Replace(id, item) : item;
	}
}
