using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ICode;

[System.Serializable]
public class EquipmentItem : InventoryItem {
	//Region to equip
	public EquipmentRegion equipmentRegion;
	public List<BonusAttribute> bonus;
	public StateMachine defaultAttack;

	public override void GenerateRandomData ()
	{
		base.GenerateRandomData ();
		foreach (BonusAttribute attr in bonus) {
			attr.curValue = Random.Range (attr.minValue, attr.maxValue);		
		}	
	}

	public override string GetTooltip ()
	{
		string t= base.GetTooltip ();
		foreach (BonusAttribute attr in bonus) {
			if(attr.curValue != 0){

				t+=	"\n"+UITools.ColorString((attr.curValue>0?" +":"")+attr.curValue.ToString()+" "+attr.name,attr.curValue>0?Color.green:Color.red);

			}
		}
		return t;
	} 

	public override string Serialize ()
	{
		string bonusData=string.Empty;
		foreach(BonusAttribute attr in bonus){
			bonusData+=attr.name+"."+attr.curValue.ToString()+"#";
		}
		return base.Serialize ()+";"+bonusData;
	}
	
	public override void Deserialize (string[] data)
	{
		base.Deserialize (data);
		if (data.Length > 3) {
			string[] bonusDataSplit = data [3].Split ('#');
			foreach (string bonusSplit in bonusDataSplit) {
				string[] bonusData = bonusSplit.Split ('.');
				if (bonusData.Length > 1) {
					string attrName = bonusData [0];
					int attrValue = 0;
					if(int.TryParse(bonusData[1],out attrValue)){
					//int attrValue = System.Convert.ToInt32 (bonusData [1]);
						bonus.Find (x => x.name == attrName).curValue = attrValue;
					}
				}
			}
		}
	}

}
