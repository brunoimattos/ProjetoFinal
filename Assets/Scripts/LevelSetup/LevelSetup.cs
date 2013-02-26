using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSetup : MonoBehaviour {

	
	public Transform DungeonCreator;
	public Transform Player_Marty;
	
	private List<ConcreteRoom> dungeonRooms;
	
	
	void Start () {
		if(DungeonCreator == null) Debug.LogError("No Dungeon Creator assigned.");
		
		if(Player_Marty == null) Debug.LogError("No Player assigned.");
		
		dungeonRooms = DungeonCreator.GetComponent<CreateDungeon>().MakeDungeon();
		
		placePlayer(Player_Marty, dungeonRooms);
		
	}
	
	
	private void placePlayer(Transform player, List<ConcreteRoom> rooms)
	{
		int rnd = Random.Range(0, rooms.Count);
		
		ConcreteRoom rndRoom = rooms[rnd];
		
		Debug.Log(rndRoom.ToString());
		
		player.transform.position = new Vector3(rndRoom.getRoomPrefab().localScale.x * rndRoom.x, rndRoom.getRoomPrefab().localScale.y * rndRoom.y, -1);
	}
	
	void Update () {
	
	}
	
	void OnGUI()
	{
		if(GUI.Button(new Rect(0, 0, 100, 50), "Gerar de Novo"))
		{
			dungeonRooms = DungeonCreator.GetComponent<CreateDungeon>().generateNewDungeon();
			placePlayer(Player_Marty, dungeonRooms);
		}
	}
}
