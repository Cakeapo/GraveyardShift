using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Host : MonoBehaviour
{
    public enum Controller { Empty, Captured, Player1, Player2, Player3, Player4 };
    public Controller ControllerSel;

    public float SpeedModifier = 1;

    public bool Frozen = false;
    public bool CanSwitch = false;
    public bool Dead = false;
    public bool Shovel = false;
    public bool Victory = false;

    public int RandomDirection0FB;
    public int RandomDirection0LR;

    private NavMeshAgent Agent;
    // Use this for initialization
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Invoke("NewDestination", Random.Range(1, 2));
        //Invoke("NewDirectionFB", Random.Range(1, 2));
        //Invoke("NewDirectionLR", Random.Range(1, 2));
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.instance.Hosts.Contains(this.gameObject) == false && Dead == false)
        {
            GM.instance.Hosts.Add(this.gameObject);
        }

        if (GM.instance.Detected.Contains(this.gameObject) && Victory)
        {
            GM.instance.Detected.Remove(this.gameObject);
        }

        Agent.speed = GM.instance.HostSpeed * 65 * SpeedModifier;

        if (ControllerSel == Controller.Captured)
        {
            GameObject CaptureEffect = Instantiate(GM.instance.HostCapture, transform.position, Quaternion.identity);
            CaptureEffect.transform.LookAt(GM.instance.Keeper.transform.position);
            if (GM.instance.Detected.Contains(this.gameObject))
            {
                GM.instance.Detected.Remove(this.gameObject);
            }
            gameObject.SetActive(false);
            /*
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<CapsuleCollider>().enabled = false;
            Agent.enabled = false;
            //Dead = false;
            transform.LookAt(GM.instance.Keeper.transform.position);
            transform.Translate(Vector3.forward * GM.instance.HostSpeed);
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * 0.99f, transform.localScale.y * 0.99f, transform.localScale.z * 0.99f);
            }
            */
        }

        if (Dead)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }

        //NPC AI
        if (!Dead && ControllerSel != Controller.Captured)
        {
            if (!Frozen)
            {
                if (ControllerSel == Controller.Empty)
                {
                    GetComponent<NavMeshAgent>().enabled = true;

                    if (Vector3.Distance(Agent.destination, transform.position) < GM.instance.DetectionRange)
                    {
                        print("grave inspected");
                        Frozen = true;
                        Invoke("Unfreeze", GM.instance.InspectTime);
                        CancelInvoke("NewDestination");
                        Invoke("NewDestination", GM.instance.InspectTime);
                    }

                    /*
                    if (RandomDirection0FB == 1)
                    {
                        transform.Translate(Vector3.forward * GM.instance.HostSpeed);
                    }
                    if (RandomDirection0FB == 2)
                    {
                        transform.Translate(Vector3.back * GM.instance.HostSpeed);
                    }
                    if (RandomDirection0LR == 1)
                    {
                        transform.Translate(Vector3.left * GM.instance.HostSpeed);
                    }
                    if (RandomDirection0LR == 2)
                    {
                        transform.Translate(Vector3.right * GM.instance.HostSpeed);
                    }
                    */
                }
                else
                {
                    if (!CanSwitch)
                    {
                        Invoke("CanSwitchDelay", GM.instance.SwitchCooldown);
                    }
                    Agent.updatePosition = false;
                    Agent.updateRotation = false;
                    //Agent.enabled = false;
                }
            }
        }
        else if (ControllerSel != Controller.Captured)
        {
            Agent.enabled = false;

            if (transform.localEulerAngles.x < 90)
            {
                transform.Rotate(Vector3.right * 1);
                transform.Translate(Vector3.forward * 0.02f);
            }

            if (GM.instance.Hosts.Contains(this.gameObject))
            {
                GM.instance.Hosts.Remove(this.gameObject);
                print("removed");
            }
        }
        else
        {
            Dead = true;
            Agent.enabled = false;
            /*
            if (GM.instance.Host1 = this.gameObject)
            {
                GM.instance.Player1Captured = true;
            }
            if (GM.instance.Host2 = this.gameObject)
            {
                GM.instance.Player2Captured = true;
            }
            if (GM.instance.Host3 = this.gameObject)
            {
                GM.instance.Player3Captured = true;
            }
            */
        }

        //Keyboard
        {
            /*
            //Player 1 Controls
            if (ControllerSel == Controller.Player1 && GM.instance.SelectedPlayerSel == GM.SelectedPlayer.HostOne)
            {
                if (GM.instance.Host1 == this.gameObject)
                {
                    if (!Victory & !Dead)
                    {
                        transform.localEulerAngles = new Vector3(0, 0, 0);

                        if (Input.GetKey(KeyCode.W))
                        {
                            transform.Translate(Vector3.forward * GM.instance.HostSpeed * SpeedModifier);
                        }
                        if (Input.GetKey(KeyCode.S))
                        {
                            transform.Translate(Vector3.back * GM.instance.HostSpeed * SpeedModifier);
                        }
                        if (Input.GetKey(KeyCode.A))
                        {
                            transform.Translate(Vector3.left * GM.instance.HostSpeed * SpeedModifier);
                        }
                        if (Input.GetKey(KeyCode.D))
                        {
                            transform.Translate(Vector3.right * GM.instance.HostSpeed * SpeedModifier);
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.E) || Victory & !Dead)
                    {
                        if (Vector3.Distance(GM.instance.Grave1.transform.position, transform.position) < GM.instance.DetectionRange || Victory)
                        {
                            if (GM.instance.Detected.Contains(this.gameObject))
                            {
                                GM.instance.Detected.Remove(this.gameObject);
                            }
                            Victory = true;
                            //GetComponent<Rigidbody>().useGravity = false;
                            //transform.Translate(Vector3.up * 0.1f);
                            //if (GM.instance.Grave1.transform.localPosition.y > -1)
                            {
                                //GM.instance.Grave1.transform.Translate(Vector3.down * 0.05f);
                            }
                        }
                        else if (GM.instance.Hosts.Count >= 4)
                        {
                            GM.instance.Host1 = null;
                            Dead = true;
                            GetComponent<Renderer>().material = GM.instance.HostMat;
                        }
                        else if (GM.instance.Hosts.Count == 3)
                        {
                            if (GM.instance.Host2.GetComponent<Host>().Dead || GM.instance.Host2.GetComponent<Host>().ControllerSel == Controller.Captured)
                            {
                                GM.instance.Host1 = null;
                                Dead = true;
                                GetComponent<Renderer>().material = GM.instance.HostMat;
                            }
                            if (GM.instance.Host3.GetComponent<Host>().Dead || GM.instance.Host3.GetComponent<Host>().ControllerSel == Controller.Captured)
                            {
                                GM.instance.Host1 = null;
                                Dead = true;
                                GetComponent<Renderer>().material = GM.instance.HostMat;
                            }
                        }
                        else if (GM.instance.Hosts.Count == 2)
                        {
                            if (GM.instance.Host2.GetComponent<Host>().Dead || GM.instance.Host2.GetComponent<Host>().ControllerSel == Controller.Captured)
                            {
                                if (GM.instance.Host3.GetComponent<Host>().Dead || GM.instance.Host3.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    GM.instance.Host1 = null;
                                    Dead = true;
                                    GetComponent<Renderer>().material = GM.instance.HostMat;
                                }
                            }
                        }
                    }

                    if (Input.GetKey(KeyCode.Q))
                    {
                        SpeedModifier = 2;
                    }
                    else
                    {
                        SpeedModifier = 1;
                    }
                }
                else if (ControllerSel != Controller.Captured)
                {
                    ControllerSel = Controller.Empty;
                }
            }

            //Player 2 Controls
            if (ControllerSel == Controller.Player2 && GM.instance.SelectedPlayerSel == GM.SelectedPlayer.HostTwo)
            {
                if (GM.instance.Host2 == this.gameObject)
                {
                    if (!Victory & !Dead)
                    {
                        transform.localEulerAngles = new Vector3(0, 0, 0);

                        if (Input.GetKey(KeyCode.W))
                        {
                            transform.Translate(Vector3.forward * GM.instance.HostSpeed * SpeedModifier);
                        }
                        if (Input.GetKey(KeyCode.S))
                        {
                            transform.Translate(Vector3.back * GM.instance.HostSpeed * SpeedModifier);
                        }
                        if (Input.GetKey(KeyCode.A))
                        {
                            transform.Translate(Vector3.left * GM.instance.HostSpeed * SpeedModifier);
                        }
                        if (Input.GetKey(KeyCode.D))
                        {
                            transform.Translate(Vector3.right * GM.instance.HostSpeed * SpeedModifier);
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.E) || Victory & !Dead)
                    {
                        if (Vector3.Distance(GM.instance.Grave2.transform.position, transform.position) < GM.instance.DetectionRange || Victory)
                        {
                            if (GM.instance.Detected.Contains(this.gameObject))
                            {
                                GM.instance.Detected.Remove(this.gameObject);
                            }
                            Victory = true;
                            
                            //GetComponent<Rigidbody>().useGravity = false;
                            //transform.Translate(Vector2.up * 0.1f);
                            //if (GM.instance.Grave2.transform.localPosition.y > -1)
                            {
                                //GM.instance.Grave2.transform.Translate(Vector2.down * 0.05f);
                            }
                        }
                        else if (GM.instance.Hosts.Count >= 4)
                        {
                            GM.instance.Host2 = null;
                            Dead = true;
                            GetComponent<Renderer>().material = GM.instance.HostMat;
                        }
                        else if (GM.instance.Hosts.Count == 3)
                        {
                            if (GM.instance.Host1.GetComponent<Host>().Dead || GM.instance.Host1.GetComponent<Host>().ControllerSel == Controller.Captured)
                            {
                                GM.instance.Host2 = null;
                                Dead = true;
                                GetComponent<Renderer>().material = GM.instance.HostMat;
                            }
                            if (GM.instance.Host3.GetComponent<Host>().Dead || GM.instance.Host3.GetComponent<Host>().ControllerSel == Controller.Captured)
                            {
                                GM.instance.Host2 = null;
                                Dead = true;
                                GetComponent<Renderer>().material = GM.instance.HostMat;
                            }
                        }
                        else if (GM.instance.Hosts.Count == 2)
                        {
                            if (GM.instance.Host1.GetComponent<Host>().Dead || GM.instance.Host1.GetComponent<Host>().ControllerSel == Controller.Captured)
                            {
                                if (GM.instance.Host3.GetComponent<Host>().Dead || GM.instance.Host3.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    GM.instance.Host2 = null;
                                    Dead = true;
                                    GetComponent<Renderer>().material = GM.instance.HostMat;
                                }
                            }
                        }
                    }

                    if (Input.GetKey(KeyCode.Q))
                    {
                        SpeedModifier = 2;
                    }
                    else
                    {
                        SpeedModifier = 1;
                    }
                }
                else if (ControllerSel != Controller.Captured)
                {
                    ControllerSel = Controller.Empty;
                }
            }

            //Player 3 Controls
            if (ControllerSel == Controller.Player3 && GM.instance.SelectedPlayerSel == GM.SelectedPlayer.HostThree)
            {
                if (GM.instance.Host3 == this.gameObject)
                {
                    if (!Victory & !Dead)
                    {
                        transform.localEulerAngles = new Vector3(0, 0, 0);

                        if (Input.GetKey(KeyCode.W))
                        {
                            transform.Translate(Vector3.forward * GM.instance.HostSpeed * SpeedModifier);
                        }
                        if (Input.GetKey(KeyCode.S))
                        {
                            transform.Translate(Vector3.back * GM.instance.HostSpeed * SpeedModifier);
                        }
                        if (Input.GetKey(KeyCode.A))
                        {
                            transform.Translate(Vector3.left * GM.instance.HostSpeed * SpeedModifier);
                        }
                        if (Input.GetKey(KeyCode.D))
                        {
                            transform.Translate(Vector3.right * GM.instance.HostSpeed * SpeedModifier);
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.E) || Victory & !Dead)
                    {
                        if (Vector3.Distance(GM.instance.Grave3.transform.position, transform.position) < GM.instance.DetectionRange || Victory)
                        {
                            if (GM.instance.Detected.Contains(this.gameObject))
                            {
                                GM.instance.Detected.Remove(this.gameObject);
                            }
                            Victory = true;
                            
                            //GetComponent<Rigidbody>().useGravity = false;
                            //transform.Translate(Vector3.up * 0.1f);
                            //if (GM.instance.Grave3.transform.localPosition.y > -1)
                            {
                                //GM.instance.Grave3.transform.Translate(Vector3.down * 0.05f);
                            }
                        }
                        else if (GM.instance.Hosts.Count >= 4)
                        {
                            GM.instance.Host3 = null;
                            Dead = true;
                            GetComponent<Renderer>().material = GM.instance.HostMat;
                        }
                        else if (GM.instance.Hosts.Count == 3)
                        {
                            if (GM.instance.Host2.GetComponent<Host>().Dead || GM.instance.Host2.GetComponent<Host>().ControllerSel == Controller.Captured)
                            {
                                GM.instance.Host3 = null;
                                Dead = true;
                                GetComponent<Renderer>().material = GM.instance.HostMat;
                            }
                            if (GM.instance.Host1.GetComponent<Host>().Dead || GM.instance.Host1.GetComponent<Host>().ControllerSel == Controller.Captured)
                            {
                                GM.instance.Host3 = null;
                                Dead = true;
                                GetComponent<Renderer>().material = GM.instance.HostMat;
                            }
                        }
                        else if (GM.instance.Hosts.Count == 2)
                        {
                            if (GM.instance.Host2.GetComponent<Host>().Dead || GM.instance.Host2.GetComponent<Host>().ControllerSel == Controller.Captured)
                            {
                                if (GM.instance.Host1.GetComponent<Host>().Dead || GM.instance.Host1.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    GM.instance.Host3 = null;
                                    Dead = true;
                                    GetComponent<Renderer>().material = GM.instance.HostMat;
                                }
                            }
                        }
                    }

                    if (Input.GetKey(KeyCode.Q))
                    {
                        SpeedModifier = 2;
                    }
                    else
                    {
                        SpeedModifier = 1;
                    }
                }
                else if (ControllerSel != Controller.Captured)
                {
                    ControllerSel = Controller.Empty;
                }
            }
            */
        }

        //Controllers
        if (!Frozen)
        {
            if (GM.instance.keeperPlayerSel == GM.KeeperPlayer.one)
            {
                //Player 1 Controls
                if (ControllerSel == Controller.Player1)// && GM.instance.SelectedPlayerSel == GM.SelectedPlayer.HostOne)
                {
                    if (GM.instance.Host1 == this.gameObject)
                    {
                        if (!Victory & !Dead)
                        {
                            transform.localEulerAngles = new Vector3(0, 0, 0);

                            //print("X " + Input.GetAxis("P2 Horizontal"));
                            //print("Y " + Input.GetAxis("P2 Vertical"));

                            //if (Input.GetAxis("Axis 2") < 0f)
                            if (Input.GetAxis("P2 Vertical") < 0)
                            {
                                transform.Translate(Vector3.forward * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 2") > 0f)
                            if (Input.GetAxis("P2 Vertical") > 0)
                            {
                                transform.Translate(Vector3.back * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 1") < 0f)
                            if (Input.GetAxis("P2 Horizontal") < 0)
                            {
                                transform.Translate(Vector3.left * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 1") > 0f)
                            if (Input.GetAxis("P2 Horizontal") > 0)
                            {
                                transform.Translate(Vector3.right * GM.instance.HostSpeed * SpeedModifier);
                            }
                        }

                        if (Input.GetButtonDown("P2 A") == true)
                        {
                            if (Shovel)
                            {
                                Frozen = true;
                                Invoke("Unfreeze", GM.instance.DigTime);
                                Invoke("Dig", GM.instance.DigTime);

                                /*
                                if (Vector3.Distance(GM.instance.Grave1.transform.position, transform.position) < GM.instance.DetectionRange || Victory)
                                {
                                    if (GM.instance.Detected.Contains(this.gameObject))
                                    {
                                        GM.instance.Detected.Remove(this.gameObject);
                                    }
                                    Victory = true;
                                }
                                */
                            }
                            else
                            {
                                if (GM.instance.Sheds.Count >= 1)
                                {
                                    if (Vector3.Distance(GM.instance.Sheds[0].transform.position, transform.position) < 3 || Victory)
                                    {
                                        Frozen = true;
                                        Invoke("Unfreeze", GM.instance.InspectTime);
                                        Shovel = true;
                                    }
                                    else if (GM.instance.Sheds.Count >= 2)
                                    {
                                        if (Vector3.Distance(GM.instance.Sheds[1].transform.position, transform.position) < 3 || Victory)
                                        {
                                            Frozen = true;
                                            Invoke("Unfreeze", GM.instance.InspectTime);
                                            Shovel = true;
                                        }
                                        else if (GM.instance.Sheds.Count >= 3)
                                        {
                                            if (Vector3.Distance(GM.instance.Sheds[2].transform.position, transform.position) < 3 || Victory)
                                            {
                                                Frozen = true;
                                                Invoke("Unfreeze", GM.instance.InspectTime);
                                                Shovel = true;
                                            }
                                            else if (GM.instance.Sheds.Count >= 4)
                                            {
                                                if (Vector3.Distance(GM.instance.Sheds[3].transform.position, transform.position) < 3 || Victory)
                                                {
                                                    Frozen = true;
                                                    Invoke("Unfreeze", GM.instance.InspectTime);
                                                    Shovel = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (Input.GetButtonDown("P2 B") == true & !Dead && CanSwitch)
                        {
                            if (GM.instance.Hosts.Count >= 4)
                            {
                                GM.instance.Host1 = null;
                                Dead = true;
                                GetComponent<Renderer>().material = GM.instance.HostMat;
                            }
                            else if (GM.instance.Hosts.Count == 3)
                            {
                                if (GM.instance.Host2.GetComponent<Host>().Dead || GM.instance.Host2.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    GM.instance.Host1 = null;
                                    Dead = true;
                                    GetComponent<Renderer>().material = GM.instance.HostMat;
                                }
                                if (GM.instance.Host3.GetComponent<Host>().Dead || GM.instance.Host3.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    GM.instance.Host1 = null;
                                    Dead = true;
                                    GetComponent<Renderer>().material = GM.instance.HostMat;
                                }
                            }
                            else if (GM.instance.Hosts.Count == 2)
                            {
                                if (GM.instance.Host2.GetComponent<Host>().Dead || GM.instance.Host2.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    if (GM.instance.Host3.GetComponent<Host>().Dead || GM.instance.Host3.GetComponent<Host>().ControllerSel == Controller.Captured)
                                    {
                                        GM.instance.Host1 = null;
                                        Dead = true;
                                        GetComponent<Renderer>().material = GM.instance.HostMat;
                                    }
                                }
                            }
                        }

                        if (Input.GetButton("P2 X") == true)
                        {
                            SpeedModifier = 2;
                        }
                        else
                        {
                            SpeedModifier = 1;
                        }
                    }
                    else if (ControllerSel != Controller.Captured)
                    {
                        ControllerSel = Controller.Empty;
                    }
                }
                
                //Player 2 Controls
                if (ControllerSel == Controller.Player2)// && GM.instance.SelectedPlayerSel == GM.SelectedPlayer.HostTwo)
                {
                    if (GM.instance.Host1 == this.gameObject)
                    {
                        if (!Victory & !Dead)
                        {
                            transform.localEulerAngles = new Vector3(0, 0, 0);

                            //print("X " + Input.GetAxis("P3 Horizontal"));
                            //print("Y " + Input.GetAxis("P3 Vertical"));

                            //if (Input.GetAxis("Axis 2") < 0f)
                            if (Input.GetAxis("P3 Vertical") < 0)
                            {
                                transform.Translate(Vector3.forward * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 2") > 0f)
                            if (Input.GetAxis("P3 Vertical") > 0)
                            {
                                transform.Translate(Vector3.back * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 1") < 0f)
                            if (Input.GetAxis("P3 Horizontal") < 0)
                            {
                                transform.Translate(Vector3.left * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 1") > 0f)
                            if (Input.GetAxis("P3 Horizontal") > 0)
                            {
                                transform.Translate(Vector3.right * GM.instance.HostSpeed * SpeedModifier);
                            }
                        }

                        if (Input.GetButtonDown("P3 A") == true)
                        {
                            if (Shovel)
                            {
                                Frozen = true;
                                Invoke("Unfreeze", GM.instance.DigTime);
                                Invoke("Dig", GM.instance.DigTime);

                                /*
                                if (Vector3.Distance(GM.instance.Grave1.transform.position, transform.position) < GM.instance.DetectionRange || Victory)
                                {
                                    if (GM.instance.Detected.Contains(this.gameObject))
                                    {
                                        GM.instance.Detected.Remove(this.gameObject);
                                    }
                                    Victory = true;
                                }
                                */
                            }
                            else
                            {
                                if (GM.instance.Sheds.Count >= 1)
                                {
                                    if (Vector3.Distance(GM.instance.Sheds[0].transform.position, transform.position) < 3 || Victory)
                                    {
                                        Frozen = true;
                                        Invoke("Unfreeze", GM.instance.InspectTime);
                                        Shovel = true;
                                    }
                                    else if (GM.instance.Sheds.Count >= 2)
                                    {
                                        if (Vector3.Distance(GM.instance.Sheds[1].transform.position, transform.position) < 3 || Victory)
                                        {
                                            Frozen = true;
                                            Invoke("Unfreeze", GM.instance.InspectTime);
                                            Shovel = true;
                                        }
                                        else if (GM.instance.Sheds.Count >= 3)
                                        {
                                            if (Vector3.Distance(GM.instance.Sheds[2].transform.position, transform.position) < 3 || Victory)
                                            {
                                                Frozen = true;
                                                Invoke("Unfreeze", GM.instance.InspectTime);
                                                Shovel = true;
                                            }
                                            else if (GM.instance.Sheds.Count >= 4)
                                            {
                                                if (Vector3.Distance(GM.instance.Sheds[3].transform.position, transform.position) < 3 || Victory)
                                                {
                                                    Frozen = true;
                                                    Invoke("Unfreeze", GM.instance.InspectTime);
                                                    Shovel = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (Input.GetButtonDown("P3 B") == true & !Dead && CanSwitch)
                        {
                            if (GM.instance.Hosts.Count >= 4)
                            {
                                GM.instance.Host1 = null;
                                Dead = true;
                                GetComponent<Renderer>().material = GM.instance.HostMat;
                            }
                            else if (GM.instance.Hosts.Count == 3)
                            {
                                if (GM.instance.Host2.GetComponent<Host>().Dead || GM.instance.Host2.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    GM.instance.Host1 = null;
                                    Dead = true;
                                    GetComponent<Renderer>().material = GM.instance.HostMat;
                                }
                                if (GM.instance.Host3.GetComponent<Host>().Dead || GM.instance.Host3.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    GM.instance.Host1 = null;
                                    Dead = true;
                                    GetComponent<Renderer>().material = GM.instance.HostMat;
                                }
                            }
                            else if (GM.instance.Hosts.Count == 2)
                            {
                                if (GM.instance.Host2.GetComponent<Host>().Dead || GM.instance.Host2.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    if (GM.instance.Host3.GetComponent<Host>().Dead || GM.instance.Host3.GetComponent<Host>().ControllerSel == Controller.Captured)
                                    {
                                        GM.instance.Host1 = null;
                                        Dead = true;
                                        GetComponent<Renderer>().material = GM.instance.HostMat;
                                    }
                                }
                            }
                        }

                        if (Input.GetButton("P3 X") == true)
                        {
                            SpeedModifier = 2;
                        }
                        else
                        {
                            SpeedModifier = 1;
                        }
                    }
                    else if (ControllerSel != Controller.Captured)
                    {
                        ControllerSel = Controller.Empty;
                    }
                }
                
                //Player 3 Controls
                if (ControllerSel == Controller.Player3)// && GM.instance.SelectedPlayerSel == GM.SelectedPlayer.HostThree)
                {
                    if (GM.instance.Host1 == this.gameObject)
                    {
                        if (!Victory & !Dead)
                        {
                            transform.localEulerAngles = new Vector3(0, 0, 0);

                            //print("X " + Input.GetAxis("P4 Horizontal"));
                            //print("Y " + Input.GetAxis("P4 Vertical"));

                            //if (Input.GetAxis("Axis 2") < 0f)
                            if (Input.GetAxis("P4 Vertical") < 0)
                            {
                                transform.Translate(Vector3.forward * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 2") > 0f)
                            if (Input.GetAxis("P4 Vertical") > 0)
                            {
                                transform.Translate(Vector3.back * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 1") < 0f)
                            if (Input.GetAxis("P4 Horizontal") < 0)
                            {
                                transform.Translate(Vector3.left * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 1") > 0f)
                            if (Input.GetAxis("P4 Horizontal") > 0)
                            {
                                transform.Translate(Vector3.right * GM.instance.HostSpeed * SpeedModifier);
                            }
                        }

                        if (Input.GetButtonDown("P4 A") == true)
                        {
                            if (Shovel)
                            {
                                Frozen = true;
                                Invoke("Unfreeze", GM.instance.DigTime);
                                Invoke("Dig", GM.instance.DigTime);

                                /*
                                if (Vector3.Distance(GM.instance.Grave1.transform.position, transform.position) < GM.instance.DetectionRange || Victory)
                                {
                                    if (GM.instance.Detected.Contains(this.gameObject))
                                    {
                                        GM.instance.Detected.Remove(this.gameObject);
                                    }
                                    Victory = true;
                                }
                                */
                            }
                            else
                            {
                                if (GM.instance.Sheds.Count >= 1)
                                {
                                    if (Vector3.Distance(GM.instance.Sheds[0].transform.position, transform.position) < 3 || Victory)
                                    {
                                        Frozen = true;
                                        Invoke("Unfreeze", GM.instance.InspectTime);
                                        Shovel = true;
                                    }
                                    else if (GM.instance.Sheds.Count >= 2)
                                    {
                                        if (Vector3.Distance(GM.instance.Sheds[1].transform.position, transform.position) < 3 || Victory)
                                        {
                                            Frozen = true;
                                            Invoke("Unfreeze", GM.instance.InspectTime);
                                            Shovel = true;
                                        }
                                        else if (GM.instance.Sheds.Count >= 3)
                                        {
                                            if (Vector3.Distance(GM.instance.Sheds[2].transform.position, transform.position) < 3 || Victory)
                                            {
                                                Frozen = true;
                                                Invoke("Unfreeze", GM.instance.InspectTime);
                                                Shovel = true;
                                            }
                                            else if (GM.instance.Sheds.Count >= 4)
                                            {
                                                if (Vector3.Distance(GM.instance.Sheds[3].transform.position, transform.position) < 3 || Victory)
                                                {
                                                    Frozen = true;
                                                    Invoke("Unfreeze", GM.instance.InspectTime);
                                                    Shovel = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (Input.GetButtonDown("P4 B") == true & !Dead && CanSwitch)
                        {
                            if (GM.instance.Hosts.Count >= 4)
                            {
                                GM.instance.Host1 = null;
                                Dead = true;
                                GetComponent<Renderer>().material = GM.instance.HostMat;
                            }
                            else if (GM.instance.Hosts.Count == 3)
                            {
                                if (GM.instance.Host2.GetComponent<Host>().Dead || GM.instance.Host2.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    GM.instance.Host1 = null;
                                    Dead = true;
                                    GetComponent<Renderer>().material = GM.instance.HostMat;
                                }
                                if (GM.instance.Host3.GetComponent<Host>().Dead || GM.instance.Host3.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    GM.instance.Host1 = null;
                                    Dead = true;
                                    GetComponent<Renderer>().material = GM.instance.HostMat;
                                }
                            }
                            else if (GM.instance.Hosts.Count == 2)
                            {
                                if (GM.instance.Host2.GetComponent<Host>().Dead || GM.instance.Host2.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    if (GM.instance.Host3.GetComponent<Host>().Dead || GM.instance.Host3.GetComponent<Host>().ControllerSel == Controller.Captured)
                                    {
                                        GM.instance.Host1 = null;
                                        Dead = true;
                                        GetComponent<Renderer>().material = GM.instance.HostMat;
                                    }
                                }
                            }
                        }

                        if (Input.GetButton("P4 X") == true)
                        {
                            SpeedModifier = 2;
                        }
                        else
                        {
                            SpeedModifier = 1;
                        }
                    }
                    else if (ControllerSel != Controller.Captured)
                    {
                        ControllerSel = Controller.Empty;
                    }
                }
                
            }

            if (GM.instance.keeperPlayerSel == GM.KeeperPlayer.two)
            {
                //Player 1 Controls
                if (ControllerSel == Controller.Player1)// && GM.instance.SelectedPlayerSel == GM.SelectedPlayer.HostOne)
                {
                    if (GM.instance.Host1 == this.gameObject)
                    {
                        if (!Victory & !Dead)
                        {
                            transform.localEulerAngles = new Vector3(0, 0, 0);

                            //print("X " + Input.GetAxis("P1 Horizontal"));
                            //print("Y " + Input.GetAxis("P1 Vertical"));

                            //if (Input.GetAxis("Axis 2") < 0f)
                            if (Input.GetAxis("P1 Vertical") < 0)
                            {
                                transform.Translate(Vector3.forward * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 2") > 0f)
                            if (Input.GetAxis("P1 Vertical") > 0)
                            {
                                transform.Translate(Vector3.back * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 1") < 0f)
                            if (Input.GetAxis("P1 Horizontal") < 0)
                            {
                                transform.Translate(Vector3.left * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 1") > 0f)
                            if (Input.GetAxis("P1 Horizontal") > 0)
                            {
                                transform.Translate(Vector3.right * GM.instance.HostSpeed * SpeedModifier);
                            }
                        }

                        if (Input.GetButtonDown("P1 A") == true)
                        {
                            if (Shovel)
                            {
                                Frozen = true;
                                Invoke("Unfreeze", GM.instance.DigTime);
                                Invoke("Dig", GM.instance.DigTime);

                                /*
                                if (Vector3.Distance(GM.instance.Grave1.transform.position, transform.position) < GM.instance.DetectionRange || Victory)
                                {
                                    if (GM.instance.Detected.Contains(this.gameObject))
                                    {
                                        GM.instance.Detected.Remove(this.gameObject);
                                    }
                                    Victory = true;
                                }
                                */
                            }
                            else
                            {
                                if (GM.instance.Sheds.Count >= 1)
                                {
                                    if (Vector3.Distance(GM.instance.Sheds[0].transform.position, transform.position) < 3 || Victory)
                                    {
                                        Frozen = true;
                                        Invoke("Unfreeze", GM.instance.InspectTime);
                                        Shovel = true;
                                    }
                                    else if (GM.instance.Sheds.Count >= 2)
                                    {
                                        if (Vector3.Distance(GM.instance.Sheds[1].transform.position, transform.position) < 3 || Victory)
                                        {
                                            Frozen = true;
                                            Invoke("Unfreeze", GM.instance.InspectTime);
                                            Shovel = true;
                                        }
                                        else if (GM.instance.Sheds.Count >= 3)
                                        {
                                            if (Vector3.Distance(GM.instance.Sheds[2].transform.position, transform.position) < 3 || Victory)
                                            {
                                                Frozen = true;
                                                Invoke("Unfreeze", GM.instance.InspectTime);
                                                Shovel = true;
                                            }
                                            else if (GM.instance.Sheds.Count >= 4)
                                            {
                                                if (Vector3.Distance(GM.instance.Sheds[3].transform.position, transform.position) < 3 || Victory)
                                                {
                                                    Frozen = true;
                                                    Invoke("Unfreeze", GM.instance.InspectTime);
                                                    Shovel = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (Input.GetButtonDown("P1 B") == true & !Dead && CanSwitch)
                        {
                            if (GM.instance.Hosts.Count >= 4)
                            {
                                GM.instance.Host1 = null;
                                Dead = true;
                                GetComponent<Renderer>().material = GM.instance.HostMat;
                            }
                            else if (GM.instance.Hosts.Count == 3)
                            {
                                if (GM.instance.Host2.GetComponent<Host>().Dead || GM.instance.Host2.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    GM.instance.Host1 = null;
                                    Dead = true;
                                    GetComponent<Renderer>().material = GM.instance.HostMat;
                                }
                                if (GM.instance.Host3.GetComponent<Host>().Dead || GM.instance.Host3.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    GM.instance.Host1 = null;
                                    Dead = true;
                                    GetComponent<Renderer>().material = GM.instance.HostMat;
                                }
                            }
                            else if (GM.instance.Hosts.Count == 2)
                            {
                                if (GM.instance.Host2.GetComponent<Host>().Dead || GM.instance.Host2.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    if (GM.instance.Host3.GetComponent<Host>().Dead || GM.instance.Host3.GetComponent<Host>().ControllerSel == Controller.Captured)
                                    {
                                        GM.instance.Host1 = null;
                                        Dead = true;
                                        GetComponent<Renderer>().material = GM.instance.HostMat;
                                    }
                                }
                            }
                        }

                        if (Input.GetButton("P1 X") == true)
                        {
                            SpeedModifier = 2;
                        }
                        else
                        {
                            SpeedModifier = 1;
                        }
                    }
                    else if (ControllerSel != Controller.Captured)
                    {
                        ControllerSel = Controller.Empty;
                    }
                }

                //Player 2 Controls
                if (ControllerSel == Controller.Player2)// && GM.instance.SelectedPlayerSel == GM.SelectedPlayer.HostTwo)
                {
                    if (GM.instance.Host1 == this.gameObject)
                    {
                        if (!Victory & !Dead)
                        {
                            transform.localEulerAngles = new Vector3(0, 0, 0);

                            //print("X " + Input.GetAxis("P3 Horizontal"));
                            //print("Y " + Input.GetAxis("P3 Vertical"));

                            //if (Input.GetAxis("Axis 2") < 0f)
                            if (Input.GetAxis("P3 Vertical") < 0)
                            {
                                transform.Translate(Vector3.forward * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 2") > 0f)
                            if (Input.GetAxis("P3 Vertical") > 0)
                            {
                                transform.Translate(Vector3.back * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 1") < 0f)
                            if (Input.GetAxis("P3 Horizontal") < 0)
                            {
                                transform.Translate(Vector3.left * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 1") > 0f)
                            if (Input.GetAxis("P3 Horizontal") > 0)
                            {
                                transform.Translate(Vector3.right * GM.instance.HostSpeed * SpeedModifier);
                            }
                        }

                        if (Input.GetButtonDown("P3 A") == true)
                        {
                            if (Shovel)
                            {
                                Frozen = true;
                                Invoke("Unfreeze", GM.instance.DigTime);
                                Invoke("Dig", GM.instance.DigTime);

                                /*
                                if (Vector3.Distance(GM.instance.Grave1.transform.position, transform.position) < GM.instance.DetectionRange || Victory)
                                {
                                    if (GM.instance.Detected.Contains(this.gameObject))
                                    {
                                        GM.instance.Detected.Remove(this.gameObject);
                                    }
                                    Victory = true;
                                }
                                */
                            }
                            else
                            {
                                if (GM.instance.Sheds.Count >= 1)
                                {
                                    if (Vector3.Distance(GM.instance.Sheds[0].transform.position, transform.position) < 3 || Victory)
                                    {
                                        Frozen = true;
                                        Invoke("Unfreeze", GM.instance.InspectTime);
                                        Shovel = true;
                                    }
                                    else if (GM.instance.Sheds.Count >= 2)
                                    {
                                        if (Vector3.Distance(GM.instance.Sheds[1].transform.position, transform.position) < 3 || Victory)
                                        {
                                            Frozen = true;
                                            Invoke("Unfreeze", GM.instance.InspectTime);
                                            Shovel = true;
                                        }
                                        else if (GM.instance.Sheds.Count >= 3)
                                        {
                                            if (Vector3.Distance(GM.instance.Sheds[2].transform.position, transform.position) < 3 || Victory)
                                            {
                                                Frozen = true;
                                                Invoke("Unfreeze", GM.instance.InspectTime);
                                                Shovel = true;
                                            }
                                            else if (GM.instance.Sheds.Count >= 4)
                                            {
                                                if (Vector3.Distance(GM.instance.Sheds[3].transform.position, transform.position) < 3 || Victory)
                                                {
                                                    Frozen = true;
                                                    Invoke("Unfreeze", GM.instance.InspectTime);
                                                    Shovel = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (Input.GetButtonDown("P3 B") == true & !Dead && CanSwitch)
                        {
                            if (GM.instance.Hosts.Count >= 4)
                            {
                                GM.instance.Host1 = null;
                                Dead = true;
                                GetComponent<Renderer>().material = GM.instance.HostMat;
                            }
                            else if (GM.instance.Hosts.Count == 3)
                            {
                                if (GM.instance.Host2.GetComponent<Host>().Dead || GM.instance.Host2.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    GM.instance.Host1 = null;
                                    Dead = true;
                                    GetComponent<Renderer>().material = GM.instance.HostMat;
                                }
                                if (GM.instance.Host3.GetComponent<Host>().Dead || GM.instance.Host3.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    GM.instance.Host1 = null;
                                    Dead = true;
                                    GetComponent<Renderer>().material = GM.instance.HostMat;
                                }
                            }
                            else if (GM.instance.Hosts.Count == 2)
                            {
                                if (GM.instance.Host2.GetComponent<Host>().Dead || GM.instance.Host2.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    if (GM.instance.Host3.GetComponent<Host>().Dead || GM.instance.Host3.GetComponent<Host>().ControllerSel == Controller.Captured)
                                    {
                                        GM.instance.Host1 = null;
                                        Dead = true;
                                        GetComponent<Renderer>().material = GM.instance.HostMat;
                                    }
                                }
                            }
                        }

                        if (Input.GetButton("P3 X") == true)
                        {
                            SpeedModifier = 2;
                        }
                        else
                        {
                            SpeedModifier = 1;
                        }
                    }
                    else if (ControllerSel != Controller.Captured)
                    {
                        ControllerSel = Controller.Empty;
                    }
                }

                //Player 3 Controls
                if (ControllerSel == Controller.Player3)// && GM.instance.SelectedPlayerSel == GM.SelectedPlayer.HostThree)
                {
                    if (GM.instance.Host1 == this.gameObject)
                    {
                        if (!Victory & !Dead)
                        {
                            transform.localEulerAngles = new Vector3(0, 0, 0);

                            //print("X " + Input.GetAxis("P4 Horizontal"));
                            //print("Y " + Input.GetAxis("P4 Vertical"));

                            //if (Input.GetAxis("Axis 2") < 0f)
                            if (Input.GetAxis("P4 Vertical") < 0)
                            {
                                transform.Translate(Vector3.forward * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 2") > 0f)
                            if (Input.GetAxis("P4 Vertical") > 0)
                            {
                                transform.Translate(Vector3.back * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 1") < 0f)
                            if (Input.GetAxis("P4 Horizontal") < 0)
                            {
                                transform.Translate(Vector3.left * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 1") > 0f)
                            if (Input.GetAxis("P4 Horizontal") > 0)
                            {
                                transform.Translate(Vector3.right * GM.instance.HostSpeed * SpeedModifier);
                            }
                        }

                        if (Input.GetButtonDown("P4 A") == true)
                        {
                            if (Shovel)
                            {
                                Frozen = true;
                                Invoke("Unfreeze", GM.instance.DigTime);
                                Invoke("Dig", GM.instance.DigTime);

                                /*
                                if (Vector3.Distance(GM.instance.Grave1.transform.position, transform.position) < GM.instance.DetectionRange || Victory)
                                {
                                    if (GM.instance.Detected.Contains(this.gameObject))
                                    {
                                        GM.instance.Detected.Remove(this.gameObject);
                                    }
                                    Victory = true;
                                }
                                */
                            }
                            else
                            {
                                if (GM.instance.Sheds.Count >= 1)
                                {
                                    if (Vector3.Distance(GM.instance.Sheds[0].transform.position, transform.position) < 3 || Victory)
                                    {
                                        Frozen = true;
                                        Invoke("Unfreeze", GM.instance.InspectTime);
                                        Shovel = true;
                                    }
                                    else if (GM.instance.Sheds.Count >= 2)
                                    {
                                        if (Vector3.Distance(GM.instance.Sheds[1].transform.position, transform.position) < 3 || Victory)
                                        {
                                            Frozen = true;
                                            Invoke("Unfreeze", GM.instance.InspectTime);
                                            Shovel = true;
                                        }
                                        else if (GM.instance.Sheds.Count >= 3)
                                        {
                                            if (Vector3.Distance(GM.instance.Sheds[2].transform.position, transform.position) < 3 || Victory)
                                            {
                                                Frozen = true;
                                                Invoke("Unfreeze", GM.instance.InspectTime);
                                                Shovel = true;
                                            }
                                            else if (GM.instance.Sheds.Count >= 4)
                                            {
                                                if (Vector3.Distance(GM.instance.Sheds[3].transform.position, transform.position) < 3 || Victory)
                                                {
                                                    Frozen = true;
                                                    Invoke("Unfreeze", GM.instance.InspectTime);
                                                    Shovel = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (Input.GetButtonDown("P4 B") == true & !Dead && CanSwitch)
                        {
                            if (GM.instance.Hosts.Count >= 4)
                            {
                                GM.instance.Host1 = null;
                                Dead = true;
                                GetComponent<Renderer>().material = GM.instance.HostMat;
                            }
                            else if (GM.instance.Hosts.Count == 3)
                            {
                                if (GM.instance.Host2.GetComponent<Host>().Dead || GM.instance.Host2.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    GM.instance.Host1 = null;
                                    Dead = true;
                                    GetComponent<Renderer>().material = GM.instance.HostMat;
                                }
                                if (GM.instance.Host3.GetComponent<Host>().Dead || GM.instance.Host3.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    GM.instance.Host1 = null;
                                    Dead = true;
                                    GetComponent<Renderer>().material = GM.instance.HostMat;
                                }
                            }
                            else if (GM.instance.Hosts.Count == 2)
                            {
                                if (GM.instance.Host2.GetComponent<Host>().Dead || GM.instance.Host2.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    if (GM.instance.Host3.GetComponent<Host>().Dead || GM.instance.Host3.GetComponent<Host>().ControllerSel == Controller.Captured)
                                    {
                                        GM.instance.Host1 = null;
                                        Dead = true;
                                        GetComponent<Renderer>().material = GM.instance.HostMat;
                                    }
                                }
                            }
                        }

                        if (Input.GetButton("P4 X") == true)
                        {
                            SpeedModifier = 2;
                        }
                        else
                        {
                            SpeedModifier = 1;
                        }
                    }
                    else if (ControllerSel != Controller.Captured)
                    {
                        ControllerSel = Controller.Empty;
                    }
                }

            }

            if (GM.instance.keeperPlayerSel == GM.KeeperPlayer.three)
            {
                //Player 1 Controls
                if (ControllerSel == Controller.Player1)// && GM.instance.SelectedPlayerSel == GM.SelectedPlayer.HostOne)
                {
                    if (GM.instance.Host1 == this.gameObject)
                    {
                        if (!Victory & !Dead)
                        {
                            transform.localEulerAngles = new Vector3(0, 0, 0);

                            //print("X " + Input.GetAxis("P1 Horizontal"));
                            //print("Y " + Input.GetAxis("P1 Vertical"));

                            //if (Input.GetAxis("Axis 2") < 0f)
                            if (Input.GetAxis("P1 Vertical") < 0)
                            {
                                transform.Translate(Vector3.forward * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 2") > 0f)
                            if (Input.GetAxis("P1 Vertical") > 0)
                            {
                                transform.Translate(Vector3.back * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 1") < 0f)
                            if (Input.GetAxis("P1 Horizontal") < 0)
                            {
                                transform.Translate(Vector3.left * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 1") > 0f)
                            if (Input.GetAxis("P1 Horizontal") > 0)
                            {
                                transform.Translate(Vector3.right * GM.instance.HostSpeed * SpeedModifier);
                            }
                        }

                        if (Input.GetButtonDown("P1 A") == true)
                        {
                            if (Shovel)
                            {
                                Frozen = true;
                                Invoke("Unfreeze", GM.instance.DigTime);
                                Invoke("Dig", GM.instance.DigTime);

                                /*
                                if (Vector3.Distance(GM.instance.Grave1.transform.position, transform.position) < GM.instance.DetectionRange || Victory)
                                {
                                    if (GM.instance.Detected.Contains(this.gameObject))
                                    {
                                        GM.instance.Detected.Remove(this.gameObject);
                                    }
                                    Victory = true;
                                }
                                */
                            }
                            else
                            {
                                if (GM.instance.Sheds.Count >= 1)
                                {
                                    if (Vector3.Distance(GM.instance.Sheds[0].transform.position, transform.position) < 3 || Victory)
                                    {
                                        Frozen = true;
                                        Invoke("Unfreeze", GM.instance.InspectTime);
                                        Shovel = true;
                                    }
                                    else if (GM.instance.Sheds.Count >= 2)
                                    {
                                        if (Vector3.Distance(GM.instance.Sheds[1].transform.position, transform.position) < 3 || Victory)
                                        {
                                            Frozen = true;
                                            Invoke("Unfreeze", GM.instance.InspectTime);
                                            Shovel = true;
                                        }
                                        else if (GM.instance.Sheds.Count >= 3)
                                        {
                                            if (Vector3.Distance(GM.instance.Sheds[2].transform.position, transform.position) < 3 || Victory)
                                            {
                                                Frozen = true;
                                                Invoke("Unfreeze", GM.instance.InspectTime);
                                                Shovel = true;
                                            }
                                            else if (GM.instance.Sheds.Count >= 4)
                                            {
                                                if (Vector3.Distance(GM.instance.Sheds[3].transform.position, transform.position) < 3 || Victory)
                                                {
                                                    Frozen = true;
                                                    Invoke("Unfreeze", GM.instance.InspectTime);
                                                    Shovel = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (Input.GetButtonDown("P1 B") == true & !Dead && CanSwitch)
                        {
                            if (GM.instance.Hosts.Count >= 4)
                            {
                                GM.instance.Host1 = null;
                                Dead = true;
                                GetComponent<Renderer>().material = GM.instance.HostMat;
                            }
                            else if (GM.instance.Hosts.Count == 3)
                            {
                                if (GM.instance.Host2.GetComponent<Host>().Dead || GM.instance.Host2.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    GM.instance.Host1 = null;
                                    Dead = true;
                                    GetComponent<Renderer>().material = GM.instance.HostMat;
                                }
                                if (GM.instance.Host3.GetComponent<Host>().Dead || GM.instance.Host3.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    GM.instance.Host1 = null;
                                    Dead = true;
                                    GetComponent<Renderer>().material = GM.instance.HostMat;
                                }
                            }
                            else if (GM.instance.Hosts.Count == 2)
                            {
                                if (GM.instance.Host2.GetComponent<Host>().Dead || GM.instance.Host2.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    if (GM.instance.Host3.GetComponent<Host>().Dead || GM.instance.Host3.GetComponent<Host>().ControllerSel == Controller.Captured)
                                    {
                                        GM.instance.Host1 = null;
                                        Dead = true;
                                        GetComponent<Renderer>().material = GM.instance.HostMat;
                                    }
                                }
                            }
                        }

                        if (Input.GetButton("P1 X") == true)
                        {
                            SpeedModifier = 2;
                        }
                        else
                        {
                            SpeedModifier = 1;
                        }
                    }
                    else if (ControllerSel != Controller.Captured)
                    {
                        ControllerSel = Controller.Empty;
                    }
                }

                //Player 2 Controls
                if (ControllerSel == Controller.Player2)// && GM.instance.SelectedPlayerSel == GM.SelectedPlayer.HostTwo)
                {
                    if (GM.instance.Host1 == this.gameObject)
                    {
                        if (!Victory & !Dead)
                        {
                            transform.localEulerAngles = new Vector3(0, 0, 0);

                            //print("X " + Input.GetAxis("P2 Horizontal"));
                            //print("Y " + Input.GetAxis("P2 Vertical"));

                            //if (Input.GetAxis("Axis 2") < 0f)
                            if (Input.GetAxis("P2 Vertical") < 0)
                            {
                                transform.Translate(Vector3.forward * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 2") > 0f)
                            if (Input.GetAxis("P2 Vertical") > 0)
                            {
                                transform.Translate(Vector3.back * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 1") < 0f)
                            if (Input.GetAxis("P2 Horizontal") < 0)
                            {
                                transform.Translate(Vector3.left * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 1") > 0f)
                            if (Input.GetAxis("P2 Horizontal") > 0)
                            {
                                transform.Translate(Vector3.right * GM.instance.HostSpeed * SpeedModifier);
                            }
                        }

                        if (Input.GetButtonDown("P2 A") == true)
                        {
                            if (Shovel)
                            {
                                Frozen = true;
                                Invoke("Unfreeze", GM.instance.DigTime);
                                Invoke("Dig", GM.instance.DigTime);

                                /*
                                if (Vector3.Distance(GM.instance.Grave1.transform.position, transform.position) < GM.instance.DetectionRange || Victory)
                                {
                                    if (GM.instance.Detected.Contains(this.gameObject))
                                    {
                                        GM.instance.Detected.Remove(this.gameObject);
                                    }
                                    Victory = true;
                                }
                                */
                            }
                            else
                            {
                                if (GM.instance.Sheds.Count >= 1)
                                {
                                    if (Vector3.Distance(GM.instance.Sheds[0].transform.position, transform.position) < 3 || Victory)
                                    {
                                        Frozen = true;
                                        Invoke("Unfreeze", GM.instance.InspectTime);
                                        Shovel = true;
                                    }
                                    else if (GM.instance.Sheds.Count >= 2)
                                    {
                                        if (Vector3.Distance(GM.instance.Sheds[1].transform.position, transform.position) < 3 || Victory)
                                        {
                                            Frozen = true;
                                            Invoke("Unfreeze", GM.instance.InspectTime);
                                            Shovel = true;
                                        }
                                        else if (GM.instance.Sheds.Count >= 3)
                                        {
                                            if (Vector3.Distance(GM.instance.Sheds[2].transform.position, transform.position) < 3 || Victory)
                                            {
                                                Frozen = true;
                                                Invoke("Unfreeze", GM.instance.InspectTime);
                                                Shovel = true;
                                            }
                                            else if (GM.instance.Sheds.Count >= 4)
                                            {
                                                if (Vector3.Distance(GM.instance.Sheds[3].transform.position, transform.position) < 3 || Victory)
                                                {
                                                    Frozen = true;
                                                    Invoke("Unfreeze", GM.instance.InspectTime);
                                                    Shovel = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (Input.GetButtonDown("P2 B") == true & !Dead && CanSwitch)
                        {
                            if (GM.instance.Hosts.Count >= 4)
                            {
                                GM.instance.Host1 = null;
                                Dead = true;
                                GetComponent<Renderer>().material = GM.instance.HostMat;
                            }
                            else if (GM.instance.Hosts.Count == 3)
                            {
                                if (GM.instance.Host2.GetComponent<Host>().Dead || GM.instance.Host2.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    GM.instance.Host1 = null;
                                    Dead = true;
                                    GetComponent<Renderer>().material = GM.instance.HostMat;
                                }
                                if (GM.instance.Host3.GetComponent<Host>().Dead || GM.instance.Host3.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    GM.instance.Host1 = null;
                                    Dead = true;
                                    GetComponent<Renderer>().material = GM.instance.HostMat;
                                }
                            }
                            else if (GM.instance.Hosts.Count == 2)
                            {
                                if (GM.instance.Host2.GetComponent<Host>().Dead || GM.instance.Host2.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    if (GM.instance.Host3.GetComponent<Host>().Dead || GM.instance.Host3.GetComponent<Host>().ControllerSel == Controller.Captured)
                                    {
                                        GM.instance.Host1 = null;
                                        Dead = true;
                                        GetComponent<Renderer>().material = GM.instance.HostMat;
                                    }
                                }
                            }
                        }

                        if (Input.GetButton("P2 X") == true)
                        {
                            SpeedModifier = 2;
                        }
                        else
                        {
                            SpeedModifier = 1;
                        }
                    }
                    else if (ControllerSel != Controller.Captured)
                    {
                        ControllerSel = Controller.Empty;
                    }
                }

                //Player 3 Controls
                if (ControllerSel == Controller.Player3)// && GM.instance.SelectedPlayerSel == GM.SelectedPlayer.HostThree)
                {
                    if (GM.instance.Host1 == this.gameObject)
                    {
                        if (!Victory & !Dead)
                        {
                            transform.localEulerAngles = new Vector3(0, 0, 0);

                            //print("X " + Input.GetAxis("P4 Horizontal"));
                            //print("Y " + Input.GetAxis("P4 Vertical"));

                            //if (Input.GetAxis("Axis 2") < 0f)
                            if (Input.GetAxis("P4 Vertical") < 0)
                            {
                                transform.Translate(Vector3.forward * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 2") > 0f)
                            if (Input.GetAxis("P4 Vertical") > 0)
                            {
                                transform.Translate(Vector3.back * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 1") < 0f)
                            if (Input.GetAxis("P4 Horizontal") < 0)
                            {
                                transform.Translate(Vector3.left * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 1") > 0f)
                            if (Input.GetAxis("P4 Horizontal") > 0)
                            {
                                transform.Translate(Vector3.right * GM.instance.HostSpeed * SpeedModifier);
                            }
                        }

                        if (Input.GetButtonDown("P4 A") == true)
                        {
                            if (Shovel)
                            {
                                Frozen = true;
                                Invoke("Unfreeze", GM.instance.DigTime);
                                Invoke("Dig", GM.instance.DigTime);

                                /*
                                if (Vector3.Distance(GM.instance.Grave1.transform.position, transform.position) < GM.instance.DetectionRange || Victory)
                                {
                                    if (GM.instance.Detected.Contains(this.gameObject))
                                    {
                                        GM.instance.Detected.Remove(this.gameObject);
                                    }
                                    Victory = true;
                                }
                                */
                            }
                            else
                            {
                                if (GM.instance.Sheds.Count >= 1)
                                {
                                    if (Vector3.Distance(GM.instance.Sheds[0].transform.position, transform.position) < 3 || Victory)
                                    {
                                        Frozen = true;
                                        Invoke("Unfreeze", GM.instance.InspectTime);
                                        Shovel = true;
                                    }
                                    else if (GM.instance.Sheds.Count >= 2)
                                    {
                                        if (Vector3.Distance(GM.instance.Sheds[1].transform.position, transform.position) < 3 || Victory)
                                        {
                                            Frozen = true;
                                            Invoke("Unfreeze", GM.instance.InspectTime);
                                            Shovel = true;
                                        }
                                        else if (GM.instance.Sheds.Count >= 3)
                                        {
                                            if (Vector3.Distance(GM.instance.Sheds[2].transform.position, transform.position) < 3 || Victory)
                                            {
                                                Frozen = true;
                                                Invoke("Unfreeze", GM.instance.InspectTime);
                                                Shovel = true;
                                            }
                                            else if (GM.instance.Sheds.Count >= 4)
                                            {
                                                if (Vector3.Distance(GM.instance.Sheds[3].transform.position, transform.position) < 3 || Victory)
                                                {
                                                    Frozen = true;
                                                    Invoke("Unfreeze", GM.instance.InspectTime);
                                                    Shovel = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (Input.GetButtonDown("P4 B") == true & !Dead && CanSwitch)
                        {
                            if (GM.instance.Hosts.Count >= 4)
                            {
                                GM.instance.Host1 = null;
                                Dead = true;
                                GetComponent<Renderer>().material = GM.instance.HostMat;
                            }
                            else if (GM.instance.Hosts.Count == 3)
                            {
                                if (GM.instance.Host2.GetComponent<Host>().Dead || GM.instance.Host2.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    GM.instance.Host1 = null;
                                    Dead = true;
                                    GetComponent<Renderer>().material = GM.instance.HostMat;
                                }
                                if (GM.instance.Host3.GetComponent<Host>().Dead || GM.instance.Host3.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    GM.instance.Host1 = null;
                                    Dead = true;
                                    GetComponent<Renderer>().material = GM.instance.HostMat;
                                }
                            }
                            else if (GM.instance.Hosts.Count == 2)
                            {
                                if (GM.instance.Host2.GetComponent<Host>().Dead || GM.instance.Host2.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    if (GM.instance.Host3.GetComponent<Host>().Dead || GM.instance.Host3.GetComponent<Host>().ControllerSel == Controller.Captured)
                                    {
                                        GM.instance.Host1 = null;
                                        Dead = true;
                                        GetComponent<Renderer>().material = GM.instance.HostMat;
                                    }
                                }
                            }
                        }

                        if (Input.GetButton("P4 X") == true)
                        {
                            SpeedModifier = 2;
                        }
                        else
                        {
                            SpeedModifier = 1;
                        }
                    }
                    else if (ControllerSel != Controller.Captured)
                    {
                        ControllerSel = Controller.Empty;
                    }
                }

            }

            if (GM.instance.keeperPlayerSel == GM.KeeperPlayer.four)
            {
                //Player 1 Controls
                if (ControllerSel == Controller.Player1)// && GM.instance.SelectedPlayerSel == GM.SelectedPlayer.HostOne)
                {
                    if (GM.instance.Host1 == this.gameObject)
                    {
                        if (!Victory & !Dead)
                        {
                            transform.localEulerAngles = new Vector3(0, 0, 0);

                            //print("X " + Input.GetAxis("P1 Horizontal"));
                            //print("Y " + Input.GetAxis("P1 Vertical"));

                            //if (Input.GetAxis("Axis 2") < 0f)
                            if (Input.GetAxis("P1 Vertical") < 0)
                            {
                                transform.Translate(Vector3.forward * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 2") > 0f)
                            if (Input.GetAxis("P1 Vertical") > 0)
                            {
                                transform.Translate(Vector3.back * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 1") < 0f)
                            if (Input.GetAxis("P1 Horizontal") < 0)
                            {
                                transform.Translate(Vector3.left * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 1") > 0f)
                            if (Input.GetAxis("P1 Horizontal") > 0)
                            {
                                transform.Translate(Vector3.right * GM.instance.HostSpeed * SpeedModifier);
                            }
                        }

                        if (Input.GetButtonDown("P1 A") == true)
                        {
                            if (Shovel)
                            {
                                Frozen = true;
                                Invoke("Unfreeze", GM.instance.DigTime);
                                Invoke("Dig", GM.instance.DigTime);

                                /*
                                if (Vector3.Distance(GM.instance.Grave1.transform.position, transform.position) < GM.instance.DetectionRange || Victory)
                                {
                                    if (GM.instance.Detected.Contains(this.gameObject))
                                    {
                                        GM.instance.Detected.Remove(this.gameObject);
                                    }
                                    Victory = true;
                                }
                                */
                            }
                            else
                            {
                                if (GM.instance.Sheds.Count >= 1)
                                {
                                    if (Vector3.Distance(GM.instance.Sheds[0].transform.position, transform.position) < 3 || Victory)
                                    {
                                        Frozen = true;
                                        Invoke("Unfreeze", GM.instance.InspectTime);
                                        Shovel = true;
                                    }
                                    else if (GM.instance.Sheds.Count >= 2)
                                    {
                                        if (Vector3.Distance(GM.instance.Sheds[1].transform.position, transform.position) < 3 || Victory)
                                        {
                                            Frozen = true;
                                            Invoke("Unfreeze", GM.instance.InspectTime);
                                            Shovel = true;
                                        }
                                        else if (GM.instance.Sheds.Count >= 3)
                                        {
                                            if (Vector3.Distance(GM.instance.Sheds[2].transform.position, transform.position) < 3 || Victory)
                                            {
                                                Frozen = true;
                                                Invoke("Unfreeze", GM.instance.InspectTime);
                                                Shovel = true;
                                            }
                                            else if (GM.instance.Sheds.Count >= 4)
                                            {
                                                if (Vector3.Distance(GM.instance.Sheds[3].transform.position, transform.position) < 3 || Victory)
                                                {
                                                    Frozen = true;
                                                    Invoke("Unfreeze", GM.instance.InspectTime);
                                                    Shovel = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (Input.GetButtonDown("P1 B") == true & !Dead && CanSwitch)
                        {
                            if (GM.instance.Hosts.Count >= 4)
                            {
                                GM.instance.Host1 = null;
                                Dead = true;
                                GetComponent<Renderer>().material = GM.instance.HostMat;
                            }
                            else if (GM.instance.Hosts.Count == 3)
                            {
                                if (GM.instance.Host2.GetComponent<Host>().Dead || GM.instance.Host2.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    GM.instance.Host1 = null;
                                    Dead = true;
                                    GetComponent<Renderer>().material = GM.instance.HostMat;
                                }
                                if (GM.instance.Host3.GetComponent<Host>().Dead || GM.instance.Host3.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    GM.instance.Host1 = null;
                                    Dead = true;
                                    GetComponent<Renderer>().material = GM.instance.HostMat;
                                }
                            }
                            else if (GM.instance.Hosts.Count == 2)
                            {
                                if (GM.instance.Host2.GetComponent<Host>().Dead || GM.instance.Host2.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    if (GM.instance.Host3.GetComponent<Host>().Dead || GM.instance.Host3.GetComponent<Host>().ControllerSel == Controller.Captured)
                                    {
                                        GM.instance.Host1 = null;
                                        Dead = true;
                                        GetComponent<Renderer>().material = GM.instance.HostMat;
                                    }
                                }
                            }
                        }

                        if (Input.GetButton("P1 X") == true)
                        {
                            SpeedModifier = 2;
                        }
                        else
                        {
                            SpeedModifier = 1;
                        }
                    }
                    else if (ControllerSel != Controller.Captured)
                    {
                        ControllerSel = Controller.Empty;
                    }
                }

                //Player 2 Controls
                if (ControllerSel == Controller.Player2)// && GM.instance.SelectedPlayerSel == GM.SelectedPlayer.HostTwo)
                {
                    if (GM.instance.Host1 == this.gameObject)
                    {
                        if (!Victory & !Dead)
                        {
                            transform.localEulerAngles = new Vector3(0, 0, 0);

                            //print("X " + Input.GetAxis("P2 Horizontal"));
                            //print("Y " + Input.GetAxis("P2 Vertical"));

                            //if (Input.GetAxis("Axis 2") < 0f)
                            if (Input.GetAxis("P2 Vertical") < 0)
                            {
                                transform.Translate(Vector3.forward * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 2") > 0f)
                            if (Input.GetAxis("P2 Vertical") > 0)
                            {
                                transform.Translate(Vector3.back * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 1") < 0f)
                            if (Input.GetAxis("P2 Horizontal") < 0)
                            {
                                transform.Translate(Vector3.left * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 1") > 0f)
                            if (Input.GetAxis("P2 Horizontal") > 0)
                            {
                                transform.Translate(Vector3.right * GM.instance.HostSpeed * SpeedModifier);
                            }
                        }

                        if (Input.GetButtonDown("P2 A") == true)
                        {
                            if (Shovel)
                            {
                                Frozen = true;
                                Invoke("Unfreeze", GM.instance.DigTime);
                                Invoke("Dig", GM.instance.DigTime);

                                /*
                                if (Vector3.Distance(GM.instance.Grave1.transform.position, transform.position) < GM.instance.DetectionRange || Victory)
                                {
                                    if (GM.instance.Detected.Contains(this.gameObject))
                                    {
                                        GM.instance.Detected.Remove(this.gameObject);
                                    }
                                    Victory = true;
                                }
                                */
                            }
                            else
                            {
                                if (GM.instance.Sheds.Count >= 1)
                                {
                                    if (Vector3.Distance(GM.instance.Sheds[0].transform.position, transform.position) < 3 || Victory)
                                    {
                                        Frozen = true;
                                        Invoke("Unfreeze", GM.instance.InspectTime);
                                        Shovel = true;
                                    }
                                    else if (GM.instance.Sheds.Count >= 2)
                                    {
                                        if (Vector3.Distance(GM.instance.Sheds[1].transform.position, transform.position) < 3 || Victory)
                                        {
                                            Frozen = true;
                                            Invoke("Unfreeze", GM.instance.InspectTime);
                                            Shovel = true;
                                        }
                                        else if (GM.instance.Sheds.Count >= 3)
                                        {
                                            if (Vector3.Distance(GM.instance.Sheds[2].transform.position, transform.position) < 3 || Victory)
                                            {
                                                Frozen = true;
                                                Invoke("Unfreeze", GM.instance.InspectTime);
                                                Shovel = true;
                                            }
                                            else if (GM.instance.Sheds.Count >= 4)
                                            {
                                                if (Vector3.Distance(GM.instance.Sheds[3].transform.position, transform.position) < 3 || Victory)
                                                {
                                                    Frozen = true;
                                                    Invoke("Unfreeze", GM.instance.InspectTime);
                                                    Shovel = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (Input.GetButtonDown("P2 B") == true & !Dead && CanSwitch)
                        {
                            if (GM.instance.Hosts.Count >= 4)
                            {
                                GM.instance.Host1 = null;
                                Dead = true;
                                GetComponent<Renderer>().material = GM.instance.HostMat;
                            }
                            else if (GM.instance.Hosts.Count == 3)
                            {
                                if (GM.instance.Host2.GetComponent<Host>().Dead || GM.instance.Host2.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    GM.instance.Host1 = null;
                                    Dead = true;
                                    GetComponent<Renderer>().material = GM.instance.HostMat;
                                }
                                if (GM.instance.Host3.GetComponent<Host>().Dead || GM.instance.Host3.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    GM.instance.Host1 = null;
                                    Dead = true;
                                    GetComponent<Renderer>().material = GM.instance.HostMat;
                                }
                            }
                            else if (GM.instance.Hosts.Count == 2)
                            {
                                if (GM.instance.Host2.GetComponent<Host>().Dead || GM.instance.Host2.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    if (GM.instance.Host3.GetComponent<Host>().Dead || GM.instance.Host3.GetComponent<Host>().ControllerSel == Controller.Captured)
                                    {
                                        GM.instance.Host1 = null;
                                        Dead = true;
                                        GetComponent<Renderer>().material = GM.instance.HostMat;
                                    }
                                }
                            }
                        }

                        if (Input.GetButton("P2 X") == true)
                        {
                            SpeedModifier = 2;
                        }
                        else
                        {
                            SpeedModifier = 1;
                        }
                    }
                    else if (ControllerSel != Controller.Captured)
                    {
                        ControllerSel = Controller.Empty;
                    }
                }

                //Player 3 Controls
                if (ControllerSel == Controller.Player3)// && GM.instance.SelectedPlayerSel == GM.SelectedPlayer.HostThree)
                {
                    if (GM.instance.Host1 == this.gameObject)
                    {
                        if (!Victory & !Dead)
                        {
                            transform.localEulerAngles = new Vector3(0, 0, 0);

                            //print("X " + Input.GetAxis("P3 Horizontal"));
                            //print("Y " + Input.GetAxis("P3 Vertical"));

                            //if (Input.GetAxis("Axis 2") < 0f)
                            if (Input.GetAxis("P3 Vertical") < 0)
                            {
                                transform.Translate(Vector3.forward * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 2") > 0f)
                            if (Input.GetAxis("P3 Vertical") > 0)
                            {
                                transform.Translate(Vector3.back * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 1") < 0f)
                            if (Input.GetAxis("P3 Horizontal") < 0)
                            {
                                transform.Translate(Vector3.left * GM.instance.HostSpeed * SpeedModifier);
                            }
                            //if (Input.GetAxis("Axis 1") > 0f)
                            if (Input.GetAxis("P3 Horizontal") > 0)
                            {
                                transform.Translate(Vector3.right * GM.instance.HostSpeed * SpeedModifier);
                            }
                        }

                        if (Input.GetButtonDown("P3 A") == true)
                        {
                            if (Shovel)
                            {
                                Frozen = true;
                                Invoke("Unfreeze", GM.instance.DigTime);
                                Invoke("Dig", GM.instance.DigTime);

                                /*
                                if (Vector3.Distance(GM.instance.Grave1.transform.position, transform.position) < GM.instance.DetectionRange || Victory)
                                {
                                    if (GM.instance.Detected.Contains(this.gameObject))
                                    {
                                        GM.instance.Detected.Remove(this.gameObject);
                                    }
                                    Victory = true;
                                }
                                */
                            }
                            else
                            {
                                if (GM.instance.Sheds.Count >= 1)
                                {
                                    if (Vector3.Distance(GM.instance.Sheds[0].transform.position, transform.position) < 3 || Victory)
                                    {
                                        Frozen = true;
                                        Invoke("Unfreeze", GM.instance.InspectTime);
                                        Shovel = true;
                                    }
                                    else if (GM.instance.Sheds.Count >= 2)
                                    {
                                        if (Vector3.Distance(GM.instance.Sheds[1].transform.position, transform.position) < 3 || Victory)
                                        {
                                            Frozen = true;
                                            Invoke("Unfreeze", GM.instance.InspectTime);
                                            Shovel = true;
                                        }
                                        else if (GM.instance.Sheds.Count >= 3)
                                        {
                                            if (Vector3.Distance(GM.instance.Sheds[2].transform.position, transform.position) < 3 || Victory)
                                            {
                                                Frozen = true;
                                                Invoke("Unfreeze", GM.instance.InspectTime);
                                                Shovel = true;
                                            }
                                            else if (GM.instance.Sheds.Count >= 4)
                                            {
                                                if (Vector3.Distance(GM.instance.Sheds[3].transform.position, transform.position) < 3 || Victory)
                                                {
                                                    Frozen = true;
                                                    Invoke("Unfreeze", GM.instance.InspectTime);
                                                    Shovel = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (Input.GetButtonDown("P3 B") == true & !Dead && CanSwitch)
                        {
                            if (GM.instance.Hosts.Count >= 4)
                            {
                                GM.instance.Host1 = null;
                                Dead = true;
                                GetComponent<Renderer>().material = GM.instance.HostMat;
                            }
                            else if (GM.instance.Hosts.Count == 3)
                            {
                                if (GM.instance.Host2.GetComponent<Host>().Dead || GM.instance.Host2.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    GM.instance.Host1 = null;
                                    Dead = true;
                                    GetComponent<Renderer>().material = GM.instance.HostMat;
                                }
                                if (GM.instance.Host3.GetComponent<Host>().Dead || GM.instance.Host3.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    GM.instance.Host1 = null;
                                    Dead = true;
                                    GetComponent<Renderer>().material = GM.instance.HostMat;
                                }
                            }
                            else if (GM.instance.Hosts.Count == 2)
                            {
                                if (GM.instance.Host2.GetComponent<Host>().Dead || GM.instance.Host2.GetComponent<Host>().ControllerSel == Controller.Captured)
                                {
                                    if (GM.instance.Host3.GetComponent<Host>().Dead || GM.instance.Host3.GetComponent<Host>().ControllerSel == Controller.Captured)
                                    {
                                        GM.instance.Host1 = null;
                                        Dead = true;
                                        GetComponent<Renderer>().material = GM.instance.HostMat;
                                    }
                                }
                            }
                        }

                        if (Input.GetButton("P3 X") == true)
                        {
                            SpeedModifier = 2;
                        }
                        else
                        {
                            SpeedModifier = 1;
                        }
                    }
                    else if (ControllerSel != Controller.Captured)
                    {
                        ControllerSel = Controller.Empty;
                    }
                }

            }
        }

        if (Victory)
        {
            GetComponent<Rigidbody>().useGravity = false;
            transform.Translate(Vector3.up * 0.1f);
            if (ControllerSel == Controller.Player1)
            {
                if (GM.instance.Grave1.transform.localPosition.y > -1)
                {
                    GM.instance.Grave1.transform.Translate(Vector3.down * 0.05f);
                }
            }
            if (ControllerSel == Controller.Player2)
            {
                if (GM.instance.Grave2.transform.localPosition.y > -1)
                {
                    GM.instance.Grave2.transform.Translate(Vector3.down * 0.05f);
                }
            }
            if (ControllerSel == Controller.Player3)
            {
                if (GM.instance.Grave3.transform.localPosition.y > -1)
                {
                    GM.instance.Grave3.transform.Translate(Vector3.down * 0.05f);
                }
            }
        }
    }

    void CanSwitchDelay()
    {
        CanSwitch = true;
    }

    void Unfreeze()
    {
        Frozen = false;
    }

    void Dig()
    {
        if (ControllerSel == Controller.Player1)
        {
            if (Vector3.Distance(GM.instance.Grave1.transform.position, transform.position) < GM.instance.DetectionRange || Victory)
            {
                if (GM.instance.Detected.Contains(this.gameObject))
                {
                    GM.instance.Detected.Remove(this.gameObject);
                }
                Victory = true;
            }
        }
        if (ControllerSel == Controller.Player2)
        {
            if (Vector3.Distance(GM.instance.Grave2.transform.position, transform.position) < GM.instance.DetectionRange || Victory)
            {
                if (GM.instance.Detected.Contains(this.gameObject))
                {
                    GM.instance.Detected.Remove(this.gameObject);
                }
                Victory = true;
            }
        }
        if (ControllerSel == Controller.Player3)
        {
            if (Vector3.Distance(GM.instance.Grave3.transform.position, transform.position) < GM.instance.DetectionRange || Victory)
            {
                if (GM.instance.Detected.Contains(this.gameObject))
                {
                    GM.instance.Detected.Remove(this.gameObject);
                }
                Victory = true;
            }
        }
    }

    void NewDirectionFB()
    {
        RandomDirection0FB = Random.Range(0, 3);
        Invoke("NewDirectionFB", Random.Range(1, 5));
    }
    void NewDirectionLR()
    {
        RandomDirection0LR = Random.Range(0, 3);
        Invoke("NewDirectionLR", Random.Range(1, 5));
    }

    void NewDestination()
    {
        if (ControllerSel == Controller.Empty & !Dead)
        {
            float RandomSpeed = Random.Range(0.0f, 1.0f);

            if (RandomSpeed < 0.2f)
            {
                SpeedModifier = 2f;
            }
            else if (RandomSpeed < 0.4f)
            {
                SpeedModifier = 0;
            }
            else
            {
                SpeedModifier = 1;
            }
            Agent.destination = GM.instance.Graves[Random.Range(0, GM.instance.Graves.Count)].transform.position;
            Invoke("NewDestination", Random.Range(3, 15));
        }
    }

}
