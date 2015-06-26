using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class UIAttribute : MonoBehaviour {
	public string attribute;
	public UIAttribute.DesiplayRatio ratio;
	[SerializeField]
	private Text attributeName;
	[SerializeField]
	private Image attributeBar;
	[SerializeField]
	private Text currentValue;
	[SerializeField]
	private Text maximumValue;
	[SerializeField]
	private Button raiseButton;
	[HideInInspector]
	public int maxValue;
	[HideInInspector]
	public int curValue;
	private AttributeHandler handler;

	public void Initialize(AttributeHandler handler){
		this.handler = handler;	
		if (raiseButton != null) {
			raiseButton.onClick.AddListener (delegate {
				handler.IncreaseAttribute(attribute);
			});
		}
	}

	private void Update(){
		if(attributeBar != null ){
			attributeBar.fillAmount=Mathf.Lerp(attributeBar.fillAmount,(float)curValue/((float)maxValue+0.1f),Time.deltaTime*2);
		}

		if (raiseButton != null && handler != null) {
			raiseButton.gameObject.SetActive(handler.freePoints>0);
		}

	}

	public void OnAttributeChange(ObjectAttribute attribute){
		maxValue = (ratio==DesiplayRatio.CurrentToValue?(attribute.Value + attribute.TemporaryValue):(attribute.MaxValue + attribute.TemporaryValue));
		curValue = (ratio==DesiplayRatio.CurrentToValue?attribute.CurrentValue:attribute.Value);

		//Debug.Log (attribute.AttributeName+" "+(float)curValue +" "+ (float)maxValue);
		if (currentValue != null) {
			currentValue.text = curValue.ToString ();
		}
		if (maximumValue != null) {
			maximumValue.text = maxValue.ToString ();
		}
		if (attributeName != null) {
			attributeName.text = attribute.AttributeName;
		}
	}

	public enum DesiplayRatio{
		CurrentToValue,
		ValueToMaximum
	}
}
