using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Pause : MonoBehaviour {

    public GameObject Paused, Controls, Options, GameOverTest, QuitConfirm;
    public UI_GameOver GameOver;
    public bool IsPaused, InControls, InOptions, InQuitConfirm;

	// Use this for initialization
	void Start () {
        GameOver = GameObject.Find("UI_GameOver_Manager").GetComponent<UI_GameOver>();
        IsPaused = false;
        InControls = false;
        InOptions = false;
        InQuitConfirm = false;
	}
	
	// Update is called once per frame
	void Update () {
		

        if (Input.GetKeyDown(KeyCode.P))
        {

            if (IsPaused == false)
            {
                if(GameOver.IsGameover == false)
                {
                    Time.timeScale = 0;
                    Paused.SetActive(true);
                    GameOverTest.SetActive(false);
                    IsPaused = true;
                }
              
            }
            else
            {
                if (InControls == false)
                {
                    if (InOptions == false)
                    {
                        if (InQuitConfirm == false)
                        {
                            Time.timeScale = 1;
                            Paused.SetActive(false);
                            GameOverTest.SetActive(true);
                            IsPaused = false;
                        }
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
        QuitConfirm.SetActive(false);
        Paused.SetActive(true);
        InControls = false;
        InOptions = false;
        InQuitConfirm = false;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        Paused.SetActive(false);
        IsPaused = false;
    }
    public void ConfirmQuit()
    {
        QuitConfirm.SetActive(true);
        Paused.SetActive(false);
        InQuitConfirm = true;
    }
    public void QuitToMain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainUI");
    }
}
