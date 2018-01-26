using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Ayi,
    Ayiogluayi,
    Medic,
    Gunner,
    RPGGunner,

    Count,
}

public abstract class Enemy : MonoBehaviour
{
    public Vector3 moveDirection;
    public float speed = 1.0f;
    public bool isEnraged = false;
	public virtual void Start ()
    {

	}

    public virtual void Update ()
    {
        HandleMovement();
	}

    public abstract float GetDifficulty();
    public abstract EnemyType GetEnemyType();

    public virtual void HandleMovement()
    {
        transform.position = transform.position + moveDirection * speed * Time.deltaTime;
    }

    public static Enemy CreateWithType(EnemyType enemyType)
    {
        Enemy newEnemy = null;

        return newEnemy;
    }
}
