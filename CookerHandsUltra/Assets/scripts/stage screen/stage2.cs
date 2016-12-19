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
            introText = "Stage 2: The Sautee\n\nPHEW that was a close one!\nThe health inspector is still here in the resturant waiting on his order. YIKES!\n\nKeeping the chopped food on a single plate, you decide to start to saute the ingredients.\nBut lingering on this side of the kitchen are hungry flies swooping in and nabbing your ingredients.\nYou continue to keep venturing into this madness.\n\n ";
            introText += "Press X to Continue.";
        }
        GetComponent<Text>().text = introText;
    }
}
