using UnityEngine;
using System.Collections;

public class Grater : MonoBehaviour {
	public bool beingHeld;
	public bool breakFood = false;
	public GameObject gratingAnim;
	public float animationTime;
	public float actualTime;

	// Use this for initialization
	void Start () {
		breakFood = false;
		animationTime = 1f;
		actualTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		beingHeld = this.GetComponent<GrabbableObject> ().grabbed;
		if (actualTime > 0) {
			actualTime -= Time.deltaTime;
		}
		if (actualTime <= 0) {
			if (gratingAnim.activeSelf) {
				gratingAnim.SetActive (false);	
			}
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Cheese" && beingHeld) {
			if (col.gameObject.GetComponent<Cheese> ().beingHeld) {
				breakFood = true;
				// show the grating animation for .5 seconds
				gratingAnim.SetActive(true);
				gratingAnim.transform.position = new Vector3 (transform.position.x, 
					transform.position.y + 1.0f, transform.position.z);
				actualTime = animationTime;
			}
		}
	}
}
