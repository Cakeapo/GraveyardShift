using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_GameOver : MonoBehaviour {

    public GameObject GameOver, AnyKey, GameOverAnim, GOTest;
    public bool IsGameover, CanExit;

	// Use this for initialization
	void Start () {

        IsGameover = false;
        CanExit = false;
	}
	
	// Update is called once per frame
	void Update () {


		if (IsGameover)
        {

            Invoke("GameoverDelay", 3);
          
        }

        if(CanExit == true)
        {
            if (Input.anyKey)
            {
                SceneManager.LoadScene("MainUI");
            }
        }

	}
    public void GameoverDelay()
    {
        AnyKey.SetActive(true);
        CanExit = true;
    }


    public void GameOverTest()
    {
        GameOver.SetActive(true);
        GOTest.SetActive(false);
        IsGameover = true;
        GameOverAnim.GetComponent<Animator>().Play("GameOver");
    }
}
