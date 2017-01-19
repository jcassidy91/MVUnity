using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

	public float maxHealth;
	Slider healthBar;
	Text healthText;
	public float hp;

	void Start () {
		hp = maxHealth;
		healthBar = GameObject.Find ("PlayerHealthBar").GetComponent<Slider>();
		healthText = GameObject.Find ("HealthText").GetComponent<Text>();
	}

	void UpdateSlider () {
		healthBar.value = hp / maxHealth;
		healthText.text = hp + " / " + maxHealth;
	}

	public void UpdateHealth(float amount) {
		hp += amount;
		hp = Mathf.Max (hp, 0);
		UpdateSlider ();
	}
}
