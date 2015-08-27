using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ItemUtility  {
	private static Dictionary<int,UIContainer> cache = new Dictionary<int, UIContainer> ();

	public static UIContainer GetContainer(int id){
		UIContainer container;
		if (cache.TryGetValue (id, out container)) {
			return container;
		} else {
			UIContainer[] containers = GameObject.FindObjectsOfType<UIContainer> ();
			for (int i = 0; i < containers.Length; i++) {
				container=containers[i];
				if(!cache.ContainsKey(container.id)){
					cache.Add(container.id,container);	
				}
			}
			cache.TryGetValue (id, out container);	
		}
		return container;
	}

	public static T GetContainer<T>() where T : UIContainer{
		T[] containers = GameObject.FindObjectsOfType<T> ();
		return containers.Length > 0 ? containers [0] : default(T);
	}

	public static T[] GetContainers<T>() where T : UIContainer{
		T[] containers = GameObject.FindObjectsOfType<T> ();
		return containers;
	}
}
