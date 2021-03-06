﻿using UnityEngine;
using System.Collections;

public class Player1 : MonoBehaviour {

	// Need to handle score keeping
	public int score;

	// Has 3 states: swatting, holding, and idle
	public enum states {swatting, holding, idle};
	public states currentState;

	// animation states
	public GameObject swatting;
	public GameObject holding;
	public GameObject idle;

	// Swat Balancing
	public bool recentlySwatted;
	public float swatTimer;
	public float inSwatState;

	// Player Action Control
	public float enemiesSwatting;
	public bool canSwat;
	public GameObject gameManager;
	public bool holdingFood;
	public bool holdingKnife;
	public Camera cuttingLevelCam;

	// Use this for initialization
	void Start () {
		enemiesSwatting = 0;
		score = 0;
		currentState = states.idle;
		canSwat = true;
		recentlySwatted = false;
		holdingFood = false;
		holdingKnife = false;
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
		if (transform.position.y < -3.5f){
			transform.position = new Vector3 (transform.position.x, -3.5f, 0f);

		}
		if (transform.position.y > 6.0f){
			transform.position = new Vector3 (transform.position.x, 6.0f, 0f);

		}
		if (gameManager.GetComponent<GameManager> ().gratingLevel.activeSelf) {
			transform.position = new Vector3 (transform.position.x,transform.position.y,11.0f);
		}

		// Swatting should have a CD
		if (!recentlySwatted) {
			if (Input.GetKey (KeyCode.Joystick1Button0) && currentState == states.idle) {
				currentState = states.swatting;
				inSwatState = 0f;
			}
		} else {
			swatTimer -= Time.deltaTime;
			if (swatTimer <= 0f) {
				recentlySwatted = false;
			}
		}
		if (inSwatState >= 1f && currentState == states.swatting) {
			currentState = states.idle;
			inSwatState = 0f;
		} else {
			if (states.swatting == currentState) {
				inSwatState += Time.deltaTime;
			}
		}

	}

	void OnTriggerStay(Collider col){
		if (col.gameObject.tag == "knife" || col.gameObject.tag == "Pan1" || col.gameObject.tag == "Cheese" ||
		    col.gameObject.tag == "Grater" || col.gameObject.tag == "Pan2") {
			GrabbableObject obj = col.gameObject.GetComponent<GrabbableObject> ();
			// Make sure you can hold something : when idle and the object your on isn't being held
			if (currentState == states.idle && !col.gameObject.GetComponent<GrabbableObject> ().grabbed) {
				if (Input.GetKey (KeyCode.Joystick1Button1)) {
					if (!obj.grabbed) {
						obj.toggleGrabbed (true, transform);
						if (col.gameObject.tag == "knife") {
							holdingKnife = true;
						}
						if (col.gameObject.tag == "Pan1" && currentState != states.holding
						    || col.gameObject.tag == "Pan2" && currentState != states.holding) {
							col.GetComponent<PanTracking> ().held = true;
						}
						currentState = states.holding;
					} 
				}	
			}
			// if your holding something unleash it
			if (currentState == states.holding && obj.grabbed) {
				if (Input.GetKeyUp (KeyCode.Joystick1Button1)) {
					obj.toggleGrabbed (false, transform);
					currentState = states.idle;
					if (col.gameObject.tag == "Pan1" || col.gameObject.tag == "Pan2") {
						col.GetComponent<PanTracking> ().held = false;
					}
					holdingKnife = false;
				}
			}
		} 
		if (col.gameObject.tag == "Food") {
			if (col.gameObject.GetComponent<FoodClass> ().cut) {
				GrabbableObject obj = col.gameObject.GetComponent<GrabbableObject> ();
				// Make sure you can hold something : when idle and the object your on isn't being held
				if (currentState == states.idle && !col.gameObject.GetComponent<GrabbableObject> ().grabbed) {
					if (Input.GetKey (KeyCode.Joystick1Button1) && 
						!col.gameObject.GetComponent<FoodClass> ().taken) {
						if (!obj.grabbed) {
							holdingFood = true;
							obj.toggleGrabbed (true, transform);
							currentState = states.holding;
						} 
					}	
				}
				// if your holding something unleash it
				if (currentState == states.holding && !col.gameObject.GetComponent<FoodClass> ().taken 
					&& obj.grabbed) {
					if (Input.GetKeyUp (KeyCode.Joystick1Button1)) {
						obj.toggleGrabbed (false, transform);
						currentState = states.idle;
						holdingFood = false;
					}
				}
				// if the enemy got the food, unleash it
				if (obj.grabbed && col.gameObject.GetComponent<FoodClass>().taken){
					currentState = states.idle;
					obj.grabbed = false;
					holdingFood = false;
				}
			}
		}
		if(col.gameObject.tag == "Enemy" && !(gameManager.GetComponent<GameManager>().getLevel() == -60.0f)){
			// Kill the enemy and add to score based on enemy type
			if(currentState == states.swatting){
				currentState = states.idle;
				recentlySwatted = true;
				swatTimer = 1f;
				col.gameObject.GetComponent<Enemy>().kill();
				score += 3;
			}
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "Enemy" && !(gameManager.GetComponent<GameManager>().getLevel() == -60.0f)){
			// Kill the enemy and add to score based on enemy type
			if(currentState == states.swatting){
				currentState = states.idle;
				recentlySwatted = true;
				swatTimer = 1f;
				col.gameObject.GetComponent<Enemy>().kill();
				score += 3;
			}
		}
		if (col.gameObject.tag == "Player" && holdingKnife) {
			col.gameObject.GetComponent<Player2> ().score -= 3;
			cuttingLevelCam.GetComponent<camShake>().ShakeCam (1f, 0.5f);
			transform.position = new Vector3 (transform.position.x - 1.5f, transform.position.y, transform.position.z);
		}
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Plate"){
			currentState = states.idle;
			if (holdingFood) {
				score += 3;
				holdingFood = false;
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
