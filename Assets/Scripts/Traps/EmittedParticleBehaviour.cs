using UnityEngine;
using System.Collections;

public class EmittedParticleBehaviour : MonoBehaviour
{

	private float _lifespan;
	public float lifespan
	{
		set { this._lifespan = value; }
		get { return this._lifespan; }
	}
	
	private Vector3 _movement_velocity;
	public Vector3 movement_velocity
	{
		set { this._movement_velocity = value; }
		get { return this._movement_velocity; }
	}
	
	
	private bool _configured = false;
	public bool configured
	{
		set {this._configured = value; }
	}
			
	void Update() 
	{
		if(_configured)
		{
			this.transform.Translate(this._movement_velocity * Time.deltaTime, Space.World);
		}	
	}
	
}
