using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float maxSpawnPosX;
    public float minSpawnPosX;

    public float maxSpawnPosY;
    public float minSpawnPosY;

    public float spawnCooldown;

    GameObject player;
    [SerializeField] int enemies = 10;
    [SerializeField ]int enemiesSpawned;
    float timeUntilSpawn = 0;
    int wave;

    void Start()
    {
        wave = 1;
        player = GameObject.Find("Player");
    }

    void Update()
    {
        timeUntilSpawn += Time.deltaTime;

        if (timeUntilSpawn >= spawnCooldown)
        {
         // Do your enemy spawns here
         if(enemiesSpawned < enemies)
         {
            spawnEnemy();
            enemiesSpawned++;
         }

         // Reset for next spawn
         timeUntilSpawn = 0;
        }
    }

    void spawnEnemy()
    {
        float spawnPointX = Random.Range(minSpawnPosX, maxSpawnPosX);
        float spawnPointY = Random.Range(minSpawnPosY, maxSpawnPosY);
        Vector2 spawnPosition = new Vector2(spawnPointX, spawnPointY);

        Instantiate(enemy, spawnPosition, Quaternion.identity);
    }
}
