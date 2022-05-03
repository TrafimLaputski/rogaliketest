using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    [SerializeField] Image bg;
    [SerializeField] GameObject player;
    [SerializeField] GameObject Gun;
    [SerializeField] GameObject Canvas;
    [SerializeField] Button home;
    [SerializeField] Button nextLevel;
    float energy;
    int timer = 100;
    private void FixedUpdate()
    {
        timer--;
        if (timer == 0)
        {
            player.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
            Gun.SetActive(true);
            Canvas.SetActive(true);
        }
        else if (timer < -1)
        {
            timer = -1;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
           energy = collision.gameObject.GetComponent<Energy>().NewEnergy;
            collision.gameObject.GetComponent<Energy>().NewEnergy = 0;
            bg.gameObject.SetActive(true);
            if (energy >= 0.5)
            {
                home.gameObject.SetActive(true);
            }

            if (energy == 1)
            {
                nextLevel.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        bg.gameObject.SetActive(false);
        home.gameObject.SetActive(false);
        nextLevel.gameObject.SetActive(false);
    }
}
