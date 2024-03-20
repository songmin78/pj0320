using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaponcheck : MonoBehaviour
{
    [Header("��Ÿ ����")]
    [SerializeField] float speed = 1.0f;

    [Header("�������")]
    [SerializeField] bool Counter = false;//ī���� ����
    [SerializeField] float AttackdamageMax = 1f;//���� ������
    [SerializeField] bool arrow = false;//ȭ���ΰ��� Ȯ��
    [SerializeField] bool punch = false;//����������� Ȯ��
    [SerializeField] bool magic = false;//�����ΰ��� Ȯ��
     

    void Start()
    {
    }

    void Update()
    {
        shotarrow();
    }

    public void Attackdamage(int _weaponType)
    {
        if(_weaponType == 0)//���Ÿ� ����
        {
            AttackdamageMax = 5;
            Debug.Log("���Ÿ�");
        }
        else if(_weaponType == 1)//���� ����
        {
            Debug.Log("�ٰŸ�");
        }
        else if(_weaponType == 2)//��������
        {
            Debug.Log("����");
        }
    }

    public void Counterdamage(int _weaponType)
    {
        if (_weaponType == 0)//���Ÿ� ����
        {
            Debug.Log("���Ÿ� ī����");
        }
        else if (_weaponType == 1)//���� ����
        {
            Debug.Log("�ٰŸ� ī����");
        }
        else if (_weaponType == 2)//��������
        {
            Debug.Log("���� ī����");
        }
    }

    private void shotarrow()
    {
        if(arrow == true)
        {
            transform.position += transform.up * Time.deltaTime * speed;
        }
    }

}
