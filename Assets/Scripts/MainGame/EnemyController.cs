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
       LeftWalk,
       RightWalk,
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
    private EnemyStateus este;
    //
    [SerializeField] GameObject muzzle;
    //
    private EnemyShootGun eshot;

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
        este = GetComponent<EnemyStateus>();
        eshot = muzzle.GetComponent<EnemyShootGun>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(este.Health <= 0)
        //{
        //    if(cCon.isGrounded)
        //    {
        //        moveDirection = Vector3.zero;
        //    }
        //    moveDirection = new Vector3(0, 3, 0) * Time.deltaTime;
        //    cCon.Move(moveDirection);
        //    return;
        //}
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
        //Debug.Log("TargetRenge:" + targetRenge);
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
                    }
                }
                break;
            case EnemyState.Walk:
                if(targetT == null)
                {
                    CurrentEnemyState = EnemyState.Stand;
                    return;
                }
                int r = Random.Range(0, 2);
                if(r == 0)
                {
                    CurrentEnemyState = EnemyState.LeftWalk;
                    motionTimer = walkChengeTime;
                }
                else
                {
                    CurrentEnemyState = EnemyState.RightWalk;
                    motionTimer = walkChengeTime;
                }
                if (targetRenge >= chaceRange)
                {
                    CurrentEnemyState = EnemyState.Chase;
                }
                break;
            case EnemyState.LeftWalk:
                CurrentEnemyState = EnemyState.Walk;
                break;
            case EnemyState.RightWalk:
                CurrentEnemyState = EnemyState.Walk;
                break;
            case EnemyState.Chase:
                if(targetT == null)
                {
                    CurrentEnemyState = EnemyState.Stand;
                }
                if(targetRenge < chaceRange)
                {
                    CurrentEnemyState = EnemyState.Stand;
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
        switch (CurrentEnemyState)
        {
            case EnemyState.Stand:
                break;
            case EnemyState.Walk:
                targetRenge += Random.Range(-4.0f, 4.0f);
                break;
            case EnemyState.LeftWalk:
                CheckNextPos(1);
                break;
            case EnemyState.RightWalk:
                CheckNextPos(-1);
                break;
            case EnemyState.Chase:
                moveDirection = (targetT.position - transform.position).normalized;
                moveDirection *= chaseSpeed * Time.deltaTime;
                moveDirection.y = 0;
                break;
            case EnemyState.Charge:
                break;
            case EnemyState.ShortAttack:
                Debug.Log("Punch");
                break;
            case EnemyState.LongAttack:
                eshot.Fire();
                Debug.Log("Fire");
                break;
        }
        if (!cCon.isGrounded)
        {
            moveDirection.y += gravity * Time.deltaTime;
        }
        cCon.Move(moveDirection);
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

    //移動先の計算
    //引数：（回転方向 R=-1,L=1）
    private void CheckNextPos(int lr)
    {
        targetRad = Mathf.Atan2(transform.position.z - targetT.position.z, transform.position.x - targetT.position.x);
        targetRad += lr * moveRotPaSec * Mathf.Deg2Rad;
        Vector3 worldVec = targetT.position + new Vector3(Mathf.Cos(targetRad), 0, Mathf.Sin(targetRad)) * targetRenge;
        moveDirection = (worldVec - transform.position).normalized;
        moveDirection *= moveSpeed * Time.deltaTime;
        moveDirection.y = 0;
    }
}