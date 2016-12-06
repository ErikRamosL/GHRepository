using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerMovement : MonoBehaviour {

	public Vector3 FallChp;
	public bool onGround;
	public float jumpHeight;

	private Rigidbody rb;

	void Start () {

		rb = GetComponent<Rigidbody>();

	}

	void Update () {
		Jump();
		CallBack();

	}

	void Jump () {

		if (onGround == true && Input.GetKey(KeyCode.Space)){

			rb.velocity(rb.velocity.x, jumpHeight, rb.velocity.z);

		}
	
	}

	void OnCollisionEnter (Collision col) {

		if (col.collide.CompareTag("floor")){

			onGround = true;

		} 

	}

	void OnCollisionExit (Collision col) {

		if (col.collide.CompareTag("floor")){
		onGround = false;
		}

	}

	void CallBack () {

		if (onGround == true) {

			FallChp = transform.position;

		}

		if (onGround == false && transform.position.y < 3f) {

			transform.position = FallChp;

		}

	}
}