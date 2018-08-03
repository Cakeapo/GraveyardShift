using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Pause : MonoBehaviour {

    public GameObject Paused, Controls, Options;
    public bool IsPaused, InControls, InOptions;

	// Use this for initialization
	void Start () {

        IsPaused = false;
        InControls = false;
        InOptions = false;
	}
	
	// Update is called once per frame
	void Update () {
		

        if (Input.GetKeyDown(KeyCode.P))
        {

            if (IsPaused == false)
            {
                Time.timeScale = 0;
                Paused.SetActive(true);
                IsPaused = true;
            }
            else
            {
                if (InControls == false)
                {
                    if (InOptions == false)
                    {
                        Time.timeScale = 1;
                        Paused.SetActive(false);
                        IsPaused = false;
                    }
                }
             
            }
        }
	}

    public void ControlsMenu()
    {
        Controls.SetActive(true);
        Paused.SetActive(false);
        InControls = true;
    }
    public void OptionsMenu()
    {
        Options.SetActive(true);
        Paused.SetActive(false);
        InOptions = true;
    }
    public void BacktoPause()
    {
        Controls.SetActive(false);
        Options.SetActive(false);
        Paused.SetActive(true);
        InControls = false;
        InOptions = false;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        Paused.SetActive(false);
        IsPaused = false;
    }
    public void QuitToMain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainUI");
    }
}
