using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun2D : MonoBehaviour
{
    public Transform shootpoint;
    public GameObject bulletPrefab;

    private void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Instantiate(bulletPrefab, shootpoint.position, transform.rotation);
        }
    }
}
