using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    Transform Attackbox;

    [Header("�Ϲݰ���")]
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject sword;
    [SerializeField] GameObject magic;

    [Header("ī���� ����")]
    [SerializeField] GameObject ctArrow;
    [SerializeField] GameObject ctSword;
    [SerializeField] GameObject ctMagic;

    [Header("��Ÿ")]
    [SerializeField] float horizontals;
    [SerializeField] float verticals;
    private float MaxHP;
    [SerializeField] float Checkchange = 0;//���� ȸ�� ����
    [SerializeField] float Yeulerchange = 0;//���� ������ �����ϱ� 0 <-> -180
    [SerializeField] private float Horposition = 0f;//���� ������ġ
    [SerializeField] private float Verposition = 0f;//���� ������ġ
    public int eulercheck =0;

    [Header("ī���� ����")]
    [SerializeField] bool arrowcheck = false;
    [SerializeField] float Hitgauge = 0f;//���� ī���� ������ 1�̵Ǹ� ī����
    [SerializeField] private bool countergagecheck = false;//�������� �ö󰡴��� �Ȱ����� Ȯ��


    [Header("�÷��̾� ����")]
    [SerializeField] float playerspeed = 5f;//�÷��̾ �̵��ϴ� �ӵ�
    [SerializeField] public int Weapontype;//���� ����Ʈ

    [Header("�÷��̾��� �ɷ�ġ ����")]
    [SerializeField,Range(1,5)] int GameHP;//���ӳ� �÷��̾� ü��

    Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        MaxHP = GameHP;
    }

    void Start()
    {
        Weapontype = 0;
    }

    void Update()
    {
        move();
        Anim();
        WeaponChange();
        countergage();
        bowattack();
        fullhit();
    }

    public void move()
    {
        horizontals = Input.GetAxisRaw("Horizontal");//�¿�
        verticals = Input.GetAxisRaw("Vertical");//����
        

        if (horizontals != 0)
        {
            verticals = 0;
        }
        else if (verticals != 0)
        {
            horizontals = 0;
        }
        transform.position += new Vector3(horizontals * playerspeed, verticals * playerspeed, 0) * Time.deltaTime;
        Changecheck();
    }
    private void Anim()
    {
        animator.SetFloat("Horizontal", (int)horizontals);
        animator.SetFloat("Vertical", (int)verticals);

        if (horizontals != 0)
        {
            transform.localScale = new Vector3(horizontals, 1, 1);
        }
    }

    public void bowattack()
    {
        if(Input.GetKeyDown(KeyCode.K))//�Ϲ� ����
        {
            GameObject go = null;

            if (Weapontype == 0)//���Ÿ�(Ȱ)�ϰ��
            {
                if(arrowcheck == false)
                {
                    go = Instantiate(arrow);//ȭ���� ��ȯ
                    go.transform.eulerAngles = new Vector3(0, 0, Checkchange);//���⿡ ���� �߻�
                    go.transform.position = transform.position + new Vector3(Horposition, Verposition, 0);//�ڱ⺸�� �տ��� �߻�
                }
                else if( arrowcheck == true)
                {
                    go = Instantiate(ctArrow);//ȭ���� ��ȯ
                    go.transform.eulerAngles = new Vector3(0, 0, Checkchange);//���⿡ ���� �߻�
                    go.transform.position = transform.position + new Vector3(Horposition, Verposition, 0);//�ڱ⺸�� �տ��� �߻�

                    Weaponcheck weaponcheck_2 = go.GetComponent<Weaponcheck>();
                    weaponcheck_2.Counterdamage(Weapontype, eulercheck);
                    return;
                    
                }

            }
            else if(Weapontype == 1)
            {
                return;
            }
            else if(Weapontype == 2)//���������� ���
            {
                go = Instantiate(magic);
            }
            Weaponcheck weaponcheck = go.GetComponent<Weaponcheck>();
            weaponcheck.Attackdamage(Weapontype, eulercheck);
        }

        if(Input.GetKeyDown(KeyCode.L))//ī���� ����
        {
            GameObject count = null;
            if (Weapontype == 0)//���Ÿ�(Ȱ)�ϰ��
            {
                if (arrowcheck == false)
                {
                    arrowcheck = true;
                }
                else if(arrowcheck == true)
                {
                    arrowcheck = false;
                }
                return;
            }
            else if (Weapontype == 1)//������ ���
            {
                //count = Instantiate(sword);
            }
            else if (Weapontype == 2)//���������� ���
            {
                //count  = Instantiate(magic);
            }
            Weaponcheck weaponcheck = count.GetComponent<Weaponcheck>();
            weaponcheck.Counterdamage(Weapontype, eulercheck);
        }
    }

    private void WeaponChange()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if(Weapontype == 0)//���Ÿ� ���� ���� => 0
            {
                Hitgauge = 0;
                Weapontype = 1;
            }
            else if(Weapontype == 1)//�ٰŸ� ���� => 1
            {
                Weapontype = 2;
            }
            else if(Weapontype == 2)//���Ÿ� �������� => 2
            {
                Weapontype = 0;
            }
            else
            {
                Weapontype = 0;
            }
        }
    }

    public void Changecheck()//���ݹ����� �˷��ִ� �ڵ�
    {
        if(verticals == 1)//������ ����ų��
        {
            if(Weapontype == 0)//Ȱ�� ���
            {
                Yeulerchange = 0;//�ݴ� üũ �ݴ�� �����Ÿ� -180
                Checkchange = 0;//ȸ�� ��
                Verposition = 0.5f;//�� �Ʒ� üũ
                Horposition = 0;//�¿� üũ
                eulercheck = 0;//�ٶ󺸴� ���� üũ -> 0�� ����
            }//Ȱ
            else if(Weapontype == 1)//Į�� ���
            {
                Yeulerchange = 0;
                Checkchange = 90;
                Verposition = 0.15f;
                Verposition = 0.1f;
                eulercheck = 1;//1�� ���ʹ���
            }

        }
        else if(verticals == -1)//�Ʒ����� ����ų��
        {
            if(Weapontype == 0)
            {
                Yeulerchange = 0;
                Checkchange = 180;
                Verposition = -0.5f;
                Horposition = 0;
                eulercheck = 0;//�ٶ󺸴� ���� üũ -> 0�� ����
            }//Ȱ
            else if(Weapontype == 1)
            {
                Yeulerchange = -180;
                Checkchange = -90;
                Verposition = 0.15f;
                Verposition = 0.1f;
                eulercheck = 2;//2�� ���� ����
            }
        }

        if (horizontals == 1)//�������� ����ų��
        {
            if(Weapontype == 0)
            {
                Yeulerchange = 0;
                Checkchange = 270;
                Horposition = 0.5f;
                Verposition = 0;
                eulercheck = 0;//�ٶ󺸴� ���� üũ -> 0�� ����
            }//Ȱ
            else if (Weapontype == 1)//���� ������ ���
            {
                Yeulerchange = 0;
                Checkchange = 0;
                Horposition = 0.15f;
                Verposition = -0.1f;
                eulercheck = 3;//3�� ������ ����
            }
        }
        else if(horizontals == -1)//������ ����ų��
        {
            if(Weapontype == 0)
            {
                Yeulerchange = 0;
                Checkchange = 90;
                Horposition = -0.5f;
                Verposition = 0;
                eulercheck = 0;//�ٶ󺸴� ���� üũ -> 0�� ����
            }//Ȱ
            else if (Weapontype == 1)//���� ������ ���
            {
                Yeulerchange = -180;
                Checkchange = -90;
                Horposition = -0.15f;
                Verposition = -0.1f;
                eulercheck = 4;//4�� �Ʒ�����
            }
        }
    }

    private void countergage()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            countergagecheck = true;
        }
        if(countergagecheck == true)
        {
            Hitgauge += Time.deltaTime;
            if (Hitgauge >= 1)
            {
                Hitgauge = 1;
            }
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            countergagecheck = false;
        }

    }

    private void fullhit()
    {
        if(Input.GetKeyUp(KeyCode.K))//���������ϰ�쿡 �ߵ��ϴ� �ڵ�
        {
            GameObject go = null;
            if (Weapontype == 1)//������ ���
            {
                if (Hitgauge >= 1)
                {
                    Hitgauge = 0;//ī���� �������� �ʱ�ȭ
                    go = Instantiate(ctSword);//ī���� ���⸦ ��ȯ
                    go.transform.eulerAngles = new Vector3(0, Yeulerchange, Checkchange);//�ٶ󺸰��ִ� �������� ����
                    go.transform.position = transform.position + new Vector3(Horposition, Verposition, 0);//�ڱ���ġ���� �տ��� ��ȯ
                    Debug.Log("Ǯ��¡");

                    Weaponcheck weaponcheck_2 = go.GetComponent<Weaponcheck>();
                    weaponcheck_2.Counterdamage(Weapontype,eulercheck);
                    return;
                }
                else if (Weapontype != 0)
                {
                    Hitgauge = 0;
                    go = Instantiate(sword);
                    go.transform.eulerAngles = new Vector3(0, Yeulerchange, Checkchange);
                    go.transform.position = transform.position + new Vector3(Horposition, Verposition, 0);
                    Debug.Log("�⺻����");

                    Weaponcheck weaponcheck = go.GetComponent<Weaponcheck>();
                    weaponcheck.Attackdamage(Weapontype, eulercheck); 
                }
            }
            else
            {
                return;
            }
        }
    }
}
