using UnityEngine;
using System.Collections;

public class SpiderMove : MonoBehaviour {

    private Vector3 border;
    public FoodClass target;

    public float speed;
    public bool food = false;
    public bool dead = false;

    // Use this for initialization
    void Start()
    {
        border = new Vector3(transform.position.x, 8f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!food && target != null)
        {
            transform.position += (target.transform.position - transform.position).normalized * speed * Time.deltaTime;
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

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Food")
        {
			// Check if the food was obtained already
			if ((other.gameObject.GetComponent<FoodClass>().transform.parent == null && !this.food)){
				this.food = true;	
				other.gameObject.GetComponent<FoodClass>().transform.parent = this.transform;
				other.gameObject.GetComponent<FoodClass>().taken = true;
			}
        }
    }
}
