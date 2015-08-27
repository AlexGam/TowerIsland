using UnityEngine;
using System.Collections;

public class ExecutionHandler : BaseModule {
	public override string[] Callbacks {
		get {
			return new string[]{
				"Awake",
				"Start",
				"OnApplicationQuit",
				"OnApplicationPause",
				"OnLevelWasLoaded",
				"OnMouseDown"
			};
		}
	}

	private void Awake(){
		Execute ("Awake", new ModuleEventData ());
	}

	private void Start(){
		Execute ("Start", new ModuleEventData ());
	}

	private void OnApplicationQuit(){
		Execute ("OnApplicationQuit", new ModuleEventData ());
	}

	private void OnApplicationPause(){
		Execute ("OnApplicationPause", new ModuleEventData ());
	}

	private void OnLevelWasLoaded(int levelIndex){
		Execute ("OnLevelWasLoaded", new ModuleEventData ());
	}
}
