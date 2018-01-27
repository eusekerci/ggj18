using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;

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

    public LightningBoltScript lightning;
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
            lightning.gameObject.SetActive(true);
            lightning.transform.position = (PlayerOne.position + PlayerTwo.position) / 2.0f;
            lightning.GetComponent<LineRenderer>().widthMultiplier = LazerMinWidth + (CurrentDistance / MaxDistance) * (LazerMaksWidth - LazerMinWidth);
            //lightning.transform.localScale = new Vector3(1, LazerMinWidth + (CurrentDistance / MaxDistance) * (LazerMaksWidth - LazerMinWidth), CurrentDistance);
            lightning.transform.LookAt(PlayerTwo);


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
            lightning.gameObject.SetActive(false);
            if (CurrentDistance < MinRangeToReconnect)
            {
                IsConnected = true;
            }
        }
	}
}
