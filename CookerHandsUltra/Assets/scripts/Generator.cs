using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Generator : MonoBehaviour {

    public FoodClass slice;
    public List<FoodClass> food;

    public SpiderMove spider;
    public List<SpiderMove> spiders;

    public FlyMove fly;
    public List<FlyMove> flies;

    public MouseMove mouse;
    public List<MouseMove> mice;

    public GameObject manager;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update() {

        //Add(x) to end of the list
        //Insert(x) to given index
        //Remove(x) remove first x observed
        //RemoveAt(x) remove item at given index
        //Count to know how many elements you have


        //Food Generator
        //if(knife.cutAlready)
        if (food.Count< 5)
        {
            Debug.Log("Generate Food");
			Vector3 position = new Vector3(Random.Range(manager.GetComponent<GameManager>().getLevel() - 7.0f, manager.GetComponent<GameManager>().getLevel() + 7.0f)
                , -2.4f, 0);
            FoodClass item = (FoodClass)Instantiate(slice, position, transform.rotation);
            food.Add(item);
        }



        //Spider Generator
        if (manager.GetComponent<GameManager>().getLevel() == 100.0f)
        {
            for (int i = 0; i < food.Count; i++)
            {
                if (!food[i].targeted)
                {
                    Debug.Log("Generate Spider");
                    Vector3 position = new Vector3(food[i].transform.position.x, 8f, 0);
                    SpiderMove item = (SpiderMove)Instantiate(spider, position, transform.rotation);
                    item.target = food[i];
                    food[i].targeted = true;
                    spiders.Add(item);
                }
            }

            for (int i = 0; i < spiders.Count; i++)
            {
                if (spiders[i].dead)
                {
                    spiders.RemoveAt(i);
                }
            }
        }

        //Fly Generator
        if (manager.GetComponent<GameManager>().getLevel() == 0.0f)
        {
            for (int i = 0; i < food.Count; i++)
            {
                if (!food[i].targeted)
                {
                    Debug.Log("Generate Fly");
                    Vector3 position = new Vector3(Random.Range(-7f, 7f), 8f, 0);
                    FlyMove item = (FlyMove)Instantiate(fly, position, transform.rotation);
                    item.target = food[i];
                    food[i].targeted = true;
                    flies.Add(item);
                }
             }
        }


        //Mouse Generator
        if (manager.GetComponent<GameManager>().getLevel() == -60.0f)
        {
            for (int i = 0; i < food.Count; i++)
            {
                if (!food[i].targeted)
                {
                    Debug.Log("Generate Mouse");
                    Vector3 position = new Vector3(-9f, food[i].transform.position.y, 0);
                    MouseMove item = (MouseMove)Instantiate(mouse, position, transform.rotation);
                    item.target = food[i];
                    food[i].targeted = true;
                    mice.Add(item);
                }
            }
        }
    }
}
