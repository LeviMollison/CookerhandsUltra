using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseGenerator : MonoBehaviour {

    public MouseMove mouse;
    public List<MouseMove> mice;

    int mouseCount = 0;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (mouseCount < 5)
        {
            Vector3 position = new Vector3(9f, -2.4f, 0);
            MouseMove item = (MouseMove)Instantiate(mouse, position, transform.rotation);
            mice.Add(item);
            mouseCount++;
        }
    }
}
