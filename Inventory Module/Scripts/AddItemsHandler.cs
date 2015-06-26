using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using ICode;

public class AddItemsHandler : MonoBehaviour {
	public TriggerType trigger;
	public string triggerTag="Player";
	public bool openContainer;
	public bool closeContainer;
	public bool clearContainer;
	public StateMachine execute;
	public bool generateRandomData=true;

	public int[] containerIds;
	public UsableItem[] items;

	private ICodeBehaviour behaviour;
	private UIContainer container;


	private void Start(){
		for (int i=0; i< items.Length; i++) {
			if(items[i]== null){
				Debug.Log(gameObject);
			}
			items[i]=(UsableItem)Instantiate(items [i]);
			if(generateRandomData){
				items[i].GenerateRandomData();
			}
		}
	}

	private void OnMouseDown(){
		if(EventSystem.current.IsPointerOverGameObject () || !this.enabled || trigger != TriggerType.OnClick){
			return;
		}
		AddItems ();
	}

	private void OnTriggerEnter(Collider other){
		if (trigger == TriggerType.OnTriggerEnter && other.tag== triggerTag) {
			AddItems();
		}
	}

	private void OnTriggerExit(Collider other){
		if (trigger == TriggerType.OnTriggerEnter && closeContainer && other.tag== triggerTag) {
			CloseContainer();
		}
	}

	private void AddItems(){
		container = GetContainer ();

		if (container != null) {
			if (clearContainer) {
				container.Clear ();
			}

			if (execute != null) {
				if(behaviour == null){
					behaviour = gameObject.AddBehaviour(execute);
					behaviour.stateMachine.SetVariable ("Items", items);
					behaviour.stateMachine.SetVariable ("Container", container.gameObject);
				}
				return;
			}

			for (int i=0; i< items.Length; i++) {
				if (items [i] != null) {
					container.Add (items[i]);
				}
			}

			if(openContainer){
				OpenContainer();
			}
		}
	}

	private void OpenContainer(){
		if (container != null) {
			container.GetComponentInParent<CanvasGroupActivator> ().Activate ();
		}
	}

	private void CloseContainer(){
		if (container != null) {
			container.GetComponentInParent<CanvasGroupActivator> ().Deactivate ();
		}
	}

	private UIContainer GetContainer(){
		for(int j = 0;j<containerIds.Length;j++){
			UIContainer container=ItemUtility.GetContainer(containerIds[j]);
			if(container != null && container.HasFreeSlots()){
				return container;
			}
		}		
		return null;
	}
}

public enum TriggerType{
	OnClick,
	OnTriggerEnter
}
