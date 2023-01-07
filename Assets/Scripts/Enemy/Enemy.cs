using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Object.Destroy(gameObject);
    }
}
