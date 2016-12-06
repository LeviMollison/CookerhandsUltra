using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Need to handle score keeping
	public int score;

	// Has 3 states: swatting, holding, and idle
	enum states {swatting, holding, idle};
	states currentState;

	float swatTimer;
	bool canSwat;
	GameObject gameManager;

	// Use this for initialization
	void Start () {
		score = 0;
		currentState = states.idle;
		canSwat = true;
	}

	
	// Update is called once per frame
	void Update () {

		// Swatting a spider = +2; swatting mouse +3; swatting fly +1
		if(states.holding == currentState || swatTimer - Time.time < 1.0f){
			canSwat = false;
		}
		else{
			canSwat = true;
			currentState = states.idle;
		}
		if (Input.GetKey(KeyCode.JoystickButton0)){
			if(canSwat){
				currentState = states.swatting;
				swatTimer = Time.time;
			}
		}
		// Swatting should have a CD

		// ensure the z-axis is where it needs to be
		transform.position = new Vector3(transform.position.x, transform.position.y,gameManager.GetComponent<GameManager>().getLevel());
	}

	void OnTriggerEnter(Collider enemy){
		if(enemy.tag == "enemy"){
			if (currentState == states.swatting) {
				// Kill the enemy and add to score based on enemy type
			} else {
				// Subtract from score based on enemy type
			}
		}
	}

	// need a function for when an enemy touches food
	void foodLost(){
		score -= 3;
	}
}
