using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Enemy roamerPrefab;
    public Enemy chaserPrefab;
    public Enemy spitterPrefab;
    public XPBall xpBallPrefab;

    public Transform[] spawnPoints;

    public float cooldown = 7.5f;

    private float currentCooldown = 5;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentCooldown <= 0)
        {
            Enemy newEnemy = Instantiate(GetRandomEnemyPrefab(), spawnPoints[Random.Range(0, spawnPoints.Length)].position,
                Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward));
            newEnemy.OnDeath += OnEnemyDeath;
            currentCooldown = cooldown;
        }
        else
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    Enemy GetRandomEnemyPrefab()
    {
        if (Random.Range(0, 1) > .5f) return roamerPrefab;
        if (Random.Range(0, 1) > .5f) return chaserPrefab;
        return spitterPrefab;

    }

    void OnEnemyDeath(int xp, Vector3 pos)
    {
        for (int i = 0; i < xp; i++)
        {
            Instantiate(xpBallPrefab, pos, Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward));
        }
    }
}
