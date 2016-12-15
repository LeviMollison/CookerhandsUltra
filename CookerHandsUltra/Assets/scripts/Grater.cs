using UnityEngine;
using System.Collections;

public class Grater : MonoBehaviour {
	public bool beingHeld;
	public bool breakFood = false;

	// Use this for initialization
	void Start () {
		breakFood = false;
	}
	
	// Update is called once per frame
	void Update () {
		beingHeld = this.GetComponent<GrabbableObject> ().grabbed;
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Cheese" && beingHeld) {
			if (col.gameObject.GetComponent<Cheese> ().beingHeld) {
				breakFood = true;
			}
		}
	}
}
