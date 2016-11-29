using UnityEngine;
using System.Collections;

public class GrabbableObject : MonoBehaviour {

	public Transform target;
	public bool grabbed;
	// Use this for initialization
	void Start () {
		grabbed = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (grabbed) {
			// move connected to player
			transform.position = new Vector3(target.position.x - 1.0f, target.position.y, 0.0f);
		}
	}

	public void toggleGrabbed(bool val){
		grabbed = val;
	}
}
