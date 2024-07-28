using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waifu : MonoBehaviour
{
    public static int affection;//singletone ���� ������ ȣ���� ��ġ
    public static string affection_status;//singletone ���� ������ ȣ���� ����

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
