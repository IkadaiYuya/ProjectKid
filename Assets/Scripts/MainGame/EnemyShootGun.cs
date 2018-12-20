using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootGun : MonoBehaviour {

    //
    [SerializeField] private float spread = 0.3f;
    //
    private Vector3 direction;
    //
    [SerializeField] private float range = 100.0f;
    //
    [SerializeField] private float damage = 1.0f;
    //
    [SerializeField] private float fireRate = 15.0f;
    //
    private float nextTimeFire;
    //
    [SerializeField] private Transform muzzlePos;
    //
    [SerializeField] private ParticleSystem mFlsh;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        direction.x = Random.Range(-spread, spread);
        direction.y = Random.Range(-spread, spread);
        if(nextTimeFire <= 0)
        {
            Shoot();
            nextTimeFire = fireRate * Time.deltaTime;
        }
    }

    private void Shoot()
    {
        MuzzlFlash();
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position + muzzlePos.position, muzzlePos.transform.forward + direction, out hit, range))
        {
        }
        Debug.DrawRay(this.transform.position + muzzlePos.position, (muzzlePos.transform.forward + direction) * range, Color.black);
    }

    private void MuzzlFlash()
    {
        mFlsh.Play();
    }

    public void Fire()
    {
        nextTimeFire -= Time.deltaTime;
    }
}