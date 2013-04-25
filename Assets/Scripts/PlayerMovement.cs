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
		
		//this.rigidbody.MovePosition( this.transform.position + (movement * playerSpeed * Time.deltaTime));
	}
	
	
	/*void OnTriggerStay(Collider collider)
	{
		if(collider.gameObject.CompareTag("Trap"))
		{
			Debug.Log("LOL, I'm dead!");	
		}
	}*/
	
	void OnCollisionEnter(Collision col)
	{	
		Debug.Log("Hit!");
		if(col.gameObject.CompareTag("Trap"))
		{
			Application.LoadLevel("workshop");
			Debug.Log("LOL, I'm dead!");	
		}
	}
	
	
}
