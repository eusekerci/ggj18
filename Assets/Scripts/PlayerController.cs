using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerName;
    public float speed;
    private Rigidbody rb;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
        float x = playerName == 1 ? Input.GetAxis("Horizontal") : Input.GetAxis("Horizontal2");
        float y = playerName == 1 ? Input.GetAxis("Vertical") : Input.GetAxis("Vertical2");

        rb.velocity = new Vector3(x, y, 0).normalized * speed;
	}
}
