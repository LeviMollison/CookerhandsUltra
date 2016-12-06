using UnityEngine;
using System.Collections;

public class SpiderMove : MonoBehaviour {

    private Vector3 border;
    public Transform target;

    public float speed;
    public bool food = false;
    //public bool dead = false;

    // Use this for initialization
    void Start()
    {
        border = new Vector3(transform.position.x, 8f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!food)
        {
            transform.position += (target.position - transform.position).normalized * speed * Time.deltaTime;
        }
        else
        {
            transform.position += (border - transform.position).normalized * 2 * speed * Time.deltaTime;
        }

        /*if (dead)
        {
            transform.position += (target.position - transform.position).normalized * speed * Time.deltaTime;
        }*/
    }

    public bool getFood(){return food;}

    public float getX(){return transform.position.x;}

    public float getY(){return transform.position.y;}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Food")
        {
            Debug.Log("colliding");
            food = true;
        }
    }

    /*void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("colliding");
        food = true;
    }*/
}
