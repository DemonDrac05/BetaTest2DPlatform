using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttackDistanceCheck : MonoBehaviour
{
    public GameObject PlayerTarget { get; set; }

    public Boss boss;

    private void Awake()
    {
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");

        boss = GetComponentInParent<Boss>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerTarget)
        {
            boss.SetNormalAttackDistance(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerTarget)
        {
            boss.SetNormalAttackDistance(false);
        }
    }
}
