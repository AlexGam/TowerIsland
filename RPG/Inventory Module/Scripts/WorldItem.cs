using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using ICode;

public class WorldItem : MonoBehaviour {
	[ItemString]
	public InventoryItem item;
	public int[] containerIds;
	public StateMachine onClick;

	public bool createInstance=true;

	private void Start(){
		if (item == null) {
			enabled=false;
			return;
		}
		if (createInstance) {
			item = (InventoryItem)Instantiate (item);
			item.GenerateRandomData();
		}
	}

	private void OnMouseDown(){
		if (!this.enabled || EventSystem.current.IsPointerOverGameObject ()) {
			return;
		}
		if (AddToContainer ()) {
			Destroy (gameObject);	
		}
	}

	private bool AddToContainer(){
		InventoryContainer[] containers = ItemUtility.GetContainers<InventoryContainer> ();
		for (int i = 0; i < containers.Length; i++) {
			for(int j = 0;j<containerIds.Length;j++){
				if(containers[i].id==containerIds[j] && containers[i].Add(item)){
					return true;
				}
			}		
		}
		return false;
	}
}
