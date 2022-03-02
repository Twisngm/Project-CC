using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Collider2D collision;
    public float dmg;

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
            Debug.Log("피격 : " + dmg);
            if (collision.GetComponentInParent<Player>().shield > 0)
            {
                collision.GetComponentInParent<Player>().shield -= dmg;
            }
            else
                collision.GetComponentInParent<Player>().nowHP -= dmg;

            Destroy(gameObject);
        }
    }
}
/* 마크 */
