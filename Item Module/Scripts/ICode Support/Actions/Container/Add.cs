using UnityEngine;
using System.Collections;

namespace ICode.Actions.ItemSystem{
	[Category("RPG/Modules/Item/Container")]
	[Tooltip("Adds an item to container.")]
	[System.Serializable]
	public class Add : StateAction {
		[SharedPersistent]
		[Tooltip("GameObject that has an UIContainer component.")]
		public FsmGameObject gameObject;
		[NotRequired]
		[Tooltip("Item name to add.")]
		public FsmString itemName;
		[NotRequired]
		[Shared]
		[Tooltip("Item to add.")]
		public FsmObject item;
		[Tooltip("Should random data been generated?")]
		public FsmBool generateRandomData;

		
		public override void OnEnter ()
		{
			UIContainer container = gameObject.Value.GetComponent<UIContainer> ();
			if (container != null) {
				if (item.Value != null) {
					BaseItem mItem=(BaseItem)Instantiate (item.Value);
					if(generateRandomData.Value){
						mItem.GenerateRandomData();
					}
					container.Add(mItem);
				} else {
					ItemDatabase database = ItemDatabase.Load ();
					if (database != null) {
						BaseItem mItem = (BaseItem)Instantiate (database.GetItem (itemName));
						if(generateRandomData.Value){
							mItem.GenerateRandomData();
						}
						container.Add (mItem);
					}
				}
			}
			Finish ();
		}
	}
}
