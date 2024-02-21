using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCheckPoint : MonoBehaviour
{
    public GameObject PlayerTarget;

    [field: SerializeField] public Transform checkPoint;

    [field: SerializeField] public float distanceFromCheckpointToRight;

    [field: SerializeField] public float distanceFromCheckpointToLeft;

    public Vector3 playerCheckPoint;

    public bool atCheckPointReached;

    private void Start()
    {
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == PlayerTarget)
        {
            playerCheckPoint = new Vector3(checkPoint.position.x, checkPoint.position.y, transform.position.z);
            atCheckPointReached = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerTarget)
        {
            atCheckPointReached = false;
        }
    }
}
