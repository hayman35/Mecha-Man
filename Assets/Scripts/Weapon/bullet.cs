using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    [SerializeField] float damage = 10f;
    [SerializeField] GameObject impactFX;
    Rigidbody2D rb;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() 
    {
        rb.velocity = transform.right * speed;
    }

    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Enemy")
        {
            Health enemy = other.GetComponent<Health>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(1f,0),ForceMode2D.Impulse);
        }
        //  GameObject impactfx = Instantiate(impactFX, transform.position, transform.rotation);
    }
}
