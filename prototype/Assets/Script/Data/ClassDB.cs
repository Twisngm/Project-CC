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

        // ���ּ��� ������ �߰�
        ClassData.Add("Fire_Sorcerer", new ClassDB("Fire_Sorcerer",
        Resources.Load("Ui/Icon/Class/Fire_Sorcerer_Icon", typeof(Sprite)) as Sprite,
        Resources.Load("Ui/Icon/Attack/Fire_Sorcerer_Attack_Icon", typeof(Sprite)) as Sprite,
        "<color=orange><size=23><b>ȭ����</b></size></color>\n" +
        "������ ���� ���������� ���ư��� �ҵ��̸� ������.\n" +
        "���� Ȯ���� ������ ȭ�����·� �����.\n" +
        "<color=orange><size=23><b>���ط�</b></size></color> : ���ݷ��� 80%\n" +
        "<color=orange><size=23><b>Ư�̻���</b></size></color> : \n" +
        "ȭ�� Ȯ�� : 20%\n" +
        "ȭ�� ���ط� : ���ݷ��� 20% 5�ʰ� ����",
         Resources.Load("Ui/Icon/Skill/Fire_Sorcerer_Skill_Icon", typeof(Sprite)) as Sprite,
         "<color=orange><size=23><b>���¿��</b></size></color>\n" +
         "��Ÿ�� : 8��\n" +
         "������ ���鿡�� �� 8�� �����Ѵ�.\n" +
         "������ ��Ÿ�� ���̸� ���ط��� �����Ѵ�.\n" +
         "<color=orange><size=23><b> ���ط� </b></size></color> : Ÿ�ݴ� ���ݷ��� 40%\n" +
         "<color=orange><size=23><b> Ư�̻��� </b></size></color>:\n" +
         "��Ÿ�� ���� ���ݽ� ���ط� : Ÿ�ݴ� ���ݷ��� 60% "
          )) ;
  
        
     

        // ���� ������ �߰�
        ClassData.Add("Pirate", new ClassDB("Pirate",
            Resources.Load("Ui/Icon/Class/Pirate_Icon", typeof(Sprite)) as Sprite,
            Resources.Load("Ui/Icon/Attack/Pirate_Attack_Icon", typeof(Sprite)) as Sprite,
            "<color=orange><size=23><b>��Ʋ�� ����Ʈ</b></size></color>\n" +
            "��Ÿ�� : 12��\n" +
            "�ڽ��� ��ġ�� ���븦 ��ġ�Ѵ�.\n" +
            "����� �ֺ� ���鿡�� ������� �Ŵ� ������ �����.\n" +
            "<color=orange><size=23><b>Ư�̻���</b></size></color> : \n" +
            "���� ȿ�� :<color=orange><size=23><b> �޴� ���ط� </b></size></color>15% ����\n" +
            "���� ���ӽð� : 6��",
             Resources.Load("Ui/Icon/Skill/Pirate_Skill_Icon", typeof(Sprite)) as Sprite,
             "<color=orange><size=23><b>�ػ�˼�</b></size></color>\n" +
             "������ ������ �ִ� 3ȸ�� ���Ӱ����� ���Ѵ�.\n" +
             "<color=orange><size=23><b>���ط�</b></size></color> : 1Ÿ ~3Ÿ ���� ���ݷ���(70 % / 75 % / 80 %)"
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
