using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircuitBuzzSaw : MonoBehaviour {
	
	public List<Transform> circuit;
	public float movement_speed;
	
	private Transform current_vertex, next_vertex;
	private Vector3 movement_direction;
	private int current_idx = 0;
	
	void Start()
	{
		// 2 points should yield a PingPong behaviour, but who knows what can happen?
		if(circuit.Count < 2)
		{
			Debug.LogError("Please fill in at least 2 points!");
		}
		
		// Starting from the first declared point!
		this.transform.position = circuit[0].position;
		
		current_vertex = circuit[current_idx % circuit.Count];
		current_idx++;
		next_vertex = circuit[current_idx % circuit.Count];
		movement_direction = (next_vertex.position - current_vertex.position).normalized;
	}
	
	
	void LateUpdate()
	{
		this.transform.position += movement_speed * movement_direction * Time.deltaTime;
		
		if(Vector3.Distance(next_vertex.position, this.transform.position) <= 0.5f)
		{
			this.transform.position = next_vertex.position;
			current_vertex = circuit[current_idx % circuit.Count];
			current_idx++;
			next_vertex = circuit[current_idx % circuit.Count];
			
			movement_direction = (next_vertex.position - current_vertex.position).normalized;
		}
	}
	
	void OnTriggerEnter(Collider col)
	{
		/*if(col.CompareTag("CircuitPart"))
		{
			Debug.Log("Changing direction!");
			current_vertex = circuit[current_idx % circuit.Count];
			current_idx++;
			next_vertex = circuit[current_idx % circuit.Count];
			
			movement_direction = (next_vertex.position - current_vertex.position).normalized;
			Debug.Log("Going from: " + current_vertex.position + " to: " + next_vertex.position);
		}*/
	}
}
