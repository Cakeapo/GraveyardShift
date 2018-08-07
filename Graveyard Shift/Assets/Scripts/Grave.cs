using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : MonoBehaviour
{
    public enum Owner { Empty, Player1, Player2, Player3 };
    public Owner OwnerSel;

    public GameObject Stone;
    public GameObject Mound;
    public bool Claimed = false;
    public bool Dug = false;

    private float StartScale;
    // Use this for initialization
    void Start ()
    {
        if (Stone == null)
        {
            Stone = transform.GetChild(0).gameObject;
        }

        StartScale = Stone.transform.localScale.x;
        Mound = transform.parent.transform.Find("GraveMound").gameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {

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
                    if (Vector3.Distance(GM.instance.Host1.transform.position, transform.position) < GM.instance.DetectionRange &! GM.instance.Host1.GetComponent<Host>().Victory)
                    {
                        if (Stone.transform.localScale.x < StartScale * 1.5f)
                        {
                            Stone.transform.localScale = Stone.transform.localScale * 1.01f;
                        }
                    }
                    else
                    {
                        if (Stone.transform.localScale.x > StartScale * 1)
                        {
                            Stone.transform.localScale = Stone.transform.localScale * 0.99f;
                        }
                    }
                }
                else
                {
                    if (Stone.transform.localScale.x > StartScale * 1)
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
                    if (Vector3.Distance(GM.instance.Host2.transform.position, transform.position) < GM.instance.DetectionRange &! GM.instance.Host2.GetComponent<Host>().Victory)
                    {
                        if (Stone.transform.localScale.x < StartScale * 1.5)
                        {
                            Stone.transform.localScale = Stone.transform.localScale * 1.01f;
                        }
                    }
                    else
                    {
                        if (Stone.transform.localScale.x > StartScale * 1)
                        {
                            Stone.transform.localScale = Stone.transform.localScale * 0.99f;
                        }
                    }
                }
                else
                {
                    if (Stone.transform.localScale.x > StartScale * 1)
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
                    if (Vector3.Distance(GM.instance.Host3.transform.position, transform.position) < GM.instance.DetectionRange &! GM.instance.Host3.GetComponent<Host>().Victory)
                    {
                        if (Stone.transform.localScale.x < StartScale * 1.5)
                        {
                            Stone.transform.localScale = Stone.transform.localScale * 1.01f;
                        }
                    }
                    else
                    {
                        if (Stone.transform.localScale.x > StartScale * 1)
                        {
                            Stone.transform.localScale = Stone.transform.localScale * 0.99f;
                        }
                    }
                }
                else
                {
                    if (Stone.transform.localScale.x > StartScale * 1)
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

        if (!Dug)
        {
            if (GM.instance.keeperPlayerSel == GM.KeeperPlayer.one)
            {
                if (Input.GetButtonDown("P2 A"))
                {
                    if (Vector3.Distance(Mound.transform.position, GM.instance.Host1.transform.position) < GM.instance.DetectionRange && GM.instance.Host1.GetComponent<Host>().Shovel)
                    {
                        if (GM.instance.Grave2 != this && GM.instance.Grave3 != this)
                        {
                            Invoke("Replace", GM.instance.DigTime);
                            /*
                            Instantiate(GM.instance.GraveDug, Mound.transform.position, Quaternion.identity);
                            Mound.GetComponent<Renderer>().enabled = false;
                            Dug = true;
                            */
                        }
                    }
                }
                if (Input.GetButtonDown("P3 A"))
                {
                    if (GM.instance.Grave1 != this && GM.instance.Grave3 != this)
                    {
                        if (Vector3.Distance(Mound.transform.position, GM.instance.Host2.transform.position) < GM.instance.DetectionRange && GM.instance.Host2.GetComponent<Host>().Shovel)
                        {
                            Invoke("Replace", GM.instance.DigTime);
                        }
                    }
                }
                if (Input.GetButtonDown("P4 A"))
                {
                    if (GM.instance.Grave1 != this && GM.instance.Grave2 != this)
                    {
                        if (Vector3.Distance(Mound.transform.position, GM.instance.Host3.transform.position) < GM.instance.DetectionRange && GM.instance.Host3.GetComponent<Host>().Shovel)
                        {
                            Invoke("Replace", GM.instance.DigTime);
                        }
                    }
                }
            }

            if (GM.instance.keeperPlayerSel == GM.KeeperPlayer.two)
            {
                if (Input.GetButtonDown("P1 A"))
                {
                    if (Vector3.Distance(Mound.transform.position, GM.instance.Host1.transform.position) < GM.instance.DetectionRange && GM.instance.Host1.GetComponent<Host>().Shovel)
                    {
                        if (GM.instance.Grave2 != this && GM.instance.Grave3 != this)
                        {
                            Invoke("Replace", GM.instance.DigTime);
                        }
                    }
                }
                if (Input.GetButtonDown("P3 A"))
                {
                    if (GM.instance.Grave1 != this && GM.instance.Grave3 != this)
                    {
                        if (Vector3.Distance(Mound.transform.position, GM.instance.Host2.transform.position) < GM.instance.DetectionRange && GM.instance.Host2.GetComponent<Host>().Shovel)
                        {
                            Invoke("Replace", GM.instance.DigTime);
                        }
                    }
                }
                if (Input.GetButtonDown("P4 A"))
                {
                    if (GM.instance.Grave1 != this && GM.instance.Grave2 != this)
                    {
                        if (Vector3.Distance(Mound.transform.position, GM.instance.Host3.transform.position) < GM.instance.DetectionRange && GM.instance.Host3.GetComponent<Host>().Shovel)
                        {
                            Invoke("Replace", GM.instance.DigTime);
                        }
                    }
                }
            }

            if (GM.instance.keeperPlayerSel == GM.KeeperPlayer.three)
            {
                if (Input.GetButtonDown("P1 A"))
                {
                    if (Vector3.Distance(Mound.transform.position, GM.instance.Host1.transform.position) < GM.instance.DetectionRange && GM.instance.Host1.GetComponent<Host>().Shovel)
                    {
                        if (GM.instance.Grave2 != this && GM.instance.Grave3 != this)
                        {
                            Invoke("Replace", GM.instance.DigTime);
                        }
                    }
                }
                if (Input.GetButtonDown("P2 A"))
                {
                    if (GM.instance.Grave1 != this && GM.instance.Grave3 != this)
                    {
                        if (Vector3.Distance(Mound.transform.position, GM.instance.Host2.transform.position) < GM.instance.DetectionRange && GM.instance.Host2.GetComponent<Host>().Shovel)
                        {
                            Invoke("Replace", GM.instance.DigTime);
                        }
                    }
                }
                if (Input.GetButtonDown("P4 A"))
                {
                    if (GM.instance.Grave1 != this && GM.instance.Grave2 != this)
                    {
                        if (Vector3.Distance(Mound.transform.position, GM.instance.Host3.transform.position) < GM.instance.DetectionRange && GM.instance.Host3.GetComponent<Host>().Shovel)
                        {
                            Invoke("Replace", GM.instance.DigTime);
                        }
                    }
                }
            }

            if (GM.instance.keeperPlayerSel == GM.KeeperPlayer.four)
            {
                if (Input.GetButtonDown("P1 A"))
                {
                    if (Vector3.Distance(Mound.transform.position, GM.instance.Host1.transform.position) < GM.instance.DetectionRange && GM.instance.Host1.GetComponent<Host>().Shovel)
                    {
                        if (GM.instance.Grave2 != this && GM.instance.Grave3 != this)
                        {
                            Invoke("Replace", GM.instance.DigTime);
                        }
                    }
                }
                if (Input.GetButtonDown("P3 A"))
                {
                    if (GM.instance.Grave1 != this && GM.instance.Grave3 != this)
                    {
                        if (Vector3.Distance(Mound.transform.position, GM.instance.Host2.transform.position) < GM.instance.DetectionRange && GM.instance.Host2.GetComponent<Host>().Shovel)
                        {
                            Invoke("Replace", GM.instance.DigTime);
                        }
                    }
                }
                if (Input.GetButtonDown("P3 A"))
                {
                    if (GM.instance.Grave1 != this && GM.instance.Grave2 != this)
                    {
                        if (Vector3.Distance(Mound.transform.position, GM.instance.Host3.transform.position) < GM.instance.DetectionRange && GM.instance.Host3.GetComponent<Host>().Shovel)
                        {
                            Invoke("Replace", GM.instance.DigTime);
                        }
                    }
                }
            }
        }
    }

    void Replace()
    {
        Instantiate(GM.instance.GraveDug, Mound.transform.position, Quaternion.identity);
        Mound.GetComponent<Renderer>().enabled = false;
        Dug = true;
    }
}
