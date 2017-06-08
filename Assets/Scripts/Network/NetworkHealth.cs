﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkHealth : NetworkBehaviour {
	public RectTransform healthBar;
	public GameObject healthCanvas;

	public const float maxHealth = 100;
	[SyncVar (hook = "OnUpdateHealth")]
	public float currentHealth = maxHealth;

	void Start () {
		//healthCanvas = transform.Find ("Healthbar Canvas").gameObject;
		healthBar = healthCanvas.transform.FindChild ("HealthBackground").FindChild ("HealthForeground").GetComponent<RectTransform> ();
		if (!isLocalPlayer) healthCanvas.SetActive (false);
	}

	void Update() {
		if (!isLocalPlayer) return;
		healthCanvas.transform.LookAt (Camera.main.transform);
	}

	public void UpdateHealth (float amount) {
		currentHealth += amount;
		currentHealth = Mathf.Max (currentHealth, 0);
	}

	public void SetHealth (float amount) {
		currentHealth = amount;
		currentHealth = Mathf.Max (currentHealth, 0);
	}

	public float GetHealth () {
		return currentHealth;
	}

	public float GetMaxHealth () {
		return maxHealth;
	}

	public void OnUpdateHealth (float h) {
		currentHealth = h;
		healthBar.sizeDelta = new Vector2(h, healthBar.sizeDelta.y);
	}
}
