using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkHealth : NetworkBehaviour {
	public Slider healthBar;
	Text healthText;
	GameObject hud;

	public float maxHealth;
	[SyncVar (hook = "UpdateHealthSlider")]
	public float health;

	void Start () {
		health = maxHealth;

		hud = transform.Find ("HUD").gameObject;
		healthBar = hud.transform.FindChild ("PlayerHealthBar").GetComponent<Slider> ();
		healthText = healthBar.transform.gameObject.transform.FindChild ("HealthText").GetComponent<Text> ();
	}

	public void UpdateHealth (float amount) {
		if (!isServer) return;
		Debug.Log ("updating health");
		health += amount;
		health = Mathf.Max (health, 0);
	}

	public void SetHealth (float amount) {
		if (!isServer) return;
		health = amount;
		health = Mathf.Max (health, 0);
	}

	public float GetHealth () {
		return health;
	}

	public float GetMaxHealth () {
		return maxHealth;
	}

	void UpdateHealthSlider (float h) {

//		hud = transform.Find ("HUD").gameObject;
//		healthBar = hud.transform.FindChild ("PlayerHealthBar").GetComponent<Slider> ();
//		healthText = healthBar.transform.gameObject.transform.FindChild ("HealthText").GetComponent<Text> ();

		healthBar.value = h / GetMaxHealth();
		healthText.text = h + " / " + GetMaxHealth ();
	}
}
