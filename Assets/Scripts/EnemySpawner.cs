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
        accumulatedTime = accumulatedTime + Time.deltaTime;
        targetDifficulty = Mathf.Max(Mathf.Sqrt(accumulatedTime), 2);
        if(currentDifficulty < targetDifficulty - 1)
        {
            Debug.Log("Current diff: " + currentDifficulty + " target: " + targetDifficulty);
            Enemy newEnemy = GetRandomEnemy();

            Vector2 randomVector = Random.insideUnitCircle.normalized;
            newEnemy.moveDirection = randomVector;
            newEnemy.transform.position = -randomVector * 5.0f;

            currentDifficulty = currentDifficulty + newEnemy.GetDifficulty();

            allEnemies.Add(newEnemy);
        }
	}

    public void OnEnemyEnraged(Enemy enemy)
    {
        Vector2 randomVector = Random.insideUnitCircle.normalized;
        enemy.moveDirection = randomVector;
        enemy.transform.position = -randomVector * 5.0f;
        enemy.isEnraged = true;

        currentDifficulty = currentDifficulty - enemy.GetDifficulty();
    }

    public void OnEnemyKilled(Enemy enemy)
    {
        allEnemies.Remove(enemy);
        if(enemy.isEnraged == false)
        {
            currentDifficulty = currentDifficulty - enemy.GetDifficulty();
        }
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
