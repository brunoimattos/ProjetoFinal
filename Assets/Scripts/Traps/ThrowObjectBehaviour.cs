using UnityEngine;
using System.Collections;

public class ThrowObjectBehaviour : MonoBehaviour {

	public Transform throwable_object;
	public Transform object_receiver;
	public float throw_speed = -1;
	public float throw_interval = -1;
	
	private Vector3 throw_direction;
	private Transform throwable_instantiated;
	private bool isRunning;
	
	void Start () {
		if(throwable_object == null)
		{
			Debug.LogError("Please tell me what to throw! :(");
		}	
		
		if(object_receiver == null)
		{
			Debug.LogError("Please tell me where to throw it! :(");
		}
		
		if(throw_speed < 0)
		{
			Debug.LogError("Please tell me how fast I should throw it! :(");
		}
		
		if(throw_interval < 0)
		{
			Debug.LogError("Please tell how often I should throw it! :(");
		}
		
		throw_direction = (object_receiver.transform.position - this.transform.position).normalized;
		
		//StartCoroutine(throw_object(throw_interval, throw_speed, throw_direction));	
		StartCoroutine ("throw_object");
		
		isRunning = true;
		
		//throw_object(throw_interval, throw_speed, throw_direction);
	}
	
	//IEnumerator throw_object(float throw_interval, float throw_speed, Vector3 throw_direction)
	IEnumerator throw_object()
	{
		while(true){
			throwable_instantiated = GameObject.Instantiate(throwable_object ,this.transform.position, Quaternion.identity) as Transform;
			throwable_instantiated.name = "throwable_saw";
			Physics.IgnoreCollision(throwable_instantiated.collider, this.collider);
			throwable_instantiated.parent = this.gameObject.transform;
			throwable_instantiated.GetComponent<ThrowableObjectBehaviour>().throw_object(this.throw_speed * this.throw_direction);
			yield return new WaitForSeconds(this.throw_interval);
		}
		
	}
	
	void OnEnable()
	{	
		if(isRunning == false)
			StartCoroutine("throw_object");	
		else
			Debug.Log("Ja estava rodando!");
	}
	
	void OnDisable()
	{
		//StopCoroutine("throw_object");
		StopAllCoroutines();
		isRunning = false;
	}
	
	
	/*void throw_object(float throw_interval, float throw_speed, Vector3 throw_direction)
	{
		while(true)
		{
			throwable_instantiated = GameObject.Instantiate(throwable_object ,this.transform.position, Quaternion.identity) as Transform;
			throwable_object.GetComponent<ThrowableObjectBehaviour>().throw_object(throw_speed * throw_direction);
			StartCoroutine(wait_cooldown(throw_interval));
		}
	}
	
	IEnumerator wait_cooldown(float cooldown_time)
	{
		yield return new WaitForSeconds(cooldown_time);
	} */
	
}
