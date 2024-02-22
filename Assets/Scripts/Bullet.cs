using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    private float distanceTravelled = 0f;
    private Vector3 lastPosition;
    private float damage;
    private float speed;
    private float range;

    public void Initialize(WeaponConfig config)
    {
        damage = config.damage;
        speed = config.speed;
        range = config.range;

        rb.velocity = transform.right * speed;
    }

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        distanceTravelled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;

        if (distanceTravelled >= range)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Health health = hitInfo.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (hitInfo.gameObject.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}