using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float cameraFollowSpeed;
    public Transform player;
    [SerializeField] CameraBounds2D bounds;
    Vector2 maxXPositions, maxYPositions;

    void Awake()
    {
        bounds.Initialize(GetComponent<Camera>());
        maxXPositions = bounds.maxXlimit;
        maxYPositions = bounds.maxYlimit;
    }

    void FixedUpdate()
    {
        Vector3 position = transform.position;
        Vector3 targetPosition = new Vector3(Mathf.Clamp(player.position.x, maxXPositions.x, maxXPositions.y), Mathf.Clamp(player.position.y, maxYPositions.x, maxYPositions.y), position.z);
        transform.position = Vector3.Lerp(position, targetPosition, Time.fixedDeltaTime * cameraFollowSpeed);
        if(player.transform.position.x <= maxXPositions.x - 3)
        {
            player.transform.position = new Vector2(maxXPositions.x - 3, player.transform.position.y);
        }
    }
}
