using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public bool walking;
    CharacterController _target;
    public float range;
    public Transform slamCenter;
    public float slamRadius;

    public float cooldown;

    public ParticleSystem slamParticle;

    int _attackCount;
    float _currentCooldown;

    Animator _animator;
    protected override void Start()
    {
        base.Start();
        _target = FindObjectOfType<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        if (_target)
        {
            _direction = (_target.transform.position - transform.position);
            _direction.z = 0;
            _direction.Normalize();
            transform.up = Vector3.RotateTowards(transform.up, _direction, rotationSpeed * Mathf.Deg2Rad * TimeManager.deltaTime, 1).normalized;
            if (Vector3.Distance(_target.transform.position, transform.position) < range)//attack
            {
                _animator.SetBool("Walking", false);
                if (_currentCooldown <= 0)
                {
                    if (_attackCount % 3 == 2)
                    {
                        _animator.SetTrigger("Slam");
                    }
                    else
                    {
                        _animator.SetTrigger("Punch");
                    }
                    _currentCooldown = cooldown;
                    _attackCount++;
                }
                else
                {
                    _currentCooldown -= TimeManager.deltaTime;
                }
            }
            else
            {
                _currentCooldown = Mathf.Clamp(_currentCooldown + TimeManager.deltaTime / 10, 0, cooldown);
                _animator.SetBool("Walking", true);
                MoveTo(transform.position + _direction * TimeManager.deltaTime * speed);
            }
        }
        else
        {
            _animator.SetBool("Walking", false);

        }
    }
    public void PunchPlayer()
    {
        _target.TakeDamage(damage, this);
    }

    public void SlamFloor()
    {
        if (Physics2D.OverlapCircle(slamCenter.position, slamRadius, 1 << 8))
        {
            _target.TakeDamage(damage, this);
        }
        slamParticle.Play();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = (Color.red + Color.yellow) / 2;
        Gizmos.DrawWireSphere(slamCenter.position, slamRadius);

    }
}
