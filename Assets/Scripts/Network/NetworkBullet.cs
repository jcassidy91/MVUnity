using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkBullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision) {
		Destroy(gameObject);
		var hit = collision.gameObject;
		var health = hit.GetComponent<NetworkHealth>();
		if (health != null) {
			health.UpdateHealth (-10);
		}
	}
}
