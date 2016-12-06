using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public bool dead;
	// Use this for initialization
	void Start () {
		dead = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (dead) {
			
			Destroy(this.gameObject);
		}
	}

	public void kill(){
		dead = true;
		this.GetComponent<SpiderMove> ().dead = true;
	}
}
