using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangerInvisibility : MonoBehaviour {

	public GameObject player;
	byte red = 255;
	byte blue = 54;
	byte green = 54;
	float invisTimer = 30; // Change the # to change the cooldown length
	IEnumerator co;
	bool stopped;

	// Use this for initialization
	void Start () {
		co = Time ();
		StartCoroutine (co);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (1) && invisTimer == 0) {
			StartCoroutine (Invis ());
		}
		if (invisTimer == 0) {
			Debug.Log ("Ready"); // Can replace with something
			StopCoroutine(co);
		}

	}

	IEnumerator Invis() {
		player.GetComponent<Renderer> ().material.color = new Color32 (red, green, blue, 15);
		yield return new WaitForSeconds (6); // Change the # in () to change how long invis lasts
		player.GetComponent<Renderer> ().material.color = new Color32 (red, green, blue, 255);

		invisTimer = 30; // Change the # to change the cooldown length
		StartCoroutine (co);
		Debug.Log ("Not Ready"); // Can replace with something
	}

//	IEnumerator Time() {
//
//		Debug.Log ("Not Ready"); // Can replace with something
//
//		while (invisTimer > 0) {
//			Debug.Log (invisTimer);
//			invisTimer -= 1;
//			yield return new WaitForSeconds (1);
//		}
//	}

	IEnumerator TimerTimer() {
		Debug.Log ("Not Ready");

		invisTimer = 30;
		stopped = false;
		do {
			yield return new WaitForSeconds(1);
			invisTimer -= 1;
			while(!stopped && invisTimer > 0) {

			}
		}
	}

}
