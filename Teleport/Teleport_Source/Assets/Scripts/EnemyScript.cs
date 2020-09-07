using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject bullet;
    public GameObject player;

    public float speed;

    public float shootTime;
    float nextShotTime;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        shootPlayer();
        rotateTowardsPlayer();
    }

    void shootPlayer()
    {
        nextShotTime += Time.deltaTime;
        if(nextShotTime >= shootTime)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            nextShotTime = 0;
        }
    }

    void rotateTowardsPlayer()
    {
        Vector3 vectorToTarget = player.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
    }
}
