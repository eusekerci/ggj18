using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            targetDifficulty = Mathf.Max(Mathf.Sqrt(accumulatedTime), 2);
            if (currentDifficulty < targetDifficulty - 1)
            {
                Enemy newEnemy = GetRandomEnemy();
                newEnemy.spawner = this;

                float randomValue = Random.Range(0.0f, 1.0f);
                float cameraSize = Camera.main.orthographicSize;
                Vector2 entryPoint;
                Vector2 exitPoint;
                if(randomValue < 0.25f)
                {
                    entryPoint = new Vector2(-cameraSize, Random.Range(-cameraSize, cameraSize));
                    exitPoint = new Vector2(cameraSize, Random.Range(-cameraSize, cameraSize));
                }
                else if (randomValue < 0.5f)
                {
                    entryPoint = new Vector2(cameraSize, Random.Range(-cameraSize, cameraSize));
                    exitPoint = new Vector2(-cameraSize, Random.Range(-cameraSize, cameraSize));
                }
                else if (randomValue < 0.5f)
                {
                    entryPoint = new Vector2(Random.Range(-cameraSize, cameraSize), cameraSize);
                    exitPoint = new Vector2(Random.Range(-cameraSize, cameraSize), -cameraSize);
                }
                else
                {
                    entryPoint = new Vector2(Random.Range(-cameraSize, cameraSize), cameraSize);
                    exitPoint = new Vector2(Random.Range(-cameraSize, cameraSize), -cameraSize);
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
        enemy.GetComponent<Renderer>().material.color = Color.red;
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
