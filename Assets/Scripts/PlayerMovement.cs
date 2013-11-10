using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
	public float playerSpeed;
	private Vector2 touchDeltaPosition;
	private delegate void DoMovement();
	private DoMovement doMovement;
	
	private Vector3 lerpFromPosition;
	private Vector3 lerpToPosition;
	private float startLerpTime;
	private float lerpJourneyLength;
	public bool isLerping;
	
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
		
		lerpFromPosition = this.transform.position;
		lerpToPosition = this.transform.position;
	}


	void FixedUpdate()
	{
		if (doMovement != null)
		{
			doMovement();
		}
	}
	
	public void setLerping(bool booleanValue){
		isLerping = booleanValue;
	}
	
	void doPCMovement()
	{
		//Movimento com setas.
		
		/*Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		
		if(movement.magnitude > 1.0f){
			movement.Normalize();
		}
		
		this.transform.Translate(movement * confusion * playerSpeed * Time.deltaTime);
		*/
		
		//this.rigidbody.AddForce( this.transform.position + (movement * playerSpeed * Time.deltaTime));
		
		// Movimento com clique do mouse.
		
		if (Input.GetMouseButton(0)){
			
			Vector3 realWorldPosition;
			
			realWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) * confusion;
			realWorldPosition.y = this.transform.position.y;
			lerpFromPosition = this.transform.position;
			lerpToPosition = realWorldPosition;
			startLerpTime = Time.time;
			lerpJourneyLength = Vector3.Distance(lerpFromPosition, lerpToPosition);
			isLerping = true;
		}
		
		if (isLerping){
			MovePlayer();
		}
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
			
			Vector3 realWorldPosition;
			
			realWorldPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) * confusion;
			realWorldPosition.y = this.transform.position.y;
			lerpFromPosition = this.transform.position;
			lerpToPosition = realWorldPosition;
			startLerpTime = Time.time;
			lerpJourneyLength = Vector3.Distance(lerpFromPosition, lerpToPosition);
			isLerping = true;
		}
		
		if (isLerping){
			MovePlayer();
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
		
		if (isLerping){
			lerpToPosition = new Vector3 (lerpToPosition.x * -1, lerpToPosition.y, lerpToPosition.z * -1);
		}
	}
	
	public void MovePlayer()
    {
		float distCovered = (Time.time - startLerpTime) * playerSpeed;
		float fracJourney = distCovered / lerpJourneyLength;
		this.transform.position = Vector3.Lerp(lerpFromPosition, lerpToPosition, fracJourney);
		if (fracJourney == 1.0f){
			isLerping = false;
		}
    }
	
	public void snapToPosition(Vector3 toPosition)
	{
		/* This function sets this transforms position. It is used when the player changes rooms. */
		this.transform.position = new Vector3(toPosition.x, this.transform.position.y, toPosition.z);
	}
}
