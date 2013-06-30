using UnityEngine;
using System.Collections;

public class EmittedParticleBehaviour : MonoBehaviour
{

	private float _lifespan;
	private float _max_lifespan;
	public float lifespan
	{
		set 
		{ 
			this._lifespan = value;
			this._max_lifespan = value;
		}
		get
		{
			return this._lifespan;
		}
	}
	
	private Vector3 _movement_velocity;
	public Vector3 movement_velocity 
	{
		set 
		{
			this._movement_velocity = value;
		}
		get
		{
			return this._movement_velocity;
		}
	}
	
	
	private bool _configured = false;
	public bool configured
	{
		set {this._configured = value; }
	}
	
	void Awake()
	{
		renderer.material = set_material_alpha(this.renderer.material, 2);
	}
		
	void Update() 
	{
		if(_configured)
		{
			// Multiplicacao para que o objeto seja destruido antes da textura ficar totalmente invisivel.
			if(_lifespan <= 0.2 * _max_lifespan)
			{
				Destroy(this.gameObject);
				//Debug.Log("Alpha ao morrer: " + renderer.material.color.a);
			}
			
			renderer.material = set_material_alpha(this.renderer.material, _lifespan/_max_lifespan);
			
			_lifespan -= Time.deltaTime;
				
			transform.Translate(_movement_velocity * Time.deltaTime, Space.World);	
						
		}	
	}
	
	private Material set_material_alpha(Material material, float alpha_value)
	{
		Color color = material.color;
		color.a = alpha_value;
		material.color = color;
		return material;
	}
	
}
