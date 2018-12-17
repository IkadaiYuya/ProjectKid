using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthDelaySlider : MonoBehaviour
{
	[SerializeField] private float delay = 0.1f;

	Slider slider;
	[SerializeField] PlayerStatus playerStatus;
	// Use this for initialization
	void Start()
	{
		slider = GetComponent<Slider>();
		slider.maxValue = playerStatus.MaxHealth;
		slider.value = playerStatus.MaxHealth;
	}

	// Update is called once per frame
	void Update()
	{
		if (playerStatus.Health < slider.value) {
			slider.value -= delay * Time.deltaTime;
		}
		else {
			slider.value = playerStatus.Health;
		}
	}
}
