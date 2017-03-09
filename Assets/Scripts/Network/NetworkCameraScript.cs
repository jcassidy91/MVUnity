using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkCameraScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (!GetComponentInParent<NetworkPlayerController> ().isLocalPlayer) {
			gameObject.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
