using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int score;
    GameObject player;

    void Awake()
    {
        player = GameObject.Find("Player");
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
