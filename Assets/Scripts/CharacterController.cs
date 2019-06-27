using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CharacterController : MonoBehaviour
{
    public FloatReference moveSpeed;
    public FloatReference AttackSpeed;
    public FloatReference attackDamage;
    public FloatReference maxHealth;
    public float currentHealth;

    public SoundValue takeDamageSound;
    public SoundValue swingSwordSound;
    public SoundValue dealDamageSound;

    public ScriptableEvent playerHealthUpdated;
    public ParticleSystem playerDeathParticle;

    public bool locked = false;

    private Rigidbody2D _rb2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;



    Vector2 knockback;


    private List<Enemy> hitEnemies = new List<Enemy>();

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rb2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth.Value;
        _animator.SetFloat("AttackSpeed", AttackSpeed.Value);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (TimeManager.paused) return;
        if (!locked)
        {
            hitEnemies.Clear();
            Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (direction.magnitude > 1)
            {
                direction = direction.normalized;
            }
            direction += (Vector3)knockback;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.up = Vector3.Slerp(transform.up, (mousePos - transform.position).normalized, .333f);
            _rb2D.MovePosition(transform.position + direction * TimeManager.fixedDeltaTime * moveSpeed.Value);
            if (Input.GetMouseButtonDown(0))
            {
                _animator.SetTrigger("Swing");
            }
            knockback = knockback * .9f;
        }
        _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, Color.white, .1f);
    }

    public void HitEnemy(Enemy enemy)
    {
        if (!hitEnemies.Contains(enemy))
        {
            Director.GetManager<SoundManager>().PlaySound(dealDamageSound);
            enemy.TakeDamage(attackDamage.Value);
            hitEnemies.Add(enemy);
        }
    }

    public void PlaySwordSwingSound()
    {
        Director.GetManager<SoundManager>().PlaySound(swingSwordSound);
    }

    public void TakeDamage(int damage, Enemy source)
    {
        Director.GetManager<SoundManager>().PlaySound(takeDamageSound);
        if (source) knockback += ((Vector2)(transform.position - source.transform.position)).normalized * damage / 10;
        _spriteRenderer.color = Color.red;
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        if (playerHealthUpdated)
        {
            playerHealthUpdated.Invoke(currentHealth);
        }
        if (currentHealth <= 0)
        {
            Instantiate(playerDeathParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
