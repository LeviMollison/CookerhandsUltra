using UnityEngine;
using System.Collections;

public class GratingLevel : MonoBehaviour {

	public int grates;
	public int foodCollected;
	public int foodLost;
	public int maxFood;
	public int foodStolen;
	public bool makeFood;
	public bool levelStarted;
	public GameObject cheese;
	public GameObject grater;
	public GameObject circle;

	// New Level
	public int circleSize;

	public bool levelWon;
	public bool levelOver;

	public GameObject generator;
	// Use this for initialization
	void Start () {
		maxFood = 10;
		foodStolen = 0;
		foodCollected = 0;
		levelWon = false;
		levelOver = false;
		circleSize = 3;
		levelStarted = true;
	}

	// Update is called once per frame
	void Update () {
		if(circleSize >= maxFood){
			levelWon = true;
			levelOver = true;
		}
		else if (circleSize <= 0) {
			levelWon = false;
			levelOver = true;
		}
		if (grater.GetComponent<Grater> ().breakFood) {
			makeFood = true;
			circleSize += 1;
			grater.GetComponent<Grater> ().breakFood = false;
		} else {
			makeFood = false;
		}
		// Update the circle's size here
		circle.GetComponent<Transform>().transform.localScale = 
			new Vector3 (((float)circleSize/10.0f) + 1.0f,((float)circleSize/10.0f) + 1.0f,11);
	}

	public void stealFood(){
		circleSize -= 1;
	}

	public bool levelComplete(){
		return levelOver;
	}
}
