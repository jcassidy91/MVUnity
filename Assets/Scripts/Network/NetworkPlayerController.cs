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
	GameObject cam, hud, lowHealthOverlay;
	Vector2 sensitivity = new Vector2(1, 1);
	Vector2 smoothing = new Vector2(3, 3);
	Vector2 clampInDegrees = new Vector2(360, 180);
	Vector2 targetDirection, targetCharacterDirection, _mouseAbsolute, _smoothMouse;

	Vector3 spawnpoint;
	NetworkHealth health;
	Slider healthBar;
	Text healthText;

	bool clientraycast;
	bool serverraycast;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		cam = transform.Find("Main Camera").gameObject;
		targetDirection = cam.transform.localRotation.eulerAngles;
		targetCharacterDirection = transform.localRotation.eulerAngles;
		lr = cam.GetComponent<LineRenderer> ();
		health = GetComponent<NetworkHealth> ();
		spawnpoint = cam.transform.position;
		isShooting = false;
		hud = transform.Find ("HUD").gameObject;
		healthBar = hud.transform.FindChild ("PlayerHealthBar").GetComponent<Slider> ();
		healthText = healthBar.transform.gameObject.transform.FindChild ("HealthText").GetComponent<Text> ();
		lowHealthOverlay = hud.transform.FindChild ("LowHealthOverlay").gameObject;
		lowHealthOverlay.SetActive (false);
	}

	void Update() {
		if (!isLocalPlayer) {
			return;
		}
		ManageHealth ();
		Look ();
		Move ();
		Jump ();
		Shoot ();

		Debug.Log ((serverraycast == clientraycast).ToString());
	}

	[Command]
	public void CmdSayYup () {
		Debug.Log("Yup");
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
		if (health.GetHealth() == 0) {
			Respawn ();
		}
		SetHealthOverlay(health.GetHealth() < health.GetMaxHealth() / 10);
		//health.UpdateHealthSlider ();
	}

	void Look () {
		Screen.lockCursor = lockCursor;
		var targetOrientation = Quaternion.Euler(targetDirection);
		var targetCharacterOrientation = Quaternion.Euler(targetCharacterDirection);
		var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
		mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));
		_smoothMouse.x = Mathf.Lerp(_smoothMouse.x, mouseDelta.x, 1f / smoothing.x);
		_smoothMouse.y = Mathf.Lerp(_smoothMouse.y, mouseDelta.y, 1f / smoothing.y);
		_mouseAbsolute += _smoothMouse;
		if (clampInDegrees.x < 360) _mouseAbsolute.x = Mathf.Clamp(_mouseAbsolute.x, -clampInDegrees.x * 0.5f, clampInDegrees.x * 0.5f);
		var xRotation = Quaternion.AngleAxis(-_mouseAbsolute.y, targetOrientation * Vector3.right);
		cam.transform.localRotation = xRotation;
		if (clampInDegrees.y < 360) _mouseAbsolute.y = Mathf.Clamp(_mouseAbsolute.y, -clampInDegrees.y * 0.5f, clampInDegrees.y * 0.5f);
		cam.transform.localRotation *= targetOrientation;
		var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, transform.up);
		transform.localRotation = yRotation;
		transform.localRotation *= targetCharacterOrientation;
		if (cam.transform.position.y < -100) health.UpdateHealth (-10);

		clientraycast = Physics.Raycast (new Ray (cam.transform.position, cam.transform.forward));
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
			Ray ray = new Ray(transform.position, transform.forward);
			lr.SetPosition (0, this.transform.position);
			lr.SetPosition (1, this.transform.position + this.transform.forward * 1000f);
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

	void Respawn () {
		transform.position = spawnpoint;
		health.SetHealth (1000);
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
		//Ray ray = cam.GetComponent<Camera> ().ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		Debug.Log (ray);
		Debug.DrawRay (ray.origin, ray.direction, Color.green);
		Debug.Log (Physics.Raycast (ray, out hit));
		if (Physics.Raycast (ray, out hit)) {
			NetworkHealth health = hit.transform.GetComponent<NetworkHealth> ();
			Debug.Log(hit.transform.name);
			if (health != null) {
				Debug.Log ("update health");
				health.UpdateHealth (-1);
			}
		}
	}

	bool isGrounded() {
		RaycastHit[] below = Physics.BoxCastAll(transform.position, new Vector3(0.5f,0.5f,0.5f), Vector3.down, GetComponent<Transform>().rotation, 0.05f);
		return Array.Exists (below, e => !e.transform.GetComponent<Collider>().name.Contains("Player"));
	}

	void SetHealthOverlay(bool on) {
		//lowHealthOverlay.SetActive (on);
	}

}