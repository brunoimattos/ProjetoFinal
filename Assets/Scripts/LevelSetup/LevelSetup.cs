using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSetup : MonoBehaviour {

	
	public Transform DungeonCreator;
	public Transform Player_Marty;
	
	private Dungeon dungeon;
	
	
	
	void Start () {
		if(DungeonCreator == null) Debug.LogError("No Dungeon Creator assigned.");
		
		if(Player_Marty == null) Debug.LogError("No Player assigned.");
		
		dungeon = DungeonCreator.GetComponent<CreateDungeon>().MakeDungeon();
		
		placePlayer(Player_Marty, dungeon);
		
	}
	
	
	private void placePlayer(Transform player, Dungeon dungeon)
	{
		ConcreteRoom initialRoom = dungeon.getInitialRoom();		
				
		player.transform.position = new Vector3(initialRoom.getRoomPrefab().localScale.x * initialRoom.x, initialRoom.getRoomPrefab().localScale.y * initialRoom.y, -1);
	}
	
	void Update () {
	
	}
	
	void OnGUI()
	{
		if(GUI.Button(new Rect(0, 0, 100, 50), "Gerar de Novo"))
		{
			dungeon = DungeonCreator.GetComponent<CreateDungeon>().generateNewDungeon();
			placePlayer(Player_Marty, dungeon);
		}
	}
}
