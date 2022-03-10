using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    float AttackDelayTime;
    bool leftshoot;

    bool enemytag;
    bool rangeEnemytag;

    Enemy enemy;
    RangeEnemy rangeEnemy;
    ConditionManager CM;

    public float distance;
    Quaternion eyedirection;
    Quaternion left = Quaternion.Euler(0, 180, 0);
    Quaternion right = Quaternion.Euler(0, 0, 0);


    void Start()
    {
        if (gameObject.tag == "Enemy")
        {
            enemy = GetComponent<Enemy>();
            enemytag = true;
            rangeEnemytag = false;
            AttackDelayTime = enemy.AttackSpeed;
        }

        else if (gameObject.tag == "RangeEnemy")
        {
            rangeEnemy = GetComponent<RangeEnemy>();
            rangeEnemytag = true;
            enemytag = false;
            AttackDelayTime = rangeEnemy.AttackSpeed;
        }



        CM = GetComponent<ConditionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        AttackDelayTime -= Time.deltaTime;

        if (AttackDelayTime < 0)
        {
            AttackDelayTime = 0;
        }

        /*float*/
        distance = Vector3.Distance(transform.position, target.transform.position);
        if (transform.position.x - target.transform.position.x < 0)
        {
            leftshoot = true;
        }
        else
        {
            leftshoot = false;
        }
        if (!CM.isFaint)
        {
            if (enemytag == true)
            {
                if (distance <= enemy.fieldOfVision)
                {
                    FaceTarget();
                    if (distance <= enemy.atkRange && AttackDelayTime == 0)
                    {
                        enemy.Attack();

                        AttackDelayTime = enemy.AttackSpeed;
                    }
                    else
                    {
                        MoveToTarget();
                    }
                }
            }
            else if (rangeEnemytag == true)
            {
                if (distance <= rangeEnemy.fieldOfVision)
                {
                    FaceTarget();
                    if (distance <= rangeEnemy.atkRange && AttackDelayTime == 0)
                    {
                        //rangeEnemy.RangeAttack(target.transform.position);
                        if (distance <= rangeEnemy.satkRange)
                        {
                            Vector2 direction;
                            direction = leftshoot ? Vector2.left : Vector2.right; // leftshoot이 참이면 left, 거짓이면 right를 direction에 저장
                            rangeEnemy.backstep(direction);
                        }
                        else
                        {
                            rangeEnemy.RangeAttack(target.transform.position);
                        }
                        AttackDelayTime = rangeEnemy.AttackSpeed;
                    }
                    else if (distance >= rangeEnemy.atkRange - 1)
                    {
                        MoveToTarget();
                    }
                }
            }
        }
    }

    void MoveToTarget()
    {
        float dir = Mathf.Abs(target.transform.position.x - transform.position.x);
        //  dir = (dir < 0) ? -1 : 1;
        if (dir >= 1)
        {
            if (enemytag == true)
                transform.Translate(new Vector2(1, 0) * enemy.moveSpeed * Time.deltaTime);

            else if (rangeEnemytag == true)
                transform.Translate(new Vector2(1, 0) * rangeEnemy.moveSpeed * Time.deltaTime);
        }

    }


    void FaceTarget()
    {

        if (target.transform.position.x - transform.position.x < 0) // 타겟이 왼쪽에 있을 때
        {
            if (eyedirection != left)
            {
                // 멈추는 함수 구현할것
                eyedirection = left;
            }
            
            //gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else // 타겟이 오른쪽에 있을 때
        {
            if (eyedirection != right)
            {
                // 멈추는 함수 구현할것
                eyedirection = right;
            }

            //gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        gameObject.transform.rotation = eyedirection;
    }
}