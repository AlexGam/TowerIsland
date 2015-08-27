using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using ICode;

public class InventorySlot : UsableSlot {
	public Text stack;

	public override BaseItem Replace (BaseItem item)
	{
		return (container != null && (item == null || item is InventoryItem )) ? container.Replace(id, item) : item;
	}

	public override void OnUpdate ()
	{
		base.OnUpdate ();
		if (observedItem != null) {
			int mStack = (observedItem as InventoryItem).stack;
			
			stack.text = mStack.ToString ();
			if (mStack < 1) {
				Replace (null);		
			}
		} else {
			stack.text="";
		}
	}
	
	public override void OnEndDrag ()
	{
		if (draggedItem != null) {
			InventoryItem item = draggedItem as InventoryItem;
			RaycastHit hit;
			if(!EventSystem.current.IsPointerOverGameObject() && item.prefab != null && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)){
				Vector3 worldPos = hit.point;
				GameObject go = GameObject.Instantiate (item.prefab,worldPos+Vector3.up, Quaternion.identity) as GameObject;
				WorldItem worldItem=go.GetComponent<WorldItem>();
				if(worldItem != null){
					worldItem.item=item;
					worldItem.createInstance=false;
				}
				draggedItem = null;
			}else{
				base.OnEndDrag ();
			}
		}
	}
}
