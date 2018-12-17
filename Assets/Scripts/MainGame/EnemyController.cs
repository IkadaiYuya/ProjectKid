using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum EnemyState
    {
       Non = -1,
       Stand,
       Walk,
       ShortAttack,
       LongAttack,

    }
    // this Charactor Move Direction
    private Vector3 moveDirection;
    // this Charactor Move Speed
    [SerializeField] private float moveSpeed = 10.0f;
    // this Charactor Rotetion Speed
    [SerializeField] private float rotSpeed = 360.0f;
    // Distance to Target
    private float targetRenge;
    // this Charactor Gravity
    [SerializeField] private float gravity = -9.81f;
    // this Charactor Controller
    private CharacterController cCon;
    // this Animator
    private Animator animator;
    // Target Charactor Transform
    [SerializeField] private Transform targetT;
    // Use this for initialization
    void Start()
    {
        cCon = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }


}
