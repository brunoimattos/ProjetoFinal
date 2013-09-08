using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleEmissionBehaviour : MonoBehaviour {
	
	public bool burst_mode = true;
	public float burst_cooldown = 2.0f;		
	
	
	public enum ParticleEffect{ CONFUSE, DELAY };
	
	public ParticleEffect particleEffect;
	public int particle_count = 2;
	public float particle_lifespan = 4.0f;
	public float emission_angle = 30.0f;
	public float particle_initial_speed = 0.5f;
	public Material particle_material;
	
	public Transform particle_prefab;
	
	private List<Transform> particles;
	private Transform inst_particle;
	private Vector3 emission_direction;
	private EmittedParticleBehaviour particleApi;
	private Transform my_particle;
	
	//private MeshFilter _particle_mesh_filter;
	//private Mesh _helper_mesh;
	
	private ObjectPoolBehaviour obj_pool_api;
	
	void Awake()
	{
		// Caching the particle's Mesh Filter :)
		//GameObject helper = GameObject.CreatePrimitive(PrimitiveType.Plane);	
		//_helper_mesh = helper.GetComponent<MeshFilter>().mesh;
		//Destroy(helper);
		
		obj_pool_api = GameObject.FindGameObjectWithTag("ObjectPoolManager").GetComponent<ObjectPoolBehaviour>();
		if(obj_pool_api == null)
		{
			Debug.LogError("No access to ObjectPoolApi found!");	
		}
	}
	
	void Start()
	{
		particles = new List<Transform>();
		//my_particle = create_particle(particle_material);
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
			if(obj_pool_api.particle_count > 0)
			{
				inst_particle = obj_pool_api.get_particle_from_pool();
				inst_particle.position = transform.position;
				inst_particle.gameObject.SetActive(true);
			}
			else
			{
				inst_particle = create_particle(particle_material, transform.position, Quaternion.identity);	
			}
			
			
			particleApi = inst_particle.GetComponent<EmittedParticleBehaviour>();		
						
			inst_particle.parent = this.transform;
			
			particleApi.movement_velocity = get_random_direction(emission_angle) * particle_speed;
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
	
	private Vector3 get_random_direction_old(float opening_angle)
	{
		Vector3 random_direction = (Random.insideUnitSphere + this.transform.position).normalized;
		
		
		
		float angle = Mathf.Acos(Vector3.Dot(random_direction, this.transform.forward)/(Vector3.Magnitude(random_direction) * Vector3.Magnitude(this.transform.forward))) * Mathf.Rad2Deg;
		
		
		while(angle > (opening_angle/2) || angle < -(opening_angle/2))
		{
			random_direction = (Random.insideUnitSphere - this.transform.position).normalized;
			angle = Mathf.Acos(Vector3.Dot(random_direction, this.transform.forward)/(Vector3.Magnitude(random_direction) * Vector3.Magnitude(this.transform.forward))) * Mathf.Rad2Deg;
		}
			
		random_direction.z = 0;
		
		random_direction = Quaternion.Euler(0, 0, 90) * random_direction.normalized;
		
		Debug.DrawRay(this.transform.position, random_direction, Color.red, 5);
		
		return random_direction;

	}
	
	private Vector3 get_random_direction(float opening_angle)
	{
		float direction_angle = Random.Range(-(opening_angle/2), opening_angle/2);	
		Vector3 random_direction = Quaternion.AngleAxis(direction_angle, transform.up) * transform.forward;
	
		return random_direction;

	}
	
	private Transform create_particle(Material material, Vector3 position, Quaternion rotation)
	{
		Transform instantiated = GameObject.Instantiate(particle_prefab, position, rotation) as Transform;
		return instantiated;
	}

}
