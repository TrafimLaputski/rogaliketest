using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Energy : MonoBehaviour
{
    [SerializeField] Image bar;
    [SerializeField] float maxEnergy;
    float energyCount;
    float Timer = 500;
    void Start()
    {
        bar.fillAmount = 0f;
    }

    public float NewEnergy
    {
        set
        {
            energyCount += value;
            if (energyCount > maxEnergy)
            {
                energyCount = maxEnergy;
            }
            bar.color = new Color(1, 1, 1,1);
            Timer = 500;
            bar.fillAmount = energyCount / maxEnergy;
        }

        get
        {
            return (energyCount / maxEnergy);
        }
    }

    private void FixedUpdate()
    {
        Timer--;
        if (Timer <= 100 && Timer >0 )
        {
            bar.color = new Color(1, 1, 1, Timer/100);
        }
        else if (Timer < 0)
        {
            Timer = 0;
        }
     
    }
}
