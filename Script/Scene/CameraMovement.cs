using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [field: SerializeField] public Transform player;

    public Vector2 maxPosition;
    public Vector2 minPosition;
    
    private void LateUpdate()
    {
        if(transform.position != player.position)
        {
            Vector3 playerPosition = new Vector3(player.position.x,player.position.y,transform.position.z);

            playerPosition.x = Mathf.Clamp(playerPosition.x,minPosition.x,maxPosition.x);

            transform.position = playerPosition;
        }
    }
}
