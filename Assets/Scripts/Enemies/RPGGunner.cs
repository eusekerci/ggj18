using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGGunner : Enemy
{
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public override float GetDifficulty()
    {
        return 3.0f;
    }

    public override void HandleMovement()
    {

        base.HandleMovement();
    }

    public override EnemyType GetEnemyType()
    {
        return EnemyType.RPGGunner;
    }
}
