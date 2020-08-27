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

    bool waveIsDone = true;
    int enemyCount;

    float posX, posY;
    Vector2 pos;

    void Update()
    {
        waveCountText.text = "WAVE : " + waveCount.ToString();

        if(waveIsDone = enemyCount == 0)
        {
            downTime += Time.deltaTime;
            if(downTime >= timeBetweenWaves)
            {
                waveCount++;
                downTime = 0;
                StartCoroutine(waveSpawner());
            }
                 
        }
    }

    public void updateEnemyCount()
    {
        enemyCount--;
    }

    IEnumerator waveSpawner()
    {    
        waveIsDone = false;

        for (int i = 0; i < enemyToSpawn; i++)
        {
            enemyCount += 1;
            posX = Random.Range(-14, 14);
            posY = Random.Range(5, -4.2f);
            pos = new Vector2(posX, posY);

            GameObject enemyClone = Instantiate(enemy,pos,transform.rotation);

            yield return new WaitForSeconds(spawnRate);
        }
   
        spawnRate -= 0.15f;
        enemyToSpawn += 4;
        waveIsDone = true;
          
    }

}
