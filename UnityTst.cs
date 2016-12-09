using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float moveSpeed;
	public float JumpHeight;

	public int JumpsDefault;
	public int Jumps;

	public bool OnGround = false;

	public string PlatName;
	public string PlatTag;
	public Text SunRotation;

	public Vector3 FallChp;
	public Vector3 PlatSize;
	public Vector3 PlatPos;
	public Vector3 FallChpCalculations;

	public Transform rot;

	public GameObject pickupP;
	public GameObject ExtraJump;

	public AudioSource PickUpSound;


	private bool spaceHold;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		
		rb = GetComponent<Rigidbody> ();
		rot = rot.GetComponent<Transform> ();
		PickUpSound = GetComponent<AudioSource> ();

	}


	// Update is called once per frame
	void FixedUpdate () {
		
		movement ();
		jump ();


	}


	void Update () {
		
		SetSRText ();
		SetJumps ();
		CallBack ();
		

	}

	//Sets how many jumps 
	void SetJumps () {
		
		if (OnGround != false && Jumps != JumpsDefault) {
			
			Jumps = JumpsDefault;

		}

	}

	// Teleports player back to nearest edge of platform when it 
	void CallBack (Collision col) {

		PlatSize = col.GetComponent<Collider> ().bounds.size;
		PlatName = col.collider.name;
		PlatPos = col.transform.position;
		// PlatTag = ;
		
		FallChpCalculations.x = transform.position.x /*- (GetComponent<Collider> ().bounds.size.x)*/;
		FallChpCalculations.y = (PlatPos.x + PlatSize.x / 2) - (GetComponent<Collider> ().bounds.size.x);

		// X boundary CallBack
		if (transform.position.y < (PlatPos.y + 10) && (transform.position.x < (PlatPos.x + PlatSize.x / 2) - (GetComponent<Collider> ().bounds.size.x) && transform.position.x > (PlatPos.x - PlatSize.x / 2) + (GetComponent<Collider> ().bounds.size.x)) ) {
				
			FallChp.x = (transform.position.x);

		} 

		// Z boundary CallBack
		if (transform.position.y < (PlatPos.y + 10) && (transform.position.z < (PlatPos.z + PlatSize.z / 2) - (GetComponent<Collider> ().bounds.size.z) && transform.position.z > (PlatPos.z - PlatSize.z / 2) + (GetComponent<Collider> ().bounds.size.z)) ) {

			FallChp.z = (transform.position.z);

		} 

		
		// CallBack Action #1
		if (transform.position.y <= PlatPos.y - 3) {
			transform.position = new Vector3 (FallChp.x, FallChp.y + 0.5f, FallChp.z);
			rb.velocity = new Vector3 (0f,0f,0f);
			transform.Rotate (0f, 0f, 0f);
		}
    
    // CallBack Action #2
    if (Input.GetKey ("f")) {

			transform.position = new Vector3 (0f,0.5f,0f);
			rb.velocity = new Vector3 (0f,0f,0f);
			transform.Rotate (0f, 0f, 0f);

		}

	}


	void movement (){ // player movement
		
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * moveSpeed);

		
	}


	void OnCollisionEnter (Collision col) { // When it collides with floor
		
		if (col.collider.CompareTag ("floor")) {
			OnGround = true;
			Jumps = JumpsDefault;
		}

	}


	void OnCollisionExit (Collision col) { // when stops colliding with floor

		if (col.collider.CompareTag ("floor")) {
			
			OnGround = false;
		}
	}


	void jump (){ // jump action.
		
		if (Input.GetKey (KeyCode.Space) && spaceHold == false && (OnGround == true || Jumps > 0)) {
			
			rb.velocity = new Vector3 (rb.velocity.x,JumpHeight,rb.velocity.z);
			Jumps -= 1;

			if (OnGround == false) {
				Instantiate (ExtraJump, new Vector3 (transform.position.x, transform.position.y - (GetComponent<Collider>().bounds.size.y/2), transform.position.z), Quaternion.identity); // Make particle appear
			}
		}

		if (Input.GetKey (KeyCode.Space)) {
			spaceHold = true;

		} else {
			spaceHold = false;
		}

	}


	void SetSRText () {
		SunRotation.text = "Sun Rotation: " + rot.localRotation.eulerAngles;
	}


	void OnTriggerEnter (Collider trig) {
		
		if (trig.gameObject.CompareTag ("pickup")) {

			Instantiate (pickupP, transform.position, Quaternion.identity); // Make particle appear
			trig.gameObject.SetActive (false); // Set trigger to not active
			PickUpSound.Play (); // Play Sound
      
		}

	}

}
