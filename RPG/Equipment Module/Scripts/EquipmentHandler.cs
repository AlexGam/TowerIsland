using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ICode;

public class EquipmentHandler : MonoBehaviour {
	public List<EquipmentItemIdentifier> equipment;
	public StateMachine defaultAttack;
	private PhotonView photonView;
	private int attackGroup=823414;
	private void Start(){
		if (transform.root != transform) {
			EquipmentHandler handler = transform.root.gameObject.AddComponent<EquipmentHandler> ();
			handler.equipment = equipment;
			handler.defaultAttack=defaultAttack;
			Destroy(this);
			return;
		}
		photonView = PhotonView.Get (gameObject);
		if (photonView != null) {
			if ( !photonView.isMine) {
				photonView.RPC ("NetworkEquipRequest", photonView.owner, null);
			}else{
				if (defaultAttack != null) {
					gameObject.AddBehaviour(defaultAttack,attackGroup,true);
				}		
			}
		}
	}
	
	
	public void OnEquip(EquipmentItem item){
		OnUnEquip (item);
		foreach (EquipmentItemIdentifier identifier in equipment) {
			if(identifier.item.itemName == item.itemName){
				identifier.attachment.ForEach(x=>x.SetActive(true));
			}		
		}

		if (photonView != null && photonView.isMine && item.equipmentRegion == EquipmentRegion.Hands && item.defaultAttack != null) {
			gameObject.AddBehaviour(item.defaultAttack,attackGroup,true);
		}

		PhotonView.Get (gameObject).RPC ("OnNetworkEquip",PhotonTargets.Others,item.itemName);
	}
	
	
	public void OnUnEquip(EquipmentItem item){
		foreach (EquipmentItemIdentifier identifier in equipment) {
			if(identifier.item.equipmentRegion == item.equipmentRegion){
				identifier.attachment.ForEach(x=>x.SetActive(false));
			}		
		}

		if (photonView != null &&photonView.isMine && item.equipmentRegion == EquipmentRegion.Hands) {
			if(defaultAttack != null){
				gameObject.AddBehaviour(defaultAttack,attackGroup,true);
			}else{
				Destroy(gameObject.GetBehaviour(attackGroup));
			}
		}
		PhotonView.Get (gameObject).RPC ("OnNetworkUnEquip",PhotonTargets.Others,(int)item.equipmentRegion);
	}
	
	[RPC]
	private void NetworkEquipRequest(){
		foreach (EquipmentItemIdentifier mItem in equipment) {
			foreach(GameObject go in mItem.attachment){
				if(go.activeSelf){
					PhotonView.Get (gameObject).RPC ("OnNetworkEquip",PhotonTargets.Others,mItem.item.itemName);
				}
			}		
		}
	}
	
	[RPC]
	private void OnNetworkEquip(string itemName){
		foreach (EquipmentItemIdentifier identifier in equipment) {
			if(identifier.item.itemName == itemName){
				identifier.attachment.ForEach(x=>x.SetActive(true));
			}		
		}
	}
	
	[RPC]
	private void OnNetworkUnEquip(int region){
		foreach (EquipmentItemIdentifier identifier in equipment) {
			if((int)identifier.item.equipmentRegion == region){
				identifier.attachment.ForEach(x=>x.SetActive(false));
			}		
		}
	}
	
}

[System.Serializable]
public class EquipmentItemIdentifier{
	[ItemString]
	public EquipmentItem item;
	public List<GameObject> attachment;
	
}