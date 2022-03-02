using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDB : MonoBehaviour
{
    new public string name;
    public Sprite icon;
    public string info;

    public int Hit_Stack = 0;
    int temp = 0;
    float[] originSpeed;
    public float Hit_Stack_Reset_CoolTime = 5;
    public float Hit_Stack_Reset_CoolTime_Cur = 5;

    public Dictionary<string, ItemDB> ItemData; 


    static ItemDB instance = null;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        originSpeed = new float[3];
    }

    public static ItemDB Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ItemData = new Dictionary<string, ItemDB>(); 


        // 아이템 데이터 추가
        
        ItemData.Add("Self-defense_charm", new ItemDB("Self-defense_charm", 
            Resources.Load("Ui/Icon/Item/Self-defense_charm",typeof(Sprite)) as Sprite, 
            "<color=orange><size=23><b>호신부적</b></size></color>" +
            "\n아이템 설명:" +
            "\n회피율 10% 증가" +
            "\n\"무려 죽어도 경험치를 잃지 않을 거 같은 부적\""));

        ItemData.Add("Longsword", new ItemDB("Longsword",
            Resources.Load("Ui/Icon/Item/Longsword", typeof(Sprite)) as Sprite,
            "<color=orange><size=23><b>롱소드</b></size></color>\n" +
            "아이템 설명:\n" +
            "공격력 30 증가\n" +
            "\"마을에서 350원으로 산 그냥 검\""));

        ItemData.Add("Mokoko", new ItemDB("Mokoko",
            Resources.Load("Ui/Icon/Item/Mokoko", typeof(Sprite)) as Sprite,
            "<color=orange><size=23><b>모코코</b></size></color>\n" +
            "아이템 설명:\n" +
            "적을 타격 시 이동 속도 3% 증가 (최대 5 스택)\n" +
            "\"기분 좋은 향기가 솔솔\""));

        for (int i = 0; i < PartyManager.Instance.PartyList.Count; i++)
        {
            originSpeed[i] = PartyManager.Instance.PartyList[i].GetComponent<Player>().MaxSpeed;
        }

        if(InventoryManager.Instance.ItemNumber.Contains("Selfdefense_charm"))
             Selfdefense_charm();

        if (InventoryManager.Instance.ItemNumber.Contains("Longsword"))
                Longsword();
    }

    // Update is called once per frame
    void Update()
    {
        if(Hit_Stack_Reset_CoolTime_Cur != 0)
        {
            Hit_Stack_Reset_CoolTime_Cur -= Time.deltaTime;
        }

        if(Hit_Stack_Reset_CoolTime_Cur < 0)
        {
            Hit_Stack_Reset_CoolTime_Cur = 0;
        }
        if (InventoryManager.Instance.ItemNumber.Contains("Mokoko"))
            Mokoko();
    }

    public ItemDB(string _name, Sprite _icon, string _info)
    {
        name = _name;
        icon = _icon;
        info = _info;
    }

    public void Selfdefense_charm()
    {
        for(int i = 0; i<PartyManager.Instance.PartyList.Count; i++)
        {
            PartyManager.Instance.PartyList[i].GetComponent<Player>().Avd += 10;
        }
    }

    public void Longsword()
    {
        for (int i = 0; i < PartyManager.Instance.PartyList.Count; i++)
        {
            PartyManager.Instance.PartyList[i].GetComponent<Player>().Atk += 30;
        }
    }
    
    public void Mokoko()
    { 
     
    

        for (int i = 0; i < PartyManager.Instance.PartyList.Count; i++)
        {
            float n;
            float increaseSpeed = originSpeed[i] + (originSpeed[i] * 0.03f * (Hit_Stack > 5 ? 5 : Hit_Stack));
            PartyManager.Instance.PartyList[i].GetComponent<Player>().MaxSpeed = increaseSpeed;
        }

        if(Hit_Stack != temp)
        {
            Hit_Stack_Reset_CoolTime_Cur = Hit_Stack_Reset_CoolTime;
        }
       
        if(Hit_Stack_Reset_CoolTime_Cur == 0)
        {
            Hit_Stack = 0;
            Hit_Stack_Reset_CoolTime_Cur = Hit_Stack_Reset_CoolTime;
        }

        temp = Hit_Stack;
    }
    

}
