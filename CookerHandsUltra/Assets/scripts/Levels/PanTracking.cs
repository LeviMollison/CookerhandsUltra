using UnityEngine;
using System.Collections;

public class PanTracking : MonoBehaviour {
	public bool held;
	public int panState;
	public Color defaultColor;
	// Use this for initialization
	void Start () {
		held = false;
		panState = 1;
		defaultColor = this.GetComponent<Renderer> ().material.color;
	}
	
	// Update is called once per frame
	void Update () {
		// change visuals based on pan state
		if (panState == 1) {
			this.GetComponent<Renderer> ().material.color = defaultColor;
		} else if (panState == 2) {
			this.GetComponent<Renderer> ().material.color = Color.yellow;
		} else if (panState == 3) {
			this.GetComponent<Renderer> ().material.color = Color.black;
		}
	}
}
