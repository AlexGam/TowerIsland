using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using ICode;

public class CraftingSlot : UISlot{
	public Text itemName;
	public Text description;

	public override void OnItemUpdate ()
	{
		base.OnItemUpdate ();
		InventoryItem item = observedItem as InventoryItem;
		if (itemName != null) {
			itemName.text=UITools.ColorString(item.itemName,item.color);
		}
		
		if (description != null) {
			description.text=item.description;
		}
	}

	public override void OnBeginDrag ()
	{

	}

	private InventoryItem mItem;
	private IngredientContainer ingredientContainer;

	public void SelectCraftingSlot(CraftingSlot slot){
		icon.sprite = slot.icon.sprite;
		itemName.text = slot.itemName.text;
		description.text = slot.description.text;
		mItem = slot.observedItem as InventoryItem;
		if (ingredientContainer == null) {
			ingredientContainer=GetComponentInChildren<IngredientContainer>();
		}
		ingredientContainer.Add(mItem);
	}

	public void StartCrafting(){
		if (mItem == null) {
			return;
		}
		if (mItem.onCraft != null) {
			GameObject go = new GameObject ("ProcessCrafting");
			ICodeBehaviour behaviour = go.AddBehaviour (mItem.onCraft);
			
			behaviour.stateMachine.SetVariable ("Item", mItem);
		} 
	}

	public bool HasIngredients(List<InventoryItem.Ingredient> ingredients,InventoryContainer container){
		return true;
	} 
}
