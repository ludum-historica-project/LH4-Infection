using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Enemy roamerPrefab;
    public Enemy chaserPrefab;
    public Enemy spitterPrefab;

    public Boss bossPrefab;
    public XPBall xpBallPrefab;

    public EnemyLimits limits;

    public float cooldown = 3;

    public GameOverScreen gameOverScreen;


    private float currentCooldown = 5;

    public int roamerCount = 1;
    public int chaserCount = 0;
    public int spitterCount = 0;

    List<Enemy> _listRoamers = new List<Enemy>();
    List<Enemy> _listChasers = new List<Enemy>();
    List<Enemy> _listSpitters = new List<Enemy>();

    bool _bossDead;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 200; i++)
        {
            Debug.DrawLine(GetRandomSpawnPosition(), Vector3.zero, Color.red, 5);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCooldown <= 0)
        {
            SpawnEnemies();
            currentCooldown = cooldown;
        }
        else
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    public void SpawnBoss()
    {
        Debug.Log("Spawning Boss");
        var boss = Instantiate(bossPrefab, GetRandomSpawnPosition(), Quaternion.identity);
        boss.OnDeath += OnEnemyDeath;
        boss.OnDeath += OnBossDeath;
        GetComponent<Animator>().enabled = false;
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector2 limitsSize = limits.transform.localScale;

        Vector3 spawn = Quaternion.AngleAxis(Random.Range(0, 360f), Vector3.forward) * Vector3.up;

        float horizontalMult = ((limitsSize.x / 2) / Mathf.Abs(spawn.x));
        float verticalMult = ((limitsSize.y / 2) / Mathf.Abs(spawn.y));

        Vector3 closestHorizontal = spawn * horizontalMult;
        Vector3 closestVertical = spawn * verticalMult;
        return closestHorizontal.magnitude > closestVertical.magnitude ? closestVertical : closestHorizontal;
    }


    void SpawnEnemies()
    {
        CleanLists();
        if (_listRoamers.Count < roamerCount)
        {
            _listRoamers.Add(GetEnemy(roamerPrefab));
        }

        if (_listChasers.Count < chaserCount)
        {
            _listChasers.Add(GetEnemy(chaserPrefab));
        }

        if (_listSpitters.Count < spitterCount)
        {
            _listSpitters.Add(GetEnemy(spitterPrefab));
        }
    }

    Enemy GetEnemy(Enemy prefab)
    {
        Enemy newEnemy = Instantiate(prefab, GetRandomSpawnPosition(),
                 Quaternion.identity);
        Vector3 up = new Vector3(Random.Range(-5f, 5), Random.Range(-5f, 5)) - newEnemy.transform.position;
        up.z = 0;
        newEnemy.transform.up = up;
        newEnemy.OnDeath += OnEnemyDeath;
        return newEnemy;
    }

    void OnEnemyDeath(int xp, Vector3 pos)
    {
        for (int i = 0; i < xp; i++)
        {
            Instantiate(xpBallPrefab, pos, Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward));
        }
    }

    void OnBossDeath(int xp, Vector3 pos)
    {
        roamerCount = chaserCount = spitterCount = 0;
        _bossDead = true;
    }

    void CleanLists()
    {
        _listRoamers.RemoveAll(item => item == null);
        _listChasers.RemoveAll(item => item == null);
        _listSpitters.RemoveAll(item => item == null);
        Debug.Log("Count: " + (_listRoamers.Count + _listChasers.Count + _listSpitters.Count) + ", BossDead: " + _bossDead);
        if (_bossDead && (_listRoamers.Count + _listChasers.Count + _listSpitters.Count) <= 0)
        {
            Debug.Log("Passed");

            if (gameOverScreen != null)
            {
                gameOverScreen.Win();
                gameOverScreen = null;
            }
        }
    }
}
