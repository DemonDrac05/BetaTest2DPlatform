using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInRangeAttack : MonoBehaviour
{
    public GameObject PlayerTarget { get; set; }

    public Boss boss;

    public Player player;

    private void Awake()
    {
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");

        boss = GetComponentInParent<Boss>();

        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerTarget
            && boss.attackTimer < 0.7f && boss.attackTimer > 0.3f)
        {
            boss.SetInAttackRange(true);

            #region Let Damage On Player
            if (player.GotDamagedTime <= 0f 
                || player.GotDamagedTime >= player.GotDamagedDuration)
                player.CurrentHealth -= 5f;
            #endregion
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerTarget)
        {
            boss.SetInAttackRange(false);
        }
    }
}
