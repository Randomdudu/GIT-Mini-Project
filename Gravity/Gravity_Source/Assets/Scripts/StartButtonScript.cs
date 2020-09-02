using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StartButtonScript : MonoBehaviour
{
    TGameManager gameManager;
    void Start()
    {
        
    }
    void update()
    {

    }

    public void onClick()
    {
        print("button clicked");
        gameManager = GetComponent<TGameManager>();
        gameManager.pause = false;

    }
}
