using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{

    public FireSorcerer FS;
    
    public GameObject Explosion;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        FS = GameObject.FindGameObjectWithTag("Player").GetComponent<FireSorcerer>();
        Invoke("Destroy", 15f);
    }

    // Update is called once per frame
    void Update()
    {
              transform.Translate(Vector2.down * FS.FireBall_Speed * Time.deltaTime);


        if (FS.FireBall_Induction && target != null)
        {
            
                Vector2 direction = new Vector2(
                    transform.position.x - target.transform.position.x,
                    transform.position.y - target.transform.position.y
                );


                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
                Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, 1000 * Time.deltaTime);
                transform.rotation = rotation;

            
        }

        if (target != null && target.transform.parent.gameObject.activeSelf == false)
            gameObject.SetActive(false);
    }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Wall")
            {
                Debug.Log("adf");
                gameObject.SetActive(false);
            }
            else if (collision.gameObject.layer == 11)
            {
                gameObject.SetActive(false);
                FS.SetFlame();
                if (FS.SetFlame())
                {
                    collision.GetComponentInParent<ConditionManager>().isFlame = true;
                    collision.GetComponentInParent<ConditionManager>().FlameDuring = 5;
                    collision.GetComponentInParent<ConditionManager>().FlameDMG = (FS.Atk * 0.2f * FS.Burning_Enhanece_[1] * FS.Fierce_Flame_);
                }
                collision.GetComponentInParent<Enemy>().nowHP -= FS.Atk * 0.8f;
                collision.GetComponentInParent<Enemy>().Hit();
                if (FS.Explosion_Enhance)
                {
                    GameObject explosion = Instantiate(Explosion, transform.position, transform.rotation);
                    explosion.SetActive(true);
                }
            }
        }

        void Destroy()
        {
            gameObject.SetActive(false);
        }

    
}