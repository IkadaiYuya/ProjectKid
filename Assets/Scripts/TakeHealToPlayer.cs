using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeHealToPlayer : MonoBehaviour {
	[SerializeField] PlayerStatus playerStatus;
	[SerializeField] private float damage = 10.0f;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown("A")) {
			playerStatus.TakeHeal(damage);
		}
	}
}
