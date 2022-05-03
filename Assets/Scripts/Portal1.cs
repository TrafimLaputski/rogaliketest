using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Portal1 : MonoBehaviour
{
   [SerializeField] bool go;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (go)
        {
            GetComponent<LevelManager>().GameScene();
        }
        else
        {
            GetComponent<LevelManager>().CloseGame();
        }
    }
}
