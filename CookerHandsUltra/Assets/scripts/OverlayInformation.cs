using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OverlayInformation : MonoBehaviour {

	public string overlay;
	public string playerOneScore;
	public string playerTwoScore;
	public int time;
	public string timer;
	public string levelPercent;
	public string foodContaminated;
	public string newLine;
	public GameObject man;
	public GameManager manager;
	// Use this for initialization
	void Start () {
		manager = man.GetComponent<GameManager> ();
		newLine = "\n";
	}
	
	// Update is called once per frame
	void Update () {
		// update
		// Level 1, cutting
		if (manager.getLevel() == 100.0f){
			playerOneScore = manager.playerOne.GetComponent<Player1>().score.ToString() + newLine;
			playerTwoScore = manager.playerTwo.GetComponent<Player2> ().score.ToString() + newLine;
			time = (int)manager.actualTime;
			timer = time.ToString() + newLine;
			levelPercent = (manager.getLevelPercent ()*100).ToString() + "% " +newLine;
			foodContaminated = (manager.getLevelContaminatedPercent ()*100).ToString() + "% " + newLine;
			overlay = 
				"Player 1 Score: " + playerOneScore
				+ "Player 2 Score: " + playerTwoScore
				+ "Time Remaining: " + timer
				+ "Level Complete: " + levelPercent
				+ "Contaminated Foods " + foodContaminated;
		}
		// Level 2: Sauteing
		if (manager.getLevel() == 0.0f){
			playerOneScore = manager.playerOne.GetComponent<Player1>().score.ToString() + newLine;
			playerTwoScore = manager.playerTwo.GetComponent<Player2> ().score.ToString() + newLine;
			// Level time remaining
			string levelTimeRemaining;
			levelTimeRemaining = ((int)manager.sauteingLevel.GetComponent<SauteingLevel> ().levelTimer).ToString() + newLine;
			foodContaminated = (manager.getLevelContaminatedPercent ()*100).ToString() + "% " + newLine;

			overlay = 
				"Player 1 Score: " + playerOneScore
				+ "Player 2 Score: " + playerTwoScore
				+ "Time till level won: " + levelTimeRemaining
				+ "Contaminated Foods " + foodContaminated;
		}
		// Level 3: Grating
		if (manager.getLevel() == -60.0f){
			playerOneScore = manager.playerOne.GetComponent<Player1>().score.ToString() + newLine;
			playerTwoScore = manager.playerTwo.GetComponent<Player2> ().score.ToString() + newLine;
			// circle size
			string circleSize;
			circleSize = manager.gratingLevel.GetComponent<GratingLevel> ().circleSize.ToString();
			// size needed
			string circleSizeNeeded;
			circleSizeNeeded = manager.gratingLevel.GetComponent<GratingLevel> ().maxFood.ToString() +newLine;
			time = (int)manager.actualTime;
			timer = time.ToString() + newLine;

			overlay = 
				"Player 1 Score: " + playerOneScore
			+ "Player 2 Scor: " + playerTwoScore
			+ "Cheese Generated: " + circleSize + "/ " + circleSizeNeeded
			+ "Time Remaining: " + timer;
		}
		// end game screen
		if (manager.gameOver){
			playerOneScore = manager.playerOne.GetComponent<Player1>().score.ToString() + newLine;
			playerTwoScore = manager.playerTwo.GetComponent<Player2> ().score.ToString() + newLine;

			overlay = 
				"Player 1 Score: " + playerOneScore
			+ "Player 2 Score: " + playerTwoScore
				+ newLine + newLine 
				+"Press Options \nto Play Again";
		}
		GetComponent<Text> ().text = overlay;
	}
}
