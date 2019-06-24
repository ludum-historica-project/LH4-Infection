using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3;
    SpriteRenderer _spriteRenderer;
    protected System.Action OnTakeDamage = () => { };
    public virtual int Damage
    {
        get
        {
            return 3;
        }
    }
    public virtual void Start()
    {
        transform.up = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward) * transform.up;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void Update()
    {
        _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, Color.white, .1f);
    }

    public void TakeDamage(int damage)
    {
        OnTakeDamage();
        health -= damage;
        _spriteRenderer.color = Color.red;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
