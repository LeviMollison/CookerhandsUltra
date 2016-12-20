using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class stage2 : MonoBehaviour {
    public int inputState;
    public string introText;

    // Use this for initialization
    void Start () {
        introText = "We ran our scene.";
        inputState = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (inputState == 0)
        {
            introText = "Stage 2: The Sautee\n\nPHEW that was a close one!\n" +
            	"The health inspector is still here in the resturant waiting on his order. YIKES!\n\n" +
            	"Keeping the chopped food on a single plate, you decide to start to saute the ingredients.\n" +
            	"But lingering on this side of the kitchen are hungry flies swooping in and nabbing your ingredients.\n" +
            	"You continue to keep venturing into this madness.\n\n ";
            introText += "Press O to Continue.";
        }
		if (inputState == 1) {
			introText = "Directions:\n";
			introText += "Hold X to Pick Up a Pan and Use the Left Hand Joy Stick to Move Hand. \n";
			introText += "Keep both pans in movement to prevent food from burning\n";
			introText += "Keep the flies from stealing your leftover ingredients also!\n";
			introText += "Press O to Continue.";
		}
        GetComponent<Text>().text = introText;
    }
}
