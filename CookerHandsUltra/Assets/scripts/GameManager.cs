using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Game Timer
	public float actualTime;
	private float gameTime;

	// For keeping track of player needs
	public Player playerOne;
	public Player playerTwo;

	// Need a max amount of food to be stolen
	public int maxFood;
	public int foodStolen;

	// Detecting who won and if the game's over
	bool gameWon;
	bool gameOver;

	// Detecting levels
	enum levels {titleScreen, cutting, sauteing, grating};
	levels currentLevel;
	public GameObject[] cameras;

	// Use this for initialization
	void Start () {
		gameTime = 300.0f;
		actualTime = gameTime;
		maxFood = 10;
		foodStolen = 0;
		gameOver = false;
		currentLevel = levels.titleScreen;

		// SceneManager.LoadScene(0);
	}
	
	// Update is called once per frame
	void Update () {
		actualTime -= Time.deltaTime;
		// If the time hit 0 you lose
		if (actualTime == 0 && !gameOver){
			// Bring it to end game screen
			// Reset the game for now
		}else{
			// if enemies steal 2/3 of food, game's done
			float stolenFoodRatio = (float)foodStolen / (float)maxFood;
			if(stolenFoodRatio > (2.0/3.0)){
				gameWon = false;
				gameOver = true;
			}
		}
		// Do a reset if the game's over back to main title screen
		// If the game's not over, check what level your on and track the state of that level
		if (currentLevel == levels.titleScreen){
			// click a key to switch scenes to the cutting scene
			// SceneManager.LoadScene(1); currentLevel = levels.cutting;
		}
		if (currentLevel == levels.cutting) {
			// is the level over
		}
		if (currentLevel == levels.sauteing) {
			// is the level over
		}
	}

	void stealFood(){
		
	}

	// Provide proper x-axis for players
	public float getLevel(){
		if (currentLevel == levels.cutting) {
			return 100.0f;
		}
		if (currentLevel == levels.sauteing) {
			return 0.0f;
		}
		// default will be title screen
		if (currentLevel == levels.grating){
			return -60.0f;
		}
		return 100.0f;
	}
}
