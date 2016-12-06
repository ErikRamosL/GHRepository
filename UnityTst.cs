
public Vector3 FallChp;
public bool onGround;
public float jumpHeight;

private Rigidbody rb;

Void Start () {

	rb = 	GetComponent<Rigidbody>();

}

Void Update () {
	Jump();
	CallBack();

}

Void Jump () {

	if (onGround == true && Input.GetKey(KeyCode.Space)){

		rb.velocity(rb.velocity.x, jumpHeight, rb.velocity.z);

	}
	
}

Void OnCollisionEnter (Collision col) {

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