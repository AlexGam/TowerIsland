using UnityEngine;
using UnityEditor;
using System.Collections;

public static class CreateItemDatabase {
	[MenuItem("Assets/Create/Zerano Assets/RPG/Item/ItemDatabase")]
	public static void CreateItemDatabaseAsset()
	{
		ICode.FSMEditor.AssetCreator.CreateAsset<ItemDatabase>();
	}
}
