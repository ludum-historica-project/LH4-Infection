using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPBall : MonoBehaviour
{
    public float collectionRange = 4;

    CharacterController character;
    Rigidbody2D _rb2D;
    float _travelTime;
    // Start is called before the first frame update
    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _rb2D.AddForce(transform.up * Random.Range(1, 5) / TimeManager.deltaTime);
        character = FindObjectOfType<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeManager.paused) return;
        if (character != null && Vector3.Distance(transform.position, character.transform.position) < collectionRange)
        {
            _travelTime += TimeManager.deltaTime;
            _rb2D.velocity = _rb2D.velocity.magnitude * Vector3.Slerp(_rb2D.velocity.normalized, (character.transform.position - transform.position).normalized, _travelTime / 5)
                + (character.transform.position - transform.position).normalized * _travelTime * 10;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CharacterController>())
        {
            Director.GetManager<UpgradesManager>().AddXP(1);
            Destroy(gameObject);
        }
    }
}
