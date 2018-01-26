using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ConnectionHitTest : MonoBehaviour {

	void Start ()
    {
        MessageBus.OnEvent<OnConnectionHit>().Subscribe(evnt =>
        {
            Destroy(this.gameObject);
        });
	}
	
	void Update ()
    {
		
	}
}
