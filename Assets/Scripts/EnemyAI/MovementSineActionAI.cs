using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YK.EnemyAI
{
    public class MovementSineActionAI : MonoBehaviour
    {
        Rigidbody2D _rigidbody2D;
        public float speedY = 2f;

        public float amplitude = 1f, frequency = 2f;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Vector2 pos = _rigidbody2D.position;

            float sinVal = Mathf.Sin(pos.y * frequency) * amplitude;

            Vector2 direction = Vector2.right * sinVal + Vector2.down;

            _rigidbody2D.MovePosition(_rigidbody2D.position + direction * speedY * Time.deltaTime);
        }
    }
}