using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransfer : MonoBehaviour
{
    [field: SerializeField] public GameObject PlayerTarget;

    [field: SerializeField] public float addMinBorder;

    [field: SerializeField] public float addMaxBorder;

    [field: SerializeField] public Vector3 playerChangePos;

    public CameraMovement cam;

    private void Start()
    {
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");

        cam = FindObjectOfType<CameraMovement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerTarget)
        {
            cam.minPosition.x = cam.maxPosition.x + addMinBorder;
            cam.maxPosition.x = cam.minPosition.x + addMaxBorder;
            collision.transform.position += playerChangePos;
        }
    }
}
