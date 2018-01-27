using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int PlayerName;
    public float Speed;
    private Rigidbody rb;
    public Connection Connection;

    public PlayerController otherController;

    public GameManager gameManager;
    public GameObject PlayerDeathParticlePrefab;
    public SpriteRenderer Renderer;


    void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
        if(gameManager.state == GameManager.State.Game)
        {
            float x = PlayerName == 1 ? Input.GetAxis("Horizontal2") : Input.GetAxis("Horizontal");
            float y = PlayerName == 1 ? Input.GetAxis("Vertical2") : Input.GetAxis("Vertical");

            if (Mathf.Abs(x) > 0.2f || Mathf.Abs(y) > 0.2f)
                rb.velocity = new Vector3(x, y, 0).normalized * Speed;
            else
                rb.velocity = new Vector3(0, 0, 0).normalized;

            if (Connection.IsConnected)
            {
                Vector3 vectorToOther = otherController.transform.position - transform.position;
                transform.up = vectorToOther.normalized;
            }
            else
            {
                Vector3 velocityVec = new Vector3(x, y, 0).normalized;
                if (rb.velocity.magnitude > 0)
                {
                    transform.up = velocityVec.normalized;
                }
            }
            if(Input.GetKeyUp(KeyCode.Space))
            {
                Connection.IsConnected = false;
            }
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObject = collision.transform.gameObject;
        if (gameManager.state == GameManager.State.Game)
        {
            Enemy collidedEnemy = collidedObject.GetComponent<Enemy>();
            if (collidedEnemy != null)
            {
                collidedEnemy.OnHitPlayer();
                if (collidedEnemy.GetEnemyType() != EnemyType.Medic)
                {
                    StartCoroutine(PlayerDeathLaserCoroutine(transform.position));
                    Renderer.enabled = false;
                }
            }
        }
    }

    IEnumerator PlayerDeathLaserCoroutine(Vector3 position)
    {
        ParticleSystem particleSystem = GameObject.Instantiate(PlayerDeathParticlePrefab).GetComponent<ParticleSystem>();
        particleSystem.transform.position = position;
        particleSystem.Play();
        yield return new WaitForSeconds(1.3f);
        GameObject.Destroy(particleSystem.gameObject);
    }
}
