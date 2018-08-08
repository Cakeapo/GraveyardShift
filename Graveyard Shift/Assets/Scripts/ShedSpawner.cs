using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShedSpawner : MonoBehaviour {

    public GameObject shed;

	// Use this for initialization
	void Start ()
    {
        Instantiate(shed, transform.position, transform.rotation);
	}
	
}
