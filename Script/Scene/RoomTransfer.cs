using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransfer : MonoBehaviour
{
    [field: SerializeField] public GameObject PlayerTarget;
    public Vector2 cameraChange;
    public Vector3 playerChange;

    private CameraMovement camMovement;

    public Camera mainCamera;

    private void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        
        camMovement = FindObjectOfType<CameraMovement>();

        PlayerTarget = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerTarget)
        {
            camMovement.maxPosition.x = camMovement.maxPosition.x
                + 2 * playerChange.x * (Mathf.Pow(mainCamera.orthographicSize,2) - 1);
            camMovement.minPosition.x = camMovement.maxPosition.x - cameraChange.x;
            collision.transform.position += playerChange;
        }
    }
}
