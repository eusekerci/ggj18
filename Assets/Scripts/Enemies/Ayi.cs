using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ayi : Enemy
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
        return 1.0f;
    }

    public override EnemyType GetEnemyType()
    {
        return EnemyType.Ayi;
    }
}
