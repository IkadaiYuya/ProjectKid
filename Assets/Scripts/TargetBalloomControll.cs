using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBalloomControll : MonoBehaviour {
	CapsuleCollider capsuleCollider;
	Animator animator;
	// Use this for initialization
	void Start () {
		capsuleCollider = GetComponent<CapsuleCollider>();
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("X")) {
			animator.SetTrigger("Damaged");
		}
	}
}
