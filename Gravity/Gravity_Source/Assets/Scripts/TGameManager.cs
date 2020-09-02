using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TGameManager : MonoBehaviour
{
    public bool pause = true;

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

}
