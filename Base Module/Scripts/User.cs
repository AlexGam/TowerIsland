using UnityEngine;
using System.Collections;

public class User {
	public string name;
	public string password;
	public Player player;

	public User(string username){
		this.name = username;
		player = new Player ();
	}
}

public class Player{
	public string name;
	public int level;
	public string custom;
}