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


        // ������ ������ �߰�
        
        ItemData.Add("Self-defense_charm", new ItemDB("Self-defense_charm", 
            Resources.Load("Ui/Icon/Item/Self-defense_charm",typeof(Sprite)) as Sprite, 
            "<color=orange><size=23><b>ȣ�ź���</b></size></color>" +
            "\n������ ����:" +
            "\nȸ���� 10% ����" +
            "\n\"���� �׾ ����ġ�� ���� ���� �� ���� ����\""));

        ItemData.Add("Longsword", new ItemDB("Longsword",
            Resources.Load("Ui/Icon/Item/Longsword", typeof(Sprite)) as Sprite,
            "<color=orange><size=23><b>�ռҵ�</b></size></color>\n" +
            "������ ����:\n" +
            "���ݷ� 30 ����\n" +
            "\"�������� 350������ �� �׳� ��\""));

        ItemData.Add("Mokoko", new ItemDB("Mokoko",
            Resources.Load("Ui/Icon/Item/Mokoko", typeof(Sprite)) as Sprite,
            "<color=orange><size=23><b>������</b></size></color>\n" +
            "������ ����:\n" +
            "���� Ÿ�� �� �̵� �ӵ� 3% ���� (�ִ� 5 ����)\n" +
            "\"��� ���� ��Ⱑ �ּ�\""));

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
