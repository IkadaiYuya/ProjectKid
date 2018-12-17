using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageToPlayer : MonoBehaviour {
    [SerializeField] PlayerStatus playerStatus;
    [SerializeField] private float damage = 30.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("B"))
        {
            playerStatus.TakeDamage(damage);
        }
	}
}
