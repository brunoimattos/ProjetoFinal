using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
	public float confusion_cooldown;	
	private float confusion = 1.0f;
	
	private float playerSpeed;
	
	private delegate void DoMovement();
	private DoMovement doMovement;

	
	void Start()
	{
		#if UNITY_ANDROID
			doMovement += doAndroidMovement;	
			inicialTouchPosition = Vector2.zero;
			playerSpeed = 1.8f;

		#endif
		
		#if UNITY_EDITOR
			doMovement += doPCMovement;
			playerSpeed = 6.0f;
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
	
	void doPCMovement()
	{
		doPCArrowMovement();
		//doPCMouseClickMovement();
	}
	
	void doAndroidMovement()
	{
		doAndroidAnalogMovement();
		//doAndroidTouchMovement();
	}
	
	public void setConfusion(bool confused)
	{
		if(confused)
		{
			this.confusion = -1.0f;
		}
		else
		{
			this.confusion = 1.0f;
		}
	}
	
	public void setLerping(bool booleanValue){
		isLerping = booleanValue;
	}
	

	private Vector3 lerpFromPosition;
	private Vector3 lerpToPosition;
	private float startLerpTime;
	private float lerpJourneyLength;
	
	private void lerpMovePlayer()
    {
		float distCovered = (Time.time - startLerpTime) * playerSpeed;
		float fracJourney = distCovered / lerpJourneyLength;
		this.transform.position = Vector3.Lerp(lerpFromPosition, lerpToPosition, fracJourney);
		if (fracJourney >= 1.0f){
			isLerping = false;
		}
    }
	
	public void snapToPosition(Vector3 toPosition)
	{
		/* This function sets this transforms position. It is used when the player changes rooms. */
		this.transform.position = new Vector3(toPosition.x, this.transform.position.y, toPosition.z);
	}
	
	
	private bool isLerping;
	
	private void doPCMouseClickMovement()
	{
		if (Input.GetMouseButton(0)){
			
			Vector3 realWorldPosition;
			
			realWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			
			
			if(confusion == -1.0f){
				realWorldPosition += -2*(realWorldPosition - this.transform.position);
			}
			
			realWorldPosition.y = this.transform.position.y;
			
			lerpFromPosition = this.transform.position;
			lerpToPosition = realWorldPosition;
			startLerpTime = Time.time;
			lerpJourneyLength = Vector3.Distance(lerpFromPosition, lerpToPosition);
			isLerping = true;
		}
		
		if (isLerping){
			lerpMovePlayer();
		}
	}

	private void doPCArrowMovement()
	{
		Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		
		if(movement.magnitude > 1.0f){
			movement.Normalize();
		}
		
		this.transform.Translate(movement * confusion * playerSpeed * Time.deltaTime);
		
		//this.rigidbody.AddForce( this.transform.position + (movement * playerSpeed * Time.deltaTime));

	}
	
	
	private Vector2 inicialTouchPosition;
	
	private void doAndroidAnalogMovement()
	{
		if (Input.touchCount > 0)
		{
			inicialTouchPosition += Input.GetTouch(0).deltaPosition;
			
			if (inicialTouchPosition.magnitude > 10.0f)
				inicialTouchPosition = inicialTouchPosition.normalized * 10.0f;
			
			float deadzone = 3.0f;
			
			if(inicialTouchPosition.magnitude > deadzone)
			{
				Vector2 stickInput = inicialTouchPosition.normalized * ((inicialTouchPosition.magnitude - deadzone) / (1 - deadzone));
				this.transform.Translate(-stickInput.x * playerSpeed * Time.deltaTime, 0, -stickInput.y * playerSpeed * Time.deltaTime);
				}
		}
		else
		{
			inicialTouchPosition = Vector2.zero;
		}
	}
	
	private void doAndroidTouchMovement()
	{
		if (Input.touchCount > 0){
			
			Vector3 realWorldPosition;
			realWorldPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			
			if(confusion == -1.0f){
				realWorldPosition += -2*(realWorldPosition - this.transform.position);
			}
			
			realWorldPosition.y = this.transform.position.y;
			lerpFromPosition = this.transform.position;
			lerpToPosition = realWorldPosition;
			startLerpTime = Time.time;
			lerpJourneyLength = Vector3.Distance(lerpFromPosition, lerpToPosition);
			isLerping = true;
		}
		
		if (isLerping){
			lerpMovePlayer();
		}
	}
}
