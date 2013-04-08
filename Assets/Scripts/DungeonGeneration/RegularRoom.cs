using UnityEngine;
using System.Collections;

public class RegularRoom : ConcreteRoom
{
	public RegularRoom(int x, int y, int width, int height) 
		: base(x, y, width, height)
	{
		
	}
	
	public virtual void setRoomPrefab(RoomManager roomManagerScript)
	{
		this.roomPrefab = roomManagerScript.getRandomRegularRoom();
	}
	
	public override string ToString()
	{
		 return "Regular Room";
	}
}

