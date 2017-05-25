using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkCameraScript : NetworkBehaviour {

	public bool lockCursor = true;
	Vector2 sensitivity = new Vector2(1, 1);
	Vector2 smoothing = new Vector2(3, 3);
	Vector2 clampInDegrees = new Vector2(360, 180);
	Vector2 targetDirection, targetCharacterDirection, _mouseAbsolute, _smoothMouse;
	GameObject player;

	// Use this for initialization
	void Start () {
		if (!GetComponentInParent<NetworkPlayerController> ().isLocalPlayer) {
			gameObject.SetActive (false);
		}

		player = transform.parent.gameObject;
		targetDirection = transform.localRotation.eulerAngles;
		targetCharacterDirection = player.transform.localRotation.eulerAngles;

	}
	
	// Update is called once per frame
	void Update () {
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
		transform.localRotation = xRotation;
		if (clampInDegrees.y < 360) _mouseAbsolute.y = Mathf.Clamp(_mouseAbsolute.y, -clampInDegrees.y * 0.5f, clampInDegrees.y * 0.5f);
		transform.localRotation *= targetOrientation;
		var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, player.transform.up);
		player.transform.localRotation = yRotation;
		player.transform.localRotation *= targetCharacterOrientation;

	}
}
