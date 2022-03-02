using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FireSorcerer : Player
{
    [Header("불주술사")]
    public GameObject FireBallPos;
    public GameObject FireBall;
    public float FireBall_Speed;
    public float FireBall_Delay;
    public float FireBall_Delay_Cur;
    public float FlameProbability;
    public bool isFlame;
    public GameObject Inferno;
    public int Hit_num;
    public GameObject[] FlameSpread;
    public GameObject[] FlameSpreadPos;
    public float FlameSpread_During;


    [Header("스킬트리")]
    public bool Burning_Enhance;
    public bool FireBall_Induction;
    public bool Wisp;
    public bool Explosion_Enhance;
    public bool QuickShot;
    public bool Burst;
    public bool Flame_Spread;
    public bool Fierce_Flame;
    public bool Burn_All;
    public bool Rapid_Burning;

    [Header("패시브")]
    public float[] Burning_Enhanece_= new float[2];
    public float QuickShot_;
    public float Fierce_Flame_;
    public float[] Rapid_Burning_ = new float[3];

    protected override void OnEnable()
    {
        base.OnEnable();
    }

 
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        QuickShot_ = FireBall_Speed * 1.5f;
        Rapid_Burning_[0] = FireBall_Speed * 1.5f;
        Rapid_Burning_[1] = FireBall_Delay * 0.5f;
        Rapid_Burning_[2] = FlameSpread_During + 3;
    }

   

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        Burning_Enhance = GetComponent<ClassSkillTree>().SkillTree[0];
        FireBall_Induction = GetComponent<ClassSkillTree>().SkillTree[1];
        Wisp = GetComponent<ClassSkillTree>().SkillTree[2];
        Explosion_Enhance = GetComponent<ClassSkillTree>().SkillTree[3];
        QuickShot = GetComponent<ClassSkillTree>().SkillTree[4];
        Burst = GetComponent<ClassSkillTree>().SkillTree[5];
        Flame_Spread = GetComponent<ClassSkillTree>().SkillTree[6];
        Fierce_Flame = GetComponent<ClassSkillTree>().SkillTree[7];
        Burn_All = GetComponent<ClassSkillTree>().SkillTree[8];
        Rapid_Burning = GetComponent<ClassSkillTree>().SkillTree[9];

        if (FireBall_Delay_Cur != 0)
        {
            FireBall_Delay_Cur -= Time.deltaTime;
        }

        if(FireBall_Delay_Cur < 0)
        {
            FireBall_Delay_Cur = 0;
        }

        if (Burning_Enhance)
        {
            Burning_Enhanece_[0] = 1.15f;
            Burning_Enhanece_[1] = 1.10f;
        }
        else
        {
            Burning_Enhanece_[0] = 1f;
            Burning_Enhanece_[1] = 1f;
        }

        if (QuickShot)
        {
            FireBall_Speed = QuickShot_;
            Hit_num = 11;
        }

        if (Fierce_Flame)
        {
            Fierce_Flame_ = 1.15f;
        }
        else
        {
            Fierce_Flame_ = 1f;
        }

        if (Input.GetMouseButton(0) && !isAction && FireBall_Delay_Cur == 0 && Burst)
        {
            GameObject FB = Instantiate(FireBall, 
                FireBallPos.transform.position, Quaternion.Euler(0, 0, transform.rotation.y * 90 == 0 ? 90 : -90));
            FB.SetActive(true);
            FireBall_Delay_Cur = FireBall_Delay;
            if (Burn_All)
            {
                GameObject FB2 = Instantiate(FireBall, 
                new Vector2(FireBallPos.transform.position.x, FireBallPos.transform.position.y - 0.75f),Quaternion.Euler(0, 0, transform.rotation.y * 90 == 0 ? 90 : -90));
                FB2.SetActive(true);
            }
        }

        if(Rapid_Burning)
        {
            FireBall_Speed = Rapid_Burning_[0];
            FireBall_Delay = Rapid_Burning_[1];
            FlameSpread_During = Rapid_Burning_[2];
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

    protected override void Magic_Attack()
    {
        base.Magic_Attack();


        if (!Burst)
        {
            anim.SetTrigger("ATK1");


            GameObject FB = Instantiate(FireBall, FireBallPos.transform.position, Quaternion.Euler(0, 0, transform.rotation.y * 90 == 0 ? 90 : -90));
            FB.SetActive(true);


            if (Burn_All)
            {
                GameObject FB2 = Instantiate(FireBall, new Vector2(FireBallPos.transform.position.x, FireBallPos.transform.position.y - 0.75f), Quaternion.Euler(0, 0, transform.rotation.y * 90 == 0 ? 90 : -90));
                FB2.SetActive(true);
            }

        }
    }

    public override void Skill()
    {
        base.Skill();
        if (Flame_Spread)
        {
            FlameSpread[0].SetActive(true);
            FlameSpread[0].transform.position = FlameSpreadPos[0].transform.position;
            if(Burn_All)
            {
                FlameSpread[1].SetActive(true);
                FlameSpread[1].transform.position = FlameSpreadPos[1].transform.position;
            }
        }
        else
        {
            StartCoroutine("BurnItUp");
        }
    }

    IEnumerator BurnItUp()
    {
        Inferno.SetActive(true);
        yield return new WaitForSeconds(1f);
        Inferno.SetActive(false);
    }

    public bool SetFlame()
    {
        isFlame = (Random.Range(0, 100) < FlameProbability);

        return isFlame;
    }
}
