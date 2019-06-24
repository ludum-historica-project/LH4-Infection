using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoamer : Enemy
{
    public float speed = 2;
    public float stunDuration = 1;
    public float rotationSpeed = 90;


    float _currentStunTime = 0;
    Vector3 _direction;
    Rigidbody2D _rb2D;
    public override void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        base.Start();
        OnTakeDamage += Stun;
        _direction = transform.up;
    }

    public override void Update()
    {
        base.Update();
        if (_currentStunTime <= 0)
        {
            _rb2D.MovePosition(transform.position + _direction * Time.deltaTime * speed);
            transform.up = Vector3.RotateTowards(transform.up, _direction, rotationSpeed * Mathf.Deg2Rad * Time.deltaTime, 1);
        }
        else
        {
            _currentStunTime -= Time.deltaTime;
        }
    }

    void Stun()
    {
        _currentStunTime = stunDuration;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        _direction = Vector3.Reflect(_direction, collision.contacts[0].normal);
        CharacterController character = collision.collider.GetComponent<CharacterController>();
        if (character)
        {
            Debug.Log(Vector3.Dot(transform.up.normalized, Vector3.ProjectOnPlane((character.transform.position - transform.position), Vector3.forward).normalized));
            character.TakeDamage(Damage);
        }
    }
}
