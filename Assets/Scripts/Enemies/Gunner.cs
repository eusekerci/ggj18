using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : Enemy
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
        return 2.5f;
    }

    public override void OnHitConnection()
    {
        Kill();
    }

    public override void OnHitPlayer()
    {

    }

    public override void HandleMovement()
    {

        base.HandleMovement();
    }

    public override EnemyType GetEnemyType()
    {
        return EnemyType.Gunner;
    }
}
