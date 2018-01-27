using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGGunner : Enemy
{
    public Transform Player;

    public override void Start()
    {
        base.Start();
        int x = Random.Range(1, 3);
        string a = x.ToString();
        Player = GameObject.Find("Player" + a).transform;
    }

    public override void Update()
    {
        base.Update();
        transform.up = Player.transform.position - transform.position;
    }

    public override float GetDifficulty()
    {
        return 3.0f;
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
        moveDirection = (Player.transform.position - transform.position).normalized;
        base.HandleMovement();
    }

    public override EnemyType GetEnemyType()
    {
        return EnemyType.RPGGunner;
    }
}
