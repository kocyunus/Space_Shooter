using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YK.WeaponSystem;
using YK.HealthSystem;
public class Player : MonoBehaviour
{
    public float speed = 2;
    Health _health;
    [SerializeField] int _initializeHealthValue = 3;
    public Transform playerShip;


    public ScreenBounds screenBounds;

    public int health = 3;

    [SerializeField]
    private Transform liveImagesUIParent;
    List<Image> lives = new List<Image>();

    Rigidbody2D rb2d;
    Vector2 movementVector = Vector2.zero;

    public AudioClip hitClip, deathClip;
    public AudioSource hitSource;

    public GameObject explosionFX;

    public bool isAlive = true;

    public InGameMenu loseScreen;
    public Button menuButton;

    [SerializeField] Weapon _weapon;
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
        rb2d = GetComponent<Rigidbody2D>();
       
        foreach (Transform item in liveImagesUIParent)
        {
            lives.Add(item.GetComponent<Image>());
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
            return;
        //get input and move
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input.Normalize();
        movementVector = speed * input;  

        //shooting
        if (Input.GetKey(KeyCode.Space) &&_weapon!= null)
        {
            _weapon.PerformAttack();
        }

        if (Input.GetKey(KeyCode.Q) && _weapon != null)
        {
            _weapon.SwapWeapon();
        }
    }

    private void FixedUpdate()
    {
        Vector2 newPosition = rb2d.position + movementVector * Time.fixedDeltaTime;
        if (screenBounds.AmIOutOfBounds(newPosition) == false)
        {
            rb2d.MovePosition(newPosition);
            //transform.Translate(tempPosition - transform.position);
        }
    }



    public void ReduceLives()
    {
        health--;
        for (int i = 0; i < lives.Count; i++)
        {
            if (i >= health)
            {
                lives[i].color = Color.black;
            }
            else
            {
                lives[i].color = Color.white;
            }

        }
        if (health <= 0)
        {
            isAlive = false;
            hitSource.PlayOneShot(deathClip);
            GetComponent<Collider2D>().enabled = false;
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            StartCoroutine(DestroyCoroutine());
        }
        else
        {
            hitSource.PlayOneShot(hitClip);
        }
    }




    public void GetHitFeedback()
    {
        hitSource.PlayOneShot(hitClip);
    }

    public void Death()
    {
        isAlive = false;
        hitSource.PlayOneShot(deathClip);
        GetComponent<Collider2D>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        StartCoroutine(DestroyCoroutine());
    }

    public void UpdateUI()
    {
        for (int i = 0; i < lives.Count; i++)
        {
            if (i >= _health.currentHealth)
            {
                lives[i].color = Color.black;
            }
            else
            {
                lives[i].color = Color.white;
            }

        }
    }

    private IEnumerator DestroyCoroutine()
    {
        Instantiate(explosionFX, transform.position, Quaternion.identity); ;
        yield return new WaitForSeconds(deathClip.length);
        Destroy(gameObject);
        loseScreen.Toggle();
        menuButton.interactable = false;
    }

    void GetHealth() 
    {
        if (_health == null)
            _health = GetComponent<Health>();

        _health.InitializeHealth(_initializeHealthValue);
    }
}
