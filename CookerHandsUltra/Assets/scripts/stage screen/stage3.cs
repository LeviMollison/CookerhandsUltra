using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class stage3 : MonoBehaviour {
    public int inputState;
    public string myText;
    // Use this for initialization
    void Start () {
        myText = "We ran our scene.";
        inputState = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (inputState == 0)
        {
            myText = "Stage 3: The Cheese Grating\n\nYou become impressed on the progress you have made; however, " +
            	"the health inspector is growing more impatient and demands the food to be done soon.\n" +
            	"You prepare your cheese until you see rodents crawling around the area.\n" +
            	"You roll up your sleeves and decide to teach the rats who's the real boss in this kitchen.\n\n ";
            myText += "Press O to Continue.";
        }
		if (inputState == 1) {
			myText = "Directions:\n";
			myText += "Hold X to Pick Up a Pan or Grater \n";
			myText += "Move the Grater along the cheese to create cheese chunks\n";
			myText += "Fill the white circle with more cheese than the rats can take!";
		}
        GetComponent<Text>().text = myText;
    }
}
