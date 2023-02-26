using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YK.HealthSystem;
public class Enemy : MonoBehaviour
{

    Health _health;
    [SerializeField]int _initializeHealthValue = 3;
    

    public EnemySpawner enemySpawner;

    private void OnEnable()
    {
        GetHealth();
    }
    private void OnDisable()
    {
        _health._onDeath.RemoveAllListeners();
        _health._onHit.RemoveAllListeners();
    }
    private void OnDestroy()
    {
        _health._onDeath.RemoveAllListeners();
        _health._onHit.RemoveAllListeners();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ScreenBounds>())
            return;
        if (collision.transform.TryGetComponent(out IHittable hittable))
        {
            if (hittable != null)
                hittable.GetHit(1, gameObject);
        }
        if (collision.GetComponent<Player>())
        {
            Death();
        }
         
    }


    public void EnemyKilledOutsideBounds()
    {
        enemySpawner.EnemyKilled(this, false);
        Destroy(gameObject);
    }


    public void Death()
    {
        enemySpawner.EnemyKilled(this, true);
        StopAllCoroutines();
        Destroy(gameObject);
    }
    void GetHealth()
    {
        if (_health == null)
            _health = GetComponent<Health>();

        _health.InitializeHealth(_initializeHealthValue);
    }
}
