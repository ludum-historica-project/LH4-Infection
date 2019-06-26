using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoamer : Enemy
{
    public float stunDuration = 1;

    float _currentStunTime = 0;
    protected override void Start()
    {
        base.Start();
        OnTakeDamage += Stun;
        _direction = transform.up;
    }

    protected override void Update()
    {
        base.Update();
        if (_currentStunTime <= 0)
        {
            MoveTo(transform.position + _direction * Time.deltaTime * speed);
            transform.up = Vector3.RotateTowards(transform.up, _direction, rotationSpeed * Mathf.Deg2Rad * Time.deltaTime, 1);
        }
        else
        {
            _currentStunTime -= Time.deltaTime;
        }
    }

    void Stun()
    {
        if (_currentStunTime <= 0)
            _currentStunTime = stunDuration;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Stun();
        _direction = Vector3.Reflect(_direction, collision.contacts[0].normal);
        CharacterController character = collision.collider.GetComponent<CharacterController>();
        if (character)
        {
            if (Vector3.Dot(transform.up.normalized, Vector3.ProjectOnPlane((character.transform.position - transform.position), Vector3.forward).normalized) > .5)
                character.TakeDamage(damage);
        }
    }
}
