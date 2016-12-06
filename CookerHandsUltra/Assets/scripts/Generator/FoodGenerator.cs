using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FoodGenerator : MonoBehaviour {

    public FoodClass slice;
    public List<FoodClass> food;
    //public Knife knife;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        //if(knife.cutAlready)
	    if(food.Count < 5)
        {
            Vector3 position = new Vector3(Random.Range(-7f, 7f), -2.4f, 0);
            FoodClass item = (FoodClass)Instantiate(slice, position, transform.rotation);
            food.Add(item);

            //Add(x) to end of the list
            //Insert(x) to given index
            //Remove(x) remove first x observed
            //RemoveAt(x) remove item at given index
            //Count to know how many elements you have
        }
    }

    public List<FoodClass> getList()
    {
        return food;
    }
}
