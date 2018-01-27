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

    Dictionary<EnemyType, float> enemyTypeToDifficultyMap = new Dictionary<EnemyType, float>();

    List<Enemy> allEnemies = new List<Enemy>();

    public GameManager gameManager;
    public GameObject[] enemyPrefabs;

    void Start ()
    {
        currentDifficulty = 0;
        enemyTypeToDifficultyMap.Add(EnemyType.Ayi, 1.0f);
        enemyTypeToDifficultyMap.Add(EnemyType.Ayiogluayi, 2.0f);
        enemyTypeToDifficultyMap.Add(EnemyType.Medic, 1.5f);
        enemyTypeToDifficultyMap.Add(EnemyType.Gunner, 2.5f);
        enemyTypeToDifficultyMap.Add(EnemyType.RPGGunner, 3.0f);
    }
	
	void Update ()
    {
        if(gameManager.state == GameManager.State.Game)
        {
            accumulatedTime = accumulatedTime + Time.deltaTime;
            targetDifficulty = Mathf.Max((Mathf.Pow(accumulatedTime, 0.7f)), 2);
            if (currentDifficulty < targetDifficulty - 1)
            {
                Enemy newEnemy = GetRandomEnemy();
                newEnemy.spawner = this;

                float randomValue = Random.Range(0.0f, 1.0f);
                
                Vector2 entryPoint;
                Vector2 exitPoint;
                if(randomValue < 0.25f)
                {
                    entryPoint = new Vector2(Utils.xMin, Random.Range(Utils.yMin, Utils.yMax));
                    exitPoint = new Vector2(Utils.xMax, Random.Range(Utils.yMin, Utils.yMax));
                }
                else if (randomValue < 0.5f)
                {
                    entryPoint = new Vector2(Utils.xMax, Random.Range(Utils.yMin, Utils.yMax));
                    exitPoint = new Vector2(Utils.xMin, Random.Range(Utils.yMin, Utils.yMax));
                }
                else if (randomValue < 0.75f)
                {
                    entryPoint = new Vector2(Random.Range(Utils.xMin, Utils.xMax), Utils.yMax);
                    exitPoint = new Vector2(Random.Range(Utils.xMin, Utils.xMax), Utils.yMin);
                }
                else
                {
                    entryPoint = new Vector2(Random.Range(Utils.xMin, Utils.xMax), Utils.yMin);
                    exitPoint = new Vector2(Random.Range(Utils.xMin, Utils.xMax), Utils.yMax);
                }

                newEnemy.transform.position = entryPoint;
                newEnemy.moveDirection = (exitPoint - entryPoint).normalized;

                currentDifficulty = currentDifficulty + newEnemy.GetDifficulty();

                allEnemies.Add(newEnemy);
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
        allEnemies.Remove(enemy);
        if(enemy.isEnraged == false)
        {
            currentDifficulty = currentDifficulty - enemy.GetDifficulty();
        }
        GameObject.Destroy(enemy.gameObject);
        
    }

    Enemy GetRandomEnemy()
    {
        List<EnemyType> possibleEnemyTypes = new List<EnemyType>();
        float remainingDifficultyCap = targetDifficulty - currentDifficulty;
        for(int i = 0; i < (int)EnemyType.Count; i++)
        {
            float currentEnemyDif = enemyTypeToDifficultyMap[(EnemyType)i];
            if(currentEnemyDif < remainingDifficultyCap)
            {
                possibleEnemyTypes.Add((EnemyType)i);
            }
        }

        Debug.Assert(possibleEnemyTypes.Count > 0);

        int enemyTypeIndex = (int)possibleEnemyTypes[Random.Range(0, possibleEnemyTypes.Count)];
        Enemy newEnemy = GameObject.Instantiate(enemyPrefabs[enemyTypeIndex]).GetComponent<Enemy>();
        return newEnemy;
    }

    
}
