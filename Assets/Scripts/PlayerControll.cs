using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
	public enum PlayerState
	{
		idle,
		walk,
		run,
		attack_1,
		attack_2,
		attack_3,
		shot_neutral,
		shot_forward,
		shot_side_left,
		shot_side_right,
		shot_back
	}
	private PlayerState playerState;

	private Vector3 inputLAxis = Vector3.zero;
	private Vector3 inputRAxis = Vector3.zero;

	private Vector3 moveDirection = Vector3.zero;
	[SerializeField] private float walkSpeed = 3.0f;
	[SerializeField] private float runSpeed = 3.0f;
	[SerializeField] private float gravity = -9.81f;
	[SerializeField] private float rotSpeed = 540.0f;

	[SerializeField] private ParticleSystem attack;
	[SerializeField] private ParticleSystem attack2;
	CharacterController cCon;
	Animator animator;

	FindEnemy findEnemy;

	//プレイヤーから見たカメラの位置
	private Vector3 cameraForward = Vector3.zero;

	//プレイヤーの状態はここにしまっちゃおうね～
	public PlayerState CurrentPlayerState
	{
		get { return this.playerState; }
		private set { this.playerState = value; }
	}

	void Start()
	{
		cCon = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		findEnemy = GetComponent<FindEnemy>();
		if (Camera.main == null) {
			Debug.LogWarning("Warning: no main camera found. Needs a Camera tagged \"MainCamera\"", gameObject);
		}
	}

	void Update()
	{
		cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

		inputLAxis = cameraForward * Input.GetAxis("LS_v") + Camera.main.transform.right * Input.GetAxis("LS_h");
		inputRAxis = cameraForward * Input.GetAxis("RS_v") + Camera.main.transform.right * Input.GetAxis("RS_h");

		if (inputRAxis.magnitude > 0.3f) {
			transform.localRotation = Quaternion.LookRotation(inputRAxis);
		}
		else {
			if (inputLAxis.magnitude != 0) {
				transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(inputLAxis), rotSpeed * Time.deltaTime);
			}
		}
		//Anim();
		Think();
		Move();
		DebugFunc();

		if (!cCon.isGrounded) {
			moveDirection.y += gravity;
		}
		cCon.Move(moveDirection * Time.deltaTime);
	}
	void Think()
	{
		switch (playerState) {
			case PlayerState.idle:
				if (inputLAxis.magnitude != 0) {
					playerState = PlayerState.walk;
				}
				break;
			case PlayerState.walk:
				if (inputLAxis.magnitude > 0.7f) {
					playerState = PlayerState.run;
				}
				if (inputLAxis.magnitude < 0.1f) {
					playerState = PlayerState.idle;
				}
				break;
			case PlayerState.run:
				if (inputLAxis.magnitude < 0.7f) {
					playerState = PlayerState.walk;
				}
				break;
		}
	}
	void Move()
	{
		switch (playerState) {
			case PlayerState.idle:
				moveDirection = Vector3.zero;
				break;
			case PlayerState.walk:
				moveDirection = inputLAxis.normalized * walkSpeed;
				break;
			case PlayerState.run:
				moveDirection = inputLAxis.normalized * runSpeed;
				break;
		}
	}
	void Anim()
	{
		if (Input.GetButtonDown("Y")) {
			animator.SetTrigger("Attack");
			attack.Play();
			attack2.Play();
		}
		if (inputLAxis.magnitude != 0) {
			animator.SetBool("Run", true);
		}
		else {
			animator.SetBool("Run", false);
		}
	}
	void CheckEnemyRange()
	{
		if (findEnemy.DetectedEnemy != null) {

		}
	}

	void DebugFunc()
	{
		Debug.Log(playerState);
	}
}
