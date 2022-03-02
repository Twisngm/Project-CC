using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class ConditionManager : MonoBehaviour
{
    public bool isStigma;
    public float Stigma = 115;

    public bool isFlame;
    public float FlameDuring;
    public float FlameDMG;
    public float FlameDelay = 1;

    public bool isFaint;
    public float FaintDuring;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FlameDamage();
        if (FlameDuring != 0)
        {  
            Invoke("FlameOff", FlameDuring);
            FlameDuring = 0;
        }
        if(FlameDelay != 0)
        {
            FlameDelay -= Time.deltaTime;
        }
        if(FlameDelay < 0)
        {
            FlameDelay = 0;
        }

        if (FaintDuring != 0)
        {
            Invoke("FaintOff", FaintDuring);
            FaintDuring = 0;
        }
    }

    void FlameDamage()
    {
        if(isFlame && FlameDelay == 0)
        {
            gameObject.GetComponent<Enemy>().nowHP -= FlameDMG;
            FlameDelay = 1;

        }
    }

    void FlameOff()
    {
        isFlame = false;
    }

    void FaintOff()
    {
        isFaint = false;
    }

}
