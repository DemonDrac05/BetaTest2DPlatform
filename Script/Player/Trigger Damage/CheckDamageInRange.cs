using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDamageInRange : MonoBehaviour
{
    public Player player;

    public Boss boss;

    private void Update()
    {
        player = GetComponentInParent<Player>();

        boss = FindObjectOfType<Boss>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")
            && player.attackTime > 0.1f && player.attackTime < 0.4f)
        {
            player.SetInRangeAttack(true);
            boss.CurrentHealth -= 100f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")
            && player.attackTime < 0.1f && player.attackTime > 0.4f)
        {
            player.SetInRangeAttack(false);
        }
    }
}
