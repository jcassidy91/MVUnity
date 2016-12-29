using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class health : MonoBehaviour {

	public float maxHealth;
	public Slider healthBar;
	public Text HealthText;
	float hp;

	void Start () 
	{
		hp = maxHealth;
	}

	void TakeDamage (float amount) 
	{
		hp -= amount;
		hp = Mathf.Max (hp, 0);
		UpdateSlider ();
	}

	void UpdateSlider () 
	{
		healthBar.value = hp / maxHealth;
		HealthText.text = hp + "/" + maxHealth;
	}
}
