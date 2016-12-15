using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class stage1 : MonoBehaviour {
    public string currentRoom;
    public string myText;

  

    // Use this for initialization
    void Start () {
        myText = "We ran our scene.";
        currentRoom = "title";
    }
	
	// Update is called once per frame
	void Update () {
        if (currentRoom == "title")
        {
            myText = "Resturant: Droite gauche \n\nMission: This pest infested C-rated restaurant needs your help to save the place from shutting down. We need your magic hands to keep the restaurant's food pest-free and keep the health inspector happy.\n\nPress X to Continue";

            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentRoom = "livingroom";
            }
        }
        else if (currentRoom == "livingroom")
        {



            myText = "Directions:\n\n";
            myText += "Hold X to Pick Up the Knife and Use the Left Hand Joy Stick to Move Hand. \n\n";
            myText += "Hold Square to Swat and Kill Pest Away From the Food\n\n";
            myText += "Press X to Continue.";

            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentRoom = "entry";
            }

        }

        GetComponent<Text>().text = myText;
    }
}
