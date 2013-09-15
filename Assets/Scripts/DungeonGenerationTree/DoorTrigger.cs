using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour
{
	void onTriggerEnter (Collider other)
	{
		if (other.CompareTag("Player"))
			Camera.main.transform.position = other.transform.position;
	}
}

