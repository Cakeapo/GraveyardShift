using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    //public bool HostDetected = false;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (GM.instance.Hosts.Contains(other.gameObject))
        {
            //HostDetected = true;
            if (GM.instance.Detected.Contains(other.gameObject) == false)
            {
                GM.instance.Detected.Add(other.gameObject);
            }

            /*
            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                other.GetComponent<Host>().Dead = true;
            }
            */
        }
        else
        {
            //HostDetected = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (GM.instance.Detected.Contains(other.gameObject))
        {
            GM.instance.Detected.Remove(other.gameObject);
        }
    }
}
