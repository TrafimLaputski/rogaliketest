using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Gun gun;
    [SerializeField] Joystick joystick;
    [SerializeField] float speed = 2; 
    Rigidbody2D playerBody;
    Vector2 moveDir;
    bool facingRight = true;
    bool gunRotate;
    private void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        moveDir = new Vector2(joystick.Horizontal, joystick.Vertical);
        if (gunRotate)
        {
            speed  = 2;
            if ((gun.transform.localScale.y == -1) && (facingRight == true))
            {
                Flip();
            }
            else if ((gun.transform.localScale.y == 1) && (facingRight == false))
            {
                Flip();
            }

        }
        else
        {
            speed = 4;
            if (facingRight && moveDir.x < 0)
            {
                Flip();
            }
            else if (!facingRight && moveDir.x > 0)
            {
                Flip();
            }
        }

       
    }
    void FixedUpdate()
    {
        playerBody.MovePosition(playerBody.position + moveDir * speed * Time.fixedDeltaTime);
    }
    private void Flip()
    {
        if (facingRight)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        facingRight = !facingRight;
    }

    public bool GunRotate
    {
        get
        {
            return gunRotate;
        }
        set
        {
            gunRotate = value;
        }
    }


}
