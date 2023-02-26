using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YK.EnemyAI
{
    public class MovementActionAI : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        public float speed = 2;
        public float speedVariation = 0.3f;
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            speed += UnityEngine.Random.Range(0, speedVariation);
        }
        private void FixedUpdate()
        {
            _rigidbody2D.MovePosition(_rigidbody2D.position + Vector2.down * speed * Time.deltaTime);
        }
    }
}