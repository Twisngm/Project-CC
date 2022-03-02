using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pirate : Player
{
    [Header("해적")]
    public GameObject BattleShipMast;
    public float BattleShipDuring;


    [Header("스킬트리")]
    public bool DuringUP;
    public bool StigmaUP;
    public bool fastCall;
    public bool SupportShooting;
    public bool Expanding_Area;
    public bool Support_Fleet;
    public bool Sailing;
    public bool ShipSpeedUP;
    public bool Protecting_Fleet;
    public bool noteUP;


    [Header("패시브")]
    public float DuringUP_;
    public float StigmaUP_;
    public float fastCall_;
    public float Expanding_Area_;
    public float[] Support_Fleet_ = new float[2];
    public float ShipSpeedUP_;
    public float Protecting_Fleet_;
    public float noteUP_;


    ConditionManager condManager = new ConditionManager();

    float originAtk;
    float originSpeed;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

 
    protected override void Start()
    {
        base.Start();
     

        DuringUP_ = BattleShipDuring + 2;
        StigmaUP_ = condManager.Stigma + 8;
        fastCall_ = SkillCoolTime - 2;
        Expanding_Area_ = BattleShipMast.GetComponentInChildren<CircleCollider2D>().radius * 1.5f;
        Support_Fleet_[0] = Atk * 1.3f;
        Support_Fleet_[1] = SkillCoolTime + 8;
        originAtk = Atk;
        originSpeed = MaxSpeed;
        ShipSpeedUP_ = MaxSpeed * 1.15f;
        Protecting_Fleet_ = MaxHP * 0.3f;
        noteUP_ = BattleShipMast.GetComponentInChildren<Pirate_Sail>().AttackDelay - 0.5f;

    }

    protected override void Update()
    {
        base.Update();

        DuringUP = GetComponent<ClassSkillTree>().SkillTree[0];
        StigmaUP = GetComponent<ClassSkillTree>().SkillTree[1];
        fastCall = GetComponent<ClassSkillTree>().SkillTree[2];
        SupportShooting = GetComponent<ClassSkillTree>().SkillTree[3];
        Expanding_Area = GetComponent<ClassSkillTree>().SkillTree[4];
        Support_Fleet = GetComponent<ClassSkillTree>().SkillTree[5];
        Sailing = GetComponent<ClassSkillTree>().SkillTree[6];
        ShipSpeedUP = GetComponent<ClassSkillTree>().SkillTree[7];
        Protecting_Fleet = GetComponent<ClassSkillTree>().SkillTree[8];
        noteUP = GetComponent<ClassSkillTree>().SkillTree[9];

        if (DuringUP)
            BattleShipDuring = DuringUP_;
      
        if (StigmaUP)
            condManager.Stigma = StigmaUP_;

        if (fastCall)
            SkillCoolTime = fastCall_;

        if (Expanding_Area)
            BattleShipMast.GetComponentInChildren<CircleCollider2D>().radius = Expanding_Area_;
   
        if(Support_Fleet)
        {
            SkillCoolTime = Support_Fleet_[1];
            BattleShipMast.GetComponentInChildren<SpriteRenderer>().color = Color.green;
            if (BattleShipMast.activeSelf)
            {
                Atk = Support_Fleet_[0];            
            }
            else
            {
                Atk = originAtk;      
            }
        }
     
        if(Sailing)
        {
            BattleShipMast.GetComponentInChildren<Pirate_Sail>().isFollowing = true;
        }

        if(ShipSpeedUP)
        {
            if (BattleShipMast.activeSelf)
            {
                MaxSpeed = ShipSpeedUP_;
            }
            else
                MaxSpeed = originSpeed;
        }

       

        if(noteUP)
        {
            BattleShipMast.GetComponentInChildren<Pirate_Sail>().AttackDelay = noteUP_;
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    protected override void OnCollisionStay2D(Collision2D collision)
    {
        base.OnCollisionStay2D(collision);
    }

    public override void Skill()
    {
        base.Skill();
        BattleShipMastSpawn();
    }

    void BattleShipMastSpawn()
    {
        BattleShipMast.transform.position = transform.position;
        BattleShipMast.SetActive(true);
        Invoke("BattleShipMastLeave", BattleShipDuring);
    }
    void BattleShipMastLeave()
    {
        BattleShipMast.SetActive(false);
    }
    
    
  

}
