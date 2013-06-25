using UnityEngine;
using System.Collections;

public class RotateTransformBehaviour : MonoBehaviour
{
	public float rotationSpeed;
	public Vector3 rotationAxis;
	
	
	void Start()
	{
		if(rotationSpeed == 0.0f)
		{
			Debug.LogError("Please set rotationSpeed!");
		}
	}
	
	
	void FixedUpdate()
	{
		this.transform.RotateAround(rotationAxis, rotationSpeed * Time.deltaTime);		
	}
}
