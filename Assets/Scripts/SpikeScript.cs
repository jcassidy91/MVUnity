using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour {

	public float dps = 10;

	void Start () {
		
	}


	void Update () {
		
	}

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "Player") {
			var health = col.gameObject.GetComponent<Health> ();
			health.UpdateHealth (-dps);
		}

	}
}
