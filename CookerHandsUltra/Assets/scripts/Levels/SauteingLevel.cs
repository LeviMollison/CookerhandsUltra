using UnityEngine;
using System.Collections;

public class SauteingLevel : MonoBehaviour {

	public int bounces;
	public int foodLost;
	public int maxFood;
	public int foodStolen;

	public bool levelWon;
	public bool levelOver;

	public GameObject generator;
	// Use this for initialization
	void Start () {
		maxFood = 10;
		foodStolen = 0;
		levelWon = false;
		levelOver = false;
		bounces = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if((float)foodStolen / (float)maxFood > 7.0/10.0){
			levelWon = false;
			levelOver = true;
		}
		if(bounces >= 10){
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
