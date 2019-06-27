using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaser : Enemy
{
    public float fleeTime = 2;
    public float hitCooldown = 1;

    private float _currentFleeTime = 0;
    private float _currentHitCooldown = 0;
    private CharacterController _target;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        OnTakeDamage += Flee;
        _direction = transform.up;
    }

    void Flee()
    {
        _currentFleeTime = fleeTime;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        float speedMult = 1;
        if (_target != null && _aggro)
        {
            _direction = Vector3.ProjectOnPlane((_currentFleeTime <= 0 ?
                _target.transform.position - transform.position :
                transform.position - _target.transform.position), Vector3.forward).normalized;
        }
        else
        {
            _direction = Vector3.SlerpUnclamped(_direction, Vector3.Cross(_direction, Vector3.forward), Random.Range(-.05f, .05f));
            speedMult = .5f;
        }
        transform.up = Vector3.RotateTowards(transform.up, _direction, rotationSpeed * Mathf.Deg2Rad * TimeManager.deltaTime, 1);
        transform.up = Vector3.ProjectOnPlane(transform.up, Vector3.forward).normalized;
        MoveTo(transform.position + _direction * TimeManager.deltaTime * speed * speedMult);
        if (_currentFleeTime > 0) _currentFleeTime -= TimeManager.deltaTime;
        if (_currentHitCooldown > 0) _currentHitCooldown -= TimeManager.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CharacterController>())
        {
            _target = collision.GetComponent<CharacterController>();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CharacterController character = collision.collider.GetComponent<CharacterController>();
        if (character)
        {
            if (_currentHitCooldown <= 0)
            {
                character.TakeDamage(damage, this);
                _currentHitCooldown = hitCooldown;
            }
        }
    }

}
