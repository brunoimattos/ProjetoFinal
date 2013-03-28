using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public float playerSpeed;
	
	void Start()
	{

	}

	void Update()
	{
		doMovement();
	}
	
	void doMovement()
	{
		Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0); 
		
		this.transform.Translate(movement * playerSpeed * Time.deltaTime);
	}
}
