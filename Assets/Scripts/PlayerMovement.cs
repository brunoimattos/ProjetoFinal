using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public float playerSpeed;
	
	public float confusion_cooldown;
	
	private int confusion = 1;
	
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
		
		this.transform.Translate(movement * confusion * playerSpeed * Time.deltaTime);
		
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
		Debug.Log("Hit by: " + col.gameObject.tag);
		
		//Na colisao com o gas confusao: setar confusion pra -1 por confusion_cooldown segundos
		
		//Na colisao com o gas delay: testar guardar numa lista as teclas apertadas e depois de cooldown segundos tirar uma executar
		if(col.gameObject.CompareTag("Trap"))
		{
			Application.LoadLevel("workshop");
			Debug.Log("LOL, I'm dead!");	
		}
		
		if(col.gameObject.CompareTag("ConfuseGas"))
		{
			this.confusion = -1;
		}
	}

	
	
	
}
