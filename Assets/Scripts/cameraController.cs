using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class cameraController : NetworkBehaviour {

	// Use this for initialization
	void Start () {
	    if (!GetComponentInParent<playerController>().isLocalPlayer)
        {
            this.gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
