using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int PlayerName;
    public float Speed;
    public Connection Connection;
    private Rigidbody rb;

    public GameManager gameManager;

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

            rb.velocity = new Vector3(x, y, 0).normalized * Speed;

            if(Input.GetKeyUp(KeyCode.Space))
            {
                Connection.IsConnected = false;
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (gameManager.state == GameManager.State.Game)
        {
            Enemy collidedEnemy = other.gameObject.GetComponent<Enemy>();
            if (collidedEnemy != null)
            {
                collidedEnemy.OnHitPlayer();
            }
        }
    }
}
