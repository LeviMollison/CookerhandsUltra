using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Stella_GameManager : MonoBehaviour {

	// Game Timer
	public float actualTime;
	private float gameTime;

	// For keeping track of player needs
	public Player1 playerOne;
	public Player2 playerTwo;

	// Levels
	public GameObject titleScreen;
	public GameObject gameOverLevel;
    public GameObject cuttingLevel;
    public GameObject sauteingLevel;
    public GameObject gratingLevel;

	// Detecting who won and if the game's over
	bool gameWon;
	bool gameOver;

	// Detecting levels
	enum levels {titleScreen, cutting, sauteing, grating, gameOver};
	levels currentLevel;
	bool switchingLevels;

	// Use this for initialization
	void Start () {
		gameTime = 180.0f;
		actualTime = gameTime;
		gameOver = false;
		currentLevel = levels.titleScreen;
		switchingLevels = true;

		// SceneManager.LoadScene(0);
	}
	
	// Update is called once per frame
	void Update () {
		if (currentLevel != levels.titleScreen && currentLevel != levels.gameOver) {
			actualTime -= Time.deltaTime;
			// If the time hit 0 you lose
			if (actualTime == 0 && !gameOver){
				// Bring it to end game screen
				// Reset the game for now
			}
		}
		// Do a reset if the game's over back to main title screen
		// If the game's not over, check what level your on and track the state of that level
		if (currentLevel == levels.titleScreen){
			// SceneManager.LoadScene(1); currentLevel = levels.cutting;
			if (Input.GetKey(KeyCode.UpArrow)){
				this.GetComponent<CameraController> ().CameraStart (titleScreen.transform.Find("Main Camera").GetComponent<Camera>(), 
					cuttingLevel.transform.Find("Main Camera").GetComponent<Camera>());
				titleScreen.SetActive (false);
				switchingLevels = true;
                currentLevel = levels.cutting;
            }
		}
		else if (currentLevel == levels.cutting) {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.GetComponent<CameraController>().CameraStart(cuttingLevel.transform.Find("Main Camera").GetComponent<Camera>(),
                    sauteingLevel.transform.Find("Main Camera").GetComponent<Camera>());
                cuttingLevel.SetActive(false);
                switchingLevels = true;
                currentLevel = levels.sauteing;
			}
            if (switchingLevels){
				playerOne.changeLevel ();
				playerTwo.changeLevel ();
				switchingLevels = false;
			}
		}
		else if (currentLevel == levels.sauteing) {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                this.GetComponent<CameraController>().CameraStart(sauteingLevel.transform.Find("Main Camera").GetComponent<Camera>(),
                    gratingLevel.transform.Find("Main Camera").GetComponent<Camera>());
                sauteingLevel.SetActive(false);
                switchingLevels = true;
                currentLevel = levels.grating;
            }
            if (switchingLevels)
            {
                playerOne.changeLevel();
                playerTwo.changeLevel();
                switchingLevels = false;
            }
        }
		else if (currentLevel == levels.grating) {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.GetComponent<CameraController>().CameraStart(gratingLevel.transform.Find("Main Camera").GetComponent<Camera>(),
                    gameOverLevel.transform.Find("Main Camera").GetComponent<Camera>());
                gratingLevel.SetActive(false);
                switchingLevels = true;
                currentLevel = levels.gameOver;
            }
        }
		if (currentLevel == levels.gameOver) {
			if(Input.GetKey(KeyCode.JoystickButton9)){
				SceneManager.LoadScene(0);
			}
			// press a button to reload
		}
	}

	// Provide proper x-axis for players
	public float getLevel(){
        if(currentLevel == levels.titleScreen)
        {
            return 1.0f;
        }
		else if (currentLevel == levels.cutting) {
			return 100.0f;
		}
		else if (currentLevel == levels.sauteing) {
			return 0.0f;
		}
		// default will be title screen
		else if (currentLevel == levels.grating){
			return -60.0f;
		}
		return -1f;
	}

	public Vector3 getLevelBounds(){
		if (currentLevel == levels.cutting) {
			return cuttingLevel.GetComponent<Transform> ().position;
		}
		return cuttingLevel.GetComponent<Transform> ().position;
	}

	public void stealFoodInLevel(){
		if (currentLevel == levels.cutting) {
			cuttingLevel.GetComponent<CuttingLevel> ().stealFood ();
		}
		if (currentLevel == levels.sauteing) {
			sauteingLevel.GetComponent<SauteingLevel> ().stealFood ();
		}
	}

	public void collectFoodInLevel(){
		if (currentLevel == levels.cutting) {
			cuttingLevel.GetComponent<CuttingLevel> ().collectFood ();
		}
	}

	// tell level when to activate
	public bool levelOn(){
		if (currentLevel != levels.titleScreen && currentLevel != levels.gameOver) {
			return true;
		}
		return false;
	}
}
