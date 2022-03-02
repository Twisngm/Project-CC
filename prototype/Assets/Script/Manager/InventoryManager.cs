using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryWindow;
     public Image[] ClassIcons = new Image[3];
    public int CurrentIndex;
    public Image AttackIcon;
    public Image SkillIcon;
    public Text AttackInfo;
    public Text SkillInfo;
    public Button[] ClassButton;
    public Button[] AttackInfoButton;
    public List<string> ItemNumber;
    public int ItemIndex = 0;
    public GameObject[] ItemSlots;
    public Text ItemInfo;
    public GameObject[] SkillTreeWindows;
    public Image[] SkillTrees1;
    public Image[] SkillTrees2;
    public Image[] SkillTrees3;

    static InventoryManager instance = null;

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

        InventoryWindow = GameObject.Find("UI").transform.Find("InventoryWindow").gameObject;
    }

    public static InventoryManager Instance
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
    void Start()
    {
        for (int n = 0; n < SkillTrees1.Length; n++)
            for (int i = 0; i < SkillTreeDB.Instance.SkillTreeDBs.Count; i++)
            {
                if (SkillTreeDB.Instance.SkillTreeDBs[i].ClassName == PartyManager.Instance.PartyList[CurrentIndex].name)
                {
                    if (SkillTreeDB.Instance.SkillTreeDBs[i].Icon != null)
                    {
                        SkillTrees1[n].GetComponent<RectTransform>().anchoredPosition = SkillTreeDB.Instance.SkillTreeDBs[i].TreeShape;
                        SkillTrees1[n].GetComponent<SkillTree>().SkillText = SkillTreeDB.Instance.SkillTreeDBs[i].Text;
                        SkillTrees1[n].sprite = SkillTreeDB.Instance.SkillTreeDBs[i].Icon;
                        SkillTreeDB.Instance.SkillTreeDBs.RemoveAt(i);
                        break;
                    }

                }
            }

        for(int i = 0; i <ItemNumber.Count; i++)
        {
            if(ItemNumber[i] != null)
            {
                ItemSlots[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = ItemDB.Instance.ItemData[ItemNumber[i]].icon;
            }
        }


        
    }

    // Update is called once per frame
    void Update()
    {
        if (ClassDB.Instance.ClassData[PartyManager.Instance.PartyList[CurrentIndex].name].classIcon != null)
        {
            ClassIcons[0].transform.GetChild(0).GetComponentInChildren<Image>().sprite = ClassDB.Instance.ClassData[PartyManager.Instance.PartyList[0].name].classIcon;
            ClassIcons[1].transform.GetChild(0).GetComponentInChildren<Image>().sprite = ClassDB.Instance.ClassData[PartyManager.Instance.PartyList[1].name].classIcon;
            AttackIcon.sprite = ClassDB.Instance.ClassData[PartyManager.Instance.PartyList[CurrentIndex].name].attackIcon;
            AttackInfo.text = ClassDB.Instance.ClassData[PartyManager.Instance.PartyList[CurrentIndex].name].attackInfo;
            SkillIcon.sprite = ClassDB.Instance.ClassData[PartyManager.Instance.PartyList[CurrentIndex].name].skillIcon;
            SkillInfo.text = ClassDB.Instance.ClassData[PartyManager.Instance.PartyList[CurrentIndex].name].skillInfo;
        
        }

        ItemInfo.text = ItemDB.Instance.ItemData[ItemNumber[ItemIndex]].info;
       
        if(Input.GetKeyUp(KeyCode.Tab))
        {
            if (InventoryWindow.activeSelf)
            {
                InventoryWindow.SetActive(false);
            }
            else
                InventoryWindow.SetActive(true);
        }
    }

    public void SetClass_M()
    {
        CurrentIndex = 0;
    
        for (int n = 0; n < SkillTrees1.Length; n++)
            for (int i = 0; i < SkillTreeDB.Instance.SkillTreeDBs.Count; i++)
            {
                if (SkillTreeDB.Instance.SkillTreeDBs[i].ClassName == PartyManager.Instance.PartyList[CurrentIndex].name)
                {
                    if (SkillTreeDB.Instance.SkillTreeDBs[i].Icon != null)
                    {
                        SkillTrees1[n].GetComponent<RectTransform>().anchoredPosition = SkillTreeDB.Instance.SkillTreeDBs[i].TreeShape;
                        SkillTrees1[n].GetComponent<SkillTree>().SkillText = SkillTreeDB.Instance.SkillTreeDBs[i].Text;
                        SkillTrees1[n].sprite = SkillTreeDB.Instance.SkillTreeDBs[i].Icon;
                        SkillTreeDB.Instance.SkillTreeDBs.RemoveAt(i);
                        break;
                    }

                }
            }
        for (int i = 0; i < SkillTreeWindows.Length; i++)
        {
            if (CurrentIndex == i)
            {
                SkillTreeWindows[i].SetActive(true);
            }
            else
                SkillTreeWindows[i].SetActive(false);

        }
    }

    public void SetClass_L()
    {
        CurrentIndex = 1;
      
        for (int n = 0; n < SkillTrees2.Length; n++)
            for (int i = 0; i < SkillTreeDB.Instance.SkillTreeDBs.Count; i++)
            {
                if (SkillTreeDB.Instance.SkillTreeDBs[i].ClassName == PartyManager.Instance.PartyList[CurrentIndex].name)
                {
                    if (SkillTreeDB.Instance.SkillTreeDBs[i].Icon != null)
                    {
                        SkillTrees2[n].GetComponent<RectTransform>().anchoredPosition = SkillTreeDB.Instance.SkillTreeDBs[i].TreeShape;
                        SkillTrees2[n].GetComponent<SkillTree>().SkillText = SkillTreeDB.Instance.SkillTreeDBs[i].Text;
                        SkillTrees2[n].sprite = SkillTreeDB.Instance.SkillTreeDBs[i].Icon;
                        SkillTreeDB.Instance.SkillTreeDBs.RemoveAt(i);
                        break;
                    }

                }
            }

        for (int i = 0; i < SkillTreeWindows.Length; i++)
        {
            if (CurrentIndex == i)
            {
                SkillTreeWindows[i].SetActive(true);
            }
            else
                SkillTreeWindows[i].SetActive(false);

        }

    }

    public void SetClass_R()
    {
        CurrentIndex = 2;
   
        for (int n = 0; n < SkillTrees3.Length; n++)
            for (int i = 0; i < SkillTreeDB.Instance.SkillTreeDBs.Count; i++)
            {
                if (SkillTreeDB.Instance.SkillTreeDBs[i].ClassName == PartyManager.Instance.PartyList[CurrentIndex].name)
                {
                    if (SkillTreeDB.Instance.SkillTreeDBs[i].Icon != null)
                    {
                        SkillTrees3[n].GetComponent<RectTransform>().anchoredPosition = SkillTreeDB.Instance.SkillTreeDBs[i].TreeShape;
                        SkillTrees3[n].GetComponent<SkillTree>().SkillText = SkillTreeDB.Instance.SkillTreeDBs[i].Text;
                        SkillTrees3[n].sprite = SkillTreeDB.Instance.SkillTreeDBs[i].Icon;
                        SkillTreeDB.Instance.SkillTreeDBs.RemoveAt(i);
                        break;
                    }

                }
            }

        for (int i = 0; i < SkillTreeWindows.Length; i++)
        {
            if (CurrentIndex == i)
            {
                SkillTreeWindows[i].SetActive(true);
            }
            else
                SkillTreeWindows[i].SetActive(false);

        }

    }

    public void ItemChoose()
    {
        for(int i = 0; i<ItemNumber.Count; i++)
        {
            if(EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite.name == ItemNumber[i])    
                ItemIndex = i;
        }
    }
}
