using UnityEngine;
using System.Collections;

public class SauteingLevel : MonoBehaviour {

	public int foodLost;
	public int maxFood;
	public int foodStolen;

	// New Level
	public float levelTimer;
	public float currentTime;
	public float pan1Timer;
	public float pan2Timer;
	public GameObject pan1;
	public GameObject pan2;
	// pan states:  1, 2, 3

	public bool levelWon;
	public bool levelOver;

	public GameObject generator;
	// Use this for initialization
	void Start () {
		maxFood = 10;
		foodStolen = 0;
		levelWon = false;
		levelOver = false;

		// new level
		levelTimer = 15f;
		currentTime = 0f;
		pan1Timer = 0f;
		pan2Timer = 0f;

	}
	
	// Update is called once per frame
	void Update () {
		if(foodStolen >= maxFood){
			levelWon = false;
			levelOver = true;
		}
		Debug.Log (pan1Timer);
		if (!pan1.GetComponent<PanTracking> ().held) {
			pan1Timer += Time.deltaTime;
			if (pan1Timer > 4) {
				pan1.GetComponent<PanTracking> ().panState = 3;
			} else if (pan1Timer > 2) {
				pan1.GetComponent<PanTracking> ().panState = 2;
			} else {
				pan1.GetComponent<PanTracking> ().panState = 1;
			}
			if (pan1Timer >= 5) {
				levelWon = false;
				levelOver = true;
			}
		} else {
			pan1Timer = 0f;

		}
		if (!pan2.GetComponent<PanTracking> ().held) {
			pan2Timer += Time.deltaTime;
			if (pan2Timer > 4) {
				pan2.GetComponent<PanTracking> ().panState = 3;
			} else if (pan2Timer > 2) {
				pan2.GetComponent<PanTracking> ().panState = 2;
			} else {
				pan2.GetComponent<PanTracking> ().panState = 1;
			}
			if (pan2Timer >= 5) {
				levelWon = false;
				levelOver = true;
			}
		} else {
			pan2Timer = 0f;

		}

		if (currentTime >= levelTimer) {
			levelWon = true;
			levelOver = true;
		}
	}

	public void stealFood(){
		foodStolen++;
	}

	public bool levelComplete(){
		return levelOver;
	}
}
