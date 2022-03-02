using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnItUp : MonoBehaviour
{

    FireSorcerer FS;
    // Start is called before the first frame update
    void Start()
    {
        FS = GameObject.FindGameObjectWithTag("Player").GetComponent<FireSorcerer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 11)
        {
           
            StartCoroutine("Burning",collision.gameObject);
        }
    }

    IEnumerator Burning(GameObject target)
    {
        if (FS.Wisp)
        {
            target.GetComponentInParent<ConditionManager>().isFaint = true;
            target.GetComponentInParent<ConditionManager>().FaintDuring = 2f;
        }
        target.GetComponentInParent<Enemy>().Hit();
        for (int i = 0; i<FS.Hit_num; i++)
        {
           
            if(target.GetComponentInParent<ConditionManager>().isFlame)
            {
                target.GetComponentInParent<Enemy>().nowHP -= (FS.Atk * 0.6f * FS.Burning_Enhanece_[0]);
            }
            else
            target.GetComponentInParent<Enemy>().nowHP -= (FS.Atk * 0.4f * FS.Burning_Enhanece_[0]);

            yield return new WaitForSeconds(0.1f);
        }
    }
}
