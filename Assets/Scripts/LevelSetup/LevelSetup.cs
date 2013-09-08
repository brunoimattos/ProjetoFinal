using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSetup : MonoBehaviour {

	
	public Transform DungeonCreator;
	public Transform Player_Marty;
	
	private Dungeon dungeon;
	
	
	
	void Start () {
		CameraBehaviour mainCameraBehaviour;
		
		mainCameraBehaviour = Camera.main.GetComponent<CameraBehaviour>();
		
		if(DungeonCreator == null) Debug.LogError("No Dungeon Creator assigned.");
		
		if(Player_Marty == null) Debug.LogError("No Player assigned.");
		
		if(mainCameraBehaviour == null) Debug.LogError("No camera assigned.");
		
		dungeon = DungeonCreator.GetComponent<CreateDungeon>().MakeDungeon();
		
		
		placePlayerAndCamera(Player_Marty, dungeon, mainCameraBehaviour);
		
	}
	
	
	private void placePlayerAndCamera(Transform player, Dungeon dungeon, CameraBehaviour mainCameraBehaviour)
	{
		Vector3 initialPosition;
		
		ConcreteRoom initialRoom = dungeon.getInitialRoom();		
		
		
		initialPosition = new Vector3(initialRoom.getRoomPrefab().localScale.x * initialRoom.x, 1.1f, initialRoom.getRoomPrefab().localScale.z * initialRoom.y);
		mainCameraBehaviour.setInitialPosition(initialPosition);
				
		player.transform.position = initialPosition;
	}
	
	void Update () {
	
	}
	
	void OnGUI()
	{
		CameraBehaviour mainCameraBehaviour;
		
		if(GUI.Button(new Rect(0, 0, 100, 50), "Gerar de Novo"))
		{
			mainCameraBehaviour = Camera.main.GetComponent<CameraBehaviour>();
			dungeon = DungeonCreator.GetComponent<CreateDungeon>().generateNewDungeon();
			placePlayerAndCamera(Player_Marty, dungeon, mainCameraBehaviour);
		}
	}
}
