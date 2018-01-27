using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaManager : MonoBehaviour {

    public Connection Connection;
    public GameManager GameManager;
    public float MaksMana;
    public float CurrentMana;
    public float ManaSpendPerSec;
    public float ManaGainPerSec;

	void Start ()
    {
        CurrentMana = MaksMana / 2.0f;
	}
	
	void FixedUpdate ()
    {
		if(Connection.IsConnected)
        {
            CurrentMana -= ManaSpendPerSec * Time.deltaTime;
        }
        else
        {
            CurrentMana += ManaGainPerSec * Time.deltaTime;
        }

        if(CurrentMana > MaksMana || CurrentMana < 0)
        {
            GameManager.OnGameOver();
        }
	}
}
