using UnityEngine;
using System.Collections;

public class FoodClass : MonoBehaviour {

    public float delay = 1000.0f;
    public bool targeted = false;
    public bool taken = false;
    public bool gone = false;
    public bool cut = false;
    public bool collectable = false;
	public bool collected = false;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y > 7f)
        {
            Debug.Log("Food Gone");
            gone = true;
        }

        if(transform.parent == null && transform.position.y > -2.4f)
        {
//            Debug.Log("No Parent and Above Table");
        }

        if (transform.position.y == -2.4f)
        {
            delay = delay - 0.01f;
            if(delay < 0f)
            {
                targeted = false;
                delay = 1000.0f;
            }
        }

        if (cut)
        {
            this.GetComponent<Renderer>().material.color = Color.red;
            collectable = true;
        }
	}
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            taken = true;
            this.transform.parent = other.transform;
        }
		if (other.gameObject.tag == "Plate") {
			gone = true;
			collected = true;
		}
    }
		
}
