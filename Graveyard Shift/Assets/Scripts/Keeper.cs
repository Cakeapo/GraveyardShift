using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keeper : MonoBehaviour
{
    public GameObject KeeperLight;
    public GameObject Detector;
    public Animator KeeperAnimation;

    public bool LightActive = true;
    public float KeeperLightIntensity = 1;

    public bool Flashing = false;
    public float FlashIntensity = 5;

    public float RechargeMultiplier = 2;
    public float CurrentCharge = 0;

    public bool Freeze = false;
    public float FreezeTime = 1;

    public float SpeedModifier = 1;
	// Use this for initialization
	void Start ()
    {
        CurrentCharge = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {

        KeeperAnimation.SetFloat("Speed", GM.instance.KeeperSpeed * 70);
        //KeeperAnimation.gameObject.transform.eulerAngles = KeeperLight.transform.eulerAngles;

        if (LightActive)
        {
            KeeperAnimation.SetBool("Lamp", true);
        }
        else
        {
            KeeperAnimation.SetBool("Lamp", false);
        }

        if (GM.instance.keeperPlayerSel == GM.KeeperPlayer.one)
        {
            if (Input.GetAxis("P1 Vertical") < -0.1f || Input.GetAxis("P1 Vertical") > 0.1f || Input.GetAxis("P1 Horizontal") < -0.1f || Input.GetAxis("P1 Horizontal") > 0.1f)
            {
                KeeperAnimation.SetBool("Moving", true);
            }
            else
            {
                KeeperAnimation.SetBool("Moving", false);
            }
        }

        //if (GM.instance.SelectedPlayerSel == GM.SelectedPlayer.Keeper)
        if (!Freeze)
        {
            if (GM.instance.keeperPlayerSel == GM.KeeperPlayer.one)
            {
                {
                    /*
                    if (Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("P1 Vertical") < 0)
                    {
                        transform.Translate(Vector3.forward * GM.instance.KeeperSpeed * SpeedModifier);
                        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("P1 Horizontal") < 0)
                        {
                            KeeperLight.transform.localPosition = new Vector3(0.4f, 0, -0.4f);
                            KeeperLight.transform.localEulerAngles = new Vector3(5, -45, 0);
                        }
                        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("P1 Horizontal") > 0)
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
                    if (Input.GetKey(KeyCode.DownArrow) || Input.GetAxis("P1 Vertical") > 0)
                    {
                        transform.Translate(Vector3.back * GM.instance.KeeperSpeed * SpeedModifier);
                        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("P1 Horizontal") < 0)
                        {
                            KeeperLight.transform.localPosition = new Vector3(0.4f, 0, 0.4f);
                            KeeperLight.transform.localEulerAngles = new Vector3(5, -135, 0);
                        }
                        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("P1 Horizontal") > 0)
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
                    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("P1 Horizontal") < 0)
                    {
                        transform.Translate(Vector3.left * GM.instance.KeeperSpeed * SpeedModifier);
                        if (!Input.GetKey(KeyCode.UpArrow) & !Input.GetKey(KeyCode.DownArrow) && Input.GetAxis("P1 Vertical") < 0.1f && Input.GetAxis("P1 Vertical") > -0.1f)
                        {
                            KeeperLight.transform.localPosition = new Vector3(0.5f, 0, 0);
                            KeeperLight.transform.localEulerAngles = new Vector3(5, -90, 0);
                        }
                    }
                    if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("P1 Horizontal") > 0)
                    {
                        transform.Translate(Vector3.right * GM.instance.KeeperSpeed * SpeedModifier);
                        if (!Input.GetKey(KeyCode.UpArrow) & !Input.GetKey(KeyCode.DownArrow) && Input.GetAxis("P1 Vertical") < 0.1f && Input.GetAxis("P1 Vertical") > -0.1f)
                        {
                            KeeperLight.transform.localPosition = new Vector3(-0.5f, 0, 0);
                            KeeperLight.transform.localEulerAngles = new Vector3(5, 90, 0);
                        }
                    }
                    */
                }

                Vector3 NextDir = new Vector3(Input.GetAxisRaw("P1 Horizontal"), 0, -Input.GetAxisRaw("P1 Vertical"));
                if (NextDir != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(NextDir);

                    transform.Translate(Vector3.forward * 0.1f);
                }

                if (Input.GetButton("P1 X") == true)
                {
                    LightActive = false;
                }
                else if (!Flashing)
                {
                    LightActive = true;
                }

                if (LightActive)
                {
                    SpeedModifier = 1;
                    KeeperLight.SetActive(true);
                    if (!Flashing)
                    {
                        KeeperLight.GetComponent<Light>().intensity = CurrentCharge;
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

                }

                if (KeeperLight.GetComponent<Light>().intensity < KeeperLightIntensity)
                {
                    KeeperLight.GetComponent<Light>().color = new Color32(0, 255, 133, 255);
                }
                else
                {
                    KeeperLight.GetComponent<Light>().color = new Color32(0, 231, 255, 255);
                }

                if (CurrentCharge >= KeeperLightIntensity)
                {
                    if (Input.GetKeyDown(KeyCode.RightControl) || Input.GetButtonDown("P1 A") == true)
                    {
                        if (GM.instance.Detected.Count >= 1)
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
                                    CurrentCharge = RechargeMultiplier * 0.25f;
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
                    CurrentCharge += (0.01f * RechargeMultiplier);
                }
            }

            if (GM.instance.keeperPlayerSel == GM.KeeperPlayer.two)
            {
                {
                    /*
                    if (Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("P2 Vertical") < 0)
                    {
                        transform.Translate(Vector3.forward * GM.instance.KeeperSpeed * SpeedModifier);
                        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("P2 Horizontal") < 0)
                        {
                            KeeperLight.transform.localPosition = new Vector3(0.4f, 0, -0.4f);
                            KeeperLight.transform.localEulerAngles = new Vector3(5, -45, 0);
                        }
                        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("P2 Horizontal") > 0)
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
                    if (Input.GetKey(KeyCode.DownArrow) || Input.GetAxis("P2 Vertical") > 0)
                    {
                        transform.Translate(Vector3.back * GM.instance.KeeperSpeed * SpeedModifier);
                        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("P2 Horizontal") < 0)
                        {
                            KeeperLight.transform.localPosition = new Vector3(0.4f, 0, 0.4f);
                            KeeperLight.transform.localEulerAngles = new Vector3(5, -135, 0);
                        }
                        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("P2 Horizontal") > 0)
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
                    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("P2 Horizontal") < 0)
                    {
                        transform.Translate(Vector3.left * GM.instance.KeeperSpeed * SpeedModifier);
                        if (!Input.GetKey(KeyCode.UpArrow) & !Input.GetKey(KeyCode.DownArrow) && Input.GetAxis("P2 Vertical") < 0.1f && Input.GetAxis("P2 Vertical") > -0.1f)
                        {
                            KeeperLight.transform.localPosition = new Vector3(0.5f, 0, 0);
                            KeeperLight.transform.localEulerAngles = new Vector3(5, -90, 0);
                        }
                    }
                    if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("P2 Horizontal") > 0)
                    {
                        transform.Translate(Vector3.right * GM.instance.KeeperSpeed * SpeedModifier);
                        if (!Input.GetKey(KeyCode.UpArrow) & !Input.GetKey(KeyCode.DownArrow) && Input.GetAxis("P2 Vertical") < 0.1f && Input.GetAxis("P2 Vertical") > -0.1f)
                        {
                            KeeperLight.transform.localPosition = new Vector3(-0.5f, 0, 0);
                            KeeperLight.transform.localEulerAngles = new Vector3(5, 90, 0);
                        }
                    }
                    */
                }

                Vector3 NextDir = new Vector3(Input.GetAxisRaw("P2 Horizontal"), 0, -Input.GetAxisRaw("P2 Vertical"));
                if (NextDir != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(NextDir);

                    transform.Translate(Vector3.forward * 0.1f);
                }

                if (Input.GetButton("P2 X") == true)
                {
                    LightActive = false;
                }
                else
                {
                    LightActive = true;
                }

                if (LightActive)
                {
                    SpeedModifier = 1;
                    KeeperLight.SetActive(true);
                    if (!Flashing)
                    {
                        KeeperLight.GetComponent<Light>().intensity = CurrentCharge;
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

                }

                if (KeeperLight.GetComponent<Light>().intensity < KeeperLightIntensity)
                {
                    KeeperLight.GetComponent<Light>().color = new Color32(0, 255, 133, 255);
                }
                else
                {
                    KeeperLight.GetComponent<Light>().color = new Color32(0, 231, 255, 255);
                }

                if (CurrentCharge >= KeeperLightIntensity)
                {
                    if (Input.GetKeyDown(KeyCode.RightControl) || Input.GetButtonDown("P2 A") == true)
                    {
                        if (GM.instance.Detected.Count >= 1)
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
                                    CurrentCharge = RechargeMultiplier * 0.25f;
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
                    CurrentCharge += (0.01f * RechargeMultiplier);
                }
            }

            if (GM.instance.keeperPlayerSel == GM.KeeperPlayer.three)
            {
                {
                    /*
                    if (Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("P3 Vertical") < 0)
                    {
                        transform.Translate(Vector3.forward * GM.instance.KeeperSpeed * SpeedModifier);
                        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("P3 Horizontal") < 0)
                        {
                            KeeperLight.transform.localPosition = new Vector3(0.4f, 0, -0.4f);
                            KeeperLight.transform.localEulerAngles = new Vector3(5, -45, 0);
                        }
                        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("P3 Horizontal") > 0)
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
                    if (Input.GetKey(KeyCode.DownArrow) || Input.GetAxis("P3 Vertical") > 0)
                    {
                        transform.Translate(Vector3.back * GM.instance.KeeperSpeed * SpeedModifier);
                        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("P3 Horizontal") < 0)
                        {
                            KeeperLight.transform.localPosition = new Vector3(0.4f, 0, 0.4f);
                            KeeperLight.transform.localEulerAngles = new Vector3(5, -135, 0);
                        }
                        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("P3 Horizontal") > 0)
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
                    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("P3 Horizontal") < 0)
                    {
                        transform.Translate(Vector3.left * GM.instance.KeeperSpeed * SpeedModifier);
                        if (!Input.GetKey(KeyCode.UpArrow) & !Input.GetKey(KeyCode.DownArrow) && Input.GetAxis("P3 Vertical") < 0.1f && Input.GetAxis("P3 Vertical") > -0.1f)
                        {
                            KeeperLight.transform.localPosition = new Vector3(0.5f, 0, 0);
                            KeeperLight.transform.localEulerAngles = new Vector3(5, -90, 0);
                        }
                    }
                    if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("P3 Horizontal") > 0)
                    {
                        transform.Translate(Vector3.right * GM.instance.KeeperSpeed * SpeedModifier);
                        if (!Input.GetKey(KeyCode.UpArrow) & !Input.GetKey(KeyCode.DownArrow) && Input.GetAxis("P3 Vertical") < 0.1f && Input.GetAxis("P3 Vertical") > -0.1f)
                        {
                            KeeperLight.transform.localPosition = new Vector3(-0.5f, 0, 0);
                            KeeperLight.transform.localEulerAngles = new Vector3(5, 90, 0);
                        }
                    }
                    */
                }

                Vector3 NextDir = new Vector3(Input.GetAxisRaw("P3 Horizontal"), 0, -Input.GetAxisRaw("P3 Vertical"));
                if (NextDir != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(NextDir);

                    transform.Translate(Vector3.forward * 0.1f);
                }

                if (Input.GetButton("P3 X") == true)
                {
                    LightActive = false;
                }
                else
                {
                    LightActive = true;
                }

                if (LightActive)
                {
                    SpeedModifier = 1;
                    KeeperLight.SetActive(true);
                    if (!Flashing)
                    {
                        KeeperLight.GetComponent<Light>().intensity = CurrentCharge;
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

                }

                if (KeeperLight.GetComponent<Light>().intensity < KeeperLightIntensity)
                {
                    KeeperLight.GetComponent<Light>().color = new Color32(0, 255, 133, 255);
                }
                else
                {
                    KeeperLight.GetComponent<Light>().color = new Color32(0, 231, 255, 255);
                }

                if (CurrentCharge >= KeeperLightIntensity)
                {
                    if (Input.GetKeyDown(KeyCode.RightControl) || Input.GetButtonDown("P3 A") == true)
                    {
                        if (GM.instance.Detected.Count >= 1)
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
                                    CurrentCharge = RechargeMultiplier * 0.25f;
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
                    CurrentCharge += (0.01f * RechargeMultiplier);
                }
            }

            if (GM.instance.keeperPlayerSel == GM.KeeperPlayer.four)
            {
                {
                    /*
                    if (Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("P4 Vertical") < 0)
                    {
                        transform.Translate(Vector3.forward * GM.instance.KeeperSpeed * SpeedModifier);
                        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("P4 Horizontal") < 0)
                        {
                            KeeperLight.transform.localPosition = new Vector3(0.4f, 0, -0.4f);
                            KeeperLight.transform.localEulerAngles = new Vector3(5, -45, 0);
                        }
                        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("P4 Horizontal") > 0)
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
                    if (Input.GetKey(KeyCode.DownArrow) || Input.GetAxis("P4 Vertical") > 0)
                    {
                        transform.Translate(Vector3.back * GM.instance.KeeperSpeed * SpeedModifier);
                        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("P4 Horizontal") < 0)
                        {
                            KeeperLight.transform.localPosition = new Vector3(0.4f, 0, 0.4f);
                            KeeperLight.transform.localEulerAngles = new Vector3(5, -135, 0);
                        }
                        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("P4 Horizontal") > 0)
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
                    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("P4 Horizontal") < 0)
                    {
                        transform.Translate(Vector3.left * GM.instance.KeeperSpeed * SpeedModifier);
                        if (!Input.GetKey(KeyCode.UpArrow) & !Input.GetKey(KeyCode.DownArrow) && Input.GetAxis("P4 Vertical") < 0.1f && Input.GetAxis("P4 Vertical") > -0.1f)
                        {
                            KeeperLight.transform.localPosition = new Vector3(0.5f, 0, 0);
                            KeeperLight.transform.localEulerAngles = new Vector3(5, -90, 0);
                        }
                    }
                    if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("P4 Horizontal") > 0)
                    {
                        transform.Translate(Vector3.right * GM.instance.KeeperSpeed * SpeedModifier);
                        if (!Input.GetKey(KeyCode.UpArrow) & !Input.GetKey(KeyCode.DownArrow) && Input.GetAxis("P4 Vertical") < 0.1f && Input.GetAxis("P4 Vertical") > -0.1f)
                        {
                            KeeperLight.transform.localPosition = new Vector3(-0.5f, 0, 0);
                            KeeperLight.transform.localEulerAngles = new Vector3(5, 90, 0);
                        }
                    }
                    */
                }

                Vector3 NextDir = new Vector3(Input.GetAxisRaw("P4 Horizontal"), 0, -Input.GetAxisRaw("P4 Vertical"));
                if (NextDir != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(NextDir);

                    transform.Translate(Vector3.forward * 0.1f);
                }

                if (Input.GetButton("P4 X") == true)
                {
                    LightActive = false;
                }
                else
                {
                    LightActive = true;
                }

                if (LightActive)
                {
                    SpeedModifier = 1;
                    KeeperLight.SetActive(true);
                    if (!Flashing)
                    {
                        KeeperLight.GetComponent<Light>().intensity = CurrentCharge;
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

                }

                if (KeeperLight.GetComponent<Light>().intensity < KeeperLightIntensity)
                {
                    KeeperLight.GetComponent<Light>().color = new Color32(0, 255, 133, 255);
                }
                else
                {
                    KeeperLight.GetComponent<Light>().color = new Color32(0, 231, 255, 255);
                }

                if (CurrentCharge >= KeeperLightIntensity)
                {
                    if (Input.GetKeyDown(KeyCode.RightControl) || Input.GetButtonDown("P4 A") == true)
                    {
                        if (GM.instance.Detected.Count >= 1)
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
                                    CurrentCharge = RechargeMultiplier * 0.25f;
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
                    CurrentCharge += (0.01f * RechargeMultiplier);
                }
            }
        }
        else
        {
            KeeperAnimation.SetBool("Moving", false);
        }
    }

    void Flash()
    {
        Flashing = false;
        CurrentCharge = 0;
        KeeperLight.GetComponent<Light>().intensity = 0;
        LightActive = false;
    }

    void Unfreeze()
    {
        Freeze = false;
    }
}
