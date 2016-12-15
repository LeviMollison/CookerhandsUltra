using UnityEngine;
using System.Collections;

public class ignoreTheGround : MonoBehaviour {

	public GameObject plane;

	// Use this for initialization
	void Start () {
		Physics.IgnoreCollision(this.GetComponent<Collider>(), plane.GetComponent<Collider>());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
