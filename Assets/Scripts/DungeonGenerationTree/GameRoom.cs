using UnityEngine;
using System.Collections;

public class GameRoom : MonoBehaviour {
	
	public GameObject doorWest, doorEast, doorNorth, doorSouth;
	public Room room;
	
	/* A configuraçao das portas eh no padrao NLSO */
	private string doorConfig = "NLSO";
	
	void Start() 
	{
		// Remove walls if connected
		if (room.IsConnectedTo(room.GetLeft()))
		{
			doorWest.collider.isTrigger = true;
			doorWest.renderer.enabled = false;
			scaleCollider(doorWest, false);			
		}
		else
		{
			doorWest.SetActive(true);	
		}
		
		if (room.IsConnectedTo(room.GetRight()))
		{
			doorEast.collider.isTrigger = true;
			doorEast.renderer.enabled = false;
			scaleCollider(doorEast, true);
		}
		else
		{
			doorEast.SetActive(true);
		}
		
		if (room.IsConnectedTo(room.GetTop()))
		{
			doorNorth.collider.isTrigger = true;
			doorNorth.renderer.enabled = false;
			scaleCollider(doorNorth, false);
		}
		else
		{
			doorNorth.SetActive(true);
		}
		
		if (room.IsConnectedTo(room.GetBottom()))
		{
			doorSouth.collider.isTrigger = true;
			doorSouth.renderer.enabled = false;
			scaleCollider(doorSouth, true);
		}
		else
		{
			doorSouth.SetActive(true);
		}
	}
	
	void Update () 
	{
		
	}
	
	private void scaleCollider(GameObject roomDoor, bool flipCenter)
	{
		BoxCollider boxCollider = (BoxCollider)roomDoor.GetComponent(typeof(BoxCollider));
		boxCollider.size = new Vector3(0.8f, 1f, 0.5f);
		if (!flipCenter)
			boxCollider.center = new Vector3(0.0f, 0.0f, 0.2f);
		else
			boxCollider.center = new Vector3(0.0f, 0.0f, -0.2f);
	}
	
	public void createDoorConfig()
	{
		if (room.IsConnectedTo(room.GetLeft()))
		{
			doorConfig = doorConfig.Replace("O", "1");
		}
		else
		{
			doorConfig = doorConfig.Replace("O", "0");
		}
		
		if (room.IsConnectedTo(room.GetRight()))
		{
			doorConfig = doorConfig.Replace("L", "1");
		}
		else
		{
			doorConfig = doorConfig.Replace("L", "0");
		}
		
		if (room.IsConnectedTo(room.GetTop()))
		{
			doorConfig = doorConfig.Replace("N", "1");
		}
		else
		{
			doorConfig = doorConfig.Replace("N", "0");
		}
		
		if (room.IsConnectedTo(room.GetBottom()))
		{
			doorConfig = doorConfig.Replace("S", "1");
		}
		else
		{
			doorConfig = doorConfig.Replace("S", "0");
		}
	}
	
	public string getDoorConfig()
	{
		return this.doorConfig;
	}
}
