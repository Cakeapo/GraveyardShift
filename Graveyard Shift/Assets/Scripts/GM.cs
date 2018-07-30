using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    public static GM instance = null;

    public float HostSpeed = 1;
    public bool ColorTest = false;

    private float StartDelay = 0.5f;
    private bool Started = false;

    public List<GameObject> Hosts;
    public List<GameObject> Graves;

    public GameObject Host1;
    public GameObject Host2;
    public GameObject Host3;

    public GameObject Grave1;
    public GameObject Grave2;
    public GameObject Grave3;

    public Material Player1Mat;
    public Material Player2Mat;
    public Material Player3Mat;

    public Material Grave1Mat;
    public Material Grave2Mat;
    public Material Grave3Mat;

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

        if (Started && Hosts.Count > 0)
        {
            if (Host1 == null)
            {
                Host1 = Hosts[Random.Range(0, Hosts.Count)];
                if (Host1.GetComponent<Host>().ControllerSel == Host.Controller.Empty && Host1.GetComponent<Host>().Dead == false)
                {
                    Host1.GetComponent<Host>().ControllerSel = Host.Controller.Player1;
                }
                else
                {
                    GM.instance.Host1 = null;
                }
            }
            else
            {
                if (Host1.GetComponent<Host>().ControllerSel != Host.Controller.Player1)
                {
                    GM.instance.Host1 = null;
                }
            }

            if (Host2 == null)
            {
                Host2 = Hosts[Random.Range(0, Hosts.Count)];
                if (Host2.GetComponent<Host>().ControllerSel == Host.Controller.Empty && Host2.GetComponent<Host>().Dead == false)
                {
                    Host2.GetComponent<Host>().ControllerSel = Host.Controller.Player2;
                }
                else
                {
                    GM.instance.Host2 = null;
                }
            }
            else
            {
                if (Host2.GetComponent<Host>().ControllerSel != Host.Controller.Player2)
                {
                    GM.instance.Host2 = null;
                }
            }

            if (Host3 == null)
            {
                Host3 = Hosts[Random.Range(0, Hosts.Count)];
                if (Host3.GetComponent<Host>().ControllerSel == Host.Controller.Empty && Host3.GetComponent<Host>().Dead == false)
                {
                    Host3.GetComponent<Host>().ControllerSel = Host.Controller.Player3;
                }
                else
                {
                    GM.instance.Host3 = null;
                }
            }
            else
            {
                if (Host3.GetComponent<Host>().ControllerSel != Host.Controller.Player3)
                {
                    GM.instance.Host3 = null;
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
                    Grave1.GetComponent<Renderer>().material = Grave1Mat;
                }
                if (Grave2 != null)
                {
                    Grave2.GetComponent<Renderer>().material = Grave2Mat;
                }
                if (Grave3 != null)
                {
                    Grave3.GetComponent<Renderer>().material = Grave3Mat;
                }
            }
        }
    }
}
