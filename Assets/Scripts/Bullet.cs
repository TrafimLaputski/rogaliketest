using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int lifeTime;
    [SerializeField] int damage;
    [SerializeField] float speed;
    [SerializeField] int angle;
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -3);
        transform.Rotate(transform.rotation.x, transform.rotation.y, Random.Range(-angle, angle+1 ));
        lifeTime *= 50;
    }

    
    void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed);
        lifeTime--;
        if (lifeTime == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Health>())
        {
            collision.gameObject.GetComponent<Health>().HP = damage;
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
