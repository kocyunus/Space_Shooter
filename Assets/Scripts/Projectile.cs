using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YK.HealthSystem;
public class Projectile : MonoBehaviour
{
    public float speed = 10;
    public Rigidbody2D rb2d;
    public float deathDelay = 5;

    public bool disabled = false;

    Health _health;
    int _initialHealthValue = 1;
    // Start is called before the first frame update
    void Start()
    {
        _health = GetComponent<Health>();
        _health.InitializeHealth(_initialHealthValue);
        rb2d.velocity = transform.up * speed;
        StartCoroutine(DeathAfterDelay(deathDelay));
    }

    private IEnumerator DeathAfterDelay(float deathDelay)
    {
        yield return new WaitForSeconds(deathDelay);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out IHittable hittable))
        {
            if (hittable != null)
                hittable.GetHit(1, gameObject);

            Destroy(gameObject);
        }
    }


}
