using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public Player player;
    public Enemy enemy;

    public float Damage;
    // Start is called before the first frame update
    void Start()
    {
        if(tag.Equals("Normal_Attack") || tag.Equals("Skill_Attack"))
           player = GetComponentInParent<Player>();
        else if(tag.Equals("Enemy_Normal_Attack"))
        {
            enemy = GetComponentInParent<Enemy>();
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // 플레이어 공격
        if(collision.gameObject.layer == 11)
        {
            // 치명타 및 상태이상 효과 데미지 확인
            if (player != null)
            {
                Damage = player.Atk;

                if (player.SetCritical())
                {
                    Damage *= (player.CrtHit / 100);
                }
            }
            if (collision.GetComponentInParent<ConditionManager>().isStigma)
            {
                Damage *= (collision.GetComponentInParent<ConditionManager>().Stigma / 100);
            }

            if (tag.Equals("Normal_Attack")) // 일반 공격
            {
                Debug.Log(Damage);
                collision.GetComponentInParent<Enemy>().nowHP -= Damage;
             
            }
           
            else if (tag.Equals("Skill_Attack"))  // 플레이어 스킬공격
            {
                Debug.Log(Damage * 1.2f);
                collision.GetComponentInParent<Enemy>().nowHP -= Damage * 1.2f;
              
            }
          //  Debug.Log(collision.GetComponent<Enemy>().nowHP);
        }

     

        // 적 일반공격
        if(collision.gameObject.layer == 15 && tag.Equals("Enemy_Normal_Attack"))
        {
            Debug.Log("피격 :" + enemy.atkDmg);
            if(collision.GetComponentInParent<Player>().shield > 0)
            {
                collision.GetComponentInParent<Player>().shield -= enemy.atkDmg;
            }
          
            else
            collision.GetComponentInParent<Player>().nowHP -= enemy.atkDmg;
          
        }
        if (collision.gameObject.layer == 15 && tag.Equals("Enemy_Range_Attack"))
        {
            Debug.Log("피격 : " + enemy.atkDmg);
            if (collision.GetComponentInParent<Player>().shield > 0)
            {
                collision.GetComponentInParent<Player>().shield -= enemy.atkDmg;
            }
            else
                collision.GetComponentInParent<Player>().nowHP -= enemy.atkDmg;
        }
    }
}
