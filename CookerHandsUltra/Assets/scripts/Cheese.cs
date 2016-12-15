using UnityEngine;
using System.Collections;

public class Cheese : MonoBehaviour {
	public bool beingHeld;

	// Use this for initialization
	void Start () {
		beingHeld = false;
	}

	// Update is called once per frame
	void Update () {
		beingHeld = this.GetComponent<GrabbableObject> ().grabbed;
	}
		
}
