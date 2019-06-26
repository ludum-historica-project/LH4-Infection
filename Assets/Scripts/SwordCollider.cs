using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollider : MonoBehaviour
{
    public CharacterController character;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger) return;
        if (collision.GetComponent<Enemy>())
            character.HitEnemy(collision.GetComponent<Enemy>());
    }

}
