using UnityEngine;
using System.Collections;

// Checks if knife object is entering object
public class KnifeEnterCheck : MonoBehaviour {

	public Transform knife;
	bool cutAlready = false;
	public 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
		

//	void OnCollisionEnter (Collision foodItem)
//	{
//		Debug.Log ("We at least here");
//		if(foodItem.gameObject.tag == "food")
//		{
//			if (foodItem.gameObject.GetComponent<Transform>().position.y == GetComponent<Transform>().position.y) {
//				Debug.Log ("Colliding");
//				cutAlready = true;
//			}
//		}
//	}

	void OnTriggerEnter(Collider foodItem){
		if(foodItem.tag == "food" && !cutAlready)
		{
			Debug.Log ("Your in the food item");
			cutAlready = true;
		}
	}

	void OnTriggerExit(Collider foodItem){
		cutAlready = false;
	}
}
