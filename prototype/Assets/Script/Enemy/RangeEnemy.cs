using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RangeEnemy : MonoBehaviour
{
    public string enemyName;
    public float MaxHP;
    public float nowHP;

    public float atkDmg;
    public float AttackSpeed;
    public float moveSpeed;
    public float atkRange;
    public float fieldOfVision;
    public float bulletSpeed;
    public float satkRange; // 근접회피 거리

    public bool KnockBackAble;
    public bool AttackCancleAble;

    public GameObject Range_Attack;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 13)
        {
            Hit();

        }
    }

    public void RangeAttack(Vector2 target) // 원거리 몬스터 공격
    {
        anim.SetTrigger("RangeAttack");
        fire(target);
    }

    void fire(Vector2 direction)
    {
        GameObject bullet = Instantiate(Range_Attack, transform.position, transform.rotation); // 탄환을 생성
        
        Rigidbody2D bulletrigid = bullet.GetComponent<Rigidbody2D>(); // 탄환의 rigidbody 가져오기
        bulletrigid.AddForce(direction * bulletSpeed, ForceMode2D.Impulse); // 발사
        Debug.Log("Valssa!");
    }


    public void Hit()
    {
        if (AttackCancleAble)
        {
            anim.SetTrigger("Hit");
        }
        if (KnockBackAble) // 넉백
        {
            Debug.Log("뭐야");
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
    public void backstep(Vector2 direction) // 백스텝
    {
        anim.SetTrigger("backstep");
        Debug.Log("backstep");
        fire(direction);
        fire(direction);
    }
}
