using UnityEngine;
using System.Collections;

public class FoodClass : MonoBehaviour {

    public float delay;
    public float countDown;
    public bool targeted = false;
    public bool taken = false;
    public bool gone = false;
    public bool cut = false;
    public bool collectable = false;
	public bool collected = false;


	// Use this for initialization
	void Start () {
        countDown = delay;
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
			
        }

        if (transform.position.y == -2.4f)
        {
            countDown = countDown - 0.01f;
            if(countDown < 0f)
            {
                targeted = false;
                countDown = delay;
            }
        }

        if (cut)
        {
            this.GetComponent<Renderer>().material.color = Color.red;
            collectable = true;
        }
	}

	void OnTriggerStay(Collider other){
		if (other.gameObject.tag == "ground") {
			this.GetComponent<Rigidbody> ().useGravity = false;
			this.GetComponent<Rigidbody>().isKinematic = true;
		}
	}

	void OnTriggerExit(Collider other){
		if(other.gameObject.tag=="ground" && transform.parent == null) {
			this.GetComponent<Rigidbody> ().useGravity = true;
			this.GetComponent<Rigidbody>().isKinematic = false;
		}
	}
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            this.transform.parent = other.transform;
			this.GetComponent<Rigidbody> ().useGravity = false;
			this.GetComponent<Rigidbody>().isKinematic = true;
            taken = true;
        }
		if (other.gameObject.tag == "Plate") {
			gone = true;
			collected = true;
		}
    }
		
}
