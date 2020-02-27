using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class offlinePlayerC : MonoBehaviour {


	void Start () {
		
	}

	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Collectible") {
			this.GetComponent<inventory> ().AddToList ("Rock", 1);
			Destroy (other.gameObject);

		}
	}
}