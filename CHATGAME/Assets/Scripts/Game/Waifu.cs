using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waifu : MonoBehaviour
{
    private static Waifu _Instance;

    public static Waifu Instance
    {
        get 
        {
            if( _Instance == null )
            {
                GameObject singletonObject = new GameObject();
                _Instance = singletonObject.AddComponent<Waifu>();
                singletonObject.name = typeof( Waifu ).ToString() + " (Singleton)";
                SingletonManager.Instance.RegisterSingleton(_Instance);
            }
            return _Instance;
        }
    }

    public int affection_exp;//ȣ���� ����ġ
    public int affection_lv;//ȣ���� ����
    public int affection_barrel;//ȣ���� ������ �ʿ� ����ġ
    private int aff_idx;//�������� ȣ���� ��ġ
    public string affection_status;//ȣ���� ����( intruder, suspicion, member, intimate, more, boyfriend )
    public string affection_restore;//�������� �޾ƿ� ȣ������ ����
    DataManager dataManager;
    SheetData affSheet;

    void Awake()
    {
        if( _Instance == null )
        {
            _Instance = this as Waifu;
            SingletonManager.Instance.RegisterSingleton(_Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        dataManager = SingletonManager.Instance.GetSingleton<DataManager>();
        affSheet = dataManager.GetSheetData("Dialogue");

        affection_barrel = 5;

        Affection_compare();
    }

    public void Affection_ascend()
    {
        affection_exp++;
        Affection_level_calculate();
        Affection_compare();
    }

    public void Affection_descend()
    {
        if( affection_exp > 0 )
        {
            affection_exp--; 
        }
        Affection_level_calculate();
        Affection_compare();
    }

    public void Affection_level_calculate()
    {
        if(affection_exp >= affection_barrel)
        {
            affection_lv++;
            affection_barrel = affection_barrel * 2 - (affection_lv % 5);//�ӽ�
            //affection_exp = 0;
        }
    }

    public string Affection_transport()//UI�� ȣ���� ��ġ�� ������
    {
        return affection_lv.ToString();
    }

    public string Affection_compare()
    {
        var data = affSheet.GetData(aff_idx);

        if(data == null )
        {
            return "empty";//�ӽ�
        }

        if(data.TryGetValue("affection",out var aff))
        {
            affection_restore = aff.ToString();
        }

        if (affection_lv < int.Parse(affection_restore) )//excel ���Ͽ��� ȣ���� ��θ� �ҷ��� ����
        {
            //intruder
            affection_status = "intruder";
        }
        else if (affection_lv == int.Parse(affection_restore))
        {
            //member
            affection_status = "suspicion";
        }
        else if (affection_lv > int.Parse(affection_restore))
        {
            //suspicious
            affection_status = "member";
        }

        return affection_status;
    }
}
