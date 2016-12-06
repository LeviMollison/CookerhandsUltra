using UnityEngine;
using System.Collections;

public class FoodClass : MonoBehaviour {

    public bool targeted = false;
    public bool taken = false;
    public bool gone = false;

    public bool food = false;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y > 6f)
        {
            gone = true;
            Debug.Log("Food is gone");
        }
	}
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Food Trigger Works");
            taken = true;
            this.transform.parent = other.transform;
        }
    }
}
