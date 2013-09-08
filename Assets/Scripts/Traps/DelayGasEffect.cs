using UnityEngine;
using System.Collections;

public class DelayGasEffect : MonoBehaviour {

	private bool active;
	
	void Awake () {
		active = false;
	}
	
	void Update () {
		if(active)
		{
			Debug.Log("OMG! DELAYED! DELAYED!");	
		}
		
	}
	
	public void activateEffect()
	{
		this.active = true;
	}
}
