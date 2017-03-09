using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour {

	GameObject cam;
	LineRenderer lr;

	void Start () {
		cam = transform.GetChild (0).gameObject;
		lr = cam.GetComponent<LineRenderer> ();
		lr.enabled = false;
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			StartCoroutine (Shoot ());
		}
	}

	IEnumerator Shoot () {
		lr.enabled = true;
		yield return new WaitForSeconds (0.01f);
		RaycastBullet ();
		lr.enabled = false;
	}

	void RaycastBullet () {
		Ray ray = cam.GetComponent<Camera> ().ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit)) {
			Health health = hit.transform.GetComponent<Health> ();
			if (health != null) {
				health.UpdateHealth (-1);
			}
		}
	}
}
