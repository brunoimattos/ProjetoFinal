using UnityEngine;
using System.Collections;

public class testUtils : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int[] randomLine = new int[40];
		
		int[][] levelMap2 = new int[20][];
		
		//also worked without this
		for(int i = 0; i < 40; i++)
		{
			levelMap2[i] = new int[40];
		}
		
		for(int j = 0; j < 20; j++)
		{
			randomLine = Utils.getRandomBits(40, 10, true);
			levelMap2[j] = randomLine;
		}
		
		
		//printing the array		
		for(int j = 0; j < 20; j++)
		{
			for(int i = 0; i < 40; i++)
			{
				Debug.Log(levelMap2[j][i]);
			}	
		}
		
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
