using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaManager : MonoBehaviour
{
    public Connection Connection;
    public GameManager GameManager;
    public float MaksMana;
    public float CurrentMana;
    public float ManaSpendPerSec;
    public float ManaGainPerSec;

    public Transform DikeyOne;
    public Transform DikeyTwo;
    public Transform YatayOne;
    public Transform YatayTwo;

    void Start ()
    {
        CurrentMana = MaksMana / 2.0f;
	}
	
	void FixedUpdate ()
    {
		if(Connection.IsConnected)
        {
            CurrentMana += ManaSpendPerSec * Time.deltaTime;
        }
        else
        {
            CurrentMana -= ManaGainPerSec * Time.deltaTime;
        }

        if(CurrentMana > MaksMana || CurrentMana < 0)
        {
            GameManager.OnGameOver();
        }

        DikeyOne.localScale = new Vector3(0.55f, (CurrentMana / MaksMana) * 10f, 0.55f);
        DikeyTwo.localScale = new Vector3(0.55f, (CurrentMana / MaksMana) * 10f, 0.55f);
        YatayOne.localScale = new Vector3((CurrentMana / MaksMana) * 17.7f, 0.55f, 0.55f);
        YatayTwo.localScale = new Vector3((CurrentMana / MaksMana) * 17.7f, 0.55f, 0.55f);
    }
}
