using UnityEngine;
using System.Collections;

namespace ICode.Actions.Attributes{
	[Category("RPG/Modules/Item/Crafting")]
	[Tooltip("Apply damage.")]
	[System.Serializable]
	public class RemoveIngredients : StateAction {
		[SharedPersistent]
		[Tooltip("GameObject with InventoryContainer component.")]
		public FsmGameObject gameObject;
		[Shared]
		[Tooltip("Item with ingredients to remove.")]
		public FsmObject item;

		
		public override void OnEnter ()
		{
			InventoryContainer container = gameObject.Value.GetComponent<InventoryContainer> ();
			InventoryItem mItem = item.Value as InventoryItem;
			for (int i=0; i< mItem.ingredients.Count; i++) {
				InventoryItem.Ingredient ingredient=mItem.ingredients[i];
				container.ReduceItemStack(ingredient.item.itemName,ingredient.amount);
			}
			Finish ();
		}
	}
}
