using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCheckPoint : MonoBehaviour
{
    public GameObject PlayerTarget;

    [field: SerializeField] public Transform checkPoint;
    public Vector3 playerCheckPoint;

    private void Start()
    {
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == PlayerTarget)
        {
            playerCheckPoint = new Vector3(checkPoint.position.x, 
                                                    checkPoint.position.y, 
                                                    transform.position.z);
            Debug.Log("Hello");
        }
    }
}
