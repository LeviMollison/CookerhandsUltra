using UnityEngine;
using System.Collections;

public class ShowRandomFood : MonoBehaviour {

	// foods to use
	public Sprite burger;
	public Sprite friedRice;
	public Sprite lambTikka;
	public Sprite paneerTikka;
	public Sprite sandwich;
	public Sprite taco;
	public Sprite plateOfFood;


	// Use this for initialization
	void Start () {
		float randSprite = Random.Range(0f, 7f);
		if (randSprite < 1) {
			this.GetComponent<SpriteRenderer> ().sprite = burger;
		}
		else if (randSprite < 2) {
			this.GetComponent<SpriteRenderer> ().sprite = friedRice;
		} 
		else if (randSprite < 3) {
			this.GetComponent<SpriteRenderer> ().sprite = lambTikka;
		}
		else if (randSprite < 4) {
			this.GetComponent<SpriteRenderer> ().sprite = paneerTikka;
		}
		else if (randSprite < 5) {
			this.GetComponent<SpriteRenderer> ().sprite = sandwich;
		}
		else if (randSprite < 6) {
			this.GetComponent<SpriteRenderer> ().sprite = taco;
		}
		else if (randSprite < 7) {
			this.GetComponent<SpriteRenderer> ().sprite = plateOfFood;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
