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

            Vector3 vectorToOther = otherController.transform.position - transform.position;
            transform.up = vectorToOther.normalized;
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
