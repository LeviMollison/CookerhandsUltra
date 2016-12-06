using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpiderGenerator : MonoBehaviour {

    public SpiderMove spider;
    public List<SpiderMove> spiders;
    public FoodGenerator foodGen;

    int spiderCount = 0;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        //For now Random until I get access the list to get the x position of the food.
        //I need to track which spider spawned for which food and no repeats.
        

        if(spiderCount < 5)
        {
            
            Vector3 position = new Vector3(Random.Range(-7f, 7f), 8f, 0);
            SpiderMove item = (SpiderMove)Instantiate(spider, position, transform.rotation);
            spiders.Add(item);
            spiderCount++;
        }
    }
}
