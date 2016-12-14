using UnityEngine;
using System.Collections;

public class Player1 : MonoBehaviour {

	// Need to handle score keeping
	public int score;

	// Has 3 states: swatting, holding, and idle
	public enum states {swatting, holding, idle};
	public states currentState;

	// Player Action Control
	public float enemiesSwatting;
	public bool canSwat;
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
		if(states.holding == currentState){
			canSwat = false;
		}
		else{
			canSwat = true;
			currentState = states.idle;
		}
			
		if(gameManager.GetComponent<GameManager>().sauteingLevel.activeSelf){
			// bounce code
			float maxHeight = (7.0f / 2.0f) + 1.0f;
			bool reachedMaxHeight = false;
			float minHeight = (7.0f / 2.0f) - 1.0f;
			bool reachedMinHeight = false;
			if (currentState == states.holding) {
				if (transform.position.y >= maxHeight && !reachedMaxHeight){
					reachedMaxHeight = true;
				}
				if (transform.position.y <= minHeight && reachedMaxHeight) {
					reachedMinHeight = true;
				}
				if (reachedMaxHeight && reachedMaxHeight) {
					Debug.Log ("Reached both");
				}
			} else {
				reachedMaxHeight = false;
				reachedMinHeight = false;
			}
		}
		// Swatting should have a CD

	}

	void OnTriggerStay(Collider col){
		if (col.gameObject.tag == "knife" || col.gameObject.tag == "Pan") {
			GrabbableObject obj = col.gameObject.GetComponent<GrabbableObject> ();
			// ensure your not swatting or holding something else
			if (currentState != states.swatting){
				if (Input.GetKey(KeyCode.Joystick1Button1)) {
					if (!obj.grabbed && currentState != states.holding) {
						obj.toggleGrabbed (true, transform);
						currentState = states.holding;
					} 
				}
				if(Input.GetKeyUp(KeyCode.Joystick1Button1)){
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
					if (Input.GetKey(KeyCode.Joystick1Button1)) {
						if (!obj.grabbed && currentState != states.holding) {
							obj.toggleGrabbed (true, transform);
							currentState = states.holding;
						} 
					}
					if(Input.GetKeyUp(KeyCode.Joystick1Button1)){
						obj.toggleGrabbed (false, transform);
						currentState = states.idle;
					}	
				}
			}
		}
		if(col.gameObject.tag == "Enemy"){
			// Kill the enemy and add to score based on enemy type
			if (Input.GetKey(KeyCode.Joystick1Button0)){
				if(currentState == states.idle){
					col.gameObject.GetComponent<Enemy>().kill();
					score += 3;
				}
			}

		}


	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Plate"){
			currentState = states.idle;
		}
		if (col.gameObject.tag == "Enemy") {
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
