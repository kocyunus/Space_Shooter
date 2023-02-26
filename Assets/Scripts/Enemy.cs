using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YK.HealthSystem;
public class Enemy : MonoBehaviour
{
    public Player player;
    Health _health;
    [SerializeField]int _initializeHealthValue = 3;
    
    public GameObject projectile;
    public float shootingDelay;

    public bool isShooting = false;

    public float speed = 2;
    public float speedVariation = 0.3f;
    private Rigidbody2D rb2d;
    bool firstShoot = true;

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
    private void Awake()
    {
        
        player = FindObjectOfType<Player>();
        rb2d = GetComponent<Rigidbody2D>();
        speed += UnityEngine.Random.Range(0, speedVariation);
    }

    //private void Start()
    //{
    //    rb2d.velocity = Vector3.down * speed;
    //}

    private void Update()
    {
        //Vector3 movementDirection = Vector3.down * speed * Time.deltaTime;
        //transform.Translate(movementDirection, Space.World);
        if (player.isAlive)
        {
            Vector3 desiredDirection = player.transform.position - transform.position;
            float desiredAngle = Mathf.Atan2(desiredDirection.y, desiredDirection.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.AngleAxis(desiredAngle, Vector3.forward);

            if (isShooting == false)
            {
                isShooting = true;
                StartCoroutine(ShootWithDelay(shootingDelay));
            }
        }
    }

    private void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + Vector2.down * speed*Time.deltaTime);
    }
    private IEnumerator ShootWithDelay(float shootingDelay)
    {
        if (firstShoot)
        {
            firstShoot = false;
            yield return new WaitForSeconds(UnityEngine.Random.Range(0, 0.5f));
        }
        yield return new WaitForSeconds(shootingDelay);
        GameObject p = Instantiate(projectile, transform.position, transform.rotation);
        
        isShooting = false;
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
