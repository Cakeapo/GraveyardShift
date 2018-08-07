using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shed : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GM.instance.Sheds.Contains(this.gameObject) == false)
        {
            GM.instance.Sheds.Add(this.gameObject);
        }
    }
}
