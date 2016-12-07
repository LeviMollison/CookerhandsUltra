using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Camera[] cameras;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CameraReset()
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].enabled = false;
        }
    }

    public void CameraStart(float currLevel, float nextLevel){
        CameraReset();
        if (currLevel == 100.0f)
       {
            cameras[1].enabled = true;
       }
       else if (currLevel == 0.0f)
       {
            cameras[2].enabled = true;
       }
       else if (currLevel == -60.0f)
       {
            cameras[3].enabled = true;
       }     
    }
}
