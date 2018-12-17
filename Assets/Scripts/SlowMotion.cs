using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("RB")) {
			Time.timeScale = 0.1f;
		}
		else {
			Time.timeScale = 1.0f;
		}
	}
}
