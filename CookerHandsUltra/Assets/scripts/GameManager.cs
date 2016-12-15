using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum levels {titleScreen, cutting, sauteing, grating, gameOver};

public class GameManager : MonoBehaviour {

	// Game Timer
	public float actualTime;
	public float gameTime;

	// For keeping track of player needs
	public Player1 playerOne;
	public Player2 playerTwo;

    // Levels
    public GameObject titleScreen;
    public GameObject gameOverLevel;
    public GameObject cuttingLevel;
    public GameObject sauteingLevel;
    public GameObject gratingLevel;
	public GameObject plane;
	public GameObject F;
	public GameObject A;
	public GameObject pan;

    //Generates the food, spiders, flies, mice
    public GameObject generator;

	// Detecting who won and if the game's over
	public bool gameWon;
	public bool gameOver;

	// Detecting levels
	public levels currentLevel;
	public bool switchingLevels;

	// Use this for initialization
	void Start () {
		gameTime = 180.0f;
		actualTime = gameTime;
		gameOver = false;
		currentLevel = levels.titleScreen;
		switchingLevels = true;
		titleScreen.SetActive (true);
		gameOverLevel.SetActive (false);
		cuttingLevel.SetActive (false);
		sauteingLevel.SetActive (false);
		gratingLevel.SetActive (false);

		Physics.IgnoreCollision(playerOne.GetComponent<Collider>(), playerTwo.GetComponent<Collider>());

		// SceneManager.LoadScene(0);
	}
	
