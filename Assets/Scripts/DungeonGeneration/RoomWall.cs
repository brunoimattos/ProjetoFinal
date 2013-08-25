using UnityEngine;
using System.Collections;

public class RoomWall : MonoBehaviour
{
	private Vector2 _Position;
	private	Vector2 _Measures;
	
	public int x
	{
		get { return (int) _Position.x; }
		set { _Position.x = value; }
	}
	
	public int y
	{
		get { return (int) _Position.y; }
		set { _Position.y = value; }
	}
	
	
	public int Width
	{
		get { return (int) _Measures.x; }
		set { _Measures.x = value; }
	}	
	
	
	public int Height
	{
		get { return (int) _Measures.y; }
		set { _Measures.y = value; }
	}
		
}

