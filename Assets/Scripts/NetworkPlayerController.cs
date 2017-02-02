using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

[AddComponentMenu("Camera/Simple Smooth Mouse Look ")]
public class NetworkPlayerController : NetworkBehaviour {
	Vector2 _mouseAbsolute;
	Vector2 _smoothMouse;

	public Vector2 clampInDegrees = new Vector2(360, 180);
	public bool lockCursor;
	public Vector2 sensitivity = new Vector2(2, 2);
	public Vector2 smoothing = new Vector2(3, 3);
	public Vector2 targetDirection;
	public Vector2 targetCharacterDirection;
	public float movementspeed;
	Rigidbody rb;
	public float jumpSpeed;
	public int maxWall;
	public Health health;

	public bool isJumping = false;

	public GameObject characterBody;
	public Vector3 spawnpoint;

	void Start() {
		rb = characterBody.GetComponent<Rigidbody> ();
		targetDirection = transform.localRotation.eulerAngles;
		targetCharacterDirection = characterBody.transform.localRotation.eulerAngles;

		health = characterBody.GetComponent<Health> ();
		spawnpoint = transform.position;
	}

	void Update() {
		
		if (!isLocalPlayer) {
			//GetComponent<Renderer>().material.color = Color.blue;
			return;
		}

		ManageHealth ();
		Look ();
		Move ();
		Jump ();
	}

	bool isGrounded() {
		RaycastHit[] below = Physics.BoxCastAll(characterBody.transform.position, new Vector3(0.5f,0.5f,0.5f), Vector3.down, characterBody.GetComponent<Transform>().rotation,0.05f);
		if (Array.Exists(below, e => e.transform.tag == "Wall")) {
			return true;
		}

		return false;
	}

	void Jump() {
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (isGrounded()) {
				rb.AddForce(Vector3.up*jumpSpeed*4);
			}
		}
	}

	void ManageHealth() {
		if (health.GetHealth() == 0) {
			Respawn ();
		}
		health.SetHealthOverlay(health.GetHealth() < health.GetMaxHealth() / 10);
	}

	void Look() {
		Screen.lockCursor = lockCursor;

		var targetOrientation = Quaternion.Euler(targetDirection);
		var targetCharacterOrientation = Quaternion.Euler(targetCharacterDirection);

		var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

		mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));

		_smoothMouse.x = Mathf.Lerp(_smoothMouse.x, mouseDelta.x, 1f / smoothing.x);
		_smoothMouse.y = Mathf.Lerp(_smoothMouse.y, mouseDelta.y, 1f / smoothing.y);

		_mouseAbsolute += _smoothMouse;

		if (clampInDegrees.x < 360)
			_mouseAbsolute.x = Mathf.Clamp(_mouseAbsolute.x, -clampInDegrees.x * 0.5f, clampInDegrees.x * 0.5f);

		var xRotation = Quaternion.AngleAxis(-_mouseAbsolute.y, targetOrientation * Vector3.right);
		transform.localRotation = xRotation;

		if (clampInDegrees.y < 360)
			_mouseAbsolute.y = Mathf.Clamp(_mouseAbsolute.y, -clampInDegrees.y * 0.5f, clampInDegrees.y * 0.5f);

		transform.localRotation *= targetOrientation;

		var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, characterBody.transform.up);
		characterBody.transform.localRotation = yRotation;
		characterBody.transform.localRotation *= targetCharacterOrientation;

		if (transform.position.y < -100) {
			health.UpdateHealth (-10);
		}
	}

	void Move() {
		if (Input.GetKey (KeyCode.W)) {
			characterBody.transform.Translate (Vector3.forward * moveLength(transform.forward));
		}
		if (Input.GetKey (KeyCode.A)) {
			characterBody.transform.Translate (Vector3.left * moveLength(-transform.right));
		}
		if (Input.GetKey(KeyCode.S)) {
			characterBody.transform.Translate (Vector3.back * moveLength(-transform.forward));
		}
		if (Input.GetKey (KeyCode.D)) {
			characterBody.transform.Translate (Vector3.right * moveLength(transform.right));
		}
	}

	float moveLength (Vector3 direction) {
		for(var j = movementspeed*Time.deltaTime; j >= 0; j-=Time.deltaTime) {
			RaycastHit[] box = Physics.BoxCastAll (transform.position, transform.localScale/2, direction, new Quaternion(0, 0, 0, 0), j);
			if (!Array.Exists(box, e => e.transform.tag == "Wall")) {
				return j;
			}
		}
		return 0;
	}

	void Respawn() {
		characterBody.transform.position = spawnpoint;
		health.SetHealth (1000);
	}

}