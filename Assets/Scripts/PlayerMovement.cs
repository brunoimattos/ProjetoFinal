using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
	public float playerSpeed;
	private Vector2 touchDeltaPosition;
	private delegate void DoMovement();
	private DoMovement doMovement;
	
	private Vector3 realWorldPosition;
	
	public float confusion_cooldown;
	
	private int confusion = 1;
	
	void Start()
	{
		#if UNITY_ANDROID
			doMovement += doAndroidMovement;		
		#endif
		
		#if UNITY_EDITOR
			doMovement += doPCMovement;
		#endif
	}


	void FixedUpdate()
	{
		if (doMovement != null)
		{
			doMovement();
		}
	}
	
	void doPCMovement()
	{
		Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		
		if(movement.magnitude > 1.0f){
			movement.Normalize();
		}
		
		this.transform.Translate(movement * confusion * playerSpeed * Time.deltaTime);
		
		//this.rigidbody.AddForce( this.transform.position + (movement * playerSpeed * Time.deltaTime));
	}
	
	void doAndroidMovement()
	{
		/*if (Input.touchCount > 0)
		{
			if (Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				touchDeltaPosition -= Input.GetTouch(0).deltaPosition;
				touchDeltaPosition.Normalize();
			}
			
			this.transform.Translate(-touchDeltaPosition.x * playerSpeed * Time.deltaTime, 0, -touchDeltaPosition.y * playerSpeed * Time.deltaTime);
		}
		
		if (Input.touchCount == 0)
		{
			touchDeltaPosition.x = 0.0f;			
			touchDeltaPosition.y = 0.0f;			
		}*/
		
		if (Input.touchCount > 0){
			realWorldPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			//moveTo(realWorldPosition);
		}
		
	}
	
	public void setConfusion(bool confused)
	{
		if(confused)
		{
			this.confusion = -1;
		}
		else
		{
			this.confusion = 1;
		}
	}
	
	public void snapToPosition(Vector3 toPosition)
	{
		/* This function sets this transforms position. It is used when the player changes rooms. */
		this.transform.position = new Vector3(toPosition.x, this.transform.position.y, toPosition.z);
	}
}
