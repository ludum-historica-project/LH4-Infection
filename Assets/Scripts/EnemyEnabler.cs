using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEnabler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().isTrigger) return;
        if (collision.GetComponent<Enemy>())
        {
            collision.GetComponent<Enemy>().EnableAggression();
        }
    }
}
