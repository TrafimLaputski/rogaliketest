using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCoin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            collision.gameObject.GetComponent<Energy>().NewEnergy = 5;
            Destroy(gameObject);
        }
    }
}
