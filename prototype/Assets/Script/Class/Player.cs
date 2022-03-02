using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class Player : MonoBehaviour
{
    public enum Class
    {
        Supporter,
        Wizard,
        Warrior
    }

    public enum AvoidType
    {
        Rolling,
        Teleport
    }

    [SerializeField]
    Class ClassType;

    [SerializeField]
    AvoidType avoidType;

    [Header("스탯")]
    public float MaxHP; // 최대 체력
    public float nowHP; // 현재 체력
    public float shield; // 보호막
    public float Atk; // 공격력
    public float Crt; // 치명타 확률
    public float CrtHit; // 치명타 피해량
    public float Avd; // 회피율
    public float AtkDMG; // 공격 데미지
    public float SkillDMG; // 스킬 데미지
    public float TelpoDistance; // 텔포 거리

    [Header("이동")]
    public float MaxFallingSpeed; // 낙하 최대 속도
    public float Speed; // 가속도
    public float MaxSpeed; // 최고 속도
    public float JumpPower; // 점프 힘

    [Header("쿨타임")]
    public float SkillCoolTime; // 스킬쿨타임
    public float SkillCoolTime_Cur; // 스킬쿨타임 현재
    public float TelpoCoolTime;
    public float TelpoCoolTime_Cur;

    [Header("행동")]
    public int JumpCnt = 2; // 점프 가능 횟수
    public bool isRoll; // 구르기 중?
    public bool isAction; // 액션 중?
    public bool isMoving; // 움직이는 중?
    public bool isAttack; // 공격 중?
    public bool isPlatform; // 지면 닿는 중 ? 
    public bool isCritical;

    protected Rigidbody2D rigid;
    protected Animator anim;
    protected SpriteRenderer SR;
    protected float h, v;



    protected virtual void OnEnable()
    {
        this.gameObject.transform.position = PartyManager.Instance.Starting.transform.position;
    }



    protected virtual void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        SR = GetComponent<SpriteRenderer>();
        Speed = MaxSpeed;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        /// 플레이어 조작
        /// 
        if (!Pause.Instance.isPause) // 퍼즈 아닐때만 가능
        {
            CloseUP_Attack(); // 근접공격

            if (ClassType == Class.Wizard) // 마법사 타입 공격
            {
                if (Input.GetMouseButtonDown(0) && !isAction)
                {
                    Magic_Attack(); // 마법공격
                }

            }
            if (Input.GetKeyDown(KeyCode.Space) && !Input.GetKey(KeyCode.S) && JumpCnt > 0 && !isAction)
            {
                Jump(); // 점프

            }


            Rolling(); // 구르기


            if (Input.GetMouseButtonDown(1) && SkillCoolTime_Cur == 0)
            {
                Skill(); // 스킬
            }

        }
        if(SkillCoolTime_Cur != 0)
        {
            SkillCoolTime_Cur -= Time.deltaTime;
        }
     
        if(SkillCoolTime_Cur < 0)
        {
            SkillCoolTime_Cur = 0;
        }

        if (shield < 0)
        {
             nowHP -= shield;
            shield = 0;
        }

        if(TelpoCoolTime_Cur != 0)
        {
            TelpoCoolTime_Cur -= Time.deltaTime;
        }
        if(TelpoCoolTime_Cur < 0)
        {
            TelpoCoolTime_Cur = 0;
        }
    }

     protected virtual void FixedUpdate()
    {
        if (!isAttack && !isRoll)
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
            rigid.velocity = new Vector2(h * Speed * 100 * Time.deltaTime, rigid.velocity.y);
          //  rigid.AddForce(Vector2.right * h * Speed * Time.deltaTime, ForceMode2D.Impulse);
        }
        if(isAttack && isPlatform)
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }

            if (rigid.velocity.x > MaxSpeed)
            {
                rigid.velocity = new Vector2(MaxSpeed, rigid.velocity.y);

            }
            else if (rigid.velocity.x < MaxSpeed * (-1))
            {
                rigid.velocity = new Vector2(MaxSpeed * (-1), rigid.velocity.y);

            }
            if (rigid.velocity.y < MaxFallingSpeed * (-1))
            {
                rigid.velocity = new Vector2(rigid.velocity.x, MaxFallingSpeed * (-1));
            }

            if (h < 0)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);

            }
            else if (h > 0)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (h != 0)
            {
                anim.SetBool("IsRun", true);
                anim.SetBool("IsIdle", false);
                isMoving = true;
            }
            else if (h == 0)
            {
                isMoving = false;
                rigid.velocity = new Vector2(0, rigid.velocity.y);
                anim.SetBool("IsRun", false);
                anim.SetBool("IsIdle", true);
            }
        

        Debug.DrawRay(new Vector2(transform.position.x - 1f, transform.position.y - 2f), Vector2.right * 2f, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x - 1f,transform.position.y - 2f), Vector2.right,2f);
        if (hit.collider != null && hit.collider.gameObject.layer == 8)
        {
           
            anim.SetBool("IsPlat", true);
            isPlatform = true;
            if(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.Space))
            {
                gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                Invoke("DownJump", 0.1f);
            }
        }
        else if (hit.collider == null)
        {
            anim.SetBool("IsPlat", false);
            isPlatform = false;
        }

        if (isRoll)
        {
            rigid.velocity = new Vector2(MaxSpeed * h * 2, 0);
        }

        if (rigid.velocity.y < 0)
        {
            GetComponent<ConstantForce2D>().force = new Vector2(0, -50);
        }
        else
            GetComponent<ConstantForce2D>().force = new Vector2(0, 0);
    }

     protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            anim.SetBool("IsPlat", true);
        }
        else
            anim.SetBool("isPlat", false);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            JumpCnt = 2;
        }
    }

    protected void CloseUP_Attack()
    {
        if(ClassType == Class.Supporter || ClassType == Class.Warrior)
        if(Input.GetMouseButtonDown(0) && !isAction)
        {
            
            anim.SetTrigger("ATK1");
        }
    }

    virtual protected void Magic_Attack()
    {
     
    }

    protected void Rolling()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isAction && isMoving)
        {
            if (avoidType == AvoidType.Rolling)
            {
                anim.SetTrigger("Rolling");
                isRoll = true;
                Invoke("CancelRolling", 0.5f);
            }
            else if(avoidType == AvoidType.Teleport && TelpoCoolTime_Cur == 0)
            {
                transform.position = new Vector2(transform.position.x + (TelpoDistance * h), transform.position.y + (TelpoDistance * v));
                TelpoCoolTime_Cur = TelpoCoolTime;
            }
        }
        
    }

    protected void CancelRolling()
    {
        isRoll = false;
    }

    protected void Jump()
    {
        anim.SetTrigger("Jump");
        rigid.velocity = new Vector2(rigid.velocity.x, 0);            
        rigid.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
      //  rigid.velocity = new Vector2(rigid.velocity.x, JumpPower);
        
        Invoke("JumpCountDown", 0.1f);

    }


    protected void JumpCountDown()
    {
        JumpCnt--;
    }

    protected void DownJump()
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
    }

    public virtual void  Skill()
    {
       anim.SetTrigger("Skill");
       SkillCoolTime_Cur = SkillCoolTime;
      
    }


    public bool SetCritical()
    {
        isCritical = (Random.Range(0, 100) < Crt);

        return isCritical;
    }
   
    public void ShieldOver()
    {
        shield = 0;
    }

   
}
