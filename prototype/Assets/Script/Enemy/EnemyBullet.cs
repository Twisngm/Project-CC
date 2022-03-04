using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Collider2D collision;

    public Player player;

    private void Start()
    {

    }
    void Update()
    {
        OnTriggerEnter2D(collision);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.name == "Ground" || collision.gameObject.tag == "BulletWall")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {

            Destroy(gameObject);
        }
    }
}
