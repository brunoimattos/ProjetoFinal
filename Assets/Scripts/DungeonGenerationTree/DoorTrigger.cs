using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour
{
	void onTriggerEnter (Collider other)
	{
		if (other.CompareTag("Player"))
			Debug.Log("Door has been hit.");
			Camera.main.transform.position = other.transform.position;
	}
}

