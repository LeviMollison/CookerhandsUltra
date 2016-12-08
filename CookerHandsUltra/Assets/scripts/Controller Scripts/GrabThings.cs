using UnityEngine;
using System.Collections;

public class GrabThings : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider col){
		GrabbableObject obj = col.gameObject.GetComponent<GrabbableObject> ();
		if (Input.GetKey(KeyCode.JoystickButton1)) {
			if (!obj.grabbed) {
				obj.toggleGrabbed (true, transform);
				Debug.Log ("Being Grabbed");

			} 
		}
		if(Input.GetKeyUp(KeyCode.JoystickButton1)){
			obj.toggleGrabbed (false, transform);
			Debug.Log ("Released");
		}
	}
}
