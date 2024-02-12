using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageInRange : MonoBehaviour
{
    public Boss boss;

    private void Update()
    {
        boss = FindObjectOfType<Boss>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) Debug.Log("Hello");
    }
}
