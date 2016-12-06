using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseGenerator : MonoBehaviour {

    public MouseMove mouse;
    public List<MouseMove> mice;
    public FoodGenerator foodMaking;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < foodMaking.food.Count; i++)
        {
            if (!foodMaking.food[i].targeted)
            {
                Vector3 position = new Vector3(-9f, foodMaking.food[i].transform.position.y, 0);
                MouseMove item = (MouseMove)Instantiate(mouse, position, transform.rotation);
                item.target = foodMaking.food[i];
                foodMaking.food[i].targeted = true;
                mice.Add(item);
            }
        }
    }
}
