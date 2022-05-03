using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealtBar : MonoBehaviour
{
   [SerializeField] Image bar;
    Health health;
    float hp;
    void Start()
    {
        health = GetComponent<Health>();
        health.HealthAction += ChangeHp;
        hp = 1f;
    }

    void ChangeHp(float value, float oldHP)
    {
       
        hp -= value / oldHP;
        bar.fillAmount = hp;
    }
}
