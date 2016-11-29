using UnityEngine;
using System.Collections;

public class GrabThings : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col) {
		Debug.Log ("Touching Something");
		if (col.gameObject.tag == "Square") {
			Debug.Log ("Being Touched");
			if (Input.GetKey (KeyCode.JoystickButton1)) {
				Debug.Log ("Being Grabbed");
				GrabbableObject obj = col.gameObject.GetComponent<GrabbableObject> ();
				obj.toggleGrabbed (true);
			}

		}

	}
}
