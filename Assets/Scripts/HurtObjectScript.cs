using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtObjectScript : MonoBehaviour {

	Health health;

	void Start () {
		health = GameObject.Find ("Player").GetComponent<Health>();
	}

	void Update () {
		
	}

	void OnCollisionEnter(Collision col) {
		if (col.collider.gameObject.tag == "Player") {
			health.UpdateHealth (-0.5f);
		}
	}
}
