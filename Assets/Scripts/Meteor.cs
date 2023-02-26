using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour, IHittable
{
    Rigidbody2D _rigidBody2D;

    float _speed = 1f;

    [SerializeField] Transform _meteorSprtie;

    [SerializeField] float _rotationSpeed=15f;

    Vector2 _direction = Vector2.down;
    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        _meteorSprtie.Rotate(new Vector3(0, 0, _rotationSpeed * Time.deltaTime));
    }
    private void FixedUpdate()
    {
        _rigidBody2D.MovePosition(_rigidBody2D.position + _direction * _speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out IHittable hitable))
        {
            if (hitable != null)
                hitable.GetHit(1, gameObject);
        }
    }
    void Death() => Destroy(gameObject);
    public void GetHit(int damage, GameObject sender)
    {
        if (sender!= null)
        {
            Vector2 newDirection = transform.position - sender.transform.position;
            _direction = newDirection;
        }
    }
}
