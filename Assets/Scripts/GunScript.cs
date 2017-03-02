using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour {

	GameObject player;

	void Start () {
		player = gameObject;
	}

	void Update () {
		Shoot ();
	}

	void Shoot() {

		if (Input.GetKey (KeyCode.Mouse0)) {


			var pos = player.transform.position;
			var rot = new Vector3(player.transform.eulerAngles.x, GameObject.Find("Main Camera").transform.eulerAngles.y, player.transform.eulerAngles.z);
			Debug.DrawRay (pos, rot, Color.blue, 2, true);
		}

	}
}
