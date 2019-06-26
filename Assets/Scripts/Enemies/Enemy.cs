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

    protected bool _aggro;

    public System.Action<int, Vector3> OnDeath = (xp, pos) => { };

    protected Vector3 _direction;
    protected System.Action OnTakeDamage = () => { };

    protected SpriteRenderer[] _spriteRenderers;
    protected Rigidbody2D _rb2D;



    protected virtual void Start()
    {
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        _rb2D = GetComponent<Rigidbody2D>();
        _direction = transform.up;
    }

    protected void MoveTo(Vector3 position)
    {
        _rb2D.MovePosition(position);
    }

    protected virtual void Update()
    {
        foreach (var spriteRenderer in _spriteRenderers)
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, Color.white, .1f);
        }
    }

    public void TakeDamage(float damage)
    {
        OnTakeDamage();
        health -= (int)damage;
        foreach (var spriteRenderer in _spriteRenderers)
        {
            spriteRenderer.color = Color.red;
        }
        if (health <= 0)
        {
            Kill();
        }
    }

    public void EnableAggression()
    {
        _aggro = true;
    }

    private void Kill()
    {
        Destroy(gameObject);
        OnDeath(xp, transform.position);
    }
}
