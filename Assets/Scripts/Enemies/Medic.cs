using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medic : Enemy
{
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void OnHitConnection()
    {
        spawner.OnKillPlayer();
    }

    public override void OnHitPlayer()
    {
        Kill();
    }

    public override float GetDifficulty()
    {
        return 1.5f;
    }

    public override EnemyType GetEnemyType()
    {
        return EnemyType.Medic;
    }
}
