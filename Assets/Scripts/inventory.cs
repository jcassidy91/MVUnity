using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour {

	class invObj {
		string name;
		int amount;

		public invObj (string n, int q) {
			name = n;
			amount = q;
		}
	}

	List<invObj> inv;

	public void AddToList (string n, int q) {
		inv.Add(new invObj(n,q));
	}

	void Start () {
		inv = new List<invObj> ();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.A)) {
		}
	}
}