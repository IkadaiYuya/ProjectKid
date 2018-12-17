using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateus : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100.0f;
    private float health;

    Animator animator;

    public float MaxHealth
    {
        get { return maxHealth; }
        private set { maxHealth = value; }
    }
    public float Health
    {
        get { return health; }
        private set { health = value; }
    }

    private void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        animator.SetTrigger("Damaged");

        if (health <= 0f)
        {
            Debug.Log("Die");
        }
    }
}
