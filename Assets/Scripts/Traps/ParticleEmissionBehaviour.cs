using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleEmissionBehaviour : MonoBehaviour {
	
	public bool burst_mode = true;
	public float burst_cooldown = 2.0f;		
	
	public int particle_count = 2;
	public float particle_lifespan = 4.0f;
	public float emission_angle = 30.0f;
	public float particle_initial_speed = 0.5f;
	public Transform particle;
	
	private List<Transform> particles;
	private Transform inst_particle;
	private Vector3 emission_direction;
	private EmittedParticleBehaviour particleApi;
	
	void Start()
	{
		particles = new List<Transform>();
		
		StartCoroutine(timed_bursts(burst_mode, burst_cooldown));	
	}
	
	public IEnumerator timed_bursts(bool is_burst, float cooldown)
	{
		while(is_burst)
		{
			emit_particles(particle_count, emission_angle, particle_lifespan, particle_initial_speed);
			
			yield return new WaitForSeconds(cooldown);	
		}
	}
	
	private void emit_particles(int particle_count, float emission_angle, float particle_lifespan, float particle_speed)
	{
		for(int i=0; i < particle_count; i++)
		{
			inst_particle = GameObject.Instantiate(particle, this.transform.position, Quaternion.Euler(90, 0, 0)) as Transform;
			emission_direction = get_random_direction(emission_angle);
			
			particleApi = inst_particle.GetComponent<EmittedParticleBehaviour>();		
			
			if(particleApi == null)
			{
				Debug.LogError("No EmittedParticleBehaviour in instantiaded particle.")	;
			}
			
			inst_particle.parent = this.transform;
			
			particleApi.movement_velocity = emission_direction * particle_speed;
			particleApi.lifespan = particle_lifespan;
			particleApi.configured = true;	
			
			//particles.Add(inst_particle);
			
		}
		/*
		foreach(Transform particle in particles)
		{
			particleApi = particle.GetComponent<EmittedParticleBehaviour>();
			particleApi.configured = true;
		}
			particles.Clear();
		*/
		
	}
	
	private Vector3 get_random_direction(float opening_angle)
	{
		Vector3 random_direction = (Random.insideUnitSphere - this.transform.position).normalized;
				
		float angle = Mathf.Acos(Vector3.Dot(random_direction, this.transform.forward));
		
		Debug.Log("Angle: " + angle);
		while(angle > (opening_angle/2) || angle < -(opening_angle/2))
		{
			random_direction = (Random.insideUnitSphere - this.transform.position).normalized;
			angle = Mathf.Acos(Vector3.Dot(random_direction, this.transform.forward));
			Debug.Log("Tentando: Angle: " + angle);
		}
			
		random_direction.z = 0;
		
		return random_direction.normalized;

	}
}
