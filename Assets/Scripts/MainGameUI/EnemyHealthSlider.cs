using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthSlider : MonoBehaviour {
    Slider slider;
    EnemyStateus enemyStateus;

	[SerializeField] FindEnemy findEnemy;
	// Use this for initialization
	void Start () {
        slider = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
		if (findEnemy.DetectedEnemy != null) {
			enemyStateus = findEnemy.DetectedEnemy.GetComponent<EnemyStateus>();

			slider.maxValue = enemyStateus.MaxHealth;
			slider.value = enemyStateus.Health;
		}
	}
	/*
	bool UpdateGetEnemy(GameObject e_)
	{
		if (e_ == enemy) {
			return false;
		}
		else {
			enemy = e_;
			return true;
		}
	}
	*/
}
