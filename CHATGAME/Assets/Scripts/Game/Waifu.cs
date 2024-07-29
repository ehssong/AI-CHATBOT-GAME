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

    public static int affection;//singletone ���� ������ ȣ���� ��ġ
    public static string affection_status;//singletone ���� ������ ȣ���� ����

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

    public void affection_ascend()
    {
        affection++;
    }
    public void affection_descend()
    {
        affection--;
    }

    public string affection_transport()//UI�� ȣ���� ��ġ�� ������
    {
        return affection.ToString();
    }

    public void affection_compare()
    {
        if (affection < 0)//excel ���Ͽ��� ȣ���� ��θ� �ҷ��� ����
        {
            //intruder
            affection_status = "intruder";
        }
        else if (affection == 0)
        {
            //member
            affection_status = "member";
        }
        else if (affection > 0)
        {
            //suspicious
            affection_status = "suspicious";
        }
    }
}
