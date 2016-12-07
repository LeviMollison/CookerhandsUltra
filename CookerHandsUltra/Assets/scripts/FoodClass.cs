using UnityEngine;
using System.Collections;

public class FoodClass : MonoBehaviour {

    public bool idle = false;
    public bool targeted = false;
    public bool taken = false;
    public bool gone = false;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y > 7f)
        {
            Debug.Log("Food Gone");
            gone = true;
        }

        if(transform.parent == null && transform.position.y > -2.4f)
        {
            Debug.Log("No Parent and Above Table");
        }
	}
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            taken = true;
            this.transform.parent = other.transform;
        }
    }
}
