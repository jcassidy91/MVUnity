using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class playerController : NetworkBehaviour {

	Rigidbody rb;
	float speed = 10f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
            GetComponent<Renderer>().material.color = Color.blue;
            return;
		}

		GetComponent<Renderer> ().material.color = Color.red;

		float hor = Input.GetAxisRaw ("Horizontal");
		float ver = Input.GetAxisRaw ("Vertical");
        float rot = Input.GetAxis("Mouse X");

		Vector3 movement = new Vector3 (hor, 0, ver);

		transform.Translate (movement.normalized * speed * Time.deltaTime);
        transform.Rotate(Vector3.up, rot * 100f * Time.deltaTime, Space.Self);
	}
}