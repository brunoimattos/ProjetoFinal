using UnityEngine;
using System.Collections;

public class InitialRoom : ConcreteRoom
{
	public InitialRoom(int x, int y, int width, int height) : base(x, y, width, height)
	{
		Debug.Log("Created initial room!");
	}
	
	public override void setRoomPrefab(RoomManager roomManagerScript)
	{
		Debug.Log("I got called!");
		this.roomPrefab = roomManagerScript.getRandomInitialRoom();
	}
	
	public override string ToString()
	{
		 return "Initial Room";
	}
}

