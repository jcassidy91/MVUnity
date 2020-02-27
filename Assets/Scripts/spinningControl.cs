using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinningControl : MonoBehaviour {

	public GameObject player;
	public GameObject pointLight;
	public GameObject button;
	public GameObject box;
	public Quaternion rotatePosition;
	// Use this for initialization
	void Start () {
		rotatePosition = player.transform.rotation;
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseOver() {
		player.transform.Rotate (Vector3.up * (180 * Time.deltaTime));
		pointLight.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.962416f, -4.79f);
		pointLight.GetComponent<Light> ().intensity = 8;
	}

	void OnMouseExit() {
		pointLight.GetComponent<Light> ().intensity = 0;
		player.transform.rotation = rotatePosition;
	}

	void OnMouseDown() {
		button.SetActive (true);
		box.SetActive (true);

		if (player.tag == "mageClassUI") {
			box.transform.position = new Vector3 (-36f, 20f, 82.83249f);
		} else if (player.tag == "rangerClassUI") {
			box.transform.position = new Vector3 (20f, 20f, 82.83249f);
		} else if (player.tag == "warriorClassUI") {
			box.transform.position = new Vector3 (-36f, -32f, 82.83249f);
		} else if (player.tag == "engineerClassUI") {
			box.transform.position = new Vector3 (20f, -32f, 82.83249f);
		};
	}
}
