using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CameraStart(Camera currCamera, Camera nextCamera){
        currCamera.enabled = false;
        nextCamera.enabled = true;
    }
}
