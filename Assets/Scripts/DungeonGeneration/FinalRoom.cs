using UnityEngine;
using System.Collections;

public class FinalRoom : ConcreteRoom
{

	public FinalRoom(int x, int y, int width, int height) 
		: base(x, y, width, height)
	{
		
	}
	
	public override void setRoomPrefab(RoomManager roomManagerScript)
	{
		this.roomPrefab = roomManagerScript.getRandomFinalRoom();
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
		 return "Final Room";
	}
	
}

