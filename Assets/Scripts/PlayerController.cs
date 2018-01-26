using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int PlayerName;
    public float Speed;
    private Rigidbody rb;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
        float x = PlayerName == 1 ? Input.GetAxis("Horizontal2") : Input.GetAxis("Horizontal");
        float y = PlayerName == 1 ? Input.GetAxis("Vertical2") : Input.GetAxis("Vertical");

        rb.velocity = new Vector3(x, y, 0).normalized * Speed;
	}
}
