using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Need to handle score keeping
	public int score;

	// Has 3 states: swatting, holding, and idle
	enum states {swatting, holding, idle};
	states currentState;

	// Player Action Control
	float enemiesSwatting;
	bool canSwat;
	public GameObject gameManager;

	// Use this for initialization
	void Start () {
		enemiesSwatting = 0;
		score = 0;
		currentState = states.idle;
		canSwat = true;
	}

	
	// Update is called once per frame
	void Update () {

		// out of bounds
		if (transform.position.x > gameManager.GetComponent<GameManager>().getLevel()+7.0f){
			transform.position = new Vector3 (gameManager.GetComponent<GameManager>().getLevel()+7.0f, 
				transform.position.y, 0);
			
		}
		if (transform.position.x < gameManager.GetComponent<GameManager>().getLevel()-7.0f){
			transform.position = new Vector3 (gameManager.GetComponent<GameManager>().getLevel()-7.0f, 
				transform.position.y, 0);

		}
		// Swatting a spider = +2; swatting mouse +3; swatting fly +1
		if(states.holding == currentState || states.swatting == currentState){
			canSwat = false;
		}
		else{
			canSwat = true;
			currentState = states.idle;
		}
		// Swatting should have a CD

	}

	void OnTriggerEnter(Collider enemy){
		if(enemy.tag == "Enemy"){
			// Kill the enemy and add to score based on enemy type
			if (Input.GetKey(KeyCode.JoystickButton0) && enemiesSwatting < 1){
				if(canSwat){
					currentState = states.swatting;
					enemy.gameObject.GetComponent<Enemy>().kill();
					score += 3;
				}
			}

		}
	}

	void OnTriggerStay(Collider col){
		if (col.gameObject.tag == "knife") {
			GrabbableObject obj = col.gameObject.GetComponent<GrabbableObject> ();
			// ensure your not swatting or holding something else
			if (currentState != states.swatting){
				if (Input.GetKey(KeyCode.JoystickButton1)) {
					if (!obj.grabbed && currentState != states.holding) {
						obj.toggleGrabbed (true, transform);
						currentState = states.holding;
					} 
				}
				if(Input.GetKeyUp(KeyCode.JoystickButton1)){
					obj.toggleGrabbed (false, transform);
					currentState = states.idle;
				}	
			}
		}
		if (col.gameObject.tag == "Food") {
			if (col.gameObject.GetComponent<FoodClass> ().cut) {
				GrabbableObject obj = col.gameObject.GetComponent<GrabbableObject> ();
				// ensure your not swatting or holding something else
				if (currentState != states.swatting){
					if (Input.GetKey(KeyCode.JoystickButton1)) {
						if (!obj.grabbed && currentState != states.holding) {
							obj.toggleGrabbed (true, transform);
							currentState = states.holding;
						} 
					}
					if(Input.GetKeyUp(KeyCode.JoystickButton1)){
						obj.toggleGrabbed (false, transform);
						currentState = states.idle;
					}	
				}
			}
		}


	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Food" && col.gameObject.GetComponent<FoodClass>().collected) {
			currentState = states.idle;
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
