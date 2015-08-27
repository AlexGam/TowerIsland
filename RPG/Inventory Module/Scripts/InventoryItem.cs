using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class InventoryItem : UsableItem {
	//Item prefab
	public GameObject prefab;
	public int buyPrice;
	public int sellPrice;
	public int stack = 1;
	public int maxStack=20;

	public bool craftable = true;
	public ICode.StateMachine onCraft;
	public float craftDuration=2.0f;
	[ItemString]
	public List<Ingredient> ingredients;

	[System.Serializable]
	public class Ingredient{
		public InventoryItem item;
		public int amount;
	}

	public override string Serialize ()
	{
		return base.Serialize ()+";"+stack;
	}
	
	public override void Deserialize (string[] data)
	{
		stack=System.Convert.ToInt32(data[2]);
	}
}
