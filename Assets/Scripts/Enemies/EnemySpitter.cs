using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpitter : Enemy
{

    public float shootCooldown = 1;
    public float range;
    public Projectile spitterProjectilePrefab;

    private float _currentShootCooldown = 0;
    private CharacterController _target;

    float _roamDirection;

    protected override void Start()
    {
        base.Start();
        _roamDirection = Random.Range(-1f, 1);
    }

    protected override void Update()
    {
        base.Update();
        if (_target && _aggro)
        {
            Vector2 myPos = transform.position;
            Vector2 targetPos = _target.transform.position;
            _direction = Vector3.ProjectOnPlane(_target.transform.position - transform.position, Vector3.forward).normalized;
            if (Vector2.Distance(myPos, targetPos) > range)
            {
                MoveTo(transform.position + _direction * Time.deltaTime * speed);
            }
            else
            {
                //spit
                if (_currentShootCooldown <= 0)
                {
                    Shoot();
                    _currentShootCooldown = shootCooldown;
                }
                else
                {
                    _currentShootCooldown -= Time.deltaTime;
                }
            }

        }
        else
        {
            _direction = Vector3.RotateTowards(_direction, transform.right * _roamDirection, rotationSpeed * Mathf.Deg2Rad * Time.deltaTime / 10, 1).normalized;
            MoveTo(transform.position + _direction * Time.deltaTime * speed);
        }
        transform.up = Vector3.RotateTowards(transform.up, _direction, rotationSpeed * Mathf.Deg2Rad * Time.deltaTime, 1);
        transform.up = Vector3.ProjectOnPlane(transform.up, Vector3.forward).normalized;
    }

    void Shoot()
    {
        Instantiate(spitterProjectilePrefab, transform.position, Quaternion.identity).transform.up = transform.up;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CharacterController>())
        {
            _target = collision.GetComponent<CharacterController>();
        }
    }
}
