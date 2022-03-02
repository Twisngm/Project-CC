using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeDB : MonoBehaviour
{

    public int Skill_Point = 0;
   public string ClassName; 
   public bool[] skillSet = new bool[10];
   public int Index;
   public Sprite Icon;
    public string Text;
    public Vector2 TreeShape;

    public List<SkillTreeDB> SkillTreeDBs = new List<SkillTreeDB>();
    public List<string[]> SkillTexts = new List<string[]>();
    public Dictionary<string, List<SkillTreeDB>> skillTree = new Dictionary<string, List<SkillTreeDB>>();

    static SkillTreeDB instance = null;

    public static SkillTreeDB Instance
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

    public SkillTreeDB(string _name,bool _skillSet, int _Index, Sprite _Icon, string _Text, Vector2 _TreeShape) {
        this.skillSet[_Index] = _skillSet;
        this.Icon = _Icon;
        this.ClassName = _name;
        this.Text = _Text;
        this.TreeShape = _TreeShape;
    }

    public void show()
    {
        Debug.Log(skillSet);
        Debug.Log(Index);
        Debug.Log(Icon);
    }

    private void Start()
    {
        // 스킬 텍스트 목록


        // 불주술사
        string[] Fire_Sorcerer_Skill_Text = {"<size=40>불태우기 강화</size>\n\n모든 스킬의 피해량이 15%, 화염의 피해량이 10% 증가한다.",
            "<size=40>화염유도</size>\n\n<color=orange><size=23><b>화염구</b></size></color>가 적을 추적합니다.",
            "<color=orange><size=23><b>불태우기</b></size></color>가 적을 2초간 기절시킨다.",
            "<size=40>폭발강화</size>\n\n<color=orange><size=23><b>화염구</b></size></color>가 적을 공격에 성공할 시 주변에 공격력 30%의 피해량으로 폭발한다.",
            "<size=40>속사강화</size>\n\n<color=orange><size=23><b>화염구</b></size></color>의 속도가 증가한다.\n<color=orange><size=23><b>불태우기</b></size></color>의 타격획수가 3번 증가한다.",
            "<size=40>버스트</size>\n\n<color=orange><size=23><b>화염구</b></size></color>가 키다운으로 변경된다.\n<color=orange><size=23><b>화염구</b></size></color>가 <color=orange><size=23><b>버스트</b></size></color>로 변경된다.",
            "<size=40>화염방사</size>\n\n<color=orange><size=23><b>불태우기</b></size></color>가 5초간 설치되는 설치형으로 변경된다.\n불태우기가 이제 100% 확률로 적을 화염 상태로 만든다.\n<color=orange><size=23><b>불태우기</b></size></color>가<color=orange><size=23><b>화염방사</b></size></color>로 변경된다.",
            "<size=40>맹렬한 불꽃</size>\n\n화염의 피해량이 15% 증가한다.",
            "<size=40>모두 태우기</size>\n\n<color=orange><size=23><b>버스트</b></size></color>가 2개씩 나간다.\n<color=orange><size=23><b>화염방사</b></size></color>가 양쪽으로 2개가 설치된다.",
            "<size=40>빠르게 태우기</size>\n\n<color=orange><size=23><b>버스트</b></size></color>의 발사 및 투사체 속도가 증가한다.\n<color=orange><size=23><b>화염방사</b></size></color>의 지속시간이 3초 증가한다."};


        // 해적
        string[] Pirate_Skill_Text = { "<size=40>지속시간 강화</size>\n\n<color=orange><size=23><b>배틀쉽 마스트</b></size></color>의 지속시간이 2초 증가한다.",
            "<size=40>낙인 강화</size>\n\n낙인의 <color=orange><size=23><b>받는 피해량</b></size></color> 증가 효과가 8% 상승한다.",
            "<size=40>빠른 호출</size>\n\n<color=orange><size=23><b>배틀쉽 마스트</b></size></color>의 쿨타임이 2초 감소한다.",
            "<size=40>지원 사격</size>\n\n<color=orange><size=23><b>배틀쉽 마스트</b></size></color>가 낙인이 있는 적을 공격력의 130%로 2초에 한번 공격한다.",
            "<size=40>영역 확장</size>\n\n<color=orange><size=23><b>배틀쉽 마스트</b></size></color>의 범위가 증가한다.",
            "<size=40>지원 함대</size>\n\n<color=orange><size=23><b>배틀쉽 마스트</b></size></color>가 체력이 최대체력의 50% 미만인 아군의\n3초에 한번 공격력의 15%로 회복 한다.\n<color=orange><size=23><b>배틀쉽 마스트</b></size></color>의 쿨타임이 8초 증가한다.\n<color=orange><size=23><b>배틀쉽 마스트</b></size></color>가 <color=orange><size=23><b>지원 함대</b></size></color>로 변경된다.",
            "<size=40>출항</size>\n\n<color=orange><size=23><b>배틀쉽 마스트</b></size></color>가 아군과 함께 이동한다.\n<color=orange><size=23><b>배틀쉽 마스트</b></size></color>가 <color=orange><size=23><b>출항</b></size></color>으로 변경된다.",
            "<size=40>선속 강화</size>\n\n<color=orange><size=23><b>지원 함대</b></size></color> 혹은 <color=orange><size=23><b>출항</b></size></color>이 발동된 상태에서 이동속도가 15% 증가한다.",
            "<size=40>보호 함대</size>\n\n<color=orange><size=23><b>지원 함대</b></size></color>를 발동하면 최대체력의 30% 만큼의 보호막이 5초간 지속됩니다.",
            "<size=40>노트 강화</size>\n\n<color=orange><size=23><b>지원 함대</b></size></color>의 회복속도가 1초 감소합니다.\n<color=orange><size=23><b>출항</b></size></color>의 공격속도가 0.5초 감소합니다."};


        // 스킬트리 포지션 목록


        // 불주술사
        Vector2[] Fire_Sorcerer_TreeShape = { new Vector2(-370, 0), new Vector2(-220, 150), 
                                              new Vector2(-220, -150), new Vector2(-70, 300), new Vector2(-70, 0), new Vector2(80, 150), 
                                              new Vector2(80, -150), new Vector2(230, 0), new Vector2(370, 150), new Vector2(370, -150) };



        // 해적
        Vector2[] Pirate_TreeShape = { new Vector2(-370, 0), new Vector2(-220, 150),
                                       new Vector2(-220, -150), new Vector2(-70, 300), new Vector2(-70, -300), new Vector2(80, 150),
                                       new Vector2(80, -150), new Vector2(230, 300), new Vector2(370, 150), new Vector2(230, -0) };


        for (int i = 0; i < 10; i++)
        {
          // 클래스 스킬 데이터 추가
            SkillTreeDBs.Add(new SkillTreeDB("Fire_Sorcerer",false,i,Resources.Load("Ui/Icon/SkillTree/Fire_Sorcerer/Fire_Sorcerer" + i.ToString(), typeof(Sprite)) as Sprite,Fire_Sorcerer_Skill_Text[i],Fire_Sorcerer_TreeShape[i]));
            SkillTreeDBs.Add(new SkillTreeDB("Pirate",false, i, Resources.Load("Ui/Icon/SkillTree/Pirate/Pirate" + i.ToString(), typeof(Sprite)) as Sprite,Pirate_Skill_Text[i], Pirate_TreeShape[i]));
        }
        

      

       

    }

}
