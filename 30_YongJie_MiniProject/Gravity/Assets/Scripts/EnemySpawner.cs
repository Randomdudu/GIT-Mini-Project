using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public int score;
    public float spawnCooldown;

    GameObject player;
    float timeUntilSpawn = 0;
    int spawnTime;
    int spawning;
    int wave;
    int enemies = 10;

    void Start()
    {
        wave = 1;
        player = GameObject.Find("Player");
    }

    void Update()
    {
        timeUntilSpawn -= Time.deltaTime;

        if (timeUntilSpawn <= 0)
        {
         // Do your enemy spawns here
         
         // Reset for next spawn
         timeUntilSpawn = spawnCooldown;
        }
        for (int enemiesSpawned = 0; enemiesSpawned < enemies; enemiesSpawned++)
        {
            spawnEnemy();

        }
    }

    void spawnEnemy()
    {
        float spawnPointX = Random.Range(-25, 25);
        float spawnPointY = Random.Range(-0.1f, 4.5f);
        Vector2 spawnPosition = new Vector2(spawnPointX, spawnPointY);

        Instantiate(enemy, spawnPosition, Quaternion.identity);
    }
}
