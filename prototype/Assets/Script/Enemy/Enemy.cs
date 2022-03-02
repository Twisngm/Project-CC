using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Enemy : MonoBehaviour
{
    public string enemyName;
    public float MaxHP;
    public float nowHP;
 
    public float atkDmg;
    public float AttackSpeed;
    public float moveSpeed;
    public float atkRange;
    public float fieldOfVision;

    public bool KnockBackAble;
    public bool AttackCancleAble;
    
    public GameObject Nomal_Attack;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        Nomal_Attack = transform.Find("Attack").gameObject; 
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 13)
        {
            Hit();
           
        }
    }

    public void Attack()
    {
       
        anim.SetTrigger("Attack");
    }


    public void Hit()
    {
        ItemDB.Instance.Hit_Stack++; // È÷Æ® ½ºÅÃ Áõ°¡
        if (AttackCancleAble)
        {
            anim.SetTrigger("Hit");
        }
        if (KnockBackAble) // ³Ë¹é
        {
            Debug.Log("¹¹¾ß");
            gameObject.GetComponent<Rigidbody2D>().AddForce((Vector2.up * 5f), ForceMode2D.Impulse);
            gameObject.GetComponent<Rigidbody2D>().AddForce((transform.right * -5f), ForceMode2D.Impulse);
        }
        StartCoroutine("HitBlink");

    }

    IEnumerator HitBlink()
    {
        for (int i = 0; i < 3; i++)
        {
            gameObject.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0.3f), 0.2f);
            yield return new WaitForSeconds(0.2f);
            gameObject.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 1), 0.2f);
            yield return new WaitForSeconds(0.2f);
        }
    }

}
