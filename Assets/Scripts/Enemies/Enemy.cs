using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


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
    public float speed = 1.7f;
    public bool isEnraged = false;
    public EnemySpawner spawner;
    public SpriteRenderer renderer;

	public virtual void Start ()
    {

    }

    public virtual void Update ()
    {
        HandleMovement();
        CheckBounds();

    }

    public abstract float GetDifficulty();
    public abstract EnemyType GetEnemyType();
    public abstract void OnHitConnection();
    public abstract void OnHitPlayer();

    public virtual void Enrage()
    {
        spawner.OnEnemyEnraged(this);
    }

    public virtual void Kill()
    {
        spawner.OnEnemyKilled(this);
    }
    
    public void CheckBounds()
    {
        if(transform.position.x > Utils.xMax || transform.position.x < Utils.xMin || transform.position.y > Utils.yMax || transform.position.y < Utils.yMin)
        {
            Enrage();
        }
    }

    public virtual void HandleMovement()
    {
        transform.position = transform.position + moveDirection * speed * Time.deltaTime * (isEnraged ? 2 : 1);
    }

    public static Enemy CreateWithType(EnemyType enemyType)
    {
        Enemy newEnemy = null;

        return newEnemy;
    }
}
