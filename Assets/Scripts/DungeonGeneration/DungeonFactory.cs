using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonFactory : IDungeonFactory
{
	private Dungeon dungeon;
	
	public Dungeon createDungeon(int width, int height, float linearFactor)
	{
		dungeon = new Dungeon(width, height);
		
		int nRooms = dungeon.getNRooms();
		
		
		List<ConcreteRoom> createdRooms = new List<ConcreteRoom>();
		List<Vector2> lastInsertedNeighbors = new List<Vector2>();
		List<Vector2> candidateNeighbors = new List<Vector2>();
		
		Vector2 pivot;
		
		ConcreteRoom auxRoom = new ConcreteRoom(width/2, height/2, width, height);
		//map[auxRoom.x, auxRoom.y] = 1;
		
		//adding to created room list
		createdRooms.Add(auxRoom);
		
		//adding the neighbors to the neighbors list
		lastInsertedNeighbors = updateNeighbors(candidateNeighbors, auxRoom.neighbors);
		
		nRooms--;
		
		
		while(nRooms > 0)
		{
			pivot = getRandomNeighbor(candidateNeighbors, lastInsertedNeighbors, linearFactor);	
			auxRoom = new ConcreteRoom((int)pivot.x, (int)pivot.y, width, height);
			
			//don't allow duplicate room creation
			while(createdRooms.IndexOf(auxRoom) >= 0)
			{
				pivot = getRandomNeighbor(candidateNeighbors, lastInsertedNeighbors, linearFactor);
				auxRoom = new ConcreteRoom((int)pivot.x, (int)pivot.y, width, height);
			}
			
			createdRooms.Add(auxRoom);
			lastInsertedNeighbors = updateNeighbors(candidateNeighbors, auxRoom.neighbors);
			nRooms--;
			//map[auxRoom.x, auxRoom.y] = 1;
		}		
		
		
		
		//assigning initial Room
		int initialRoomIndex = Random.Range(0, createdRooms.Count);
		ConcreteRoom cRoom = createdRooms[initialRoomIndex];
		createdRooms.Remove (cRoom);
		InitialRoom initialRoom = new InitialRoom(cRoom.x, cRoom.y, width, height);
		createdRooms.Insert(initialRoomIndex, initialRoom);
		dungeon.setInitialRoom(initialRoomIndex);
		
		//assigning final room
		
			
		dungeon.setDungeonRooms(createdRooms);	
			
		return dungeon;
	}
	
	private Vector2 getRandomNeighbor(List<Vector2> candidateNeighbors, List<Vector2> lastInsertedNeighbors, float linearFactor)
	{
		Vector2 rndNeighbor;
		int rnd;	
		//Random.Range(0, 11) == random 10-sided die roll.
		//TODO: is linearFactor a intuitive name?
		if(Random.Range(0, 11) >= linearFactor && lastInsertedNeighbors.Count > 0){
			rnd = Random.Range(0, lastInsertedNeighbors.Count);
			rndNeighbor = lastInsertedNeighbors[rnd];
		} else {
			rnd = Random.Range(0, candidateNeighbors.Count - lastInsertedNeighbors.Count);
			rndNeighbor = candidateNeighbors[rnd];
		}
		
		candidateNeighbors.Remove(rndNeighbor);
		
		return rndNeighbor;
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
}

