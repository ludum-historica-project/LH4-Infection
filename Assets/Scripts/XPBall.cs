using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPBall : MonoBehaviour
{

    CharacterController character;
    Rigidbody2D _rb2D;
    float _travelTime;
    // Start is called before the first frame update
    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _rb2D.AddForce(transform.up * Random.Range(1, 5) / Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (character != null)
        {
            _travelTime += Time.deltaTime;
            _rb2D.velocity = _rb2D.velocity.magnitude * Vector3.Slerp(_rb2D.velocity.normalized, (character.transform.position - transform.position).normalized, _travelTime / 5)
                + (character.transform.position - transform.position).normalized * _travelTime * 10;

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (character == null)
        {
            character = collision.GetComponent<CharacterController>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<CharacterController>())
        {
            Director.GetManager<UpgradesManager>().AddXP(1);
            Destroy(gameObject);
        }
    }
}
