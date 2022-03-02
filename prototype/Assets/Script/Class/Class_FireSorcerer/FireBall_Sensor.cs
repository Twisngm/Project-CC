using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall_Sensor : MonoBehaviour
{
    FireBall FB;
    List<GameObject> targetList = new List<GameObject>();
    float distance = 1000000000;
    int targetNum;
    // Start is called before the first frame update
    void Start()
    {
        FB = GetComponentInParent<FireBall>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 11)
        {
            if(!targetList.Contains(collision.gameObject))
            {
                targetList.Add(collision.gameObject);
                for (int i = 0; i < targetList.Count; i++)
                {
                    if (Vector2.Distance(gameObject.transform.position, targetList[i].transform.position) < distance)
                    {
                        distance = Vector2.Distance(gameObject.transform.position, targetList[i].transform.position);
                        targetNum = i;
                    }
                
                }
                FB.target = targetList[targetNum];
            }
        }
       
    }
}
