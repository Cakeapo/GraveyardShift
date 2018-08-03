using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keeper : MonoBehaviour
{
    public GameObject KeeperLight;
    public GameObject Detector;

    public bool LightActive = true;
    public float KeeperLightIntensity = 1;

    public bool Flashing = false;
    public float FlashIntensity = 5;

    public float RechargeTime = 2;
    public float CurrentCharge = 2;

    public bool Freeze = false;
    public float FreezeTime = 1;

    public float SpeedModifier = 1;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if (GM.instance.SelectedPlayerSel == GM.SelectedPlayer.Keeper)
        if (!Freeze)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(Vector3.forward * GM.instance.KeeperSpeed * SpeedModifier);
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    KeeperLight.transform.localPosition = new Vector3(0.4f, 0, -0.4f);
                    KeeperLight.transform.localEulerAngles = new Vector3(5, -45, 0);
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    KeeperLight.transform.localPosition = new Vector3(-0.4f, 0, -0.4f);
                    KeeperLight.transform.localEulerAngles = new Vector3(5, 45, 0);
                }
                else
                {
                    KeeperLight.transform.localPosition = new Vector3(0, 0, -0.5f);
                    KeeperLight.transform.localEulerAngles = new Vector3(5, 0, 0);
                }
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(Vector3.back * GM.instance.KeeperSpeed * SpeedModifier);
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    KeeperLight.transform.localPosition = new Vector3(0.4f, 0, 0.4f);
                    KeeperLight.transform.localEulerAngles = new Vector3(5, -135, 0);
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    KeeperLight.transform.localPosition = new Vector3(-0.4f, 0, 0.4f);
                    KeeperLight.transform.localEulerAngles = new Vector3(5, 135, 0);
                }
                else
                {
                    KeeperLight.transform.localPosition = new Vector3(0, 0, 0.5f);
                    KeeperLight.transform.localEulerAngles = new Vector3(5, 180, 0);
                }
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(Vector3.left * GM.instance.KeeperSpeed * SpeedModifier);
                if (!Input.GetKey(KeyCode.UpArrow) & !Input.GetKey(KeyCode.DownArrow))
                {
                    KeeperLight.transform.localPosition = new Vector3(0.5f, 0, 0);
                    KeeperLight.transform.localEulerAngles = new Vector3(5, -90, 0);
                }
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(Vector3.right * GM.instance.KeeperSpeed * SpeedModifier);
                if (!Input.GetKey(KeyCode.UpArrow) & !Input.GetKey(KeyCode.DownArrow))
                {
                    KeeperLight.transform.localPosition = new Vector3(-0.5f, 0, 0);
                    KeeperLight.transform.localEulerAngles = new Vector3(5, 90, 0);
                }
            }
            
            if (LightActive)
            {
                SpeedModifier = 1;
                KeeperLight.SetActive(true);
                if (!Flashing)
                {
                    KeeperLight.GetComponent<Light>().intensity = CurrentCharge * 0.5f;
                }

                if (Input.GetKeyDown(KeyCode.RightShift))
                {
                    LightActive = false;
                }
            }
            else
            {
                SpeedModifier = 2;
                CurrentCharge = 0;
                if (!Flashing)
                {
                    KeeperLight.GetComponent<Light>().intensity = 0;
                }
                KeeperLight.SetActive(false);

                if (Input.GetKeyDown(KeyCode.RightShift))
                {
                    LightActive = true;
                }
            }

            if (KeeperLight.GetComponent<Light>().intensity < KeeperLightIntensity)
            {
                KeeperLight.GetComponent<Light>().color = new Color32(0, 255, 133, 255);
            }
            else
            {
                KeeperLight.GetComponent<Light>().color = new Color32(0, 231, 255, 255);
            }

            if (CurrentCharge >= RechargeTime)
            {
                if (Input.GetKeyDown(KeyCode.RightControl))
                {
                    if (GM.instance.Detected.Count > 0)
                    {
                        if (GM.instance.Detected[0].GetComponent<Host>().Victory == false)
                        {
                            KeeperLight.GetComponent<Light>().intensity = FlashIntensity;
                            Flashing = true;
                            Invoke("Flash", 0.5f);
                            CurrentCharge = 0;

                            if (GM.instance.Detected[0].GetComponent<Host>().ControllerSel == Host.Controller.Empty)
                            {
                                Freeze = true;
                                Invoke("Unfreeze", FreezeTime);
                            }
                            else
                            {
                                CurrentCharge = RechargeTime * 0.25f;
                            }

                            GM.instance.Detected[0].GetComponent<Host>().ControllerSel = Host.Controller.Captured;
                            GM.instance.Detected[0].GetComponent<Host>().Dead = true;
                            GM.instance.Hosts.Remove(GM.instance.Detected[0].gameObject);
                            GM.instance.Detected.Remove(GM.instance.Detected[0].gameObject);
                        }
                    }
                }
                else
                {
                    //KeeperLight.GetComponent<Light>().intensity = KeeperLightIntensity;
                }
            }
            else
            {
                CurrentCharge += 0.01f;
            }
        }
    }

    void Flash()
    {
        Flashing = false;
        KeeperLight.GetComponent<Light>().intensity = 0;
    }

    void Unfreeze()
    {
        Freeze = false;
    }
}
