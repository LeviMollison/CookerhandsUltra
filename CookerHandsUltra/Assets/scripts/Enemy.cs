using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public bool dead;
    public GameObject manager;
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
        if (manager.GetComponent<GameManager>().getLevel() == 100.0f)
        {
            this.GetComponent<SpiderMove>().dead = true;
        }
        else if (manager.GetComponent<GameManager>().getLevel() == 0.0f)
        {
            this.GetComponent<FlyMove>().dead = true;
        }
        else if (manager.GetComponent<GameManager>().getLevel() == -60.0f)
        {
            this.GetComponent<MouseMove>().dead = true;
        }
            
	}
}
