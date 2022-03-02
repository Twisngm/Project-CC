using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpJudge : MonoBehaviour
{

    public BoxCollider2D col;
    // Start is called before the first frame update
    void Start()
    {
        col = transform.parent.GetComponent<BoxCollider2D>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Platform")
        {

            col.isTrigger = true;
          
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Platform")
        {
            col.isTrigger = false;   
        }
    }
}
