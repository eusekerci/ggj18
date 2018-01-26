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

    public GameObject Lazer;
    public float LazerMaksWidth;
    public float LazerMinWidth;

    void Start ()
    {

	}
	
	void Update ()
    {
        OneToTwoDirection = (PlayerTwo.position - PlayerOne.position).normalized;
        CurrentDistance = (PlayerOne.position - PlayerTwo.position).magnitude;
        if(IsConnected)
        {
            Lazer.SetActive(true);
            Lazer.transform.position = (PlayerOne.position + PlayerTwo.position) / 2.0f;
            Lazer.transform.localScale = new Vector3(1, LazerMinWidth + (CurrentDistance / MaxDistance) * (LazerMaksWidth - LazerMinWidth), CurrentDistance);
            Lazer.transform.LookAt(PlayerTwo);


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
            Lazer.SetActive(false);
            if (CurrentDistance < MinRangeToReconnect)
            {
                IsConnected = true;
            }
        }
	}
}
