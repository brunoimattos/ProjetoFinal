using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonFactory : IDungeonFactory
{
	private Dungeon dungeon;
	
	public Dungeon createDungeon(int width, int height, float linearFactor)
	{
		dungeon = new Dungeon(width, height);
		
		List<ConcreteRoom> generatedRooms = createDungeonRooms(width, height, linearFactor);
		
		dungeon.setDungeonRooms(generatedRooms);
		
		return dungeon;
		
	}
	
	private List<ConcreteRoom> createDungeonRooms(int width, int height, float linearFactor)
	{
		//List of rooms the dungeon will actually have.
		List<ConcreteRoom> createdRooms = new List<ConcreteRoom>();
		
		//List of neigbors of the last inserted room. This is used for trying to keep the
		// dungeon more linear.
		List<Vector2> lastInsertedNeighbors = new List<Vector2>();
		
		//List containing all possible neighbors for random selection.
		List<Vector2> candidateNeighbors = new List<Vector2>();
		
		Vector2 pivot;
				
		//Creating the initial room
		ConcreteRoom auxRoom = createInitialRoom(dungeon);
			
		
		//adding to created room list
		createdRooms.Add(auxRoom);
		dungeon.setInitialRoom(0);
		
		//adding the neighbors to the neighbors list
		lastInsertedNeighbors = updateNeighbors(candidateNeighbors, getRoomPossibleNeighbors(auxRoom, dungeon));

		
		List<ConcreteRoom> regularRooms = createRegularRooms(createdRooms, candidateNeighbors, lastInsertedNeighbors, linearFactor, dungeon);
		
		foreach(ConcreteRoom cRoom in regularRooms)
		{
			createdRooms.Add(cRoom);
		}		
		
		//Creating the final room
		
		auxRoom = createFinalRoom(createdRooms, candidateNeighbors, lastInsertedNeighbors, linearFactor, dungeon);
		createdRooms.Add (auxRoom);

		dungeon.setFinalRoom(createdRooms.Count-1);
		
		return createdRooms;
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
	
	private List<Vector2> updateNeighbors(List<Vector2> candidateNeighbors, List<Vector2> newNeighbors)
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
	
	private List<Vector2> getRoomPossibleNeighbors(ConcreteRoom room, Dungeon dungeon)
	{
		List<Vector2> neighbors = new List<Vector2>();
		
		//West Neighbor
		if(room.x > 0)
			neighbors.Add(new Vector2(room.x + (-1), room.y + 0));
		
		//East Neighbor
		if(room.x < dungeon.width - 1)
			neighbors.Add(new Vector2(room.x + 1, room.y + 0));
		
		//North Neighbor
		if(room.y > 0)
			neighbors.Add(new Vector2(room.x + 0, room.y + (-1)));
		
		//South Neighbor
		if(room.y < dungeon.height - 1)
			neighbors.Add(new Vector2(room.x + 0, room.y + 1));
		
		return neighbors;
	}
	
	private InitialRoom createRandomInitialRoom(Dungeon dungeon)
	{
		Vector2 pivot = new Vector2(Random.Range(0, dungeon.width), Random.Range(0, dungeon.height));
		return  new InitialRoom((int)pivot.x, (int)pivot.y, dungeon.width, dungeon.height);
		
	}
	
	private ConcreteRoom createRandomRegularRoom(List<ConcreteRoom> createdRooms, List<Vector2> candidateNeighbors, List<Vector2> lastInsertedNeighbors , float linearFactor, Dungeon dungeon)
	{
		
		
		Vector2 pivot = getRandomNeighbor(candidateNeighbors, lastInsertedNeighbors, linearFactor);	
		ConcreteRoom auxRoom = new ConcreteRoom((int)pivot.x, (int)pivot.y, dungeon.width, dungeon.height);
		
		//don't allow duplicate room creation
		while(createdRooms.IndexOf(auxRoom) >= 0)
		{
			pivot = getRandomNeighbor(candidateNeighbors, lastInsertedNeighbors, linearFactor);	
			auxRoom = new ConcreteRoom((int)pivot.x, (int)pivot.y, dungeon.width, dungeon.height);
		}
			
		return auxRoom;
		//return new RegularRoom(...)
	}
	
	private FinalRoom createRandomFinalRoom(List<ConcreteRoom> createdRooms, List<Vector2> candidateNeighbors, List<Vector2> lastInsertedNeighbors , float linearFactor, Dungeon dungeon)
	{
		Vector2 pivot = getRandomNeighbor(candidateNeighbors, lastInsertedNeighbors, linearFactor);	
		FinalRoom auxRoom = new FinalRoom((int)pivot.x, (int)pivot.y, dungeon.width, dungeon.height);
		
		
		//don't allow duplicate room creation
		while(createdRooms.IndexOf(auxRoom) >= 0)
		{
			pivot = getRandomNeighbor(candidateNeighbors, lastInsertedNeighbors, linearFactor);	
			auxRoom = new FinalRoom((int)pivot.x, (int)pivot.y, dungeon.width, dungeon.height);
		}
			
		return auxRoom;
	}
		
	private InitialRoom createInitialRoom(Dungeon dungeon)
	{
		return createRandomInitialRoom(dungeon);
	}
	
	private List<ConcreteRoom> createRegularRooms(List<ConcreteRoom> createdRooms, List<Vector2> candidateNeighbors, List<Vector2> lastInsertedNeighbors , float linearFactor, Dungeon dungeon)
	{
		List<ConcreteRoom> regularRooms = new List<ConcreteRoom>();
		ConcreteRoom auxRoom;
		int nRooms = dungeon.getNRooms() - createdRooms.Count - 1; // will not work if we have more than 1 final room.
		
		while(nRooms > 0)
		{
			auxRoom = createRandomRegularRoom(createdRooms, candidateNeighbors, lastInsertedNeighbors, linearFactor, dungeon);
			lastInsertedNeighbors = updateNeighbors(candidateNeighbors, getRoomPossibleNeighbors(auxRoom, dungeon));
			regularRooms.Add(auxRoom);
			nRooms--;
		}
		
		return regularRooms;
	}
	
	private FinalRoom createFinalRoom(List<ConcreteRoom> createdRooms, List<Vector2> candidateNeighbors, List<Vector2> lastInsertedNeighbors , float linearFactor, Dungeon dungeon)
	{
		return createRandomFinalRoom(createdRooms, candidateNeighbors, lastInsertedNeighbors, linearFactor, dungeon);
	}
}

