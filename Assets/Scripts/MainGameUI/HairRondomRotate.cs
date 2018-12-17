using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairRondomRotate : MonoBehaviour
{
	float y;
	float time;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		time += Time.deltaTime;
		if (time > 0.016f * 3) {
			time = 0;
			y = Random.Range(30.0f, 90.0f);
		}
	
		this.transform.localRotation = Quaternion.Euler(y, 0, -25.0f);
	}
}
