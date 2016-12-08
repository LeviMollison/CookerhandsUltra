using UnityEngine;
using System.Collections;

// Checks if knife object is entering object
public class CuttingAction : MonoBehaviour
{

    public Transform knife;
    bool cutting = false;
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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


