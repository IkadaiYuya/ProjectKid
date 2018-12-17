using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashLight : MonoBehaviour
{
	[SerializeField] private float step = 1.0f;
	Light light;

	// Use this for initialization
	void Start()
	{
		light = GetComponent<Light>();
	}

	// Update is called once per frame
	void Update()
	{
		light.intensity = Mathf.Abs(Mathf.Sin(Time.time * step));
	}
}

