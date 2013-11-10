using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	private List<string> items;
	
	void Start () {
		items = new List<string>();
	}
	
	public bool HasItem(string name){
		if(items.Contains(name)){
			return true;
		}
		return false;
	}
	
	public void AddItem(string name){
		items.Add(name);
		
		Debug.Log("Items in inventory: ");
		foreach(string item in items)
			Debug.Log(item);
	}
}
