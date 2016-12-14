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

    private Vector3 position;
    private float delay;
    
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update() {
//        Debug.Log(manager.GetComponent<GameManager>().getLevel());
//        Debug.Log(manager.GetComponent<GameManager>().levelOn());

        //When levelOn is true, instantiate food
        if (manager.GetComponent<GameManager>().levelOn()) {

			//Food Generator
			if (food.Count < 5)
			{
                //Cutting Level
                if(manager.GetComponent<GameManager>().getLevel() == 100.0f)
                {
                    position = new Vector3(Mathf.Round(Random.Range(manager.GetComponent<GameManager>().getLevel() - 6.0f, manager.GetComponent<GameManager>().getLevel() + 6.0f))
                    , -2.4f, 0);
                    FoodClass item = (FoodClass)Instantiate(slice, position, transform.rotation);
                    //Counter for how long food remains in one location before "target" is set false
                    item.GetComponent<FoodClass>().delay = 1000f;
                    food.Add(item);
                    Debug.Log("Generate Spider Food");
                }
                //Sauteing Level
                else if(manager.GetComponent<GameManager>().getLevel() == 0.0f)
                {
                    position = new Vector3(Mathf.Round(Random.Range(manager.GetComponent<GameManager>().getLevel() - 6.0f, manager.GetComponent<GameManager>().getLevel() + 6.0f))
                    , -2.4f, 0);
                    FoodClass item = (FoodClass)Instantiate(slice, position, transform.rotation);
                    //Counter for how long food remains in one location before "target" is set false
                    item.GetComponent<FoodClass>().delay = 10f;
                    food.Add(item);
                    Debug.Log("Generate Fly Food");
                }
                //Grating Level 
                else if(manager.GetComponent<GameManager>().getLevel() == -60.0f)
                {
                    position = new Vector3(Mathf.Round(Random.Range(manager.GetComponent<GameManager>().getLevel() - 6.0f, manager.GetComponent<GameManager>().getLevel() + 6.0f))
                    , -2.4f, 6f);
                    FoodClass item = (FoodClass)Instantiate(slice, position, transform.rotation);
                    //Counter for how long food remains in one location before "target" is set false
                    item.GetComponent<FoodClass>().delay = 1000f;
                    food.Add(item);
                    Debug.Log("Generate Mouse Food");
                }
            }

            for (int i = 0; i < food.Count; i++)
            {
                if (food[i] != null)
                {
                    if (food[i].gone && food[i].transform.parent == null)
                    {
                        if (food[i].collected)
                        {
                            manager.GetComponent<GameManager>().collectFoodInLevel();
                        }
                        else
                        {
                            manager.GetComponent<GameManager>().stealFoodInLevel();
                        }

                        Destroy(food[i].gameObject);
                        food.RemoveAt(i);
                    }
                }
                else
                {
                    food.RemoveAt(i);
                }
            }

            //On Cutting Level, Spider Generator
            if (manager.GetComponent<GameManager>().getLevel() == 100.0f)
			{   
				for (int i = 0; i < food.Count; i++)
				{
					delay = Mathf.Round(Random.Range(0f, 10f));
					if (!food[i].targeted && delay == 0f)
					{
						//Debug.Log("Generate Spider");
                        //Instantiate spider with target and at target's x position
						position = new Vector3(food[i].transform.position.x, 8f, 0);
						SpiderMove item = (SpiderMove)Instantiate(spider, position, transform.rotation);
                        item.target = food[i];
						food[i].targeted = true;
						spiders.Add(item);
					}
				}

				for (int i = 0; i < spiders.Count; i++)
				{
                    //Delete if spider has no target
					if(spiders[i].target == null)
					{
						Destroy(spiders[i].gameObject);
						spiders.RemoveAt(i);
					}
                    //Delete spider if dead
					else if (spiders[i].dead)
					{
						spiders[i].target.transform.parent = null;
						spiders[i].target.targeted = false;
						spiders.RemoveAt(i);
					}

					else if(spiders[i].transform.position.y > 8f && spiders[i].target != null)
					{
						spiders[i].target.transform.parent = null;
						Destroy(spiders[i].gameObject);
						spiders.RemoveAt(i);
					}
				}
			}//End of Spider Generator

			//On Sauteing Level, Fly Generator
			else if (manager.GetComponent<GameManager>().getLevel() == 0.0f)
			{
                clearSpider();
                clearFood();
				for (int i = 0; i < food.Count; i++)
				{
                    //Create fly at different times
					delay = Mathf.Round(Random.Range(0f, 10f));
					if (!food[i].targeted && delay == 0f)
					{
						Debug.Log("Generate Fly");
                        //Instantiate Fly with random position of x and target
						position = new Vector3(Random.Range(manager.GetComponent<GameManager>().getLevel() - 7.0f, manager.GetComponent<GameManager>().getLevel() + 7.0f), 8f, 0);
						FlyMove item = (FlyMove)Instantiate(fly, position, transform.rotation);
						item.target = food[i];
						food[i].targeted = true;
						flies.Add(item);
					}
				}

                    for (int i = 0; i < flies.Count; i++)
                    {
                        //Delete if fly has no target
                        if (flies[i].target == null)
                        {
                            Destroy(flies[i].gameObject);
                            flies.RemoveAt(i);
                        }
                        //Delete if fly is dead
                        else if (flies[i].dead)
                        {
                            flies[i].target.transform.parent = null;
                            flies[i].target.targeted = false;
                            flies.RemoveAt(i);
                        }

                        else if (flies[i].transform.position.y > 8f && flies[i].target != null)
                        {
                            flies[i].target.transform.parent = null;
                            Destroy(flies[i].gameObject);
                            flies.RemoveAt(i);
                        }
                    }
                
			}//End of Fly Generator


			//On Grating Level, Mouse Generator
			else if (manager.GetComponent<GameManager>().getLevel() == -60.0f)
			{
                clearFly();
                clearFood();
				for (int i = 0; i < food.Count; i++)
				{
                    //Generate mouse at different times
					delay = Mathf.Round(Random.Range(0f, 10f));
					if (!food[i].targeted && delay == 0f)
					{
						Debug.Log("Generate Mouse");
						if(Mathf.Round(Random.Range(0f, 10f))/2 == 0)
						{
							position = new Vector3(manager.GetComponent<GameManager>().getLevel() - 7f, food[i].transform.position.y, 0f);
						}
						else
						{
							position = new Vector3(manager.GetComponent<GameManager>().getLevel() + 7f, food[i].transform.position.y, 0f);
						}
                        //Instantiate mouse with position and target
						MouseMove item = (MouseMove)Instantiate(mouse, position, transform.rotation);
						item.target = food[i];
						food[i].targeted = true;
						mice.Add(item);
					}
				}

				for (int i = 0; i < mice.Count; i++)
				{
                    //Delete mouse if has no target
					if (mice[i].target == null)
					{
						Destroy(mice[i].gameObject);
						mice.RemoveAt(i);
					}
                    //Delete mouse if dead
                    else if (mice[i].dead)
					{
						mice[i].target.transform.parent = null;
						mice[i].target.targeted = false;
						mice.RemoveAt(i);
					}
                    
					else if (mice[i].transform.position.x > 9f && mice[i].target != null)
					{
						mice[i].target.transform.parent = null;
						Destroy(mice[i].gameObject);
						mice.RemoveAt(i);
					}
				}
			}//End of Mouse Generator	
		}//End of LevelOn
    }//End of Update

    //Clear food list
    public void clearFood()
    {
        for(int i = 0; i < food.Count; i++)
        {
            if(manager.GetComponent<GameManager>().getLevel() == 0.0f && food[i] != null)
            {
                if (95f <= food[i].transform.position.x && food[i].transform.position.x <= 107f)
                {
                    Destroy(food[i].gameObject);
                    food.RemoveAt(i);
                }                       
            }
            else if (manager.GetComponent<GameManager>().getLevel() == -60.0f && food[i] != null)
            {
                if(-6f < food[i].transform.position.x && food[i].transform.position.x < 6f)
                {
                    Destroy(food[i].gameObject);
                    food.RemoveAt(i);
                }       
            }
        }
    }

    //Clear spider list
    public void clearSpider()
    {
        for(int i = 0; i < spiders.Count; i++)
        {
            if(spiders[i] != null)
            {
                Destroy(spiders[i].gameObject);
                spiders.RemoveAt(i);
            }
            
        }
    }

    //Clear fly list
    public void clearFly()
    {
        for (int i = 0; i < flies.Count; i++)
        {
            if(flies[i] != null)
            {
                Destroy(flies[i].gameObject);
                flies.RemoveAt(i);
            }
        }
    }

    //Clear mouse list
    public void clearMouse()
    {
        for (int i = 0; i < mice.Count; i++)
        {
            if (mice[i] != null)
            {
                Destroy(mice[i].gameObject);
                mice.RemoveAt(i);
            }
        }
    }
}



