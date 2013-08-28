using UnityEngine;
using System.Collections;

public class RoomWall
{
	private Vector3 _Position;
	private	Vector3 _Measures;
	
	public float x
	{
		get { return _Position.x; }
		set { _Position.x = value; }
	}
	
	public float y
	{
		get { return _Position.y; }
		set { _Position.y = value; }
	}
	
	public float z
	{
		get { return _Position.z; }
		set { _Position.z = value; }
	}	
	
	public float Width
	{
		get { return _Measures.x; }
		set { _Measures.x = value; }
	}	
	
	
	public float Height
	{
		get { return _Measures.y; }
		set { _Measures.y = value; }
	}
	
	public float Depth
	{
		get { return _Measures.z; }
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

