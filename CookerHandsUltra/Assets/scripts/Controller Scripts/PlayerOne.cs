using UnityEngine;
using System.Collections;

public class PlayerOne : MonoBehaviour {

	public float speed = 3.0F;
	public float jumpSpeed = 8.0F;
	private Vector3 moveDirection = Vector3.zero;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

		CharacterController controller = GetComponent<CharacterController>();

		moveDirection = new Vector3(Input.GetAxis("PlayerOneX"), Input.GetAxis("PlayerOneY"), 0.0f);
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;
		controller.Move(moveDirection * Time.deltaTime);


//		controller.Move(moveDirection * Time.deltaTime);
	}
}
