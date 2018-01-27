using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static float xMin = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
    public static float xMax = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0, 0)).x;
    public static float yMin = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
    public static float yMax = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, 0)).y;
}

public class EnemySpawner : MonoBehaviour
{
    float targetDifficulty;
    float accumulatedTime;
    float currentDifficulty;

    EnemyType nextEnemyType;

    Dictionary<EnemyType, float> enemyTypeToDifficultyMap = new Dictionary<EnemyType, float>();

    List<Enemy> allEnemies = new List<Enemy>();

    public GameManager gameManager;
    public GameObject[] enemyPrefabs;

    public GameObject killByLaserParticlePrefab;

    public float totalScoreCollected;

    void Start ()
    {
        totalScoreCollected = 0;
        currentDifficulty = 0;
        enemyTypeToDifficultyMap.Add(EnemyType.Ayi, 1.0f);
        enemyTypeToDifficultyMap.Add(EnemyType.Ayiogluayi, 2.0f);
        enemyTypeToDifficultyMap.Add(EnemyType.Medic, 1.5f);
        enemyTypeToDifficultyMap.Add(EnemyType.Gunner, 2.5f);
        enemyTypeToDifficultyMap.Add(EnemyType.RPGGunner, 3.0f);

        nextEnemyType = (EnemyType)Random.Range(0, (int)EnemyType.Count);
    }
	
	void Update ()
    {
        if(gameManager.state == GameManager.State.Game)
        {
            accumulatedTime = accumulatedTime + Time.deltaTime;
            targetDifficulty = Mathf.Max((Mathf.Pow(accumulatedTime, 0.6f)), 2);
            if (currentDifficulty < targetDifficulty - 1)
            {
                Enemy newEnemy = GetNextEnemy();
                if (newEnemy != null)
                {
                    newEnemy.spawner = this;

                    float randomValue = Random.Range(0.0f, 1.0f);

                    Vector2 entryPoint;
                    Vector2 exitPoint;
                    float minMult = 0.8f;
                    float maxMult = 0.8f;
                    if (randomValue < 0.25f)
                    {
                        entryPoint = new Vector2(Utils.xMin, Random.Range(Utils.yMin * minMult, Utils.yMax * maxMult));
                        exitPoint = new Vector2(Utils.xMax, Random.Range(Utils.yMin * minMult, Utils.yMax * maxMult));
                    }
                    else if (randomValue < 0.5f)
                    {
                        entryPoint = new Vector2(Utils.xMax, Random.Range(Utils.yMin * minMult, Utils.yMax * maxMult));
                        exitPoint = new Vector2(Utils.xMin, Random.Range(Utils.yMin* minMult, Utils.yMax * maxMult));
                    }
                    else if (randomValue < 0.75f)
                    {
                        entryPoint = new Vector2(Random.Range(Utils.xMin * minMult, Utils.xMax * maxMult), Utils.yMax);
                        exitPoint = new Vector2(Random.Range(Utils.xMin * minMult, Utils.xMax * maxMult), Utils.yMin);
                    }
                    else
                    {
                        entryPoint = new Vector2(Random.Range(Utils.xMin * minMult, Utils.xMax * maxMult), Utils.yMin);
                        exitPoint = new Vector2(Random.Range(Utils.xMin * minMult, Utils.xMax * maxMult), Utils.yMax);
                    }

                    newEnemy.transform.position = entryPoint;
                    newEnemy.moveDirection = (exitPoint - entryPoint).normalized;

                    currentDifficulty = currentDifficulty + newEnemy.GetDifficulty();

                    allEnemies.Add(newEnemy);
                }
            }
        }
	}

    public void OnKillPlayer()
    {
        gameManager.OnGameOver();
    }

    public void ClearAll()
    {
        for(int i = 0; i < allEnemies.Count; i++)
        {
            GameObject.Destroy(allEnemies[i].gameObject);
        }
        allEnemies.Clear();
        accumulatedTime = 0;
        currentDifficulty = 0;
    }

    IEnumerator KillByLaserCoroutine(Vector3 position)
    {
        ParticleSystem particleSystem = GameObject.Instantiate(killByLaserParticlePrefab).GetComponent<ParticleSystem>();
        particleSystem.transform.position = position;
        particleSystem.Play();
        yield return new WaitForSeconds(1.3f);
        GameObject.Destroy(particleSystem.gameObject);
    }

    public void OnEnemyEnraged(Enemy enemy)
    {
        //         Vector2 randomVector = Random.insideUnitCircle.normalized;
        //         enemy.moveDirection = randomVector;
        //         enemy.transform.position = -randomVector * 5.0f;
        //         enemy.isEnraged = true;

        //if(!enemy.isEnraged)
        {
            currentDifficulty = currentDifficulty - enemy.GetDifficulty();
        }

        enemy.moveDirection = -enemy.moveDirection;
        enemy.renderer.color = Color.red;
        enemy.isEnraged = true;

    }

    public void OnEnemyKilled(Enemy enemy)
    {
        StartCoroutine(KillByLaserCoroutine(enemy.transform.position));
        allEnemies.Remove(enemy);
        if(enemy.isEnraged == false)
        {
            currentDifficulty = currentDifficulty - enemy.GetDifficulty();
        }
        totalScoreCollected = totalScoreCollected + enemy.GetDifficulty();
        GameObject.Destroy(enemy.gameObject);
    }

    Enemy GetNextEnemy()
    {
        Enemy nextEnemy = null;
        float remainingDifficultyCap = targetDifficulty - currentDifficulty;
        if(enemyTypeToDifficultyMap[nextEnemyType] < remainingDifficultyCap)
        {
            nextEnemy = GameObject.Instantiate(enemyPrefabs[(int)nextEnemyType]).GetComponent<Enemy>();
            nextEnemyType = (EnemyType)Random.Range(0, (int)EnemyType.Count);
        }
        else if (allEnemies.Count == 0)
        {
            nextEnemyType = EnemyType.Ayi;
            nextEnemy = GameObject.Instantiate(enemyPrefabs[(int)nextEnemyType]).GetComponent<Enemy>();
            nextEnemyType = (EnemyType)Random.Range(0, (int)EnemyType.Count);
        }
        return nextEnemy;
    }

    
}
