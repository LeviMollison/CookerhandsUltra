using UnityEngine;
using System.Collections;

public class FlyMove : MonoBehaviour {

    public Transform target;
    public Collision obj;

    private Vector3 border;
    public float speed;
    public bool food;

	// Use this for initialization
	void Start () {
        border = new Vector3(Random.Range(-10f, 10f), 6f, 0);
    }
	
	// Update is called once per frame
	void Update () {
        if (!food)
        {
            Vector3 direction = target.position - transform.position;

            //turn our enemy to look in the direction of the vector.
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), .2f);

            //move our enemy in the direction of the player
            transform.position += direction.normalized * Time.deltaTime * speed;

            //OnCollisionEnter(obj);
            gotFood();

        }
        else
        {
            Vector3 direction = border - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), .2f);
            transform.position += direction.normalized * 2 * speed * Time.deltaTime;
        }
    }

    void gotFood()
    {
        if (transform.position.x > target.position.x)
        {
            food = true;
        }
    }

    /*void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Food")
        {
            food = true;
            border = new Vector3(Random.Range(-10f, 10f), 6f, 0);
        }
    }*/
}
