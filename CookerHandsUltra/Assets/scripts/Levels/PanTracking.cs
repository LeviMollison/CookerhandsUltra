using UnityEngine;
using System.Collections;

public class PanTracking : MonoBehaviour {
	public bool held;
	public int panState;
	// Use this for initialization
	void Start () {
		held = false;
		panState = 1;
	}
	
	// Update is called once per frame
	void Update () {
		// change visuals based on pan state
	}
}
