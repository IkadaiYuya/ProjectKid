using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGun : MonoBehaviour
{
	//拡散値
	[SerializeField] private float spread = 0.05f;
	//拡散用Vector3
	private Vector3 direction;
	//射程距離
	[SerializeField] private float range = 100.0f;
	//威力
	[SerializeField] private float damage = 2.0f;

	[SerializeField] private float fireRate = 15.0f;
	private float nextTimeToFire = 0f;

	//発射位置
	[SerializeField] private Transform muzzlePos;
	//高さ補正(突貫)
	[SerializeField] private Vector3 distance = Vector3.zero;

	//マズルフラッシュエフェクト
	[SerializeField] private ParticleSystem mFlash;
	[SerializeField] private ParticleSystem bFlash;

	//右スティック入力用
	private Vector3 inputRStick;

	//着弾エフェクト
	[SerializeField] private GameObject hitEffect = null;

	[SerializeField] FindEnemy findEnemy;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		direction.x = Random.Range(-spread, spread);
		direction.y = Random.Range(-spread, spread);

		//inputRStick.Set(Input.GetAxis("RS_h"), 0, Input.GetAxis("RS_v"));
		inputRStick = new Vector3(Input.GetAxis("RS_h"), 0, Input.GetAxis("RS_v")).normalized;

		if (findEnemy.DetectedEnemy != null) {
			muzzlePos.transform.LookAt(findEnemy.DetectedEnemy.transform.position + distance);
		}
		else {
			muzzlePos.transform.rotation = this.transform.rotation;
		}

		if (inputRStick.magnitude != 0 && Time.time >= nextTimeToFire) {
			nextTimeToFire = Time.time + 1f / fireRate;
			Shoot();
		}
	}
	void Shoot()
	{
		MuzzleFlash();
		RaycastHit hit;
		if (Physics.Raycast(muzzlePos.position, muzzlePos.transform.forward + direction, out hit, range)) {
			if (hit.transform.tag == "Enemy") {
				EnemyStateus enemyStateus = hit.transform.GetComponent<EnemyStateus>();
				if (enemyStateus != null) {
					enemyStateus.TakeDamage(damage);
				}
			}

			GameObject effect01 = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.point));
		}
		Debug.DrawRay(muzzlePos.position, (muzzlePos.transform.forward + direction) * range, Color.red);
	}
	void MuzzleFlash()
	{
		mFlash.Play();
		bFlash.Play();
	}
}
