using UnityEngine;
using System.Collections;

public class InitialRoom : ConcreteRoom
{
	public InitialRoom(int x, int y, int width, int height)
		: base(x, y, width, height)
	{
	}
	
	public override void setRoomPrefab(RoomManager roomManagerScript)
	{
		this.roomPrefab = roomManagerScript.getRandomInitialRoom();
	}

	public override void setWallPrefab(RoomManager roomManagerScript)
	{
		this.leftWallPrefab = roomManagerScript.getRoomWall();
		this.rightWallPrefab = roomManagerScript.getRoomWall();
		this.topWallPrefab = roomManagerScript.getRoomWall();
		this.bottomWallPrefab = roomManagerScript.getRoomWall();
	}
	
	public override string ToString()
	{
		 return "Initial Room";
	}
}

