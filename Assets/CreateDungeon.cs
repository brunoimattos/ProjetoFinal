using UnityEngine;
using System.Collections.Generic;

public class CreateDungeon : MonoBehaviour
{

	public int gridWidth;
	public int gridHeight;
	public Transform roomPrefab;
	public float linearProb;
	
	
	private int[,] dungeonMap;
	private List<ConcreteRoom> createdRooms;
	
	void Start()
	{
		
		createdRooms = new List<ConcreteRoom>();		
				
		dungeonMap = new int[gridWidth, gridHeight];
		
		//debugMap(dungeonMap);
		
		generateDungeon(ref dungeonMap, gridWidth, gridHeight);
		
		spawnRooms(dungeonMap, roomPrefab);
	}
	
	List<Vector2> updateNeighbors(List<Vector2> candidateNeighbors, List<Vector2> newNeighbors)
	{
		List<Vector2> insertedNeighbors = new List<Vector2>();
		
		foreach(Vector2 neighbor in newNeighbors)
		{
			if(!candidateNeighbors.Contains(neighbor))
			{
				candidateNeighbors.Add(neighbor);
				insertedNeighbors.Add(neighbor);
			}
		}
		
		return insertedNeighbors;
	}
	
	private Vector2 getRandomNeighbor(List<Vector2> candidateNeighbors, List<Vector2> lastInsertedNeighbors)
	{
		Vector2 rndNeighbor;
		int rnd;	

		if(Random.Range(0, 11) >= linearProb && lastInsertedNeighbors.Count > 0){
			rnd = Random.Range(0, lastInsertedNeighbors.Count);
			rndNeighbor = lastInsertedNeighbors[rnd];
		} else {
			rnd = Random.Range(0, candidateNeighbors.Count - lastInsertedNeighbors.Count);
			rndNeighbor = candidateNeighbors[rnd];
		}
		
		candidateNeighbors.Remove(rndNeighbor);
		
		return rndNeighbor;
	}
	
	private void generateDungeon(ref int[,] map, int width, int height)
	{
		
		int nRooms = Random.Range(8, 14);
		Debug.Log("nRooms: " + nRooms);
		
		List<Vector2> lastInsertedNeighbors = new List<Vector2>();
		List<Vector2> candidateNeighbors = new List<Vector2>();
		
		Vector2 pivot;
		ConcreteRoom auxRoom = new ConcreteRoom(width/2, height/2, width, height);
		
		map[auxRoom.x, auxRoom.y] = 1;
		
		//adding to created room list
		createdRooms.Add(auxRoom);
		
		//adding the neighbors to the neighbors list
		lastInsertedNeighbors = updateNeighbors(candidateNeighbors, auxRoom.neighbors);
		
		nRooms--;
				
		while(nRooms > 0)
		{
			pivot = getRandomNeighbor(candidateNeighbors, lastInsertedNeighbors);	
			auxRoom = new ConcreteRoom((int)pivot.x, (int)pivot.y, width, height);
			
			//don't allow duplicate room creation
			while(createdRooms.IndexOf(auxRoom) >= 0)
			{
				pivot = getRandomNeighbor(candidateNeighbors, lastInsertedNeighbors);
				auxRoom = new ConcreteRoom((int)pivot.x, (int)pivot.y, width, height);
			}
			
			createdRooms.Add(auxRoom);
			lastInsertedNeighbors = updateNeighbors(candidateNeighbors, auxRoom.neighbors);
			nRooms--;
			map[auxRoom.x, auxRoom.y] = 1;
		}		
	}
	
	void debugMap(int[,] map)
	{
		string line = "";
		for(int j = 0; j < gridHeight; j++)
		{
			for(int i = 0; i < gridWidth; i++)
			{
				line += map[j,i].ToString();
			}	
			Debug.Log(line);
			Debug.Log("\n");
			line = "";
		}
	}
	
	void spawnRooms(int[,] map, Transform roomPrefab)
	{
		Transform concreteRoom;
		Vector3 instPosition;
		
		for(int j = 0; j < gridHeight; j++)
		{
			for(int i = 0; i < gridWidth; i++)
			{
				if(map[i,j] == 1)
				{
					instPosition = new Vector3(roomPrefab.localScale.x * i, roomPrefab.localScale.y * j, 0);
					concreteRoom = GameObject.Instantiate(roomPrefab, instPosition, Quaternion.identity) as Transform;
				}
			}	
		}
	}
	
	void clearDungeonMap(ref int[,] map)
	{
		map = new int[gridWidth, gridHeight];	
		
		GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
		
		foreach(GameObject room in rooms)
		{
			GameObject.Destroy(room);
		}
	}
	
	void generateNewDungeon()
	{
		clearDungeonMap(ref dungeonMap);
		
		generateDungeon(ref dungeonMap, gridWidth, gridHeight);
		
		spawnRooms(dungeonMap, roomPrefab);
	}
	
	void OnGUI()
	{
		if(GUI.Button(new Rect(0, 0, 100, 50), "Gerar de Novo"))
		{
			generateNewDungeon();
		}
	}
}
