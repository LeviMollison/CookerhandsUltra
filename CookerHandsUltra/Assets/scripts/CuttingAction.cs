using UnityEngine;
using System.Collections;

// Checks if knife object is entering object
public class CuttingAction : MonoBehaviour
{
	bool cutting;
    
    // Use this for initialization
    void Start()
    {
		cutting = false;
    }

    // Update is called once per frame
    void Update()
    {
		// If it's being held, have it follow the hand
    }

    void OnTriggerEnter(Collider foodItem)
    {
        if (foodItem.tag == "Food" && !cutting)
        {
            cutting = true;
            foodItem.gameObject.GetComponent<FoodClass>().cut = true;
        }
    }

    void OnTriggerExit(Collider foodItem)
    {
        cutting = false;
    }
}


