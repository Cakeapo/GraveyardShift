using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HedgeSpawner : MonoBehaviour {

    public GameObject hedgeBit;

	// Use this for initialization
	void Start ()
    {
		hedgeBit = Resources.Load<GameObject>("Hedges/Hedge_Straight");

        int chance = Random.Range(0, 100);

        if(chance < 25)
        {
            Instantiate(hedgeBit, transform.position, transform.rotation);
        }
    }
	
}
