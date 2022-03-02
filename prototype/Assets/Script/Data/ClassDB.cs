using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassDB : MonoBehaviour
{
    public new string name;
    public Sprite classIcon;
    public Sprite attackIcon;
    public string attackInfo;
    public Sprite skillIcon;
    public string skillInfo;

   

    public Dictionary<string,ClassDB> ClassData;

    static ClassDB instance = null;

    public static ClassDB Instance
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

    public ClassDB(string _name, Sprite _classIcon, Sprite _attackIcon, string _attackInfo, Sprite _skillIcon, string _skillInfo)
    {
        this.name = _name;
        this.classIcon = _classIcon;
        this.attackIcon = _attackIcon;
        this.attackInfo = _attackInfo;
        this.skillIcon = _skillIcon;
        this.skillInfo = _skillInfo;
       

    }
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
    }

    private void Start()
    {
        ClassData = new Dictionary<string, ClassDB>();

        // 불주술사 데이터 추가
        ClassData.Add("Fire_Sorcerer", new ClassDB("Fire_Sorcerer",
        Resources.Load("Ui/Icon/Class/Fire_Sorcerer_Icon", typeof(Sprite)) as Sprite,
        Resources.Load("Ui/Icon/Attack/Fire_Sorcerer_Attack_Icon", typeof(Sprite)) as Sprite,
        "<color=orange><size=23><b>화염구</b></size></color>\n" +
        "전방을 향해 일직선으로 날아가는 불덩이를 던진다.\n" +
        "일정 확률로 상대방을 화염상태로 만든다.\n" +
        "<color=orange><size=23><b>피해량</b></size></color> : 공격력의 80%\n" +
        "<color=orange><size=23><b>특이사항</b></size></color> : \n" +
        "화염 확률 : 20%\n" +
        "화염 피해량 : 공격력의 20% 5초간 지속",
         Resources.Load("Ui/Icon/Skill/Fire_Sorcerer_Skill_Icon", typeof(Sprite)) as Sprite,
         "<color=orange><size=23><b>불태우기</b></size></color>\n" +
         "쿨타임 : 8초\n" +
         "전방의 적들에게 총 8번 공격한다.\n" +
         "상대방이 불타는 중이면 피해량이 증가한다.\n" +
         "<color=orange><size=23><b> 피해량 </b></size></color> : 타격당 공격력의 40%\n" +
         "<color=orange><size=23><b> 특이사항 </b></size></color>:\n" +
         "불타는 적을 공격시 피해량 : 타격당 공격력의 60% "
          )) ;
  
        
     

        // 해적 데이터 추가
        ClassData.Add("Pirate", new ClassDB("Pirate",
            Resources.Load("Ui/Icon/Class/Pirate_Icon", typeof(Sprite)) as Sprite,
            Resources.Load("Ui/Icon/Attack/Pirate_Attack_Icon", typeof(Sprite)) as Sprite,
            "<color=orange><size=23><b>배틀쉽 마스트</b></size></color>\n" +
            "쿨타임 : 12초\n" +
            "자신의 위치에 돛대를 설치한다.\n" +
            "돛대는 주변 적들에게 디버프를 거는 낙인을 새긴다.\n" +
            "<color=orange><size=23><b>특이사항</b></size></color> : \n" +
            "낙인 효과 :<color=orange><size=23><b> 받는 피해량 </b></size></color>15% 증가\n" +
            "돛대 지속시간 : 6초",
             Resources.Load("Ui/Icon/Skill/Pirate_Skill_Icon", typeof(Sprite)) as Sprite,
             "<color=orange><size=23><b>해상검술</b></size></color>\n" +
             "전방의 적에게 최대 3회의 연속공격을 가한다.\n" +
             "<color=orange><size=23><b>피해량</b></size></color> : 1타 ~3타 각각 공격력의(70 % / 75 % / 80 %)"
        ));
    

    }

    public void Show()
    {
        Debug.Log(name);
        Debug.Log(classIcon);
        Debug.Log(attackIcon);
        Debug.Log(attackInfo);
        Debug.Log(skillIcon);
        Debug.Log(skillInfo);
    }

}
