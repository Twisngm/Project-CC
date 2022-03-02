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
        // ��ų �ؽ�Ʈ ���


        // ���ּ���
        string[] Fire_Sorcerer_Skill_Text = {"<size=40>���¿�� ��ȭ</size>\n\n��� ��ų�� ���ط��� 15%, ȭ���� ���ط��� 10% �����Ѵ�.",
            "<size=40>ȭ������</size>\n\n<color=orange><size=23><b>ȭ����</b></size></color>�� ���� �����մϴ�.",
            "<color=orange><size=23><b>���¿��</b></size></color>�� ���� 2�ʰ� ������Ų��.",
            "<size=40>���߰�ȭ</size>\n\n<color=orange><size=23><b>ȭ����</b></size></color>�� ���� ���ݿ� ������ �� �ֺ��� ���ݷ� 30%�� ���ط����� �����Ѵ�.",
            "<size=40>�ӻ簭ȭ</size>\n\n<color=orange><size=23><b>ȭ����</b></size></color>�� �ӵ��� �����Ѵ�.\n<color=orange><size=23><b>���¿��</b></size></color>�� Ÿ��ȹ���� 3�� �����Ѵ�.",
            "<size=40>����Ʈ</size>\n\n<color=orange><size=23><b>ȭ����</b></size></color>�� Ű�ٿ����� ����ȴ�.\n<color=orange><size=23><b>ȭ����</b></size></color>�� <color=orange><size=23><b>����Ʈ</b></size></color>�� ����ȴ�.",
            "<size=40>ȭ�����</size>\n\n<color=orange><size=23><b>���¿��</b></size></color>�� 5�ʰ� ��ġ�Ǵ� ��ġ������ ����ȴ�.\n���¿�Ⱑ ���� 100% Ȯ���� ���� ȭ�� ���·� �����.\n<color=orange><size=23><b>���¿��</b></size></color>��<color=orange><size=23><b>ȭ�����</b></size></color>�� ����ȴ�.",
            "<size=40>�ͷ��� �Ҳ�</size>\n\nȭ���� ���ط��� 15% �����Ѵ�.",
            "<size=40>��� �¿��</size>\n\n<color=orange><size=23><b>����Ʈ</b></size></color>�� 2���� ������.\n<color=orange><size=23><b>ȭ�����</b></size></color>�� �������� 2���� ��ġ�ȴ�.",
            "<size=40>������ �¿��</size>\n\n<color=orange><size=23><b>����Ʈ</b></size></color>�� �߻� �� ����ü �ӵ��� �����Ѵ�.\n<color=orange><size=23><b>ȭ�����</b></size></color>�� ���ӽð��� 3�� �����Ѵ�."};


        // ����
        string[] Pirate_Skill_Text = { "<size=40>���ӽð� ��ȭ</size>\n\n<color=orange><size=23><b>��Ʋ�� ����Ʈ</b></size></color>�� ���ӽð��� 2�� �����Ѵ�.",
            "<size=40>���� ��ȭ</size>\n\n������ <color=orange><size=23><b>�޴� ���ط�</b></size></color> ���� ȿ���� 8% ����Ѵ�.",
            "<size=40>���� ȣ��</size>\n\n<color=orange><size=23><b>��Ʋ�� ����Ʈ</b></size></color>�� ��Ÿ���� 2�� �����Ѵ�.",
            "<size=40>���� ���</size>\n\n<color=orange><size=23><b>��Ʋ�� ����Ʈ</b></size></color>�� ������ �ִ� ���� ���ݷ��� 130%�� 2�ʿ� �ѹ� �����Ѵ�.",
            "<size=40>���� Ȯ��</size>\n\n<color=orange><size=23><b>��Ʋ�� ����Ʈ</b></size></color>�� ������ �����Ѵ�.",
            "<size=40>���� �Դ�</size>\n\n<color=orange><size=23><b>��Ʋ�� ����Ʈ</b></size></color>�� ü���� �ִ�ü���� 50% �̸��� �Ʊ���\n3�ʿ� �ѹ� ���ݷ��� 15%�� ȸ�� �Ѵ�.\n<color=orange><size=23><b>��Ʋ�� ����Ʈ</b></size></color>�� ��Ÿ���� 8�� �����Ѵ�.\n<color=orange><size=23><b>��Ʋ�� ����Ʈ</b></size></color>�� <color=orange><size=23><b>���� �Դ�</b></size></color>�� ����ȴ�.",
            "<size=40>����</size>\n\n<color=orange><size=23><b>��Ʋ�� ����Ʈ</b></size></color>�� �Ʊ��� �Բ� �̵��Ѵ�.\n<color=orange><size=23><b>��Ʋ�� ����Ʈ</b></size></color>�� <color=orange><size=23><b>����</b></size></color>���� ����ȴ�.",
            "<size=40>���� ��ȭ</size>\n\n<color=orange><size=23><b>���� �Դ�</b></size></color> Ȥ�� <color=orange><size=23><b>����</b></size></color>�� �ߵ��� ���¿��� �̵��ӵ��� 15% �����Ѵ�.",
            "<size=40>��ȣ �Դ�</size>\n\n<color=orange><size=23><b>���� �Դ�</b></size></color>�� �ߵ��ϸ� �ִ�ü���� 30% ��ŭ�� ��ȣ���� 5�ʰ� ���ӵ˴ϴ�.",
            "<size=40>��Ʈ ��ȭ</size>\n\n<color=orange><size=23><b>���� �Դ�</b></size></color>�� ȸ���ӵ��� 1�� �����մϴ�.\n<color=orange><size=23><b>����</b></size></color>�� ���ݼӵ��� 0.5�� �����մϴ�."};


        // ��ųƮ�� ������ ���


        // ���ּ���
        Vector2[] Fire_Sorcerer_TreeShape = { new Vector2(-370, 0), new Vector2(-220, 150), 
                                              new Vector2(-220, -150), new Vector2(-70, 300), new Vector2(-70, 0), new Vector2(80, 150), 
                                              new Vector2(80, -150), new Vector2(230, 0), new Vector2(370, 150), new Vector2(370, -150) };



        // ����
        Vector2[] Pirate_TreeShape = { new Vector2(-370, 0), new Vector2(-220, 150),
                                       new Vector2(-220, -150), new Vector2(-70, 300), new Vector2(-70, -300), new Vector2(80, 150),
                                       new Vector2(80, -150), new Vector2(230, 300), new Vector2(370, 150), new Vector2(230, -0) };


        for (int i = 0; i < 10; i++)
        {
          // Ŭ���� ��ų ������ �߰�
            SkillTreeDBs.Add(new SkillTreeDB("Fire_Sorcerer",false,i,Resources.Load("Ui/Icon/SkillTree/Fire_Sorcerer/Fire_Sorcerer" + i.ToString(), typeof(Sprite)) as Sprite,Fire_Sorcerer_Skill_Text[i],Fire_Sorcerer_TreeShape[i]));
            SkillTreeDBs.Add(new SkillTreeDB("Pirate",false, i, Resources.Load("Ui/Icon/SkillTree/Pirate/Pirate" + i.ToString(), typeof(Sprite)) as Sprite,Pirate_Skill_Text[i], Pirate_TreeShape[i]));
        }
        

      

       

    }

}
