using UnityEngine;
using System.Collections;

public class Player2 : MonoBehaviour {

	// Need to handle score keeping
	public int score;

	// Has 3 states: swatting, holding, and idle
	public enum states {swatting, holding, idle};
	public states currentState;

	// animation states
	public GameObject swatting;
	public GameObject holding;
	public GameObject idle;

	// Player Action Control
	public float enemiesSwatting;
	public bool canSwat;
	public GameObject gameManager;
	bool reachedMaxHeight = false;
	bool reachedMinHeight = false;

	// Use this for initialization
	void Start () {
		enemiesSwatting = 0;
		score = 0;
		currentState = states.idle;
		canSwat = true;
	}

	// Update is called once per frame
	void Update () {

		// animations
		if(currentState == states.swatting){
			if (idle.activeSelf) {
				idle.SetActive (false);
			}
			if (holding.activeSelf) {
				holding.SetActive (false);
			}
			if (!swatting.activeSelf) {
				swatting.SetActive (true);
			}
		}
		else if (currentState == states.holding){
			if (idle.activeSelf) {
				idle.SetActive (false);
			}
			if (swatting.activeSelf) {
				swatting.SetActive (false);
			}
			if (!holding.activeSelf) {
				holding.SetActive (true);
			}
		}
		else if (currentState == states.idle){
			if (holding.activeSelf) {
				holding.SetActive (false);
			}
			if (swatting.activeSelf) {
				swatting.SetActive (false);
			}
			if (!idle.activeSelf) {
				idle.SetActive (true);
			}
		}
		idle.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		holding.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		swatting.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);

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
			float maxHeight = 1.5f;
			float minHeight = 0.5f;
			if (currentState == states.holding) {
				if (transform.position.y >= maxHeight && !reachedMaxHeight){
					reachedMaxHeight = true;
				}
				if (transform.position.y <= minHeight && reachedMaxHeight) {
					reachedMinHeight = true;
				}
				if (reachedMaxHeight && reachedMinHeight) {
					gameManager.GetComponent<GameManager> ().sauteingLevel.GetComponent<SauteingLevel> ().completeBounce ();
					reachedMaxHeight = false;
					reachedMinHeight = false;
				}
			} else {
				reachedMaxHeight = false;
				reachedMinHeight = false;
			}
		}
		if (gameManager.GetComponent<GameManager> ().gratingLevel.activeSelf) {
			transform.position = new Vector3 (transform.position.x,transform.position.y,11.0f);
			// Track starting x position
		}
		// Swatting should have a CD

	}

	void OnTriggerStay(Collider col){
		if (col.gameObject.tag == "knife" || col.gameObject.tag == "Pan" || col.gameObject.tag =="Cheese" ||
			col.gameObject.tag =="Grater") {
			GrabbableObject obj = col.gameObject.GetComponent<GrabbableObject> ();
			// ensure your not swatting or holding something else
			if (currentState != states.swatting){
				if (Input.GetKey(KeyCode.Joystick2Button1)) {
					if (!obj.grabbed && currentState != states.holding) {
						obj.toggleGrabbed (true, transform);
						currentState = states.holding;
					} 
				}
				if(Input.GetKeyUp(KeyCode.Joystick2Button1)){
					obj.toggleGrabbed (false, transform);
					currentState = states.idle;
				}	
			}
		}
		else
		if (col.gameObject.tag == "Food") {
			if (col.gameObject.GetComponent<FoodClass> ().cut) {
				GrabbableObject obj = col.gameObject.GetComponent<GrabbableObject> ();
				// ensure your not swatting or holding something else
				if (currentState != states.swatting){
						if (Input.GetKey(KeyCode.Joystick2Button1) && col.gameObject.GetComponent<FoodClass>().transform.parent == null) {
						if (!obj.grabbed && currentState != states.holding) {
							obj.toggleGrabbed (true, transform);
							currentState = states.holding;
						} 
					}
					if(Input.GetKeyUp(KeyCode.Joystick2Button1)){
						obj.toggleGrabbed (false, transform);
						currentState = states.idle;
					}	
				}
			}
		}
		else
		if(col.gameObject.tag == "Enemy"){
			// Kill the enemy and add to score based on enemy type
			if (Input.GetKey(KeyCode.Joystick2Button0)){
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
