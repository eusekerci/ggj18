using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBG : MonoBehaviour {

	void Start ()
    {
		
	}

	void Update ()
    {
        transform.Rotate(Vector3.forward, 0.02f, Space.World);
	}
}
