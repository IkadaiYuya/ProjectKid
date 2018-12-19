using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //敵の状態
    public enum EnemyState
    {
       Non = -1,
       Stand,
       Walk,
       Chase,
       Charge,
       ShortAttack,
       LongAttack,
    }
    //
    private EnemyState enemyState = EnemyState.Stand;
    //移動量
    private Vector3 moveDirection;
    //通常移動速度
    [SerializeField] private float moveSpeed = 5.0f;
    //
    [SerializeField] private float moveRotPaSec = 10.0f;
    //
    [SerializeField] private float chaseSpeed = 1.0f;
    //回転速度
    [SerializeField] private float rotSpeed = 360.0f;
    //
    [SerializeField] private float chaceRange = 3.0f;
    //
    private Vector3 rotDistance;
    //
    private float motionTimer;
    //
    [SerializeField] private float walkChengeTime = 2.0f;
    //
    [SerializeField] private float chargeTime = 1.0f;
    //
    [SerializeField] private float shortAttackTime = 1.0f;
    //
    [SerializeField] private float longAttackTime = 1.0f;
    //
    [SerializeField] private float backTime = 0.2f;
    //
    [SerializeField] private float punchRange = 2.0f;
    //標的との距離
    private float targetRenge;
    //
    private float targetRad;
    //重力
    [SerializeField] private float gravity = -9.81f;
    //
    private Transform transform;
    //キャラクターコントローラー
    private CharacterController cCon;
    //アニメーター
    private Animator animator;
    //標的のトランスフォーム
    [SerializeField] private Transform targetT;

    //
    public EnemyState CurrentEnemyState
    {
        get {return enemyState; }
        set { enemyState = value; }
    }
    // Use this for initialization
    void Start()
    {
        transform = GetComponent<Transform>();
        cCon = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetT == null)
        {
            return;
        }
        DecTimer();
        RotationChara();
        CheckTargetRenge();
        Think();
        if (Input.GetButtonDown("A"))
        {
            CurrentEnemyState = EnemyState.Charge;
            motionTimer = chargeTime;
        }
        Move();
        Animation();
    }

    //
    private void DecTimer()
    {
        motionTimer -= Time.deltaTime;
    }

    //
    private void RotationChara()
    {
        if(CurrentEnemyState == EnemyState.ShortAttack)
        {
            return;
        }
        rotDistance = (targetT.position - transform.position).normalized;
        rotDistance.y = 0;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(rotDistance), rotSpeed * Time.deltaTime);
    }

    //
    private void CheckTargetRenge()
    {
        targetRenge = Vector3.Distance(targetT.position,transform.position);
        Debug.Log("TargetRenge:" + targetRenge);
    }

    //
    private void Think()
    {
        if(motionTimer > 0)
        {
            return;
        }
        switch (CurrentEnemyState)
        {
            case EnemyState.Stand:
                if(targetT != null)
                {
                    if (targetRenge >= chaceRange)
                    {
                        CurrentEnemyState = EnemyState.Chase;
                    }
                    else
                    {
                        CurrentEnemyState = EnemyState.Walk;
                        motionTimer = walkChengeTime;
                    }
                }
                break;
            case EnemyState.Walk:
                if(targetT == null)
                {
                    CurrentEnemyState = EnemyState.Stand;
                    return;
                }
                if (targetRenge >= chaceRange)
                {
                    CurrentEnemyState = EnemyState.Chase;
                }

                break;
            case EnemyState.Chase:
                if(targetT == null)
                {
                    CurrentEnemyState = EnemyState.Stand;
                }
                else if(targetRenge < chaceRange)
                {
                    CurrentEnemyState = EnemyState.Walk;
                    motionTimer = walkChengeTime;
                }
                break;
            case EnemyState.Charge:
                if(targetRenge <= punchRange)
                {
                    CurrentEnemyState = EnemyState.ShortAttack;
                    motionTimer = shortAttackTime;
                }
                else
                {
                    CurrentEnemyState = EnemyState.LongAttack;
                    motionTimer = longAttackTime;
                }
                break;
            case EnemyState.ShortAttack:
                CurrentEnemyState = EnemyState.Stand;
                break;
            case EnemyState.LongAttack:
                CurrentEnemyState = EnemyState.Stand;
                break;
        }
    }

    //
    private void Move()
    {
        Debug.Log("EnemyState:" + CurrentEnemyState);
        switch (CurrentEnemyState)
        {
            case EnemyState.Stand:
                break;
            case EnemyState.Walk:
                CheckNextPos();
                cCon.Move(moveDirection);
                break;
            case EnemyState.Chase:
                moveDirection = targetT.position - transform.position;
                moveDirection *= chaseSpeed * Time.deltaTime;
                moveDirection.y = 0;
                cCon.Move(moveDirection);
                break;
            case EnemyState.Charge:
                break;
            case EnemyState.ShortAttack:
                break;
            case EnemyState.LongAttack:
                break;
        }
    }

    //
    private void Animation()
    {
        switch (CurrentEnemyState)
        {
            case EnemyState.Stand:
                break;
            case EnemyState.Walk:
                break;
            case EnemyState.Charge:
                break;
            case EnemyState.ShortAttack:
                break;
            case EnemyState.LongAttack:
                break;
        }
    }

    //
    private void CheckNextPos()
    {
        int dis = 1;
        if(motionTimer <= 0)
        {
            dis = Random.Range(0, 3) - 1;
            dis = dis == 0 ? 1 : dis;
        }
        targetRad = Mathf.Atan2(transform.position.z - targetT.position.z, transform.position.x - targetT.position.x);
        targetRad += moveRotPaSec * Mathf.Deg2Rad;
        Vector3 worldVec = targetT.position + new Vector3(Mathf.Cos(targetRad), 0, Mathf.Sin(targetRad)) * targetRenge;
        moveDirection = (worldVec - transform.position) * moveSpeed * Time.deltaTime;
        moveDirection.y = 0;
    }
}