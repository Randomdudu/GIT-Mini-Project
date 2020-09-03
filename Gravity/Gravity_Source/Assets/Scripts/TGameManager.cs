using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TGameManager : MonoBehaviour
{
    public bool pause = true;
    public GameObject button;

    void Update()
    {
        if(pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void start()
    {
        pause = false;
        button.SetActive(false);
    }
}
