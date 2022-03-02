using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Pirate_Sail : MonoBehaviour
{

    public Pirate pirate;
    public float AttackDelay = 2;
    public float AttackDelayNow = 0;
    int cnt = 0;
    public List<GameObject> enemyCnt = new List<GameObject>();

    public bool isFollowing = false;
    bool AttackAble = false;

    private void OnEnable()
    {
        pirate = GameObject.Find("Pirate").GetComponent<Pirate>();
        if (pirate.Protecting_Fleet)
        {

            pirate.shield = pirate.Protecting_Fleet_;
            
            Invoke("ShieldOver", 5f);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
     void Update()
    {
        if(AttackDelayNow != 0)
         AttackDelayNow -= Time.deltaTime;
        if(AttackDelayNow < 0)
        {
            AttackDelayNow = 0;
        }
        StartCoroutine("SailAttack");

        if (isFollowing)
        {
            transform.parent.position = new Vector2(PartyManager.Instance.Starting.transform.position.x - 1.5f, PartyManager.Instance.Starting.transform.position.y + 2);
            GetComponentInChildren<Rigidbody2D>().gravityScale = 0;
            GetComponentInChildren<BoxCollider2D>().isTrigger = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.layer == 11)
        {
           
  
            collision.GetComponentInParent<ConditionManager>().isStigma = true;
            if(pirate.StigmaUP)
            {
                collision.GetComponentInParent<ConditionManager>().Stigma = pirate.StigmaUP_;
            }
                     
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 11)
        {
            if (!enemyCnt.Contains(collision.gameObject))
                enemyCnt.Add(collision.gameObject);
            if (pirate.SupportShooting && AttackAble)
            {
                AttackAble = false;
                for (int i = 0; i < enemyCnt.Count; i++)
                {
                    enemyCnt[i].GetComponentInParent<Enemy>().Hit();
                    enemyCnt[i].GetComponentInParent<Enemy>().nowHP -= (pirate.Atk * 1.3f);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            collision.GetComponentInParent<ConditionManager>().isStigma = false;
            enemyCnt.Remove(collision.gameObject);
           
        }
    }

    public IEnumerator SailAttack() // 지원사격
    {
        if (AttackDelayNow == 0 && pirate.SupportShooting)
        {
                AttackAble = true; 
                AttackDelayNow = AttackDelay;  
                
                GetComponent<SpriteRenderer>().DOColor(Color.red, 0.2f);
                yield return new WaitForSeconds(0.2f);
                GetComponent<SpriteRenderer>().DOColor(Color.gray, 0.2f);
            

        }
    }
    public void ShieldOver()
    {
        pirate.shield = 0;
    }
}
