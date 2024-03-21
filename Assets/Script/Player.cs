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
    [SerializeField] private float Horposition = 0f;//���� ������ġ
    [SerializeField] private float Verposition = 0f;//���� ������ġ


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
        bowattack();
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
            if(Weapontype == 0)//���Ÿ�(Ȱ)�ϰ��
            {
                go = Instantiate(arrow);
                go.transform.eulerAngles = new Vector3(0, 0, Checkchange);
                go.transform.position = transform.position + new Vector3(Horposition, Verposition, 0);

            }
            else if(Weapontype == 1)//������ ���
            {
                go = Instantiate(sword);
                go.transform.eulerAngles = new Vector3(0, 0, Checkchange -90 );
                go.transform.position = transform.position + new Vector3(Horposition, Verposition, 0);
            }
            else if(Weapontype == 2)//���������� ���
            {
                go = Instantiate(magic);
            }
            Weaponcheck weaponcheck = go.GetComponent<Weaponcheck>();
            weaponcheck.Attackdamage(Weapontype);
        }

        if(Input.GetKeyDown(KeyCode.L))//ī���� ����
        {
            GameObject count = null;
            if (Weapontype == 0)//���Ÿ�(Ȱ)�ϰ��
            {
                count = Instantiate(arrow);
            }
            else if (Weapontype == 1)//������ ���
            {
                count = Instantiate(sword);
            }
            else if (Weapontype == 2)//���������� ���
            {
                count  = Instantiate(magic);
            }
            Weaponcheck weaponcheck = count.GetComponent<Weaponcheck>();
            weaponcheck.Counterdamage(Weapontype);
        }
    }

    private void WeaponChange()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if(Weapontype == 0)//���Ÿ� ���� ���� => 0
            {
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

    public void Changecheck()
    {
        if(verticals == 1)
        {
            Checkchange = 0;
            Verposition = 0.5f;
            Horposition = 0;

        }
        else if(verticals == -1)
        {
            Checkchange = 180;
            Verposition = -0.5f;
            Horposition = 0;
        }

        if (horizontals == 1)
        {
            Checkchange = 270;
            if (Weapontype == 1)
            {
                Checkchange = 90;
            }
            Horposition = 0.5f;
            Verposition = 0;
        }
        else if(horizontals == -1)
        {
            Checkchange = 90;
            Horposition = -0.5f;
            Verposition = 0;
        }
    }

}
