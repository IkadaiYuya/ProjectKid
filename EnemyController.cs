using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Enemys Status
    public enum EnemyState
    {
       Non = -1,
       Stand,   // Not target
       Walk,    // Find target. Wander around the target
       Charge,  // Accumulate power 
       Punch,   // Short attack to target
       Shoot,   // Long attack to target
       Back,    // back after the attack
    }
    // this character status
    // start status is Stand
    private EnemyState enemyState = EnemyState.Stand;

    //
    private EnemyState preEnemyState = EnemyState.Stand;

    // this character move direction
    private Vector3 moveDirection;

    // this character move speed
    [SerializeField] private float moveSpeed = 3.0f;

    // this character back move speed
    [SerializeField] private float backSpeed = 15.0f;

    // this character rotation speed
    [SerializeField] private float rotSpeed = 360.0f;

    //
    [SerializeField] private float PunchRenge = 2.0f;

    //
    [SerializeField] private float walkDirectionChengeTime = 1.0f;

    //
    public float chargeTime = 1.0f;

    //
    [SerializeField] private float punchMotionTime = 1.0f;

    //
    [SerializeField] private float shootMotionTime = 1.0f;

    //
    [SerializeField] private float backMotionTime = 0.5f;

    // this character's motion timer
    private float motionTimer;

    // distance to target(Vector3)
    private Vector3 targetDistance;

    // distance to target(Renge)
    private float targetRenge;

    //
    private float targetRadian;

    // this character gravity
    [SerializeField] private float gravity = -9.81f;
    
    // this character controller
    private CharacterController cCon;
    
    // this animator
    private Animator animator;
    
    // target character transform
    [SerializeField] private Transform targetT;

    // management of this character
    public EnemyState CurrentEnemyState
    {
        get { return enemyState; }
        set { this.enemyState = value; }
    }
    // Use this for initialization
    void Start()
    {
        cCon = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        TargetCheck();
        RotateCharactor();
        Think();
        Move();
        Animation();
        motionTimer -= Time.deltaTime;
    }

    //
    private void TargetCheck()
    {
        targetDistance = targetT.position - this.transform.position;
        targetRenge = Vector3.Distance(this.transform.position, targetT.position );
    }

    // this character forcus on the target
    private void RotateCharactor()
    {
        if(targetT == null)
        {
            CurrentEnemyState = EnemyState.Stand;
            return;
        }
        if(CurrentEnemyState == EnemyState.Punch ||
            CurrentEnemyState == EnemyState.Back)
        {
            return;
        }
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.LookRotation(targetDistance.normalized), rotSpeed * Time.deltaTime);
    }

    // this chatacter think about next state from the current state
    private void Think()
    {
        if(motionTimer >= 0)
        {
            return;
        }
        switch (CurrentEnemyState)
        {
            case EnemyState.Stand:
                if(targetT != null)
                {
                    preEnemyState = CurrentEnemyState;
                    CurrentEnemyState = EnemyState.Walk;
                    WalkDistance();
                    motionTimer = walkDirectionChengeTime;
                }
                break;
            case EnemyState.Walk:
                if(targetT == null)
                {
                    preEnemyState = CurrentEnemyState;
                    CurrentEnemyState = EnemyState.Stand;
                    return;
                }
                motionTimer = walkDirectionChengeTime;
                break;
            case EnemyState.Charge:
                if(targetRenge <= PunchRenge)
                {
                    CurrentEnemyState = EnemyState.Punch;
                    motionTimer = punchMotionTime;
                }
                else
                {
                    CurrentEnemyState = EnemyState.Shoot;
                    motionTimer = shootMotionTime;
                }
                break;
            case EnemyState.Punch:
                CurrentEnemyState = EnemyState.Back;
                BackDistance();
                motionTimer = backMotionTime;
                break;
            case EnemyState.Shoot:
                CurrentEnemyState = EnemyState.Walk;
                WalkDistance();
                motionTimer = walkDirectionChengeTime;
                break;
            case EnemyState.Back:
                if(targetT == null)
                {
                    CurrentEnemyState = EnemyState.Stand;
                    return;
                }
                CurrentEnemyState = EnemyState.Walk;
                WalkDistance();
                motionTimer = walkDirectionChengeTime;
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
                
                cCon.Move(moveDirection * Time.deltaTime);
                break;
            case EnemyState.Charge:
                break;
            case EnemyState.Punch:
                break;
            case EnemyState.Shoot:
                break;
            case EnemyState.Back:
                moveDirection = -this.transform.forward * backSpeed;
                cCon.Move(moveDirection * Time.deltaTime);
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
            case EnemyState.Punch:
                break;
            case EnemyState.Shoot:
                break;
            case EnemyState.Back:
                break;
        }
    }

    private void WalkDistance()
    {

    }

    private void BackDistance()
    {

    }
}
