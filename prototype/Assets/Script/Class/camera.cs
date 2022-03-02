using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public GameObject target;
    public float offsetY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target = PartyManager.Instance.PartyList[PartyManager.Instance.PartyIndex];
        gameObject.transform.position = new Vector3(target.transform.position.x,target.transform.position.y + offsetY, -10);
    }
}
