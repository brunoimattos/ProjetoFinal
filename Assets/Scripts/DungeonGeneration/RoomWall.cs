using UnityEngine;
using System.Collections;

public class RoomWall
{
	private Vector3 _Position;
	private	Vector3 _Measures;
	
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
	
	public int z
	{
		get { return (int) _Position.z; }
		set { _Position.z = value; }
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
	
	public int Depth
	{
		get { return (int) _Measures.z; }
		set { _Measures.z = value; }
	}
	
	public Vector3 Position
	{
		get { return _Position; }
	}
	
	public Vector3 Measures
	{
		get { return _Measures; }
	}
		
}

