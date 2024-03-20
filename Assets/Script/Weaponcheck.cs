using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaponcheck : MonoBehaviour
{
    [Header("��Ÿ ����")]
    [SerializeField] float speed = 1.0f;//ȭ�� �ӵ�
    [SerializeField] float Xposition;//���� x��ǥ
    [SerializeField] float Yposition;//���� y��ǥ
    [SerializeField] float puchtime = 1.0f;// �������� ������� �ð�

    [Header("���� ����")]
    [SerializeField] GameObject Typeweapon;

    [Header("�������")]
    [SerializeField] bool Counter = false;//ī���� ����
    [SerializeField] float AttackdamageMax = 1f;//���� ������
    [SerializeField] bool arrow = false;//ȭ���ΰ��� Ȯ��
    [SerializeField] bool punch = false;//����������� Ȯ��
    [SerializeField] bool magic = false;//�����ΰ��� Ȯ��
     

    void Start()
    {
        startattack();
    }

    void Update()
    {
        shotarrow();
        weapondestory();
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

    public void startattack()
    {
        Xposition = transform.position.x;
        Yposition = transform.position.y;

        new Vector3(Xposition, Yposition, 0);
    }

    private void weapondestory()
    {
        if (arrow == true)
        {
            float Xchange = transform.position.x;
            float Ychange = transform.position.y;

            transform.position = new Vector3(Xchange, Ychange, 0);

            if (Xchange >= Xposition + 5 || Xchange <= Xposition - 5)
            {
                Destroy(Typeweapon);
            }
            else if (Ychange >= Yposition + 5 || Ychange <= Yposition - 5)
            {
                Destroy(Typeweapon);
            }
        }
        else if(punch == true)
        {
            if(puchtime >= 0)
            {
                puchtime  = puchtime -Time.deltaTime; 
            }
            else
            {
                Destroy(Typeweapon);
            }
        }
    }

}
