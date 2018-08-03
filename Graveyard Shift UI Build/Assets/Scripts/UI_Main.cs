using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Main : MonoBehaviour {

    private GameObject LeftGate, RightGate;
    public GameObject activate, Controls, Options;
    public bool started, InControls, InOptions;

	// Use this for initialization
	void Start () {
        InControls = false;
        InOptions = false;
        started = true;
        LeftGate = GameObject.Find("LeftGate");
        RightGate = GameObject.Find("RightGate");
       

    }
	
	// Update is called once per frame
	void Update () {

        if (started == true)
        {
            if (Input.anyKey)
            {
                Invoke("ButtonActive", 3);
                LeftGate.GetComponent<Animator>().Play("OpenGateLeft");
                RightGate.GetComponent<Animator>().Play("OpenGateRight");
                Camera.main.GetComponent<Animator>().Play("CameraMove");
                GameObject.Find("PressAny").SetActive(false);
                started = false;
            }
        }

    }

    public void ButtonActive()
    {
        Debug.Log("hello");
        activate.SetActive(true);
    }
    
        public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    public void ControlsMenu()
    {
        Controls.SetActive(true);
        InControls = true;
    }
    public void OptionsMenu()
    {
        Options.SetActive(true);
        InOptions = true;
    }
    public void BacktoMenu()
    {
        Controls.SetActive(false);
        Options.SetActive(false);
        InControls = false;
        InOptions = false;
    }
    public void LoadNextUI()
    {
        SceneManager.LoadScene("NextUI");
    }
    
}
