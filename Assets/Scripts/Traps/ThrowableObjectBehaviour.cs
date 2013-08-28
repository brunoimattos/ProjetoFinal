using UnityEngine;
using System.Collections;

// Remove the requirecomponent or not?
//[RequireComponent(typeof(Rigidbody))]
public class ThrowableObjectBehaviour : MonoBehaviour {
	
	private Vector3 movement_direction = Vector3.zero;
	
	void LateUpdate()
	{
		if(movement_direction != Vector3.zero)
		{
		this.transform.position += movement_direction * Time.deltaTime;
		}
	}
	
	public void throw_object(Vector3 throw_force)
	{
		this.movement_direction = throw_force;		
	}
	
	
	void OnCollisionEnter(Collision col)
	{
		// Explodes on impact with receiver.
		// Maybe we could create a ParticleEmitter when this happens, who knows.
		if(col.gameObject.CompareTag("Receiver"))
		{
			Destroy(this.gameObject);
		}
		
	}
}
