using UnityEngine;
using System.Collections;
using ICode;

public class IngredientContainer : MonoBehaviour {
	public IngredientSlot slot;
	private IngredientSlot[] slots;

	private void Start(){
		slot.gameObject.SetActive (false);
		slots = GetComponentsInChildren<IngredientSlot> (false);
	}

	public void Add(InventoryItem item){
		for (int i=0; i<this.slots.Length; i++) {
			slots[i].gameObject.SetActive(false);
		}
		slot.gameObject.SetActive (true);
		for (int i = 0; i<item.ingredients.Count; i++) {
			InventoryItem.Ingredient ingredient=item.ingredients[i];
			GameObject go= (GameObject)Instantiate(slot.gameObject);
			go.transform.SetParent(transform,false);
			IngredientSlot mSlot=go.GetComponent<IngredientSlot>();
			mSlot.amount.text=ingredient.amount.ToString();
			mSlot.icon.sprite=ingredient.item.icon;
			slots=ArrayUtility.Add<IngredientSlot>(slots,mSlot);
		}
		slot.gameObject.SetActive (false);
	}
}
