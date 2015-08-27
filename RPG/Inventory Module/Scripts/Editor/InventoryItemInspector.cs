using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditorInternal;
using ICode;
using StateMachine = ICode.StateMachine;
using ArrayUtility=ICode.ArrayUtility;

[CustomEditor(typeof(InventoryItem),true)]
public class InventoryItemInspector : UsableItemInspector {
	private ReorderableList ingredientList;

	protected override void OnEnable(){
		base.OnEnable ();
		ingredientList = new ReorderableList(serializedObject, 
		                               serializedObject.FindProperty("ingredients"), 
		                               true, true, true, true);
		ingredientList.drawElementCallback =  (Rect rect, int index, bool isActive, bool isFocused) => {
			var element = ingredientList.serializedProperty.GetArrayElementAtIndex(index);
			SerializedProperty itemProperty=element.FindPropertyRelative("item");
			rect.y += 2;
			rect.height=EditorGUIUtility.singleLineHeight;

			string itemName=itemProperty.objectReferenceValue!= null?(itemProperty.objectReferenceValue as InventoryItem).itemName:string.Empty;
			Color color = GUI.backgroundColor;
			GUI.backgroundColor = itemProperty.objectReferenceValue == null ? Color.red : color;
			rect.width-=55f;
			itemName=EditorGUI.TextField(rect,itemName);
			GUI.backgroundColor = color;
			BaseItem item=database.GetItem(itemName);
			itemProperty.objectReferenceValue = item;
			rect.x+=rect.width+5;
			rect.width=50f;
			SerializedProperty amount=element.FindPropertyRelative("amount");
			EditorGUI.PropertyField(rect,amount,GUIContent.none);
			amount.intValue=Mathf.Clamp(amount.intValue,1,int.MaxValue);
		};
		ingredientList.drawHeaderCallback = (Rect rect) => {  
			EditorGUI.LabelField(rect, "Ingredients");
		};
	}
	
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		serializedObject.Update();
		GUILayout.BeginHorizontal ();
		SerializedProperty prefabProperty = DrawProperty ("prefab");
		if (prefabProperty.objectReferenceValue != null) {
			GameObject mPrefab=prefabProperty.objectReferenceValue as GameObject;
			if(mPrefab.GetComponent<WorldItem>() == null ||
			   mPrefab.GetComponent<Collider>()== null ||
			   mPrefab.GetComponent<Rigidbody>() == null){
				Color color=GUI.backgroundColor;
				GUI.backgroundColor=Color.red;
				if(GUILayout.Button("Setup",GUILayout.Width(70))){
					GameObject prefab=(GameObject)Instantiate(mPrefab);
					if(prefab.GetComponent<WorldItem>() == null){
						WorldItem worldItem=prefab.AddComponent<WorldItem>();
						worldItem.item =target as InventoryItem;
						if(worldItem.containerIds == null){
							worldItem.containerIds= new int[0];
						}
						worldItem.containerIds=ArrayUtility.Add(worldItem.containerIds,4);
					}
					if(prefab.GetComponent<Collider>() == null){
						MeshCollider collider=prefab.AddComponent<MeshCollider>();
						collider.convex=true;
					}
					if(prefab.GetComponent<Rigidbody>() == null){
						prefab.AddComponent<Rigidbody>();
					}
					string mPath = EditorUtility.SaveFilePanelInProject (
						"Create Prefab" + prefab.name,
						"New " + prefab.name + ".prefab",
						"prefab", "");
					if(!string.IsNullOrEmpty(mPath)){
						GameObject mGameObject=PrefabUtility.CreatePrefab(mPath, prefab);
						AssetDatabase.SaveAssets();
						serializedObject.Update();
						prefabProperty.objectReferenceValue=mGameObject;
						serializedObject.ApplyModifiedProperties();
					}
					DestroyImmediate(prefab);
				}
				GUI.backgroundColor=color;
			}
		}
		GUILayout.EndHorizontal ();
		DrawProperties ("buyPrice","sellPrice","stack","maxStack");
		SerializedProperty craftable = DrawProperty ("craftable");
		if (craftable.boolValue) {
			SerializedProperty onCraft= serializedObject.FindProperty("onCraft");
			if (onCraft.objectReferenceValue != null) {
				StateMachine stateMachine=onCraft.objectReferenceValue as StateMachine;
				FsmVariable k= stateMachine.GetVariable("Item");
				if(k == null || !(k is FsmObject)){
					EditorGUILayout.HelpBox("On Craft state machine should contain a FsmObject variable with the name \"Item\"",MessageType.Error);
				}
			}
			DrawProperties("onCraft","craftDuration");
			GUILayout.Space(5f);
			GUILayout.BeginVertical(EditorStyles.inspectorFullWidthMargins);
			ingredientList.DoLayoutList();
			GUILayout.EndVertical();
			GUILayout.Space (5f);
		}
		serializedObject.ApplyModifiedProperties ();
	}
}
