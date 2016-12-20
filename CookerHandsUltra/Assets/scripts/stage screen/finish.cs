using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class finish : MonoBehaviour {
    public int inputState;
    public string introText;
	public GameObject finalSceneAnimated;
	public GameObject finalScene;

	public float animtime;

    // Use this for initialization
    void Start () {
        introText = "We ran our scene.";
        inputState = 0;
		animtime = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        if (inputState == 0)
        {
            introText = "It's Judgement Time\n\nYou successful cooked a dish and made some final touches to the plate.\n" +
            	"You walk up to the door and you see the health inspector smile at you with his grade book right in his hand." +
            	"\nYou sigh and prepare to go into the room and serve him your signature dish.\n\n";
            introText += "Press O to Continue.";
       
        }
		if (inputState == 1) {
			// run door opening animation for 2 seconds, then trasition
			finalScene.SetActive(false);
			finalSceneAnimated.SetActive (true);
			animtime += Time.deltaTime;
			if (animtime >= 1f) {
				inputState += 1;
				introText = "Press O to Continue.";
			}
		}
        GetComponent<Text>().text = introText;
    }
}
