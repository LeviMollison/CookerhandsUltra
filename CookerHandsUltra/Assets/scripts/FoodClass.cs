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
	public GameObject manager;
	public Sprite cutGarlic;


	// Use this for initialization
	void Start () {
        countDown = delay;
    }
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y > 7f || (taken && transform.parent == null))
        {
            gone = true;
        }
		if (manager.GetComponent<GameManager> ().getLevel () == -60.0f) {
			if (transform.parent == null) {
				gone = true;
			}
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
			this.GetComponent<SpriteRenderer> ().sprite = cutGarlic;
			if (!collectable) {
				transform.localScale = new Vector3 (transform.localScale.x * 0.5f, transform.localScale.y * 0.5f, 
					transform.localScale.z);
			}
            collectable = true;
        }
	}
    
    void OnTriggerEnter(Collider other)
    {
		
//        if (other.gameObject.tag == "Enemy")
//        {
//			if (this.transform.parent == null) {
//				this.transform.parent = other.transform;
//				taken = true;
//			}
//        }
		if (other.gameObject.tag == "Plate") {
			gone = true;
			collected = true;
		}
    }
		
}
