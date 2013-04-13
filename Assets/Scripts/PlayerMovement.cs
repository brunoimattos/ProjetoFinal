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
		
//		this.rigidbody.MovePosition( this.transform.position + (movement * playerSpeed * Time.deltaTime));
	}
	
/*	void OnCollisionEnter(Collision col)
	{
		this.rigidbody.velocity = Vector3.zero;
		Debug.Log("Booo ho! Marty died!");
	}
	
	void OnCollisionExit(Collision col)
	{
		this.rigidbody.velocity = Vector3.zero;
		//Debug.Log("Booo ho! Marty died!");
	}*/
}
