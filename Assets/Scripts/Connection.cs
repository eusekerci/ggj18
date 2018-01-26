using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class OnConnectionHit : GGJEvent
{
    public Transform obstacle;
}

public class OnConnectionLost : GGJEvent
{

}

public class OnConnectionEstablish : GGJEvent
{

}

public class Connection : MonoBehaviour
{
    public Transform PlayerOne;
    public Transform PlayerTwo;

    public float MinRangeToReconnect;
    public float MaxDistance;
    public bool IsConnected;
    public float CurrentDistance;
    public Vector3 OneToTwoDirection;
    public LayerMask EnemyLayer;

    void Start ()
    {
        MessageBus.OnEvent<OnConnectionEstablish>().Subscribe(evnt =>
        {
            IsConnected = true;
        });

        MessageBus.OnEvent<OnConnectionLost>().Subscribe(evnt =>
        {
            IsConnected = false;
        });
	}
	
	void Update ()
    {
        OneToTwoDirection = (PlayerTwo.position - PlayerOne.position).normalized;
        CurrentDistance = (PlayerOne.position - PlayerTwo.position).sqrMagnitude;
        if(IsConnected)
        {
            if(CurrentDistance > MaxDistance)
            {
                MessageBus.Publish(new OnConnectionLost());
            }

            //Notify when hit an enemy
            RaycastHit hit;
            if(Physics.Raycast(PlayerOne.transform.position, OneToTwoDirection, out hit, CurrentDistance, EnemyLayer))
            {
                MessageBus.Publish(new OnConnectionHit()
                {
                    obstacle = hit.transform
                });
            }
        }
        else
        {
            if(CurrentDistance < MinRangeToReconnect)
            {
                MessageBus.Publish(new OnConnectionEstablish());
            }
        }
	}
}
