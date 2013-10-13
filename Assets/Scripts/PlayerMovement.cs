using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public float playerSpeed;
	private Vector2 touchDeltaPosition;
	private delegate void DoMovement();
	private DoMovement doMovement;
	
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
		if (Input.touchCount > 0)
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
		}
	}
	
	void OnCollisionEnter(Collision col)
	{	
		if(col.gameObject.CompareTag("Trap"))
		{
			Application.LoadLevel("LevelGeneration");
			Debug.Log("LOL, I'm dead!");	
		}
		
		if(col.gameObject.CompareTag("ConfuseGas"))
		{
			Destroy(col.gameObject);

			if (GetComponent<ConfusionGasEffect>() == null)
			{
				gameObject.AddComponent("ConfusionGasEffect");
			}
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
		Debug.Log("FromPosition: " + transform.position + " ToPosition: " + toPosition);
		this.transform.position = new Vector3(toPosition.x, this.transform.position.y, toPosition.z);
	}
}
