using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour
{
	private Transform roomFloor;
	private bool roomHasChanged;
	private Room currentRoom;
	private CameraBehaviour cameraBehaviour;
	private PlayerMovement playerMovement;
	
	void Start()
	{
		roomFloor =  transform.parent.FindChild("RoomFloor");
		cameraBehaviour = Camera.main.GetComponent<CameraBehaviour>();
		playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
		currentRoom = null;
		roomHasChanged = false;
	}
	
	void Update()
	{
		if (roomHasChanged && !cameraBehaviour.isLerping())
		{
			currentRoom.setActiveTrap(false);
			roomHasChanged = false;
		}
	}
	
	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag("Player"))
		{
			roomHasChanged = true;
			playerMovement.isLerping = false;
			Vector3 nextRoomPosition = Vector3.zero;
			Vector3 nextPlayerPosition = Vector3.zero;
			currentRoom = transform.parent.GetComponent<GameRoom>().room;
			Room nextRoom = null;
			float worldX;
			float worldZ;
			switch (this.gameObject.transform.name.ToUpper()) {
				case "NORTHDOOR":

					worldX = currentRoom.GetTop().worldX;
					worldZ = currentRoom.GetTop().worldZ;
					
					nextRoomPosition = new Vector3(worldX, 0, worldZ);

					nextPlayerPosition = nextRoomPosition + Vector3.forward * (-(roomFloor.localScale.z/2) + (transform.localScale.z/2) + (other.transform.localScale.z/2) + 0.1f);
					nextPlayerPosition = new Vector3(other.transform.position.x, nextPlayerPosition.y, nextPlayerPosition.z);

					nextRoom = currentRoom.GetTop();
					
					break;
				
				case "SOUTHDOOR":	
				
					worldX = currentRoom.GetBottom().worldX;
					worldZ = currentRoom.GetBottom().worldZ;
					
					nextRoomPosition = new Vector3(worldX, 0, worldZ);
					nextPlayerPosition = nextRoomPosition + Vector3.back * (-(roomFloor.localScale.z/2) + (transform.localScale.z/2) + (other.transform.localScale.z/2) + 0.1f);
					nextPlayerPosition = new Vector3(other.transform.position.x, nextPlayerPosition.y, nextPlayerPosition.z);
					nextRoom = currentRoom.GetBottom();
				
					break;
				
				case "EASTDOOR":
				
					worldX = currentRoom.GetRight().worldX;
					worldZ = currentRoom.GetRight().worldZ;
					
					nextRoomPosition = new Vector3(worldX, 0, worldZ);
					nextPlayerPosition = nextRoomPosition + Vector3.right * (-(roomFloor.localScale.x/2) + (transform.localScale.z/2) + (other.transform.localScale.x/2) + 0.1f);
					nextPlayerPosition = new Vector3(nextPlayerPosition.x, nextPlayerPosition.y, other.transform.position.z);
					nextRoom = currentRoom.GetRight();
				
					break;
				
				case "WESTDOOR":
				
					worldX = currentRoom.GetLeft().worldX;
					worldZ = currentRoom.GetLeft().worldZ;
					
					nextRoomPosition = new Vector3(worldX, 0, worldZ);
					nextPlayerPosition = nextRoomPosition + Vector3.left * (-(roomFloor.localScale.x/2) + (transform.localScale.z/2) + (other.transform.localScale.x/2) + 0.1f);
					nextPlayerPosition = new Vector3(nextPlayerPosition.x, nextPlayerPosition.y, other.transform.position.z);
				
					nextRoom = currentRoom.GetLeft();
				
					break;
			}
			nextRoom.setActiveTrap(true);
			
			cameraBehaviour.snapToPosition(nextRoomPosition);
			other.GetComponent<PlayerMovement>().snapToPosition(nextPlayerPosition);
		}
	}
}

