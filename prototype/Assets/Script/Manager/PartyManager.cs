using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{

    public List<GameObject> PartyList;
    public GameObject Starting;
    public int PartyIndex = 0;

    float changeDelay = 2;


    static PartyManager instance = null;

    private void Awake()
    {
        if(null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static PartyManager Instance
    {
        get
        {
            if(null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Starting = PartyList[0];
    }

    // Update is called once per frame
    void Update()
    {
        Pick();
        Next();
        Back();
        CoolDown();

        if(changeDelay != 0)
        {
            changeDelay -= Time.deltaTime;
        }

        if(changeDelay < 0)
        {
            changeDelay = 0;
        }

        if(PartyIndex > PartyList.Count - 1)
        {
            PartyIndex = 0;
        }
    }

    void Pick()
    {
        for(int i = 0; i < PartyList.Count; i++)
        {
            if (i == PartyIndex)
            {
                PartyList[i].SetActive(true);
        
                Starting = PartyList[i];
            }
            else
                PartyList[i].SetActive(false);
        }
      
    }

    void Next()
    {
        if(Input.GetKeyDown(KeyCode.E) && changeDelay == 0)
        {
            PartyIndex++;
            changeDelay = 2;
    
        }
    }

    void Back()
    {
        if(Input.GetKeyDown(KeyCode.Q) && changeDelay == 0)
        {
            PartyIndex--;
            changeDelay = 2;
    
        }
    }

    void CoolDown()
    {
        for(int i = 0; i < PartyList.Count; i++)
        {
           if(PartyList[i].GetComponent<Player>().SkillCoolTime_Cur != 0 && i != PartyIndex)
            {
                PartyList[i].GetComponent<Player>().SkillCoolTime_Cur -= Time.deltaTime;
            }
        }
    }
}
