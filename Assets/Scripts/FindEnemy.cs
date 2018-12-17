using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEnemy : MonoBehaviour
{
	[SerializeField] private List<GameObject> enemy = new List<GameObject>();
	private List<GameObject> tempEnemy = new List<GameObject>();

	private GameObject detectedEnemy;

	[SerializeField] private float angle;
	[SerializeField] private float distance;

	//プレイヤーから見たカメラの位置
	private Vector3 cameraForward = Vector3.zero;
	private Vector3 inputRAxis = Vector3.zero;

	public GameObject DetectedEnemy
	{
		get { return this.detectedEnemy; }
		private set { this.detectedEnemy = value; }
	}

	void Update()
	{
		cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
		inputRAxis = cameraForward * Input.GetAxis("RS_v") + Camera.main.transform.right * Input.GetAxis("RS_h");

		if (inputRAxis.magnitude != 0) {
			CheckEnemy();
		}
		else {
			detectedEnemy = null;
		}
		
		//Debug.Log(detectedEnemy);
	}

	void CheckEnemy()
	{
		for (int i = 0; i < enemy.Count; ++i) {
			if (Vector3.Angle((enemy[i].transform.position - this.transform.position).normalized, inputRAxis) <= angle
				&& Vector3.Distance(enemy[i].transform.position, this.transform.position) <= distance) {
				for(int j = 0; j < tempEnemy.Count; ++j) {
					if (tempEnemy[j] == enemy[i]) {
						return;
					}
				}
				tempEnemy.Add(enemy[i]);
			}
			else {
				tempEnemy.Remove(enemy[i]);
			}
		}

		if (tempEnemy.Count > 0) {
			detectedEnemy = tempEnemy[0];
		}
		for (int i = 0; i < tempEnemy.Count; ++i) {
			if(Vector3.Distance(tempEnemy[i].transform.position,this.transform.position)
				< Vector3.Distance(detectedEnemy.transform.position, this.transform.position)) {
				detectedEnemy = tempEnemy[i];
			}
		}
	}
}
