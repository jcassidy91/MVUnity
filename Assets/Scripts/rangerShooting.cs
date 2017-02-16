using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangerShooting : MonoBehaviour {

	public GameObject cross;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		cross.transform.position = new Vector3 (0f, 0f, 0f);
	}
}
