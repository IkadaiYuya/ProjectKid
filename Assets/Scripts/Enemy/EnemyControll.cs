using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControll : MonoBehaviour {
    private bool shot;
    Animator animator;
	// Use this for initialization
	void Start () {
        shot = false;
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("LB"))
        {
            animator.SetTrigger("Attack");
        }
        if (Input.GetButtonDown("B"))
        {
            if (!shot)
            {
                shot = true;
                animator.SetBool("Attack_Shot", true);
            }
            else
            {
                shot = false;
                animator.SetBool("Attack_Shot", false);
            }
        }
	}
}
