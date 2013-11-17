using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {
	
	private bool _lerping = false;
	private Vector3 _fromPosition = Vector3.zero;
	private Vector3 _toPosition = Vector3.zero;
	private float _lerpStartTime = 0.0f;
	private float _journeyLength = 0.0f;
	
	public float lerpSpeed = -1.0f;
	
	
	void Start()
	{
		Screen.SetResolution(1280, 800, true);
		if (lerpSpeed < 0.0f)
			Debug.Log("LerpSpeed not set.");
	}
	
	void Update()
	{
		if (_lerping)
		{
			float distCovered = (Time.time - _lerpStartTime) * lerpSpeed;
        	float fracJourney = distCovered / _journeyLength;
			transform.position = Vector3.Lerp(_fromPosition, _toPosition, fracJourney);
		}
		if (transform.position == _toPosition)
			_lerping = false;
	}
	
	public float cameraHeight = 9.57f;
		
	public void setInitialPosition(Vector3 toPosition)
	{
		this.transform.position = new Vector3(toPosition.x, cameraHeight, toPosition.z );
	}
	
	public bool isLerping()
	{
		return _lerping;
	}
	
	public void snapToPosition(Vector3 toPosition)
	{
		_fromPosition = transform.position;
		_toPosition = new Vector3(toPosition.x, transform.position.y, toPosition.z);
		_journeyLength = Vector3.Distance(_fromPosition, _toPosition);
		_lerpStartTime = Time.time;
		_lerping = true;
	}
}
