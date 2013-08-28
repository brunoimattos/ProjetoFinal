using UnityEngine;
using System.Collections;

public class RotateTransformBehaviour : MonoBehaviour
{
	public float rotationSpeed;
	//Always world coordinates!
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
		//World coordinates!
		this.transform.RotateAround(rotationAxis, rotationSpeed * Time.deltaTime);		
	}
}
