using UnityEngine;
using System.Collections;

public class GratingLevel : MonoBehaviour {

	public int grates;
	public int foodLost;
	public int maxFood;
	public int foodStolen;
	public GameObject cheese;
	public GameObject grater;

	public bool levelWon;
	public bool levelOver;

	public GameObject generator;
	// Use this for initialization
	void Start () {
		maxFood = 10;
		foodStolen = 0;
		levelWon = false;
		levelOver = false;
		grates = 0;
	}

	// Update is called once per frame
	void Update () {
		if(foodStolen >= maxFood){
			levelWon = false;
			levelOver = true;
		}
		if(grates >= 1){
			levelWon = true;
			levelOver = true;
		}
		// Track when each player is holding a gratable object
		// Grating Objects creates food
	}

	public void stealFood(){
		foodStolen++;
	}

	public void completeGrate(){
		grates++;
	}

	public bool levelComplete(){
		return levelOver;
	}
}
