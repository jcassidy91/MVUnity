using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangerInvisibility : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (1)) {
			player.GetComponent<Renderer> ().material.SetFloat ("_Mode", 2);
		}
	}
}
