using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ICode{
	public class ICodeMaster : MonoBehaviour {
		public List<ComponentModel> components;

		[System.Serializable]
		public class ComponentModel{
			public Component component;
			public bool show;

			public ComponentModel(Component component,bool show){
				this.component=component;
				this.show=show;
			}
		}
	}
}