	// Update is called once per frame
	void Update () {
		if (currentLevel != levels.titleScreen && currentLevel != levels.gameOver) {
			actualTime -= Time.deltaTime;
			// If the time hit 0 you lose
		}
		// Do a reset if the game's over back to main title screen
		// If the game's not over, check what level your on and track the state of that level
		if (currentLevel == levels.titleScreen){
			if (Input.GetKey(KeyCode.Joystick1Button9) || Input.GetKey(KeyCode.Joystick2Button9)){
                //Switch camera and scene from title to inbetween cutting level
				cuttingLevel.SetActive(true);
				this.GetComponent<CameraController> ().CameraStart (titleScreen.transform.Find("Main Camera").GetComponent<Camera>(), 
					cuttingLevel.transform.Find("Main Camera").GetComponent<Camera>());
				currentLevel = levels.cutting;
				titleScreen.SetActive (false);
				switchingLevels = true;
			}
		}
		if (currentLevel == levels.cutting) {
			// If the game is over stop everything
			if (actualTime == 0 && !gameOver){
				cuttingLevel.SetActive (false);
				this.GetComponent<CameraController> ().CameraStart (cuttingLevel.transform.Find("Main Camera").GetComponent<Camera>(), 
					gameOverLevel.transform.Find("Main Camera").GetComponent<Camera>());
				gameOverLevel.SetActive (true);
				gameOver = true;
				gameWon = false;
			}
			else if (cuttingLevel.GetComponent<CuttingLevel>().levelComplete()){
				switchingLevels = true;
				if (cuttingLevel.GetComponent<CuttingLevel> ().levelWon) {
                    //Switch camera and scene from cutting to sauteing
					sauteingLevel.SetActive(true);
                    this.GetComponent<CameraController>().CameraStart(cuttingLevel.transform.Find("Main Camera").GetComponent<Camera>(),
                    	sauteingLevel.transform.Find("Main Camera").GetComponent<Camera>());
                    currentLevel = levels.sauteing;
					switchingLevels = true;
					cuttingLevel.SetActive (false);
				} else {
					currentLevel = levels.gameOver;
					gameOver = true;
					gameWon = false;
					gameOverLevel.SetActive (true);
					this.GetComponent<CameraController> ().CameraStart (cuttingLevel.transform.Find("Main Camera").GetComponent<Camera>(), 
						gameOverLevel.transform.Find("Main Camera").GetComponent<Camera>());
					cuttingLevel.SetActive (false);
					switchingLevels = false;
				}
			}
			if (switchingLevels){
				playerOne.changeLevel ();
				playerTwo.changeLevel ();
				switchingLevels = false;
			}
		}
		if (currentLevel == levels.sauteing) {
            // did you run out of time
			if (actualTime == 0 && !gameOver){
				sauteingLevel.SetActive (false);
				this.GetComponent<CameraController> ().CameraStart (sauteingLevel.transform.Find("Main Camera").GetComponent<Camera>(), 
					gameOverLevel.transform.Find("Main Camera").GetComponent<Camera>());
				gameOverLevel.SetActive (true);
				gameOver = true;
				gameWon = false;
			}
            else if (sauteingLevel.GetComponent<SauteingLevel>().levelComplete())
            {
                switchingLevels = true;
				if (sauteingLevel.GetComponent<SauteingLevel>().levelWon)
                {
                    //Switch camera and scene from sauteing to grating
					gratingLevel.SetActive(true);
                    this.GetComponent<CameraController>().CameraStart(sauteingLevel.transform.Find("Main Camera").GetComponent<Camera>(),
                    	gratingLevel.transform.Find("Main Camera").GetComponent<Camera>());
                    currentLevel = levels.grating;
                    switchingLevels = true;
                    sauteingLevel.SetActive(false);
                }
                else
                {
                    currentLevel = levels.gameOver;
					gameOverLevel.SetActive (true);
                    this.GetComponent<CameraController>().CameraStart(sauteingLevel.transform.Find("Main Camera").GetComponent<Camera>(),
                        gameOverLevel.transform.Find("Main Camera").GetComponent<Camera>());
                    sauteingLevel.SetActive(false);
					gameOver = true;
					gameWon = false;
					switchingLevels = false;
                }
            }
            if (switchingLevels)
            {
                playerOne.changeLevel();
                playerTwo.changeLevel();
                switchingLevels = false;
            }
        }
		if (currentLevel == levels.grating) {
            // is the level over
			plane.SetActive(false);
			if (actualTime == 0 && !gameOver){
				gratingLevel.SetActive (false);
				this.GetComponent<CameraController> ().CameraStart (gratingLevel.transform.Find("Main Camera").GetComponent<Camera>(), 
					gameOverLevel.transform.Find("Main Camera").GetComponent<Camera>());
				gameOverLevel.SetActive (true);
				gameOver = true;
				gameWon = false;
			}
            else if (gratingLevel.GetComponent<GratingLevel>().levelComplete())
            {
				if (gratingLevel.GetComponent<GratingLevel>().levelWon)
				{
					//Switch camera and scene from sauteing to grating
					gameOverLevel.SetActive(true);
					this.GetComponent<CameraController>().CameraStart(gratingLevel.transform.Find("Main Camera").GetComponent<Camera>(),
						 gameOverLevel.transform.Find("Main Camera").GetComponent<Camera>());
					currentLevel = levels.gameOver;
					gratingLevel.SetActive(false);
				}
				else
				{
					currentLevel = levels.gameOver;
					gameOverLevel.SetActive (true);
					this.GetComponent<CameraController>().CameraStart(gratingLevel.transform.Find("Main Camera").GetComponent<Camera>(),
						gameOverLevel.transform.Find("Main Camera").GetComponent<Camera>());
					sauteingLevel.SetActive(false);
					gameOver = true;
					gameWon = false;
					switchingLevels = false;
				}
            }
        }
		if (currentLevel == levels.gameOver) {
			plane.SetActive (false);
			// Detect if lost game
			if (!gameWon) {
				// display F animation
				F.SetActive (true);
				A.SetActive (false);
			} else {
				A.SetActive (true);
				F.SetActive (false);
			}
			if(Input.GetKey(KeyCode.JoystickButton9)){
				SceneManager.LoadScene(0);
			}
			// press a button to reload
		}
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
		if (currentLevel == levels.grating) {
			gratingLevel.GetComponent<GratingLevel> ().stealFood ();
		}
	}

	public void collectFoodInLevel(){
		if (currentLevel == levels.cutting) {
			cuttingLevel.GetComponent<CuttingLevel> ().collectFood ();
		}
		if (currentLevel == levels.grating) {
			gratingLevel.GetComponent<GratingLevel> ().collectFood ();
		}
	}

	// tell level when to activate
	public bool levelOn(){
		if (currentLevel != levels.titleScreen && currentLevel != levels.gameOver) {
			return true;
		}
		return false;
	}

    //Reset
    public void reset()
    {
		SceneManager.LoadScene(1);
        gameTime = 180.0f;
        actualTime = gameTime;
        gameOver = false;
        currentLevel = levels.titleScreen;
        switchingLevels = true;

        //Clear all lists
        if (generator.GetComponent<Generator>().food.Count > 0)
        {
            generator.GetComponent<Generator>().clearFood();
        }
        if (generator.GetComponent<Generator>().spiders.Count > 0)
        {
            generator.GetComponent<Generator>().clearSpider();
        }
        if (generator.GetComponent<Generator>().flies.Count > 0)
        {
            generator.GetComponent<Generator>().clearFly();
        }
        if (generator.GetComponent<Generator>().mice.Count > 0)
        {
            generator.GetComponent<Generator>().clearMouse();
        }
    }
}
