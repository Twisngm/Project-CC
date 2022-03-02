using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall_Explosion : MonoBehaviour
{
    FireSorcerer FS;
    private void OnEnable()
    {
        Invoke("Destroy", 0.5f);
    }
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
        
            collision.GetComponentInParent<Enemy>().nowHP -= FS.Atk * 0.3f;
            collision.GetComponentInParent<Enemy>().Hit();
        }
    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }
}
