using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warriorAbility : MonoBehaviour {

	public GameObject player;
	private IEnumerator coroutine;
	private float buffTimer = 5;
	private float buffCooldown = 30;
	private bool stopped;

	// Use this for initialization
	void Start () {
		coroutine = buffCooldownTimer ();
		StartCoroutine (coroutine);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (1) && stopped == true) {
			StopAllCoroutines ();
			StartCoroutine (buff ());
		}
		if (stopped == true) {
			Debug.Log ("Ready");
		}
	}

	IEnumerator buffCooldownTimer() {
		Debug.Log ("Not Ready");

		buffCooldown = 30;
		stopped = false;

		while (!stopped && buffCooldown > 0) {
			yield return new WaitForSeconds (1);
			buffCooldown -= 1;
			Debug.Log (buffCooldown);

		} 

		if(buffCooldown <= 0) {
			stopped = true;
		}
	}

	IEnumerator buff() {
		coroutine = buffCooldownTimer ();
		player.GetComponent<PlayerController>().movementspeed += 2;
		yield return new WaitForSeconds (5);
		player.GetComponent<PlayerController> ().movementspeed -= 2;

		buffCooldown = 30;
		stopped = false;
		StartCoroutine (coroutine);
	}
}
