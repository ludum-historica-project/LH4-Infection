using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CharacterController : MonoBehaviour
{
    public float speed = 5;

    public FloatReference maxHealth;
    public float currentHealth;

    public ScriptableEvent playerHealthUpdated;

    public bool locked = false;

    private Rigidbody2D _rb2D;
    private Animator _animator;


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rb2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth.Value;
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        if (!locked)
        {

            Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (direction.magnitude > 1)
            {
                direction = direction.normalized;
            }
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.up = Vector3.Slerp(transform.up, (mousePos - transform.position).normalized, .333f);
            _rb2D.MovePosition(transform.position + direction * Time.deltaTime * speed);
            if (Input.GetMouseButtonDown(0))
            {
                _animator.SetTrigger("Swing");
            }
        }
    }

    public void HitEnemy(Enemy enemy)
    {
        enemy.TakeDamage(2);
    }

    public void TakeDamage(int damage)
    {

        currentHealth -= damage;
        if (playerHealthUpdated)
        {
            playerHealthUpdated.Invoke(currentHealth);
        }
    }
}
