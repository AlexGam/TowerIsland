using UnityEngine;
using System.Collections;

public class RoomSystem : NetworkingMessageHandler {
	[SettingsSelect]
	public RoomSettings settings;
	private static RoomSystem instance;
	public static new RoomSystem current{
		get{
			if(instance== null){
				GameObject go= new GameObject("RoomSystem");
				go.AddComponent<RoomSystem>();
				return current;
			}
			return instance;
		}
	}

	private void Awake(){
		instance = this;
	}
}
