using UnityEngine;
using System.Collections;

public class ConfusionGasEffect : MonoBehaviour 
{
	public float confuseTime = 3.0f;

	
	private Texture2D confuseEffect;
	private PlayerMovement playerApi;
	
	private float alphaDecrease;
	private float alpha;
	
	void Start()
	{
		if(confuseTime < 0.0f)
		{
			Debug.LogError("Please set confuseTime!");
		}
		
		playerApi = GetComponent<PlayerMovement>();
		
		if(playerApi == null)
		{
			Debug.LogError("Eitcha lele! Cade o PlayerMovement no player?");
		}
		
		ResourceManager resourceApi = GameObject.FindGameObjectWithTag("ResourceManager").GetComponent<ResourceManager>();
		
		confuseEffect = resourceApi.getTextureByName("confuse_gas_effect");
				
		alpha = GUI.color.a;
		
		// Agora nao me pergunte pq o confuseTime ao quadrado =P
		alphaDecrease = ( alpha / (confuseTime * confuseTime ) ) * Time.deltaTime;
		
		StartCoroutine(doConfusion(confuseTime, playerApi));
	}
	
	void createGuiTexture(GameObject guiTex, Texture2D texture)
	{
		guiTex = new GameObject("ConfuseGasEffect");
		guiTex.AddComponent("GUITexture");
		guiTex.transform.localScale = new Vector3(1.0f, 1.0f, 0);
		guiTex.transform.position = new Vector3(0.5f, 0.5f, 0);
		guiTex.guiTexture.pixelInset = new Rect(0, 0, 0, 0);
		guiTex.guiTexture.texture = texture;
	}
	
	IEnumerator doConfusion(float time, PlayerMovement playerApi)
	{
		playerApi.setConfusion(true);
		
		yield return new WaitForSeconds(time);
		
		playerApi.setConfusion(false);
		Destroy(this);
		Destroy(guiText);
		
	}
	
	void OnGUI()
	{
		
		// Mudando o alpha da imagem antes de desenhar ela.
		Color c = GUI.color;
		alpha -= alphaDecrease;
		c.a = alpha;
		GUI.color = c;

		GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), confuseEffect);
	}
}
