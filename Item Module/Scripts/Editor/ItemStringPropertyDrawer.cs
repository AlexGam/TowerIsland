using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomPropertyDrawer(typeof(ItemStringAttribute))]
public class ItemStringPropertyDrawer : PropertyDrawer  {
	private ItemDatabase database;

	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
		EditorGUI.BeginProperty(position, label, property);
		if (database == null) {
			database=ItemDatabase.Load ();
		}
		string itemName=string.Empty;
		Color color = GUI.backgroundColor;

		if (property.objectReferenceValue != null) {
			itemName = (property.objectReferenceValue as BaseItem).itemName;
		} else {
			position.width-=20f;
			GUI.backgroundColor = Color.red;
		}

		itemName = EditorGUI.TextField (position,"Item", itemName);

		GUI.backgroundColor = color;

		BaseItem item=database.GetItem (itemName);
		item = item is InventoryItem ? item : null;
		property.objectReferenceValue = item;
		if (item == null) {
			position.x+=position.width;
			GUI.Label(position,EditorGUIUtility.FindTexture( "d_console.erroricon.sml" ));
		}
		EditorGUI.EndProperty();

	}
}
