using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class imageRotate : MonoBehaviour
{
	RectTransform rectTransform;
	[SerializeField] private Vector3 rot;
	// Use this for initialization
	void Start()
	{
		rectTransform = GetComponent<RectTransform>();
	}

	// Update is called once per frame
	void Update()
	{
		rectTransform.Rotate(rot * Time.deltaTime);
	}
}
