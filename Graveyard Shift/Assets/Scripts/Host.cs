using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Host : MonoBehaviour
{
    public enum Controller{ Empty, Player1, Player2, Player3};
    public Controller ControllerSel;

    public bool Dead = false;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GM.instance.Hosts.Contains(this.gameObject) == false && Dead == false)
        {
            GM.instance.Hosts.Add(this.gameObject);
        }

        //NPC AI
        if (ControllerSel == Controller.Empty)
        {
            if (!Dead)
            {

            }
            else
            {
                if (transform.localEulerAngles.x < 90)
                {
                    transform.Rotate(Vector3.right * 1);
                    transform.Translate(Vector3.forward * 0.02f);
                }

                if (GM.instance.Hosts.Contains(this.gameObject))
                {
                    GM.instance.Hosts.Remove(this.gameObject);
                }
            }
        }

        //Player Controls
        if (ControllerSel == Controller.Player1)
        {
            if (GM.instance.Host1 == this.gameObject)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    transform.Translate(Vector3.forward * GM.instance.HostSpeed);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    transform.Translate(Vector3.back * GM.instance.HostSpeed);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    transform.Translate(Vector3.left * GM.instance.HostSpeed);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    transform.Translate(Vector3.right * GM.instance.HostSpeed);
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    GM.instance.Host1 = null;
                    Dead = true;
                }
            }
            else
            {
                ControllerSel = Controller.Empty;
            }
        }

        if (ControllerSel == Controller.Player2)
        {
            if (GM.instance.Host2 == this.gameObject)
            {
                if (Input.GetKey(KeyCode.I))
                {
                    transform.Translate(Vector3.forward * GM.instance.HostSpeed);
                }
                if (Input.GetKey(KeyCode.K))
                {
                    transform.Translate(Vector3.back * GM.instance.HostSpeed);
                }
                if (Input.GetKey(KeyCode.J))
                {
                    transform.Translate(Vector3.left * GM.instance.HostSpeed);
                }
                if (Input.GetKey(KeyCode.L))
                {
                    transform.Translate(Vector3.right * GM.instance.HostSpeed);
                }

                if (Input.GetKeyDown(KeyCode.O))
                {
                    GM.instance.Host2 = null;
                    Dead = true;
                }
            }
            else
            {
                ControllerSel = Controller.Empty;
            }
        }

        if (ControllerSel == Controller.Player3)
        {
            if (GM.instance.Host3 == this.gameObject)
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    transform.Translate(Vector3.forward * GM.instance.HostSpeed);
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    transform.Translate(Vector3.back * GM.instance.HostSpeed);
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.Translate(Vector3.left * GM.instance.HostSpeed);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    transform.Translate(Vector3.right * GM.instance.HostSpeed);
                }

                if (Input.GetKeyDown(KeyCode.RightControl))
                {
                    GM.instance.Host3 = null;
                    Dead = true;
                }
            }
            else
            {
                ControllerSel = Controller.Empty;
            }
        }
    }
}
