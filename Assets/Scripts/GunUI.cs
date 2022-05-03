using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUI : MonoBehaviour
{
    [SerializeField] float ammoPrice;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shotDir;
    [SerializeField] float startTime;
    [SerializeField] int shootCount;
    Gun gun;

    private void Start()
    {
        gun = FindObjectOfType<Gun>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            gun.NewGun(gameObject.GetComponent<SpriteRenderer>().sprite, startTime, bullet, shotDir, ammoPrice, shootCount);
            Destroy(gameObject);
        }
    }
}
