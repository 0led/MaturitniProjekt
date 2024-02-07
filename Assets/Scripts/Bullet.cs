using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    // speed bylo speed = 20f;
    public float damage; // Přidáno pro nastavení poškození
    public Rigidbody2D rb;

    public float range; // Základní dosah, který můžete přizpůsobit pro každou střelu
    private float distanceTravelled = 0f; // Sledování, jak daleko střela cestovala
    private Vector3 lastPosition; // Poslední pozice střely

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        lastPosition = transform.position; // Nastavte počáteční pozici
    }

    void Update()
{
    // Vypočítejte, jak daleko střela cestovala od posledního snímku
        distanceTravelled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position; // Aktualizujte poslední pozici pro další snímek

        // Zkontrolujte, jestli střela dosáhla svého maximálního dosahu
        if (distanceTravelled >= range)
        {
            Destroy(gameObject); // Zničte střelu, pokud překročila dosah
        }
}

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Zde můžete přidat logiku pro způsobení poškození objektu, do kterého střela narazí
        // Například: hitInfo.GetComponent<Health>().TakeDamage(damage);
        
        //Destroy(gameObject); // Zničení střely po zásahu
    }

    //když spadne Bullet mimo mapu, tak se despawne
    void OnBecameInvisible()
    {
        enabled = false;
        Destroy(gameObject);
    }

}
