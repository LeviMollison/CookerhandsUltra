using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpiderGenerator : MonoBehaviour {

    public SpiderMove spider;
    public List<SpiderMove> spiders;
    public FoodGenerator foodGen;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        //For now Random until I get access the list to get the x position of the food.
        //I need to track which spider spawned for which food and no repeats.
        

        for(int i = 0; i < foodGen.food.Count; i++)
        {
            if (!foodGen.food[i].targeted)
            {
                Vector3 position = new Vector3(foodGen.food[i].transform.position.x, 8f, 0);
                SpiderMove item = (SpiderMove)Instantiate(spider, position, transform.rotation);
                item.target = foodGen.food[i];
                foodGen.food[i].targeted = true;
                spiders.Add(item);
            }
        }
    }
}
