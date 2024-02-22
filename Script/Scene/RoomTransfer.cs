using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransfer : MonoBehaviour
{
    [field: SerializeField] public GameObject PlayerTarget;

    [field: SerializeField] public float addMinBorder;

    [field: SerializeField] public float addMaxBorder;

    [field: SerializeField] public Vector3 playerChangePos;

    public Player player;

    public CameraMovement cam;

    public float orgCamMinPos, orgCamMaxPos;

    public RoomTransitionAnimation transition;

    public bool transferred;

    private void Start()
    {
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");

        player = FindObjectOfType<Player>();

        cam = FindObjectOfType<CameraMovement>();

        transition = FindObjectOfType<RoomTransitionAnimation>();

        orgCamMinPos = cam.minPosition.x;
        orgCamMaxPos = cam.maxPosition.x;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerTarget)
        {
            transferred = true;

            if (player.IsFacingRight)
            {
                cam.minPosition.x = cam.maxPosition.x + addMinBorder;
                cam.maxPosition.x = cam.minPosition.x + addMaxBorder;
                collision.transform.position += playerChangePos;
            }

            else if (!player.IsFacingRight)
            {
                cam.minPosition.x = orgCamMinPos;
                cam.maxPosition.x = orgCamMaxPos;
                collision.transform.position -= playerChangePos;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerTarget)
        {
            transferred = false;
        }
    }
}
