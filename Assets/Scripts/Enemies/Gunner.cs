using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : Enemy
{
    public enum GunnerState
    {
        Run,
        Fire
    }

    public GunnerState State;
    public GameObject GunPrefab;
    public Transform Player;
    public float FirePerSec;
    public float StateChangePerSec;

    private float fireTime;
    private float stateChangeTime;

    public override void Start()
    {
        base.Start();

        State = GunnerState.Run;

        int x = Random.Range(1, 3);
        string a = x.ToString();
        Player = GameObject.Find("Player" + a).transform;

        fireTime = 0;
        stateChangeTime = 0;
    }

    public override void Update()
    {
        if(State == GunnerState.Run)
        {
            base.Update();
        }
        else
        {
            Fire();
        }

        if (stateChangeTime < Time.time)
        {
            stateChangeTime = Time.time + StateChangePerSec;
            float rand = Random.Range(0.0f, 1.0f);
            if (rand < 0.4f)
            {
                State = GunnerState.Fire;
            }
            else
            {
                State = GunnerState.Run;
            }
        }
    }

    public override float GetDifficulty()
    {
        return 2.5f;
    }

    public override void OnHitConnection()
    {
        Kill();
    }

    public override void OnHitPlayer()
    {
        spawner.OnKillPlayer();
    }

    public override void HandleMovement()
    {
        base.HandleMovement();
    }

    public override EnemyType GetEnemyType()
    {
        return EnemyType.Gunner;
    }

    public void Fire()
    {
        if (fireTime < Time.time)
        {
            fireTime = Time.time + FirePerSec;
            Enemy nextEnemy = GameObject.Instantiate(GunPrefab, transform.position, Quaternion.identity).GetComponent<Enemy>();
            nextEnemy.moveDirection = (Player.position - transform.position).normalized;
            nextEnemy.spawner = this.spawner;
        }
    }
}
