using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class finish : MonoBehaviour {
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
            myText = "It's Judgement Time\n\nYou successful cooked a dish and made some final touches to the plate.\nYou walk up to the door and you see the health inspector smile at you with his grade book right in his hand.\nYou sigh and prepare to go into the room and serve him your signature dish.\n\n";
            myText += "Press X to Continue.";
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentRoom = "livingroom";
            }
        }
        GetComponent<Text>().text = myText;
    }
}
