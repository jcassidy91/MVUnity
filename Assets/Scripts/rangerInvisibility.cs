using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangerInvisibility : MonoBehaviour {

	public GameObject player;
	private byte red = 255;
	private byte blue = 54;
	private byte green = 54;
	private float starttime = 30;	// Change the # to change the cooldown length
	private float invisTimer = 30;
	private IEnumerator coroutine;
	bool stopped;

	// Use this for initialization
	void Start () {
		coroutine = TimerTimer ();
		StartCoroutine (coroutine);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (1) && stopped == true) {
			StopAllCoroutines ();
			StartCoroutine (Invis ());
		}
		if (stopped == true) {
			Debug.Log ("Ready");
		}

	}

	IEnumerator Invis() {
		coroutine = TimerTimer ();
		player.GetComponent<Renderer> ().material.color = new Color32 (red, green, blue, 15);
		yield return new WaitForSeconds (6); // Change the # in () to change how long invis lasts
		player.GetComponent<Renderer> ().material.color = new Color32 (red, green, blue, 255);

		invisTimer = starttime; 
		stopped = false;
		StartCoroutine (coroutine);
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

		invisTimer = starttime;
		stopped = false;

		while (!stopped && invisTimer > 0) {
			yield return new WaitForSeconds (1);
			invisTimer -= 1;
			Debug.Log (invisTimer);

		} 

		if(invisTimer <= 0) {
			stopped = true;
		}

	}

}
