using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	}
	
	void Update ()
    {
        OneToTwoDirection = (PlayerTwo.position - PlayerOne.position).normalized;
        CurrentDistance = (PlayerOne.position - PlayerTwo.position).sqrMagnitude;
        if(IsConnected)
        {
            if(CurrentDistance > MaxDistance)
            {
                IsConnected = false;
            }

            //Notify when hit an enemy
            RaycastHit hit;
            if(Physics.Raycast(PlayerOne.transform.position, OneToTwoDirection, out hit, CurrentDistance, EnemyLayer))
            {
                Enemy hitEnemy = hit.transform.gameObject.GetComponent<Enemy>();
                hitEnemy.OnHitConnection();
            }
        }
        else
        {
            if(CurrentDistance < MinRangeToReconnect)
            {
                IsConnected = true;
            }
        }
	}
}
