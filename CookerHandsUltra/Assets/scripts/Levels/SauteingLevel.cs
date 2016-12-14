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
		if(foodStolen >= maxFood){
			levelWon = false;
			levelOver = true;
		}
		if(bounces >= 1){
			levelWon = true;
			levelOver = true;
		}
	}

	public void stealFood(){
		foodStolen++;
	}

	public void completeBounce(){
		bounces++;
	}

	public bool levelComplete(){
		return levelOver;
	}
}
