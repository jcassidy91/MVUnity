using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

	public float maxHealth;
	Slider healthBar;
	Text healthText;
	float health;
	public GameObject lowHealthOverlay;

	void Start () {
		health = maxHealth;
		healthBar = GameObject.Find ("PlayerHealthBar").GetComponent<Slider>();
		healthText = GameObject.Find ("HealthText").GetComponent<Text>();
		lowHealthOverlay = GameObject.Find ("LowHealthOverlay");
		SetHealthOverlay (false);
		UpdateSlider ();
	}

	void UpdateSlider () {
		healthBar.value = health / maxHealth;
		healthText.text = health + " / " + maxHealth;
	}

	public void UpdateHealth (float amount) {
		health += amount;
		health = Mathf.Max (health, 0);
		UpdateSlider ();
	}

	public void SetHealth (float amount) {
		health = amount;
		health = Mathf.Max (health, 0);
		UpdateSlider ();
	}

	public float GetHealth () {
		return health;
	}

	public float GetMaxHealth () {
		return maxHealth;
	}

	public void SetHealthOverlay(bool on) {
		lowHealthOverlay.SetActive (on);
	}
}
