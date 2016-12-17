using UnityEngine;
using System.Collections;

public class FlyMove : MonoBehaviour {

    private Vector3 border;
    public FoodClass target;

    public float speed;
    public bool food = false;
    public bool dead = false;

    // Use this for initialization
    void Start () {
        border = new Vector3(Random.Range(-10f, 10f), 8f, 0);
    }
	
	// Update is called once per frame
	void Update () {
        if (!food && target != null)
        {
            Vector3 direction = target.transform.position - transform.position;

            //turn our enemy to look in the direction of the vector.
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), .2f);

            //move our enemy in the direction of the player
            transform.position += direction.normalized * Time.deltaTime * speed;
			this.GetComponent<SpriteRenderer> ().transform.rotation = new Quaternion (
				this.GetComponent<SpriteRenderer> ().transform.rotation.x, -1.0f,0,0);

        }
        else
        {
            Vector3 direction = border - transform.position;
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), .2f);
            transform.position += direction.normalized * 2 * speed * Time.deltaTime;
			this.GetComponent<SpriteRenderer> ().transform.rotation = new Quaternion (
				this.GetComponent<SpriteRenderer> ().transform.rotation.x, -1.0f,0,0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
      
            food = true;
        }
    }
}
