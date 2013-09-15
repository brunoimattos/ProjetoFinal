using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour
{
	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Debug.Log("Door has been hit.");
			Vector3 nextRoomPosition = Vector3.zero;
			Vector3 nextPlayerPosition = Vector3.zero;
			Room roomApi = transform.parent.GetComponent<GameRoom>().room;
			float worldX;
			float worldZ;
			switch (this.gameObject.transform.name.ToUpper()) {
				case "NORTHDOOR":

					worldX = roomApi.GetTop().worldX;
					worldZ = roomApi.GetTop().worldZ;
					
					nextRoomPosition = new Vector3(worldX, 0, worldZ);
					nextPlayerPosition = nextRoomPosition + Vector3.forward * (-(transform.parent.localScale.z/2) + (transform.localScale.z/2) + (other.transform.localScale.z/2) + 1);
					
					break;
				
				case "SOUTHDOOR":	
				
					worldX = roomApi.GetBottom().worldX;
					worldZ = roomApi.GetBottom().worldZ;
					
					nextRoomPosition = new Vector3(worldX, 0, worldZ);
					nextPlayerPosition = nextRoomPosition + Vector3.back * (-(transform.parent.localScale.z/2) + (transform.localScale.z/2) + (other.transform.localScale.z/2) + 1);
					
					break;
				
				case "EASTDOOR":
				
					worldX = roomApi.GetRight().worldX;
					worldZ = roomApi.GetRight().worldZ;
					
					nextRoomPosition = new Vector3(worldX, 0, worldZ);
					nextPlayerPosition = nextRoomPosition + Vector3.right * (-(transform.parent.localScale.x/2) + (transform.localScale.x/2) + (other.transform.localScale.x/2) + 2);
					
					break;
				
				case "WESTDOOR":
				
					worldX = roomApi.GetLeft().worldX;
					worldZ = roomApi.GetLeft().worldZ;
					
					nextRoomPosition = new Vector3(worldX, 0, worldZ);
					nextPlayerPosition = nextRoomPosition + Vector3.left * (-(transform.parent.localScale.x/2) + (transform.localScale.x/2) + (other.transform.localScale.x/2) + 2);
					
					break;
			}
			
			Camera.main.GetComponent<CameraBehaviour>().snapToPosition(nextRoomPosition);
			other.GetComponent<PlayerMovement>().snapToPosition(nextPlayerPosition);
		}
	}
}

