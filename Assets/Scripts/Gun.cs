using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [SerializeField] Image AmmoBar;
    Text textAmmo;
    [SerializeField] float maxAmmo;
    [SerializeField] float ammoPrice;
    float ammo;
    [SerializeField] Player player;
    [SerializeField] Joystick joystick;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shotDir;
    [SerializeField] float startTime;
    [SerializeField] int shootCount;
    [SerializeField] int timeBurst;
    float rotateZ;
    float timeShot = 0;
    int timeShootCount;
    int timeBurstSafe;
    void Start()
    {
        textAmmo = AmmoBar.GetComponentInChildren<Text>();
        ammo = maxAmmo;
        textAmmo.text = "" + ammo;
        timeBurst *= 50;
        timeBurstSafe = timeBurst;
        
    }

    private void Update()
    {
        rotateZ = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;

        if (Mathf.Abs(joystick.Vertical) >= 0.1f || Mathf.Abs(joystick.Horizontal) >= 0.1f)
        {
            player.GunRotate = true;
            transform.rotation = Quaternion.Euler(0, 0, rotateZ);
            if (rotateZ > 90 || rotateZ < -90)
            {
                transform.localScale = new Vector3(1, -1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            player.GunRotate = false;
        }
        if ((Mathf.Abs(joystick.Vertical) >= 0.7f || Mathf.Abs(joystick.Horizontal) >= 0.7f) && timeShot <= 0)
        {
            Shot();
        }
        timeShot -= Time.deltaTime;
    }

    void Shot()
    {
        for (int i = 0; i < shootCount; i++)
        {
            if (ammo >= ammoPrice)
            {
                Instantiate(bullet, shotDir.position, transform.rotation);
                ammo -= ammoPrice;
                AmmoBar.fillAmount = ammo / maxAmmo;
                textAmmo.text = "" + ammo;
                timeShot = startTime;
            }
            else
            {
                Debug.Log("Out Ammo");
            }

        }
        
    }

    public int Ammo
    {
        set
        {
            ammo += value;
            if (ammo > maxAmmo)
            {
                ammo = maxAmmo;
            }
            AmmoBar.fillAmount = ammo / maxAmmo;
            textAmmo.text = "" + ammo;
        }
    }
    public void NewGun( Sprite NewSprite, float NewStartTime, GameObject NewBulet, Transform NewShotDir, float NewAmmoPrice, int NewShootCount)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = NewSprite;
        startTime = NewStartTime;
        bullet = NewBulet;
        shotDir.localPosition = NewShotDir.localPosition;
        ammoPrice = NewAmmoPrice;
        shootCount = NewShootCount;
    }
}
