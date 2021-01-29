using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject impactFX;

    public void TakeDamage(float damage)
    {
        GameObject impactfx = Instantiate(impactFX, transform.position, transform.rotation);
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameObject deathfx = Instantiate(deathFX, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
