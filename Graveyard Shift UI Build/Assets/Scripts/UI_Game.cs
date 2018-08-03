using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Game : MonoBehaviour {

    public GameObject IntProgress;
    public UI_Pause puaseui;
    public UI_GameOver gameoverui;

	// Use this for initialization
	void Start () {

        IntProgress.SetActive(false);

		
	}
	
	// Update is called once per frame
	void Update () {

        if (puaseui.IsPaused == false)
        {
            if (gameoverui.IsGameover == false)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    IntProgress.GetComponent<Image>().fillAmount = 0;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    IntProgress.SetActive(true);
                    IntProgress.GetComponent<Image>().fillAmount = IntProgress.GetComponent<Image>().fillAmount + 0.025f;
                }
                else
                {
                    IntProgress.SetActive(false);
                }
            }
        }
     
	}
}
