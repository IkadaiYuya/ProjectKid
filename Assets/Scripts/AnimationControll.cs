using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControll : MonoBehaviour
{
	private Vector3 inputLAxis = Vector3.zero;
	private Vector3 inputRAxis = Vector3.zero;
	private float inputL_magnitude;
	private float inputR_magnitude;

	private Vector3 cameraForward = Vector3.zero;
	private Vector3 axis;
	private float angle;

	Animator animator;

	// Use this for initialization
	void Start()
	{
		animator = GetComponent<Animator>();
		if (Camera.main == null) {
			Debug.LogWarning("Warning: no main camera found. Needs a Camera tagged \"MainCamera\"", gameObject);
		}
	}

	// Update is called once per frame
	void Update()
	{
		cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

		inputLAxis = cameraForward * Input.GetAxis("LS_v") + Camera.main.transform.right * Input.GetAxis("LS_h");
		inputRAxis = cameraForward * Input.GetAxis("RS_v") + Camera.main.transform.right * Input.GetAxis("RS_h");

		inputL_magnitude = inputLAxis.magnitude;
		inputR_magnitude = inputRAxis.magnitude;

		axis = Vector3.Cross(this.transform.forward, inputLAxis);
		angle = Vector3.Angle(this.transform.forward, inputLAxis) * (axis.y < 0 ? -1 : 1) / 180;

		/*
		if (inputR_magnitude != 0) {
			animator.SetBool("Shot", true);
		}
		else {
			animator.SetBool("Shot", false);
		}
		animator.SetFloat("Shot_Neutral", angle);
		*/
	}
}
