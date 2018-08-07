using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movetest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        ///*
        Vector3 NextDir = new Vector3(Input.GetAxisRaw("P1 Horizontal"), 0, -Input.GetAxisRaw("P1 Vertical"));
        if (NextDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(NextDir);

            transform.Translate(Vector3.forward * 0.1f);
        }
        //*/

        /*
        float x = Input.GetAxis("P1 Horizontal");
        float y = Input.GetAxis("P1 Vertical");
        if (x != 0.0f || y != 0.0f)
        {
            float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, angle, 0);
            transform.Translate(Vector3.forward * 0.1f);
            // Do something with the angle here.
        }
        */
    }
}
