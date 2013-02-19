using System.Collections;
using System;
using UnityEngine;

public static class Utils{
	
	public static int[] getRandomBits(int n_bits)
	{
		int n_rep = (int) Math.Ceiling((double)(n_bits / 8) / sizeof(int));
		
		int[] rndBits = new int[n_bits];
		System.Random rnd;
		
		long rnd64Bits = 0;
		int rnd32Bits = 0;
		
		rnd = new System.Random();
			
		for(int i = 0; i < n_rep; i++)
		{
			rnd64Bits <<= 32;
			rnd32Bits = rnd.Next(Int32.MinValue, Int32.MaxValue);
			rnd64Bits += (long) rnd32Bits;
			Debug.Log("32 Bits: " + rnd32Bits);
			Debug.Log("64 Bits: " + rnd64Bits);
		}
		
		for(int i = 0; i < n_bits; i++)
		{
			rndBits[i] = (int)(rnd64Bits >> i & 1);
		}
			
		return rndBits;
	}
	
	public static int[] getRandomBits(int n_bits, int test, bool useOnlyInts = true)
	{
		int n_rep = (int) Math.Ceiling((double)(n_bits / 8) / sizeof(int));
		
		int[] rndBits = new int[n_bits];
		int rnd32Bits = 0;
		int harvestedBits = 0;
		
		System.Random rnd;
		rnd = new System.Random();
		
		for(int i = 0; i < n_rep; i++)
		{
			rnd32Bits = rnd.Next(Int32.MinValue, Int32.MaxValue);
			
			//each int has 32 bits, soooo
			for(int j = 0; j < sizeof(int) * 8; j++)
			{
				if(harvestedBits >= n_bits)
				{
					//our work here is done
					break;
				}
				
				rndBits[harvestedBits] = (int)(rnd32Bits >> j & 1);
				harvestedBits++;
				
			}
			
		}
		
		return rndBits;
	}
		
}
