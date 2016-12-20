using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class playerController : NetworkBehaviour {

	Rigidbody rb;
	float speed = 10f;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
			return;
		}

		GetComponent<Renderer> ().material.color = Color.red;

		float hor = Input.GetAxisRaw ("Horizontal");
		float ver = Input.GetAxisRaw ("Vertical");

		Vector3 movement = new Vector3 (hor, 0, ver);

		//rb.MovePosition (transform.position + movement.normalized * speed * Time.deltaTime);
		transform.Translate (movement.normalized * speed * Time.deltaTime);
	}
}
