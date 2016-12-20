using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class stage1 : MonoBehaviour {
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
            introText = "Resturant: Droite gauche \n\nMission: This pest infested C-rated restaurant needs your help to save the place from shutting down. We need your magic hands to keep the restaurant's food pest-free and keep the health inspector happy.\nPress O to Continue";

        }
        else if (inputState == 1)
        {



            introText = "Directions:\n\n";
            introText += "Hold X to Pick Up the Knife and Use the Left Hand Joy Stick to Move Hand. \n\n";
            introText += "Hold Square to Swat and Kill Pest Away From the Food\n\n";
            introText += "Press O to Continue.";

        }


        GetComponent<Text>().text = introText;
    }
}
