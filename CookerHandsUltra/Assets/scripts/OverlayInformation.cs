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
		playerOneScore = manager.playerOne.GetComponent<Player1>().score.ToString() + newLine;
		playerTwoScore = manager.playerTwo.GetComponent<Player2> ().score.ToString() + newLine;
		time = (int)manager.actualTime;
		timer = time.ToString() + newLine;
		levelPercent = (manager.getLevelPercent ()*100).ToString() + "% " +newLine;
		foodContaminated = (manager.getLevelContaminatedPercent ()*100).ToString() + "% " + newLine;
		overlay = 
			"Player 1: " + playerOneScore
		+ "Player 2: " + playerTwoScore
			+ "Time Remaining: " + timer
		+ "Level Complete: " + levelPercent
		+ "Contaminated Foods " + foodContaminated;
		GetComponent<Text> ().text = overlay;
	}
}
