using UnityEngine;
using System.Collections;

public class GrabbableObject : MonoBehaviour {

	public Transform hand;
	public bool grabbed = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (grabbed) {
			// move connected to player
			transform.position = new Vector3 (hand.position.x-1.0f, hand.position.y, 0.0f);
		} else {
			transform.position = new Vector3 (transform.position.x, transform.position.y, 0.0f);
		}
	}

	public void toggleGrabbed(bool val, Transform pHand){
		// Testing Push
		grabbed = val;
		hand = pHand;
	}
}
