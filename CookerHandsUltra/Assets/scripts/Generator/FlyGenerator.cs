using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlyGenerator : MonoBehaviour {

    public FlyMove fly;
    public List<FlyMove> flies;
    public FoodGenerator makeFood;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < makeFood.food.Count; i++)
        {
            if (!makeFood.food[i].targeted)
            {
                Vector3 position = new Vector3(Random.Range(-7f, 7f), 8f, 0);
                FlyMove item = (FlyMove)Instantiate(fly, position, transform.rotation);
                item.target = makeFood.food[i];
                makeFood.food[i].targeted = true;
                flies.Add(item);
            }
        }
    }
}
