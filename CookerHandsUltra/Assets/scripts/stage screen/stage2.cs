using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class stage2 : MonoBehaviour {
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
            myText = "Stage 2: The Sautee\n\nPHEW that was a close one!\nThe health inspector is still here in the resturant waiting on his order. YIKES!\n\nKeeping the chopped food on a single plate, you decide to start to saute the ingredients.\nBut lingering on this side of the kitchen are hungry flies swooping in and nabbing your ingredients.\nYou continue to keep venturing into this madness.\n\n ";
            myText += "Press X to Continue.";
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentRoom = "livingroom";
            }
        }
        GetComponent<Text>().text = myText;
    }
}
