using UnityEngine;
using System.Runtime.InteropServices;
using System;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkPlayerController : NetworkBehaviour {

	public bool lockCursor = true;
	public float movementspeed = 10;
	public float jumpSpeed = 100;
	public float bulletTime = 1000;

	bool isShooting;
	float lastTick, millisecond;

	LineRenderer lr;
	Rigidbody rb;
	GameObject cam;

	Vector3 spawnpoint;
	NetworkHealth health;
	Slider healthBar;
	Text healthText;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		cam = Camera.main.gameObject;
		lr = cam.GetComponent<LineRenderer> ();
		health = GetComponent<NetworkHealth> ();
		spawnpoint = cam.transform.position;
		isShooting = false;
	}

	void Update() {
		if (!isLocalPlayer) {
			return;
		}
		ManageHealth ();
		Move ();
		Jump ();
		Shoot ();
	}

	// FUNCTIONS

	void Jump () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (isGrounded()) {
				rb.AddForce(Vector3.up*jumpSpeed*4);
			}
		}
	}

	void ManageHealth () {
		if (health.GetHealth() <= 0) {
			CmdRespawn ();
		}
		if (cam.transform.position.y < -100) health.UpdateHealth (-10);
	}

	void Move () {
		if (Input.GetKey (KeyCode.W)) {
			transform.Translate (Vector3.forward * moveLength(cam.transform.forward));
		}
		if (Input.GetKey (KeyCode.A)) {
			transform.Translate (Vector3.left * moveLength(-cam.transform.right));
		}
		if (Input.GetKey(KeyCode.S)) {
			transform.Translate (Vector3.back * moveLength(-cam.transform.forward));
		}
		if (Input.GetKey (KeyCode.D)) {
			transform.Translate (Vector3.right * moveLength(cam.transform.right));
		}
	}

	void Shoot () {
		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			Debug.Log ("Shoot");
			Ray ray = new Ray(transform.position, transform.forward);
			lr.SetPosition (0, transform.position);
			lr.SetPosition (1, transform.position + ray.direction.normalized * 1000f);
			CmdShootRay (ray);
			isShooting = true;
		}
		lr.enabled = isShooting;
		if (isShooting) millisecond++;
		if (millisecond > bulletTime && isShooting) {
			isShooting = false;
			millisecond = 0;
		}
	}

	// UTILITIES
	[Command]
	void CmdRespawn () {
		Debug.Log ("Dead!");
		transform.position = spawnpoint;
		health.SetHealth (100);
	}

	float moveLength (Vector3 direction) {
		for(var j = movementspeed*Time.deltaTime; j >= 0; j-=Time.deltaTime) {
			RaycastHit[] box = Physics.BoxCastAll (cam.transform.position, cam.transform.localScale/2, direction, new Quaternion(0, 0, 0, 0), j);
			if (!Array.Exists(box, e => !e.transform.GetComponent<Collider>().name.Contains("Player"))) {
				return j;
			}
		}
		return 0;
	}

	[Command]
	public void CmdShootRay(Ray ray) {
		RaycastHit hit;
		Debug.Log ("Shoot Ray");
		if (Physics.Raycast (ray, out hit)) {
			Debug.Log ("Ray Cast");
			NetworkHealth hitHealth = hit.transform.GetComponent<NetworkHealth> ();
			if (hitHealth == health) return;
			if (hitHealth != null) {
				Debug.Log ("Updating Health");
				hitHealth.UpdateHealth (-10);
			}
		}
	}

	bool isGrounded() {
		RaycastHit[] below = Physics.BoxCastAll(transform.position, new Vector3(0.5f,0.5f,0.5f), Vector3.down, GetComponent<Transform>().rotation, 0.05f);
		return Array.Exists (below, e => !e.transform.GetComponent<Collider>().name.Contains("Player"));
	}

}