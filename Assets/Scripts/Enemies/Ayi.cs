using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ayi : Enemy
{
    public Sprite kaya1;
    public Sprite kaya2;

    float rotationSpeed;
    public override void Start()
    {
        base.Start();
        rotationSpeed = Random.Range(0.3f, 2.0f);
        float x = Random.Range(0.0f, 1.0f);
        if(x > 0.5f)
        {
            renderer.sprite = kaya1;
        }
        else
        {
            renderer.sprite = kaya2;
        }
    }

    public override void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime * 100);
        base.Update();
    }

    public override void OnHitConnection()
    {
        Kill();
    }

    public override void OnHitPlayer()
    {
        spawner.OnKillPlayer();
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
