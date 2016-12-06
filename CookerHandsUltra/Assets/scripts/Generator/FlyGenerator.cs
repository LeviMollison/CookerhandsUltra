using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlyGenerator : MonoBehaviour {

    public FlyMove fly;
    public List<FlyMove> flies;

    int flyCount = 0;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (flyCount < 5)
        {
            Vector3 position = new Vector3(Random.Range(-7f, 7f), 8f, 0);
            FlyMove item = (FlyMove)Instantiate(fly, position, transform.rotation);
            flies.Add(item);
            flyCount++;
        }
    }
}
