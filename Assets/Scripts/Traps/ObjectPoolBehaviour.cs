using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPoolBehaviour : MonoBehaviour
{
	private LinkedList<Transform> _particles;
	
	public int particle_count
	{
		get
		{
			if(_particles != null)
			{
				return _particles.Count;	
			}
			else
			{
				return -1;				
			}
			
		}
	}
	
	void Awake()
	{
		_particles = new LinkedList<Transform>();
	}
	
		
	public void add_particle_to_pool(Transform particle)
	{
		particle.gameObject.SetActive(false);
		_particles.AddLast(particle);
	}
	
	public Transform get_particle_from_pool()
	{
		Transform recovered_particle = _particles.First.Value;
		_particles.RemoveFirst();
		return recovered_particle;
	}
	
}
