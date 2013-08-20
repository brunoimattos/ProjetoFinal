using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public float playerSpeed;
	private Vector2 touchDeltaPosition;
	private delegate void DoMovement();
	private DoMovement doMovement;
	
	void Start()
	{
		#if UNITY_ANDROID
			doMovement += doAndroidMovement;			
		#endif
		
		#if UNITY_EDITOR
			doMovement += doPCMovement;
		#endif
	}

	void Update()
	{
		if (doMovement != null)
		{
			doMovement();
		}
	}
	
	void doPCMovement()
	{
		Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		this.transform.Translate(movement * playerSpeed * Time.deltaTime);
		
		//this.rigidbody.MovePosition( this.transform.position + (movement * playerSpeed * Time.deltaTime));
	}
	
	void doAndroidMovement()
	{
		if (Input.touchCount > 0)
		{
			if (Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				touchDeltaPosition -= Input.GetTouch(0).deltaPosition;
				touchDeltaPosition.Normalize();
			}
			
			this.transform.Translate(-touchDeltaPosition.x * playerSpeed * Time.deltaTime, -touchDeltaPosition.y * playerSpeed * Time.deltaTime, 0);
		}
		
		if (Input.touchCount == 0)
		{
			touchDeltaPosition.x = 0.0f;			
			touchDeltaPosition.y = 0.0f;			
		}
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
