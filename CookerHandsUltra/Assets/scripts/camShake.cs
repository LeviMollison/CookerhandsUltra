using UnityEngine;
using System.Collections;

public class camShake : MonoBehaviour {

	private Vector3 originalPos;
	public float shakeAmount;
	public float shakeTimer;

	// Use this for initialization
	void Start () {
	
		originalPos = transform.position;

	}
	
	// Update is called once per frame
	void Update () {

		if (shakeTimer > 0) {

			Vector2 shakePos = Random.insideUnitCircle * shakeAmount;

			transform.position = new Vector3(originalPos.x + shakePos.x, originalPos.y + shakePos.y, originalPos.z);

			shakeTimer -= Time.deltaTime;
		} else {

			transform.position = Vector3.Lerp(transform.position, originalPos, .2f);

		}
	
	}

	public void ShakeCam(float _shakeTime, float _shakeAmt)
	{
		shakeAmount = _shakeAmt;
		shakeTimer = _shakeTime;
	}
}
