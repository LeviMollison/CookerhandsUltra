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
	public GameObject gameManager;

	// Use this for initialization
	void Start () {
		score = 0;
		currentState = states.idle;
		canSwat = true;
	}

	
	// Update is called once per frame
	void Update () {

		// out of bounds
		if (transform.position.x > gameManager.GetComponent<GameManager>().getLevelBounds().x+5.0f){
			transform.position = new Vector3 (gameManager.GetComponent<GameManager>().getLevelBounds().x+5.0f, 
				transform.position.y, 0);
			
		}
		// Swatting a spider = +2; swatting mouse +3; swatting fly +1
		if(states.holding == currentState){
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

	}

	void OnTriggerEnter(Collider enemy){
		if(enemy.tag == "Enemy"){
			Debug.Log (currentState);
			if (currentState == states.swatting) {
				// Kill the enemy and add to score based on enemy type
				enemy.gameObject.GetComponent<Enemy>().kill();
				score += 3;
			} else {
				// Subtract from score based on enemy type
				foodLost();
				Debug.Log (score);
			}
		}
	}

	// need a function for when an enemy touches food
	public void foodLost(){
		score -= 3;
	}

	public void changeLevel(){
		transform.position = new Vector3(gameManager.GetComponent<GameManager>().getLevel(), transform.position.y,0.0f);
	}
}
