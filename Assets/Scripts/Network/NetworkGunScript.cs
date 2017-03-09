using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class NetworkGunScript : NetworkBehaviour {

	GameObject cam;
	LineRenderer lr;

	void Start () {
		cam = transform.GetChild (0).gameObject;
		lr = cam.GetComponent<LineRenderer> ();
		lr.enabled = false;
	}

	[Command]
	public void CmdShoot () {
		StartCoroutine (wait ());
		RaycastBullet ();
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

	IEnumerator wait () {
		lr.enabled = true;
		yield return new WaitForSeconds (0.1f);
		lr.enabled = false;
	}
}
