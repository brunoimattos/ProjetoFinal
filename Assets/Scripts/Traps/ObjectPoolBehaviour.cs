using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPoolBehaviour : MonoBehaviour{
	private LinkedList<Transform> _particles;
	private LinkedList<Transform> _blasts;
	
	public int particle_count{
		get{
			if(_particles != null){
				return _particles.Count;	
			}
			else{
				return -1;				
			}
			
		}
	}
	
	public int blast_count{
		get{
			if(_blasts != null){
				return _blasts.Count;	
			}
			else{
				return -1;				
			}
			
		}
	}
	
	void Awake()
	{
		_particles = new LinkedList<Transform>();
		_blasts = new LinkedList<Transform>();
	}
	
		
	public void add_particle_to_pool(Transform particle)
	{
		particle.gameObject.SetActive(false);
		_particles.AddLast(particle);
		Debug.Log("Number of particles: " + _particles.Count);
	}
	
	public Transform get_particle_from_pool()
	{
		Transform recovered_particle = _particles.First.Value;
		_particles.RemoveFirst();
		return recovered_particle;
	}
	
	public void add_blast_to_pool(Transform blast)
	{
		blast.gameObject.SetActive(false);
		_blasts.AddLast(blast);
	}
	
	public Transform get_blast_from_pool()
	{
		Transform recovered_blast = _blasts.First.Value;
		_blasts.RemoveFirst();
		return recovered_blast;
	}
	
}
