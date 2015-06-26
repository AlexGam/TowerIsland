using UnityEngine;
using System.Collections;

namespace ICode.Conditions.ItemSystem{
	[Category("RPG/Modules/Item/Crafting")]
	[Tooltip("Checks if the slot is cooling down.")]
	public class HasIngredients : Condition {
		[SharedPersistent]
		[Tooltip("GameObject with InventoryContainer component.")]
		public FsmGameObject gameObject;
		[Shared]
		[Tooltip("Item to check the ingredients on.")]
		public FsmObject item;
		[Tooltip("Does the result equals this condition.")]
		public FsmBool equals;
		
		private InventoryContainer container;
		public override void OnEnter ()
		{
			base.OnEnter ();
			if (gameObject.Value != null) {
				container = gameObject.Value.GetComponent<InventoryContainer> ();
			}
		}
		
		public override bool Validate ()
		{
			if (container == null) {
				container = gameObject.Value.GetComponent<InventoryContainer> ();
			}
			InventoryItem mItem = item.Value as InventoryItem;
			bool result = true;
			for (int i=0; i< mItem.ingredients.Count; i++) {
				InventoryItem.Ingredient ingredient=mItem.ingredients[i];
				if(container.GetItemStack(ingredient.item.itemName) < ingredient.amount){
					result=false;
				}
			}

			return result == equals.Value;
		}
	}
}