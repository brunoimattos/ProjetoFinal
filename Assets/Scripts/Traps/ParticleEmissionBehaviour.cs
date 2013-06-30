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
	public Material particle_material;
	
	private List<Transform> particles;
	private Transform inst_particle;
	private Vector3 emission_direction;
	private EmittedParticleBehaviour particleApi;
	private Transform my_particle;
	
	private MeshFilter _particle_mesh_filter;
	private Mesh _helper_mesh;
	
	void Awake()
	{
		// Caching the particle's Mesh Filter :)
		GameObject helper = GameObject.CreatePrimitive(PrimitiveType.Plane);	
		_helper_mesh = helper.GetComponent<MeshFilter>().mesh;
		Destroy(helper);
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
			//inst_particle = GameObject.Instantiate(particle, this.transform.position, Quaternion.Euler(-90, 0, 0)) as Transform;
			//inst_particle = GameObject.Instantiate(my_particle, this.transform.position, Quaternion.Euler(-90, 0, 0)) as Transform;
			inst_particle = create_particle(particle_material, transform.position, Quaternion.Euler(-90, 0, 0));
			
			particleApi = inst_particle.GetComponent<EmittedParticleBehaviour>();		
			
			if(particleApi == null)
			{
				Debug.LogError("No EmittedParticleBehaviour in instantiaded particle.")	;
			}
			else
			{
				Debug.Log("All is ok!");
			}
			
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
		
		Debug.Log("Angle: " + angle);
		while(angle > (opening_angle/2) || angle < -(opening_angle/2))
		{
			random_direction = (Random.insideUnitSphere - this.transform.position).normalized;
			angle = Mathf.Acos(Vector3.Dot(random_direction, this.transform.forward)/(Vector3.Magnitude(random_direction) * Vector3.Magnitude(this.transform.forward))) * Mathf.Rad2Deg;
			Debug.Log("Tentando: Angle: " + angle);
		}
			
		random_direction.z = 0;
		
		random_direction = Quaternion.Euler(0, 0, 90) * random_direction.normalized;
		
		Debug.DrawRay(this.transform.position, random_direction, Color.red, 5);
		
		return random_direction;

	}
	
	private Vector3 get_random_direction(float opening_angle)
	{
		float direction_angle = Random.Range(-(opening_angle/2), opening_angle/2);
		Debug.Log("Random Angle: " + direction_angle);
		Vector3 random_direction = Quaternion.AngleAxis(direction_angle, transform.up) * transform.forward;
		Debug.Log("Random Direction: " + random_direction);
		return random_direction;

	}

	private Transform create_particle(Material material, Vector3 position, Quaternion rotation)
	{
		GameObject particle = GameObject.CreatePrimitive(PrimitiveType.Cube);					
		_particle_mesh_filter = particle.GetComponent<MeshFilter>();
		_particle_mesh_filter.mesh = _helper_mesh;		
		
		particle.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		
		particle.name = "MyParticle";
		particle.gameObject.AddComponent<EmittedParticleBehaviour>();
				
		BoxCollider bx_cl = particle.gameObject.collider as BoxCollider;
		
		bx_cl.size = new Vector3(10.0f, 1.0f, 10.0f);
		
		particle.renderer.material = material;
		particle.transform.position = position;
		particle.transform.localEulerAngles = rotation.eulerAngles;
		particle.tag = "ConfuseGas";
		
		return particle.transform;
	}
}
