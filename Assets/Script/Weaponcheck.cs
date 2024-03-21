using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaponcheck : MonoBehaviour
{
    [Header("��Ÿ ����")]
    [SerializeField] float speed = 1.0f;//���� ���� �ӵ�
    [SerializeField] float Xposition;//���� x��ǥ
    [SerializeField] float Yposition;//���� y��ǥ
    [SerializeField] float Zposition;//���� z��ǥ

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
        else if(punch == true)
        {
            //1.������Ʈ�� �����ϸ� �� �ڸ��� ���� ���� ->��
            //2.rotation�� z���� +180 Ȥ�� -180 �� ���Ѵ�
            //3.2������ ���� ���� Time.deltatime * punchtime �ʷ� ȸ���� ���� ��Ų�� 

            transform.eulerAngles += new Vector3(0, 0, 180 * Time.deltaTime * speed);
        }
    }

    public void startattack()
    {
        Xposition = transform.position.x;
        Yposition = transform.position.y;
        Zposition = transform.position.z;

        new Vector3(Xposition, Yposition, Zposition);
    }

    private void weapondestory()
    {
        float Xchange = transform.position.x;
        float Ychange = transform.position.y;
        float Zchange = transform.position.z;

        float Zrotation = transform.eulerAngles.z;

        if (arrow == true)
        {
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
            
            transform.eulerAngles = new Vector3(0, 0, Zrotation);

            if (Zrotation >= Zposition +170 || Zrotation <= Zposition - 170)
            {
                Destroy(Typeweapon);
            }

            Debug.Log(Zrotation);
        }
    }

    private void swordattack()
    {

    }

}
