using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ICode;

public class SellContainer : UIContainer {
	public Text totalPrice;
	public StateMachine processSell;

	public void UpdateTotalPrice(){
		totalPrice.text = GetTotalPrice().ToString ();
	}
	
	private int GetTotalPrice(){
		int total = 0;
		for (int i=0; i< Items.Count; i++) {
			InventoryItem mItem=Items[i] as InventoryItem;
			if(mItem != null){
				total+=mItem.buyPrice*mItem.stack;
			}
		}
		return total;
	}

	public void ProcessSell(){
		int total = GetTotalPrice ();
		GameObject go = new GameObject ("ProcessPurchase");
		ICodeBehaviour behaviour = go.AddBehaviour(processSell);
		behaviour.stateMachine.SetVariable ("Price", -total);
		behaviour.stateMachine.SetVariable ("Container", gameObject);
	}

}
