using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ObjectAttribute {
	[SerializeField]
	private string attributeName="New Attribute";
	public string AttributeName{
		get{
			return this.attributeName;
		}
	}
	[SerializeField]
	private int maxValue;
	public int MaxValue{
		get{
			return this.maxValue;
		}
		set{
			if(maxValue != value){
				this.maxValue=value;
				onChange.Invoke(this);
			}
		}
	}
	[SerializeField]
	private int value;
	public int Value{
		get{
			return this.value;
		}
		set{
			int mValue = this.value;
			if(this.value != value){
				this.value=value;
				onChange.Invoke(this);
				if(this.value>mValue){
					onIncrease.Invoke(this);
				}else{
					onDecrease.Invoke(this);
				}
				if(this.value >= maxValue){
					onMaximumReach.Invoke(this);
				}
			}
		}
	}

	private int curValue;
	public int CurrentValue{
		get{
			return this.curValue;
		}
		set{
			if(curValue != value){
				this.curValue=value;
				onChange.Invoke(this);
			}
		}
	}
	private int tempValue;
	public int TemporaryValue{
		get{
			return this.tempValue;
		}
		set{
			if(tempValue != value){
				this.tempValue=value;
				onChange.Invoke(this);
			}
		}
	}

	public int startValue;

	public bool referenced;
	public string referenceName;
	public float multiplier=100f;
	public AnimationCurve reference;

	[SerializeField]
	private AttributeChangeEvent onChange;
	public AttributeChangeEvent OnChange{
		get{
			if(onChange == null){
				onChange=new AttributeChangeEvent();
			}
			return onChange;
		}
	}

	[SerializeField]
	private AttributeChangeEvent onIncrease;
	public AttributeChangeEvent OnIncrease{
		get{
			if(onIncrease == null){
				onIncrease=new AttributeChangeEvent();
			}
			return onIncrease;
		}
	}

	[SerializeField]
	private AttributeChangeEvent onDecrease;
	public AttributeChangeEvent OnDecrease{
		get{
			if(onDecrease == null){
				onDecrease=new AttributeChangeEvent();
			}
			return onDecrease;
		}
	}

	[SerializeField]
	private AttributeChangeEvent onMaximumReach;
	public AttributeChangeEvent OnMaximumReach{
		get{
			if(onMaximumReach == null){
				onMaximumReach=new AttributeChangeEvent();
			}
			return onMaximumReach;
		}
	}

	public ObjectAttribute(){}

	public ObjectAttribute(ObjectAttribute other){
		CopyFrom (other);
	}

	public void CopyFrom(ObjectAttribute other){
		attributeName = other.attributeName;
		maxValue = other.maxValue;
		value = other.value;
		curValue = other.curValue;
		tempValue = other.tempValue;
		startValue = other.startValue;
		referenced = other.referenced;
		referenceName = other.referenceName;
		multiplier = other.multiplier;
		reference = other.reference;

	}

	public void SetRaw(int curValue,int value,int maxValue){
		this.curValue = curValue;
		this.value = value;
		this.maxValue = maxValue;
		OnChange.Invoke (this);
	}

	public void Refresh(){
		CurrentValue = Value + TemporaryValue;
	}
}

[System.Serializable]
public class AttributeChangeEvent:UnityEvent<ObjectAttribute>{
	
}