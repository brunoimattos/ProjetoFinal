using UnityEngine;
using System.Collections;

public class RotateRigidBodyBehaviour : MonoBehaviour
{
	public float rotationSpeed;
	public Vector3 rotationAxis;
	
	
	void Start()
	{
		if(rotationSpeed == 0.0f)
		{
			Debug.LogError("Please set rotationSpeed!");
		}
	
		this.rigidbody.AddTorque(rotationAxis*rotationSpeed);
	}
}
