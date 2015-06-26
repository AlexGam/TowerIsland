using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ICode;

public class UsableSlot : UISlot {
	public Image overlay;
	private bool isCoolDown;
	public bool IsCoolDown{
		get{
			return isCoolDown;
		}
	}
	private float coolDownDuration;
	private float coolDownInitTime;

	public override BaseItem Replace (BaseItem item)
	{
		return (container != null && (item == null || item is UsableItem )) ? container.Replace(id, item) : item;
	}

	public override void OnBeginDrag ()
	{
		if (!IsCoolDown) {
			base.OnBeginDrag ();
		}
	}

	public override void OnEndDrag ()
	{
		if (!IsCoolDown) {
			base.OnEndDrag();		
		}
	}

	public override void OnDrop ()
	{
		if (!IsCoolDown) {
			base.OnDrop ();
		}
	}

	public override void OnDoubleClick ()
	{
		if (!IsCoolDown && observedItem != null && (observedItem as UsableItem).onUse != null) {
			GameObject go = new GameObject ("UseItem");
			ICodeBehaviour behaviour = go.AddBehaviour((observedItem as UsableItem).onUse);

			behaviour.stateMachine.SetVariable ("Item", observedItem);
			behaviour.stateMachine.SetVariable ("Slot", gameObject);
			FsmVariable coolDown= (observedItem as UsableItem).onUse.GetVariable("CoolDown");
			if(coolDown != null){
				if(coolDown is FsmBool && (coolDown as FsmBool).Value){
					CoolDown((observedItem as UsableItem).coolDown,(observedItem as UsableItem).containerCoolDown);
				}
			}else {
				CoolDown((observedItem as UsableItem).coolDown,(observedItem as UsableItem).containerCoolDown);
			}
		}
	}

	public override void OnUpdate ()
	{
		if (overlay != null && isCoolDown) {
			if (Time.time - coolDownInitTime < coolDownDuration) {
				overlay.fillAmount = Mathf.Clamp01 (1 - ((Time.time - coolDownInitTime) / coolDownDuration));
			} else {
				overlay.fillAmount = 0;
			}
			isCoolDown = overlay.fillAmount > 0;
		}
	}

	public void CoolDown(float coolDown, float globalCoolDown){
		if (!isCoolDown && observedItem != null) {
			coolDownDuration = coolDown;
			coolDownInitTime = Time.time;
			isCoolDown = true;
			container.BroadcastMessage ("GlobalCoolDown", globalCoolDown, SendMessageOptions.DontRequireReceiver);
		}
	}
	
	private void GlobalCoolDown(float coolDown){
		if (observedItem != null) {
			if (((Time.time + coolDownInitTime * coolDownDuration) < (Time.time + coolDownInitTime * coolDown)) || !isCoolDown) {
				coolDownDuration = coolDown;
				coolDownInitTime = Time.time;
				isCoolDown=true;
			}
		}
	}
}
