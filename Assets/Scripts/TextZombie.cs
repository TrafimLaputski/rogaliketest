using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextZombie : MonoBehaviour
{
    [SerializeField] GameObject en;
    bool agr;
    [SerializeField] GameObject shotDir;
    [SerializeField] GameObject shotRotate;
    [SerializeField] float maxSee;
    [SerializeField] float shootDist;
    [SerializeField] int patrolTime; // cекунды
    [SerializeField] float speed; //стандарт 3.5
    [SerializeField] float startTime;
    [SerializeField] GameObject bulet;
    float timeShot = 0;
    Player player;
    int patrolTimer;
    Vector2 target;
    float rotateZ;
    void Start()
    {
        player = FindObjectOfType<Player>();
        patrolTime *= 50;
        patrolTimer = patrolTime;
        startTime *= 50;
        timeShot = startTime;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < maxSee * 3)
        {
            Scan();
        }
        Vector3 difrence = player.transform.position - transform.position;
        rotateZ = Mathf.Atan2(difrence.y, difrence.x) * Mathf.Rad2Deg;
        shotRotate.transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);
    }

    private void FixedUpdate()
    {
        patrolTimer--;
        if (patrolTimer == 0)
        {
            Patrol();
        };
        timeShot--;
    }
    private void Patrol()
    {
        int _rand = Random.Range(1, 3);
        switch (_rand)
        {
            case 1:
                target = new Vector2(transform.position.x + Random.Range(-10, 11), transform.position.y + Random.Range(-10, 11));
                patrolTimer = patrolTime;
                break;
            default:
                patrolTimer = patrolTime;
                target = transform.position;
                break;
        }

    }

    private void Scan()
    {
        RaycastHit2D hit2D = Physics2D.Linecast(transform.position, player.transform.position);
        if (hit2D.collider.gameObject.tag == "Player" && hit2D.distance <= maxSee)
        {
            agr = true;
            if (player.transform.position.x < transform.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            patrolTimer = 99999999;

            if (hit2D.distance <= shootDist)
            {
                Shoot();
            }

            if (hit2D.distance > 3)
            {
                target = transform.position;
            }

            if (hit2D.distance <= 3)
            {
                if (player.transform.position.x < transform.position.x)
                {
                    target = new Vector2(transform.position.x + 3, transform.position.y);
                }
                else
                {
                    target = new Vector2(transform.position.x - 3, transform.position.y);
                }
            }
            else
            {
                target = player.transform.position;
            }
        }

    }

    void Shoot()
    {

        if (timeShot <= 0)
        {
            Instantiate(bulet, shotDir.transform.position, Quaternion.Euler(0f, 0f, rotateZ));

            timeShot = startTime;
        }
    }
}
