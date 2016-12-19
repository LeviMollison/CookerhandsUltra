using UnityEngine;
using System.Collections;

public class MouseMove : MonoBehaviour {

    private Vector3 border;
    public FoodClass target;

    public float speed;
    public bool food = false;
    public bool dead = false;


    // Use this for initialization
    void Start()
    {
        border = new Vector3(9f, transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Run from left to right
        if (!food && target != null)
        {
            transform.position += (target.transform.position - transform.position).normalized * speed * Time.deltaTime;
        }
        else
        {
            transform.position += (border - transform.position).normalized * 2 * speed * Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            this.food = true;
        }
    }
}
