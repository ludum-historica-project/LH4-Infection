using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3;
    public int damage = 5;
    public int xp = 5;
    public float speed = 2;
    public float rotationSpeed = 90;

    public System.Action<int, Vector3> OnDeath = (xp, pos) => { };

    protected Vector3 _direction;
    protected System.Action OnTakeDamage = () => { };

    protected SpriteRenderer _spriteRenderer;
    protected Rigidbody2D _rb2D;



    protected virtual void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb2D = GetComponent<Rigidbody2D>();
        transform.up = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward) * transform.up;
    }

    protected void MoveTo(Vector3 position)
    {
        _rb2D.MovePosition(position);
    }

    protected virtual void Update()
    {
        if (_spriteRenderer)
            _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, Color.white, .1f);
    }

    public void TakeDamage(float damage)
    {
        OnTakeDamage();
        health -= (int)damage;
        _spriteRenderer.color = Color.red;
        if (health <= 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        OnDeath(xp, transform.position);
        Destroy(gameObject);
    }
}
