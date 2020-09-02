using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerInfo;
    int score;

    Vector2 playerStartPos;

    void Awake()
    {
        playerStartPos = playerInfo.transform.position;
    }

    void Start()
    {
        
    }

    void Update()
    {
        fallCheck();
    }

    void fallCheck()
    {
        int fallDMG = 30;

        if (playerInfo.transform.position.y <= -5.5)
        {
            playerInfo.GetComponent<PlayerScript>().TakeDamage(fallDMG);
            playerInfo.transform.position = playerStartPos;
        }

    }

    void gameOver()
    {

    }
}   
