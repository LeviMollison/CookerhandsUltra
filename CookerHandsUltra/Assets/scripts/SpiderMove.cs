using UnityEngine;
using System.Collections;

public class SpiderMove : MonoBehaviour {

    public Transform target;
    private Vector3 border;

    public float speed;
    public bool food = false;


	// Use this for initialization
	void Start () {
        border = new Vector3(2.1f,-2.4f,0);
	}
	
	// Update is called once per frame
	void Update () {
        //Run from left to right
        if (!food)
        {
            transform.position += (target.position - transform.position).normalized * speed * Time.deltaTime;
            gotFood();
        }
        else
        {
            transform.position += (border - transform.position).normalized * 2 * speed * Time.deltaTime;
        }
    }

    void gotFood()
    {
        if (transform.position.y < target.position.y)
        {
            food = true;
        }
    }
}
