using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowClicked : MonoBehaviour {

	public GameObject box;
	public string characterClass;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnMouseDown() {
		if (box.transform.position == new Vector3(-36f, 20f, 82.83249f)) {
			characterClass = "Mage";
		} else if (box.transform.position == new Vector3(20f, 20f, 82.83249f)) {
			characterClass = "Ranger";
		} else if (box.transform.position == new Vector3(-36f, -32f, 82.83249f)) {
			characterClass = "Warrior";
		} else if (box.transform.position == new Vector3(20f, -32f, 82.83249f)) {
			characterClass = "Engineer";
		}

		Debug.Log (characterClass);
		Application.LoadLevel ("TestingInvisibility");
	}
}
