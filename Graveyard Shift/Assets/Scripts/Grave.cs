﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : MonoBehaviour
{
    public enum Owner { Empty, Player1, Player2, Player3 };
    public Owner OwnerSel;

    public GameObject Stone;
    public bool Claimed = false;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        /*
        if (Stone = null)
        {
            Stone = this.gameObject;
        }
        */

        if (GM.instance.Graves.Contains(this.gameObject) == false && Claimed == false)
        {
            GM.instance.Graves.Add(this.gameObject);
        }

        //Player 1 Grave
        if (OwnerSel == Owner.Player1)
        {
            if (GM.instance.Grave1 == this.gameObject)
            {
                if (GM.instance.Host1 != null)
                {
                    if (Vector3.Distance(GM.instance.Host1.transform.position, transform.position) < GM.instance.DetectionRange || GM.instance.Host1.GetComponent<Host>().Victory)
                    {
                        if (Stone.transform.localScale.x < 1.5)
                        {
                            Stone.transform.localScale = Stone.transform.localScale * 1.01f;
                        }
                    }
                    else
                    {
                        if (Stone.transform.localScale.x > 1)
                        {
                            Stone.transform.localScale = Stone.transform.localScale * 0.99f;
                        }
                    }
                }
                else
                {
                    if (Stone.transform.localScale.x > 1)
                    {
                        Stone.transform.localScale = Stone.transform.localScale * 0.99f;
                    }
                }
            }
            else
            {
                OwnerSel = Owner.Empty;
            }
        }

        //Player 2 Grave
        if (OwnerSel == Owner.Player2)
        {
            if (GM.instance.Grave2 == this.gameObject)
            {
                if (GM.instance.Host2 != null)
                {
                    if (Vector3.Distance(GM.instance.Host2.transform.position, transform.position) < GM.instance.DetectionRange || GM.instance.Host2.GetComponent<Host>().Victory)
                    {
                        if (Stone.transform.localScale.x < 1.5)
                        {
                            Stone.transform.localScale = Stone.transform.localScale * 1.01f;
                        }
                    }
                    else
                    {
                        if (Stone.transform.localScale.x > 1)
                        {
                            Stone.transform.localScale = Stone.transform.localScale * 0.99f;
                        }
                    }
                }
                else
                {
                    if (Stone.transform.localScale.x > 1)
                    {
                        Stone.transform.localScale = Stone.transform.localScale * 0.99f;
                    }
                }
            }
            else
            {
                OwnerSel = Owner.Empty;
            }
        }

        //Player 3 Grave
        if (OwnerSel == Owner.Player3)
        {
            if (GM.instance.Grave3 == this.gameObject)
            {
                if (GM.instance.Host3 != null)
                {
                    if (Vector3.Distance(GM.instance.Host3.transform.position, transform.position) < GM.instance.DetectionRange || GM.instance.Host3.GetComponent<Host>().Victory)
                    {
                        if (Stone.transform.localScale.x < 1.5)
                        {
                            Stone.transform.localScale = Stone.transform.localScale * 1.01f;
                        }
                    }
                    else
                    {
                        if (Stone.transform.localScale.x > 1)
                        {
                            Stone.transform.localScale = Stone.transform.localScale * 0.99f;
                        }
                    }
                }
                else
                {
                    if (Stone.transform.localScale.x > 1)
                    {
                        Stone.transform.localScale = Stone.transform.localScale * 0.99f;
                    }
                }
            }
            else
            {
                OwnerSel = Owner.Empty;
            }
        }

    }
}