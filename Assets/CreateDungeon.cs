using UnityEngine;
using System.Collections.Generic;

public class CreateDungeon : MonoBehaviour
{

	public int gridWidth;
	public int gridHeight;
	public Transform roomPrefab;
	
	
	private int[,] dungeonMap;
	private List<ConcreteRoom> createdRooms;
	private List<Vector2> candidateNeighbors;
	
	void Start()
	{
		
		createdRooms = new List<ConcreteRoom>();
		candidateNeighbors = new List<Vector2>();
		
				
		dungeonMap = new int[gridWidth, gridHeight];
		
		//debugMap(dungeonMap);
		
		generateDungeon(ref dungeonMap, gridWidth, gridHeight);
		
		spawnRooms(dungeonMap, roomPrefab);
	}
	
	void updateNeighbors(List<Vector2> newNeighbors)
	{
		foreach(Vector2 neighbor in newNeighbors)
		{
			if(!candidateNeighbors.Contains(neighbor))
			{
				candidateNeighbors.Add(neighbor);
			}
		}
	}
	
	private Vector2 getRandomNeighbor()
	{
		Vector2 rndNeighbor;
		
		int rnd = Random.Range(0, candidateNeighbors.Count);
		
		rndNeighbor = candidateNeighbors[rnd];
		candidateNeighbors.Remove(rndNeighbor);
		
		return rndNeighbor;
	}
	
	private void generateDungeon(ref int[,] map, int width, int height)
	{
		int nRooms = Random.Range(8, 14);
		Vector2 pivot;
		ConcreteRoom auxRoom = new ConcreteRoom(width/2, height/2, width, height);
		
		map[auxRoom.x, auxRoom.y] = 1;
		
		//adding to created room list
		createdRooms.Add(auxRoom);
		
		//adding the neighbors to the neighbors list
		updateNeighbors(auxRoom.neighbors);
		
		nRooms--;
				
		while(nRooms > 0)
		{
			pivot = getRandomNeighbor();
			auxRoom = new ConcreteRoom((int)pivot.x, (int)pivot.y, height, width);
			
			//don't allow duplicate room creation
			while(createdRooms.IndexOf(auxRoom) >= 0)
			{
				pivot = getRandomNeighbor();
				auxRoom = new ConcreteRoom((int)pivot.x, (int)pivot.y, height, width);
			}
			
			createdRooms.Add(auxRoom);
			updateNeighbors(auxRoom.neighbors);
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
				if(map[j,i] == 1)
				{
					instPosition = new Vector3(roomPrefab.localScale.x * i, roomPrefab.localScale.y * j, 0);
					concreteRoom = GameObject.Instantiate(roomPrefab, instPosition, Quaternion.identity) as Transform;
				}
			}	
		}
	}
}
