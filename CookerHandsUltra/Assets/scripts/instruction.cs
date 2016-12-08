using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class instruction : MonoBehaviour {
    public float actualTime;
    public float gameTime;

    // Use this for initialization
    void Start () {
        gameTime = 60.0f;
        actualTime = gameTime;
    }
	
	// Update is called once per frame
	void Update () {
        actualTime -= Time.deltaTime;
        
        if (actualTime <= 0.0f){
            
            Destroy(GameObject.Find("Canvas"));
        }


    }
}
