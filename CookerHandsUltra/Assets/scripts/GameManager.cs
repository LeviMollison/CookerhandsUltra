using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum levels {titleScreen, cutting, sauteing, grating, gameOver,
	levelCuttingTransition, levelSauteingTransition, levelGratingTransition, gameOverTransition};

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

	// In Btw Levels
	public GameObject cuttingLevelTransition;
	public GameObject sauteingLevelTransition;
	public GameObject gratingLevelTransition;
	public GameObject gameOverTransition;

	public GameObject plane;

	// Grades
	public GameObject F;
	public GameObject A;
	public GameObject B;
	public GameObject C;
	public GameObject D;
	public GameObject randomFood;

	public GameObject pan;
	public GameObject overlayInformation;

	// Sound Playing
	public AudioClip plateCollectSound;
	public AudioClip spiderSound;
	public AudioClip foodSizzle;
	public AudioClip flyBuzz;
	public AudioClip mouseSqueak;
	public AudioClip gameOverSound;
	public AudioClip bgMusicOne;
	public AudioClip victory; public int playedVictorySound = 0;

	public AudioSource audioPlayer;
	public AudioSource levelSoundEffects;
	public AudioSource bgMusic;
	public float waitTime = 0f;

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
		// Do a reset if the game's over back to main title screen
		// If the game's not over, check what level your on and track the state of that level
		if (currentLevel == levels.titleScreen) {
			if (!bgMusic.isPlaying) {
					bgMusic.clip = bgMusicOne;
					bgMusic.volume = 0.8f;
					bgMusic.Play ();
			}
			overlayInformation.SetActive (false);
			if (Input.GetKey (KeyCode.Joystick1Button9) || Input.GetKey (KeyCode.Joystick2Button9)) {
				bgMusic.volume = 0.3f;
				//Switch camera and scene from title to inbetween cutting level
				cuttingLevelTransition.SetActive (true);
				this.GetComponent<CameraController> ().CameraStart (titleScreen.transform.Find ("Main Camera").GetComponent<Camera> (), 
					cuttingLevelTransition.transform.Find ("Main Camera").GetComponent<Camera> ());
				currentLevel = levels.levelCuttingTransition;
				titleScreen.SetActive (false);
			}
		} else if (currentLevel != levels.gameOver) {
			actualTime -= Time.deltaTime;
			if (!bgMusic.isPlaying) {
				bgMusic.clip = bgMusicOne;
				bgMusic.volume = 0.3f;
				bgMusic.Play ();
			}
		} else {
			bgMusic.Stop ();
		}
		if (currentLevel == levels.levelCuttingTransition) {
			actualTime += Time.deltaTime;
			// Go through the thing, then move to cutting level
			if(Input.GetKeyUp(KeyCode.Joystick1Button2) || Input.GetKeyUp(KeyCode.Joystick2Button2)){
				cuttingLevelTransition.GetComponentInChildren<stage1> ().inputState+=1;
				if (cuttingLevelTransition.GetComponentInChildren<stage1> ().inputState >= 2) {
					cuttingLevel.SetActive (true);
					this.GetComponent<CameraController> ().CameraStart (cuttingLevelTransition.transform.Find("Main Camera").GetComponent<Camera>(), 
						cuttingLevel.transform.Find("Main Camera").GetComponent<Camera>());
					currentLevel = levels.cutting;
					cuttingLevelTransition.SetActive (false);
					overlayInformation.SetActive (true);
					switchingLevels = true;
				}
			}
		}
		if (currentLevel == levels.cutting) {
			overlayInformation.SetActive (true);
			// Play spider sound
			if(generator.GetComponent<Generator>().spiders.Count > 0){
				if (!levelSoundEffects.isPlaying) {
					levelSoundEffects.volume = 0.8f;
					levelSoundEffects.clip = spiderSound;
					levelSoundEffects.Play ();
				}
			}
			// If the game is over stop everything
			if (actualTime <= 0 && !gameOver){
				currentLevel = levels.gameOver;
				cuttingLevel.SetActive (false);
				this.GetComponent<CameraController> ().CameraStart (cuttingLevel.transform.Find("Main Camera").GetComponent<Camera>(), 
					gameOverLevel.transform.Find("Main Camera").GetComponent<Camera>());
				gameOverLevel.SetActive (true);
				gameOver = true;
				gameWon = false;
				if (levelSoundEffects.isPlaying) {
					levelSoundEffects.Stop ();
				}
				levelSoundEffects.clip = gameOverSound;
				levelSoundEffects.Play ();
			}
			else if (cuttingLevel.GetComponent<CuttingLevel>().levelComplete()){
				switchingLevels = true;
				if (cuttingLevel.GetComponent<CuttingLevel> ().levelWon) {
                    //Switch camera and scene from cutting to sauteing Intro
					sauteingLevelTransition.SetActive(true);
					overlayInformation.SetActive (false);
                    this.GetComponent<CameraController>().CameraStart(cuttingLevel.transform.Find("Main Camera").GetComponent<Camera>(),
						sauteingLevelTransition.transform.Find("Main Camera").GetComponent<Camera>());
					currentLevel = levels.levelSauteingTransition;
					cuttingLevel.SetActive (false);
					playerOne.GetComponent<Player1> ().score += 10;
					playerTwo.GetComponent<Player2> ().score += 10;
				} else {
					currentLevel = levels.gameOver;
					gameOver = true;
					gameWon = false;
					gameOverLevel.SetActive (true);
					this.GetComponent<CameraController> ().CameraStart (cuttingLevel.transform.Find("Main Camera").GetComponent<Camera>(), 
						gameOverLevel.transform.Find("Main Camera").GetComponent<Camera>());
					cuttingLevel.SetActive (false);
					switchingLevels = false;
					if (levelSoundEffects.isPlaying) {
						levelSoundEffects.Stop ();
					}
					levelSoundEffects.clip = gameOverSound;
					levelSoundEffects.Play ();
				}
			}
			if (switchingLevels){
				playerOne.changeLevel ();
				playerTwo.changeLevel ();
				switchingLevels = false;
			}
		}
		if (currentLevel == levels.levelSauteingTransition) {
			actualTime -= Time.deltaTime;
			// Go through the thing, then move to cutting level
			if(Input.GetKeyUp(KeyCode.Joystick1Button2) || Input.GetKeyUp(KeyCode.Joystick2Button2)){
				sauteingLevelTransition.GetComponentInChildren<stage2> ().inputState+=1;
				if (sauteingLevelTransition.GetComponentInChildren<stage2> ().inputState >= 2) {
					sauteingLevel.SetActive (true);
					this.GetComponent<CameraController> ().CameraStart (sauteingLevelTransition.transform.Find("Main Camera").GetComponent<Camera>(), 
						sauteingLevel.transform.Find("Main Camera").GetComponent<Camera>());
					currentLevel = levels.sauteing;
					sauteingLevelTransition.SetActive (false);
					overlayInformation.SetActive (true);
					switchingLevels = true;
				}
			}
		}
		if (currentLevel == levels.sauteing) {

			// Disregard time for this level
			actualTime += Time.deltaTime;
			// Play Sounds
			if (!audioPlayer.isPlaying) {
				audioPlayer.volume = 0.8f;
				audioPlayer.clip = foodSizzle;
				audioPlayer.Play ();
			}
			if(generator.GetComponent<Generator>().flies.Count > 0){
				if (!levelSoundEffects.isPlaying) {
					levelSoundEffects.volume = 0.8f;
					levelSoundEffects.clip = flyBuzz;
					levelSoundEffects.Play ();
				}
			}
            if (sauteingLevel.GetComponent<SauteingLevel>().levelComplete())
            {
                switchingLevels = true;
				if (sauteingLevel.GetComponent<SauteingLevel>().levelWon)
                {
                    //Switch camera and scene from sauteing to grating Intro
					gratingLevelTransition.SetActive(true);
					overlayInformation.SetActive (false);
					this.GetComponent<CameraController>().CameraStart(sauteingLevel.transform.Find("Main Camera").GetComponent<Camera>(),
						gratingLevelTransition.transform.Find("Main Camera").GetComponent<Camera>());
					currentLevel = levels.levelGratingTransition;
					sauteingLevel.SetActive (false);
					audioPlayer.Stop ();
					playerOne.GetComponent<Player1> ().score += 10;
					playerTwo.GetComponent<Player2> ().score += 10;
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
					if (levelSoundEffects.isPlaying) {
						levelSoundEffects.Stop ();
					}
					levelSoundEffects.clip = gameOverSound;
					levelSoundEffects.Play ();
					switchingLevels = false;
					audioPlayer.Stop ();
                }
            }
            if (switchingLevels)
            {
                playerOne.changeLevel();
                playerTwo.changeLevel();
                switchingLevels = false;
            }
        }
		if (currentLevel == levels.levelGratingTransition) {
			// Go through the thing, then move to cutting level
			if(Input.GetKeyUp(KeyCode.Joystick1Button2) || Input.GetKeyUp(KeyCode.Joystick2Button2)){
				gratingLevelTransition.GetComponentInChildren<stage3> ().inputState+=1;
				if (gratingLevelTransition.GetComponentInChildren<stage3> ().inputState >= 2) {
					gratingLevel.SetActive (true);
					this.GetComponent<CameraController> ().CameraStart (gratingLevelTransition.transform.Find("Main Camera").GetComponent<Camera>(), 
						gratingLevel.transform.Find("Main Camera").GetComponent<Camera>());
					currentLevel = levels.grating;
					gratingLevelTransition.SetActive (false);
					overlayInformation.SetActive (true);
					switchingLevels = true;
				}
			}
		}
		if (currentLevel == levels.grating) {
			// Play Sounds
			if(generator.GetComponent<Generator>().mice.Count > 0){
				if (waitTime <= 0f) {
					if (!levelSoundEffects.isPlaying) {
						levelSoundEffects.volume = 0.8f;
						levelSoundEffects.clip = mouseSqueak;
						levelSoundEffects.Play ();
						waitTime = 2f;
					}
				}
				waitTime -= Time.deltaTime;
			}
            // is the level over
			plane.SetActive(false);
			if (actualTime <= 0 && !gameOver){
				currentLevel = levels.gameOver;
				gratingLevel.SetActive (false);
				this.GetComponent<CameraController> ().CameraStart (gratingLevel.transform.Find("Main Camera").GetComponent<Camera>(), 
					gameOverLevel.transform.Find("Main Camera").GetComponent<Camera>());
				gameOverLevel.SetActive (true);
				gameOver = true;
				gameWon = false;
				if (levelSoundEffects.isPlaying) {
					levelSoundEffects.Stop ();
				}
				levelSoundEffects.clip = gameOverSound;
				levelSoundEffects.Play ();
			}
            else if (gratingLevel.GetComponent<GratingLevel>().levelComplete())
            {
				if (gratingLevel.GetComponent<GratingLevel>().levelWon)
				{
					//Switch camera and scene from grating to game over intro
					gameOverTransition.SetActive(true);
					overlayInformation.SetActive (false);
					this.GetComponent<CameraController>().CameraStart(gratingLevel.transform.Find("Main Camera").GetComponent<Camera>(),
						gameOverTransition.transform.Find("Main Camera").GetComponent<Camera>());
					currentLevel = levels.gameOverTransition;
					gameWon = true;
					gameOver = true;
					gratingLevel.SetActive(false);
					generator.GetComponent<Generator> ().clearMouse ();
					generator.GetComponent<Generator> ().clearFood ();
					playerOne.GetComponent<Player1> ().score += 10;
					playerTwo.GetComponent<Player2> ().score += 10;
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
					if (levelSoundEffects.isPlaying) {
						levelSoundEffects.Stop ();
					}
					levelSoundEffects.clip = gameOverSound;
					levelSoundEffects.Play ();
					switchingLevels = false;
				}
            }
        }
		if (currentLevel == levels.gameOverTransition) {
			// Go through the thing, then move to game over level
			if(Input.GetKeyUp(KeyCode.Joystick1Button2) || Input.GetKeyUp(KeyCode.Joystick2Button2)){
				if (gameOverTransition.GetComponentInChildren<finish> ().inputState < 1) {
					gameOverTransition.GetComponentInChildren<finish> ().inputState+=1;
					waitTime = 0f;
				}
				if (gameOverTransition.GetComponentInChildren<finish> ().inputState >= 2) {
					gameOverLevel.SetActive (true);
					this.GetComponent<CameraController> ().CameraStart (gameOverTransition.transform.Find("Main Camera").GetComponent<Camera>(), 
						gameOverLevel.transform.Find("Main Camera").GetComponent<Camera>());
					currentLevel = levels.gameOver;
					gameOverTransition.SetActive (false);
					overlayInformation.SetActive (true);
					if (bgMusic.isPlaying) {
						bgMusic.Stop ();
					}
					switchingLevels = true;
				}
			}
		}
		if (currentLevel == levels.gameOver) {
			plane.SetActive (false);
			// Detect if lost game
			if (!gameWon) {
				// display F animation
				F.SetActive (true);
				randomFood.SetActive (false);
			} else {
				if (waitTime <= 0f) {
					if (!bgMusic.isPlaying) {
						bgMusic.volume = 0.8f;
						bgMusic.clip = victory;
						bgMusic.Play ();
						waitTime = 30f;
					}
				}
				waitTime -= Time.deltaTime;
				randomFood.SetActive (true);
				float playerScores = playerOne.GetComponent<Player1> ().score + playerTwo.GetComponent<Player2> ().score;
				if (playerScores >= 144) {
					A.SetActive (true);
				}
				// B: kill 4 enemies per level
				else if (playerScores >= 108){
					B.SetActive (true);	
				}
				// C: kill 2 enemies per level
				else if (playerScores >= 84){
					C.SetActive (true);
				}
				// D: kill 1 enemy per level
				else{
					D.SetActive (true);
				}
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
			if (audioPlayer.isPlaying) {
				audioPlayer.Stop ();
			}
			audioPlayer.volume = 0.7f;
			audioPlayer.clip = plateCollectSound;
			audioPlayer.Play ();
		}
	}

	// tell level when to activate
	public bool levelOn(){
		if (currentLevel != levels.titleScreen && currentLevel!= levels.levelCuttingTransition &&
			currentLevel != levels.levelSauteingTransition &&
			currentLevel != levels.levelGratingTransition &&
			currentLevel != levels.gameOverTransition &&
			currentLevel != levels.gameOver) {
			return true;
		}
		return false;
	}

	public float getLevelPercent(){
		if (currentLevel == levels.cutting) {
			return ((float)cuttingLevel.GetComponent<CuttingLevel> ().foodCollected /
				(float)cuttingLevel.GetComponent<CuttingLevel> ().maxFood);
		}
		if (currentLevel == levels.sauteing) {
			return ((float)sauteingLevel.GetComponent<SauteingLevel> ().currentTime /
				(float)sauteingLevel.GetComponent<SauteingLevel> ().levelTimer);
		}
		if (currentLevel == levels.grating) {
			return ((float)gratingLevel.GetComponent<GratingLevel> ().foodCollected / 
				(float)gratingLevel.GetComponent<GratingLevel> ().maxFood);
		}
		return 0;

	}

	public float getLevelContaminatedPercent(){
		if (currentLevel == levels.cutting) {
			return ((float)cuttingLevel.GetComponent<CuttingLevel> ().foodStolen /
				(float)cuttingLevel.GetComponent<CuttingLevel> ().maxFood);
		}
		if (currentLevel == levels.sauteing) {
			return ((float)sauteingLevel.GetComponent<SauteingLevel> ().foodStolen /
				(float)sauteingLevel.GetComponent<SauteingLevel> ().maxFood);
		}
		if (currentLevel == levels.grating) {
			return ((float)gratingLevel.GetComponent<GratingLevel> ().foodStolen / 
				(float)gratingLevel.GetComponent<GratingLevel> ().maxFood);
		}
		return 0f;
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
