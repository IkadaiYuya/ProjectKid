using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour {

    //
    private List<GameObject> enemyGarage = new List<GameObject>();
    //
    [SerializeField] private float attackIntervalTime = 10.0f;
    //
    private float attackTimer;

	// Use this for initialization
	void Start () {
        attackTimer = attackIntervalTime;
        EnemyGarageControll();
        Debug.Log("EnemyGarageNum:" + enemyGarage.Count);
	}
	
	// Update is called once per frame
	void Update () {
        attackTimer -= Time.deltaTime;
		if(attackTimer < 0)
        {
            Debug.Log("EnemyAttack");
            RandomAttack();
            attackTimer = attackIntervalTime;
        }
	}

    //
    private void RandomAttack()
    {
        int attackEnemyNum = Random.Range(0, enemyGarage.Count-1);
        EnemyController eCon = enemyGarage[attackEnemyNum].GetComponent<EnemyController>();
        eCon.CurrentEnemyState = EnemyController.EnemyState.Charge;
    }

    //public method
    //
    public void RemenberEnemy(GameObject deadEne)
    {
        for(int i = enemyGarage.Count -1; i >= 0;--i)
        {
            if(enemyGarage[i] == deadEne)
            {
                enemyGarage.RemoveAt(i);
            }
        }
    }

    //
    public void EnemyGarageControll()
    {
        GameObject[] tmpEne = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < tmpEne.Length;++i)
        {
            enemyGarage.Add(tmpEne[i]);
        }
        //foreach(GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
        //{
        //    if(obj.tag == "Enemy")
        //    {
        //        for(int i = enemyGarage.Count - 1; i >= 0;--i)
        //        {
        //            if(enemyGarage[i] == obj)
        //            {
        //                Debug.Log("Break");
        //                break;
        //            }
        //            if(i == 0)
        //            {
        //                enemyGarage.Add(obj);
        //            }
        //        }
        //    }
        //}
    }
}
