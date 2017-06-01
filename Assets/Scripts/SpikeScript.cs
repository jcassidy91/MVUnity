using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour {

	public float dps = 10;
	public bool collided = false;
	public GameObject victim;

	void Update () {
		if (collided) {
			Health health = victim.GetComponent<Health> ();
			health.UpdateHealth (-dps);
		}
	}

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "Player") {
			collided = true;
			victim = col.gameObject;
		}
	}

	void OnCollisionExit (Collision col) {
		collided = false;
	}
}
