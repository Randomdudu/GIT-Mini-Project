using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform player;
    public float speed;

    void Update()
    {
        if(Vector2.Distance(transform.position, player.position) > 1)
        {
            
        }
    }

    void rotateTowards(Vector2 _player)
    {
        var offSet = 90;
        Vector2 direction = _player - (Vector2)transform.position;
        direction.Normalize();
        float angel = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angel + offSet));
    }

}
