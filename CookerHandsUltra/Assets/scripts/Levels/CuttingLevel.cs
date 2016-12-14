using UnityEngine;
using System.Collections;

public class CuttingLevel : MonoBehaviour {

	// Need a max amount of food to be stolen
	public int maxFood;
	public int foodStolen;
	public int foodCollected;

	public bool levelWon;
	public bool levelOver;

	public GameObject generator;
	// Use this for initialization
	void Start () {
		maxFood = 10;
		foodStolen = 0;
		levelWon = false;
		levelOver = false;
		foodCollected = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if((float)foodStolen / (float)maxFood > 7.0/10.0){
			levelWon = false;
			levelOver = true;
		}
//		if((float)foodCollected / (float)maxFood > 7.0/10.0){
		if(true){
			levelWon = true;
			levelOver = true;
		}

		// If the level is over, signal it

	}

	public void stealFood(){
		foodStolen++;
	}

	public void collectFood(){
		foodCollected++;
	}

	public bool levelComplete(){
		return levelOver;
	}
}
