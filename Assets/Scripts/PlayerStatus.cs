using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {
    [SerializeField] private float maxHealth = 100.0f;
    private float health;


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

    void Start () {
        health = maxHealth;
	}
	
    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0f)
        {
            Debug.Log("Die");
        }
    }

	public void TakeHeal(float amount)
	{
		health += amount;
	}
}
