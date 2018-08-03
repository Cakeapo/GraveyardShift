using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class GM : MonoBehaviour
{
    public static GM instance = null;

    public enum SelectedPlayer { HostOne, HostTwo, HostThree, Keeper};
    public SelectedPlayer SelectedPlayerSel;

    public float KeeperSpeed = 1;
    public float HostSpeed = 1;
    public bool ColorTest = false;

    private float StartDelay = 0.5f;
    private bool Started = false;

    public float DetectionRange = 5;

    public List<GameObject> Hosts;
    public List<GameObject> Graves;
    public List<GameObject> Detected;
    public List<GameObject> Sheds;

    public GameObject Keeper;

    public GameObject HostCapture;
    public GameObject Host1Escape;
    public GameObject Host2Escape;
    public GameObject Host3Escape;

    public GameObject Host1;
    public GameObject Host2;
    public GameObject Host3;

    public GameObject Grave1;
    public GameObject Grave2;
    public GameObject Grave3;

    public Material Player1Mat;
    public Material Player2Mat;
    public Material Player3Mat;
    public Material HostMat;

    public Material Grave1Mat;
    public Material Grave2Mat;
    public Material Grave3Mat;
    public Material GraveMat;

    public bool Player1Captured = false;
    public bool Player2Captured = false;
    public bool Player3Captured = false;

    public bool Player1Victory = false;
    public bool Player2Victory = false;
    public bool Player3Victory = false;
    public bool KeeperVictory = false;

    // Use this for initialization
    void Start ()
    {
		if (instance == null)
        {
            instance = this;
        }

        StartCoroutine(Delay());
	}

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(StartDelay);
        Started = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectedPlayerSel = SelectedPlayer.HostOne;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectedPlayerSel = SelectedPlayer.HostTwo;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectedPlayerSel = SelectedPlayer.HostThree;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //SelectedPlayerSel = SelectedPlayer.Keeper;
        }

        if (Started && Hosts.Count > 0)
        {
            if (Host1 == null &! Player1Captured)
            {
                Host1 = Hosts[Random.Range(0, Hosts.Count)];
                if (Host1.GetComponent<Host>().ControllerSel == Host.Controller.Empty && Host1.GetComponent<Host>().Dead == false)
                {
                    Host1.GetComponent<Host>().ControllerSel = Host.Controller.Player1;
                    Host1.GetComponent<NavMeshAgent>().enabled = false;

                }
                else// if (Host1.GetComponent<Host>().ControllerSel != Host.Controller.Captured)
                {
                    GM.instance.Host1 = null;
                }
            }
            else
            {
                if (Host1.GetComponent<Host>().ControllerSel != Host.Controller.Player1)
                {
                    //GM.instance.Host1 = null;
                }
            }

            if (Host2 == null & !Player2Captured)
            {
                Host2 = Hosts[Random.Range(0, Hosts.Count)];
                if (Host2.GetComponent<Host>().ControllerSel == Host.Controller.Empty && Host2.GetComponent<Host>().Dead == false)
                {
                    Host2.GetComponent<Host>().ControllerSel = Host.Controller.Player2;
                    Host2.GetComponent<NavMeshAgent>().enabled = false;
                }
                else// if (Host2.GetComponent<Host>().ControllerSel != Host.Controller.Captured)
                {
                    GM.instance.Host2 = null;
                }
            }
            else
            {
                if (Host2.GetComponent<Host>().ControllerSel != Host.Controller.Player2)
                {
                    //GM.instance.Host2 = null;
                }
            }

            if (Host3 == null & !Player3Captured)
            {
                Host3 = Hosts[Random.Range(0, Hosts.Count)];

                if (Host3.GetComponent<Host>().ControllerSel == Host.Controller.Empty && Host3.GetComponent<Host>().Dead == false)
                {
                    Host3.GetComponent<Host>().ControllerSel = Host.Controller.Player3;
                    Host3.GetComponent<NavMeshAgent>().enabled = false;
                }
                else// if (Host3.GetComponent<Host>().ControllerSel != Host.Controller.Captured)
                {
                    GM.instance.Host3 = null;
                }
            }
            else
            {
                if (Host3.GetComponent<Host>().ControllerSel != Host.Controller.Player3)
                {
                    //GM.instance.Host3 = null;
                }
            }

            if (ColorTest)
            {
                if (Host1 != null)
                {
                    Host1.GetComponent<Renderer>().material = Player1Mat;
                }
                if (Host2 != null)
                {
                    Host2.GetComponent<Renderer>().material = Player2Mat;
                }
                if (Host3 != null)
                {
                    Host3.GetComponent<Renderer>().material = Player3Mat;
                }
            }
            else
            {
                if (Host1 != null)
                {
                    Host1.GetComponent<Renderer>().material = HostMat;
                }
                if (Host2 != null)
                {
                    Host2.GetComponent<Renderer>().material = HostMat;
                }
                if (Host3 != null)
                {
                    Host3.GetComponent<Renderer>().material = HostMat;
                }
            }
        }

        if (Started && Graves.Count > 0)
        {
            if (Grave1 == null)
            {
                Grave1 = Graves[Random.Range(0, Graves.Count)];
                if (Grave1.GetComponent<Grave>().Claimed == false)
                {
                    Grave1.GetComponent<Grave>().OwnerSel = Grave.Owner.Player1;
                }
                else
                {
                    GM.instance.Grave1 = null;
                }
            }
            else
            {
                if (Grave1.GetComponent<Grave>().OwnerSel != Grave.Owner.Player1)
                {
                    GM.instance.Grave1 = null;
                }
            }

            if (Grave2 == null)
            {
                Grave2 = Graves[Random.Range(0, Graves.Count)];
                if (Grave2.GetComponent<Grave>().Claimed == false)
                {
                    Grave2.GetComponent<Grave>().OwnerSel = Grave.Owner.Player2;
                }
                else
                {
                    GM.instance.Grave2 = null;
                }
            }
            else
            {
                if (Grave2.GetComponent<Grave>().OwnerSel != Grave.Owner.Player2)
                {
                    GM.instance.Grave2 = null;
                }
            }

            if (Grave3 == null)
            {
                Grave3 = Graves[Random.Range(0, Graves.Count)];
                if (Grave3.GetComponent<Grave>().Claimed == false)
                {
                    Grave3.GetComponent<Grave>().OwnerSel = Grave.Owner.Player3;
                }
                else
                {
                    GM.instance.Grave3 = null;
                }
            }
            else
            {
                if (Grave3.GetComponent<Grave>().OwnerSel != Grave.Owner.Player3)
                {
                    GM.instance.Grave3 = null;
                }
            }

            if (ColorTest)
            {
                if (Grave1 != null)
                {
                    Grave1.GetComponent<Grave>().Stone.GetComponent<Renderer>().material = Grave1Mat;
                }
                if (Grave2 != null)
                {
                    Grave2.GetComponent<Grave>().Stone.GetComponent<Renderer>().material = Grave2Mat;
                }
                if (Grave3 != null)
                {
                    Grave3.GetComponent<Grave>().Stone.GetComponent<Renderer>().material = Grave3Mat;
                }
            }
            else
            {
                if (Grave1 != null)
                {
                    Grave1.GetComponent<Grave>().Stone.GetComponent<Renderer>().material = GraveMat;
                }
                if (Grave2 != null)
                {
                    Grave2.GetComponent<Grave>().Stone.GetComponent<Renderer>().material = GraveMat;
                }
                if (Grave3 != null)
                {
                    Grave3.GetComponent<Grave>().Stone.GetComponent<Renderer>().material = GraveMat;
                }
            }
        }

        //victory conditions
        if (Started)
        {
            if (Host1 != null)
            {
                if (Host1.GetComponent<Host>().Victory)
                {
                    if (!Player1Victory & !Player2Victory & !Player3Victory & !KeeperVictory)
                    {
                        Player1Victory = true;
                        Instantiate(Host1Escape, Host1.transform.position, Quaternion.identity);
                        if (Detected.Contains(Host1))
                        {
                            Detected.Remove(Host1);
                        }
                        Host1.SetActive(false);
                        print("Player 1 has escaped!");
                        print("Player 1 has won!");
                    }
                    else if (!Player1Victory)
                    {
                        Player1Victory = true;
                        Instantiate(Host1Escape, Host1.transform.position, Quaternion.identity);
                        Host1.SetActive(false);
                    }
                }
            }
            if (Host2 != null)
            {
                if (Host2.GetComponent<Host>().Victory)
                {
                    if (!Player1Victory & !Player2Victory & !Player3Victory & !KeeperVictory)
                    {
                        Player2Victory = true;
                        Instantiate(Host2Escape, Host2.transform.position, Quaternion.identity);
                        if (Detected.Contains(Host2))
                        {
                            Detected.Remove(Host2);
                        }
                        Host2.SetActive(false);
                        print("Player 2 has escaped!");
                        print("Player 2 has won!");
                    }
                    else if (!Player2Victory)
                    {
                        Player2Victory = true;
                        Instantiate(Host2Escape, Host2.transform.position, Quaternion.identity);
                        Host2.SetActive(false);
                    }
                }
            }
            if (Host3 != null)
            {
                if (Host3.GetComponent<Host>().Victory)
                {
                    if (!Player1Victory & !Player2Victory & !Player3Victory & !KeeperVictory)
                    {
                        Player3Victory = true;
                        Instantiate(Host3Escape, Host3.transform.position, Quaternion.identity);
                        if (Detected.Contains(Host3))
                        {
                            Detected.Remove(Host3);
                        }
                        Host3.SetActive(false);
                        print("Player 3 has escaped!");
                        print("Player 3 has won!");
                    }
                    else if (!Player3Victory)
                    {
                        Player3Victory = true;
                        Instantiate(Host3Escape, Host3.transform.position, Quaternion.identity);
                        Host3.SetActive(false);
                    }
                }
            }

            if (Host1 != null)
            {
                if (Host1.GetComponent<Host>().Dead)
                {
                    if (!Player1Captured)
                    {
                        Player1Captured = true;
                        print("Player 1 has been captured!");
                    }
                }
            }
            if (Host2 != null)
            {
                if (Host2.GetComponent<Host>().Dead)
                {
                    if (!Player2Captured)
                    {
                        Player2Captured = true;
                        print("Player 2 has been captured!");
                    }
                }
            }
            if (Host3 != null)
            {
                if (Host3.GetComponent<Host>().Dead)
                {
                    if (!Player3Captured)
                    {
                        Player3Captured = true;
                        print("Player 3 has been captured!");
                    }
                }
            }
            if (Player1Captured && Player2Captured && Player3Captured)
            {
                if (!KeeperVictory)
                {
                    KeeperVictory = true;
                    print("The Keeper has won!");
                }
            }
        }

    }
}
