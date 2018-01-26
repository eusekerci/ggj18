using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AyiOgluAyi : Enemy
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
        spawner.OnKillPlayer();
    }

    public override void Enrage()
    {
        Kill();
    }

    public override float GetDifficulty()
    {
        return 2.0f;
    }

    public override EnemyType GetEnemyType()
    {
        return EnemyType.Ayiogluayi;
    }
}
