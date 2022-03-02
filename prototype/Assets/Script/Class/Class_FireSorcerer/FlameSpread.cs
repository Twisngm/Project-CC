using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameSpread : MonoBehaviour
{
    FireSorcerer FS;
    private void OnEnable()
    {
        FS = GameObject.FindGameObjectWithTag("Player").GetComponent<FireSorcerer>();
        Invoke("Destroy", FS.FlameSpread_During);
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 11)
        {
            Debug.Log("¹¹¾ß");
            collision.GetComponentInParent<ConditionManager>().isFlame = true;
            collision.GetComponentInParent<ConditionManager>().FlameDuring = 5;
            collision.GetComponentInParent<ConditionManager>().FlameDMG = (FS.Atk * 0.2f * FS.Burning_Enhanece_[1] * FS.Fierce_Flame_);
        }

    }

    void Destroy()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
