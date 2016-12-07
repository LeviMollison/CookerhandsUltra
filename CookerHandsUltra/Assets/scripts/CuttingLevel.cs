using UnityEngine;
using System.Collections;

public class CuttingLevel : MonoBehaviour {
	
	public GameObject generator;
	// Use this for initialization
	void Start () {
//		for(int i = 0; i < 6; i++){
//			generator.GetComponent<FoodGenerator> ().createFood(transform.position.x, transform.position.y);	
//		}
	}
	
	// Update is called once per frame
	void Update () {
		if (generator.GetComponent<Generator> ().food.Count == 0 || generator.GetComponent<Generator>().spiders.Count == 0) {
			Debug.Log ("gameover");
		}
	}
}
