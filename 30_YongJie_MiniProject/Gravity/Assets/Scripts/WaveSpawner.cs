using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Text waveCountText;
    int waveCount = 0;

    public float spawnRate;
    public float timeBetweenWaves;
    float downTime;

    public int enemyToSpawn;

    public GameObject enemy;

    int enemyCount;

    float posX, posY;
    Vector2 pos;

    void Update()
    {
        downTime += Time.deltaTime;
        if (downTime >= timeBetweenWaves)
        {
            waveCount++;
            downTime = 0;
            StartCoroutine(waveSpawner());
        }
    }

    IEnumerator waveSpawner()
    {    
        for (int i = 0; i < enemyToSpawn; i++)
        {
            enemyCount += 1;
            posX = Random.Range(-12, 35);
            posY = Random.Range(-0.5f, -2.5f);
            pos = new Vector2(posX, posY);

            GameObject enemyClone = Instantiate(enemy,pos,transform.rotation);

            yield return new WaitForSeconds(spawnRate);
        }
        
        if(spawnRate <= 0.15f)
        {
            spawnRate = 0.15f;
        }
        else
        {
            spawnRate -= 0.15f;
        }
       
          
    }

}
