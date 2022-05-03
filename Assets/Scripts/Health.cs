using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public System.Action<float,float> HealthAction;
    [SerializeField] int hp;
    [SerializeField] GameObject en;
    [SerializeField] bool player = false;
    int oldHP;
    private void Start()
    {
        oldHP = hp;
    }
    public int HP
    {
        set
        {
            hp -= value;
            if (hp > oldHP)
            {
                hp = oldHP;
            }
            HealthAction?.Invoke(value,oldHP);
            if (hp <= 0)
            {
                if (player)
                {

                }
                else
                {
                    Destroy(gameObject);
                    Instantiate(en, transform.position, Quaternion.identity);
                }
            
            }
        }
    }

}
