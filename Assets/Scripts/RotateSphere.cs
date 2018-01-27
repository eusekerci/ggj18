using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSphere : MonoBehaviour {

    private float x;
    private float y;
    private float z;

    void Start ()
    {
        x = Random.Range(0.0f, 1.0f);
        y = Random.Range(0.0f, 1.0f);
        z = Random.Range(0.0f, 1.0f);
    }
	
	void Update ()
    {
        transform.Rotate(new Vector3(x, y, z).normalized / 10f);
        x += Time.deltaTime / 10f;
        y += Time.deltaTime / 10f;
        z += Time.deltaTime / 10f;
    }
}
