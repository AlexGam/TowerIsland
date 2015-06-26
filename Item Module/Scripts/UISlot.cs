using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UISlot: MonoBehaviour ,IDropHandler,IPointerUpHandler,IPointerEnterHandler,IPointerExitHandler,IBeginDragHandler, IDragHandler, IEndDragHandler {
	public int id;
	public Image icon;
	[HideInInspector]
	public UIContainer container;
	public static BaseItem draggedItem;
	private BaseItem updatedItem;
//	private ScrollRect scrollRect;

	public BaseItem observedItem{
		get{
			return (container != null) ? container.GetItem(id) : null;
		}
	}

	public void Initialize(int id, UIContainer container){
		this.id = id;
		this.container = container;
		//this.scrollRect=GetComponentInParent<ScrollRect> ();;
	}

	public virtual BaseItem Replace (BaseItem item)
	{
		return (container != null) ? container.Replace(id, item) : item;
	}

	#region Unity Messages
	private void Update(){
		OnUpdate ();
		if (updatedItem != observedItem) {
			updatedItem = observedItem;
			OnItemUpdate();
		}
	}

	public void OnBeginDrag(PointerEventData eventData){
		OnBeginDrag ();
		UpdateCursor();
	}
	
	public void OnDrag(PointerEventData data){
		OnDrag ();
	}
	
	public void OnEndDrag(PointerEventData eventData){
		OnEndDrag ();
		UpdateCursor();
	}
	
	public void OnDrop(PointerEventData data){
		OnDrop ();
		UpdateCursor();
	}

	public void OnPointerEnter(PointerEventData eventData){
		if (draggedItem == null) {
			UITooltip.Show (observedItem!= null?observedItem.GetTooltip():string.Empty, 200);
		}
	}

	public void OnPointerUp(PointerEventData eventData){
		OnClick ();
		if (eventData.clickCount > 1) {
			OnDoubleClick();		
		}
	}

	public void OnPointerExit(PointerEventData eventData){
		UITooltip.Hide ();
	}

	#endregion
	public virtual void OnUpdate(){}

	public virtual void OnBeginDrag()
	{
		if (observedItem != null){
			draggedItem = Replace(null);
			UpdateCursor();
		}
	}
	
	public virtual void OnDrag(){}
	
	public virtual void OnEndDrag()
	{
		if (draggedItem != null && !draggedItem.dirty) {
			BaseItem item = Replace(draggedItem);
			draggedItem = item;
		}
	}
	
	public virtual void OnDrop()
	{
		if (draggedItem != null && !draggedItem.dirty) {
			BaseItem item = Replace (draggedItem);
			draggedItem = item;
			UpdateCursor ();
		}
	}

	public virtual void OnClick(){}

	public virtual void OnDoubleClick(){}

	public virtual void OnItemUpdate(){
		if (icon != null) {
			if (observedItem == null) {
				icon.enabled=false;
			}else{
				icon.sprite = observedItem.icon;
				icon.enabled=true;
			}
		}
	}

	private void UpdateCursor ()
	{
		if (draggedItem != null){
			UICursor.Set(draggedItem.icon);
		}else{
			UICursor.Clear();
		}
	}
}
