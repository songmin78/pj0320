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
    [SerializeField] bool slowcheck = false;

    [Header("ī���� ����")]
    [SerializeField] bool arrowcheck = false;
    [SerializeField] float Hitgauge = 0f;//���� ī���� ������ 1�̵Ǹ� ī����
    [SerializeField] private bool countergagecheck = false;//�������� �ö󰡴��� �Ȱ����� Ȯ��
    [SerializeField] bool countertimercheck = false;//���� L��ų�� ���� ���� ī���� ����
    [SerializeField] float counterHittimer = 3;//���� L��ų�� ���� ���ӽð�
    private float maxcountertimer;
    [SerializeField] float slowspeed = 3;//����L��ų�� ������ ����� ���ο� ���� �ð�
    private float Maxslowspeed;
    

    [Header("�÷��̾� ����")]
    [SerializeField] float playerspeed = 5f;//�÷��̾ �̵��ϴ� �ӵ�
    [SerializeField] public int Weapontype;//���� ����Ʈ

    [Header("�÷��̾��� �ɷ�ġ ����")]
    [SerializeField,Range(1,5)] int GameHP;//���ӳ� �÷��̾� ü��

    [Header("���� ���� ���� ����(����)")]
    [SerializeField] bool magiccheck = false;//������������ ������� Ȯ��
    public bool MagicCheck => magiccheck;

    [SerializeField] float magicgage = 5f;//���� ������ 0�̵Ǹ� ��� �Ұ�
    private float Maxmagicgage;
    private float CtHorposition = 0f;
    private float CtVerposition = 0f;
    private float CtCheckchange = 0f;

    Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        MaxHP = GameHP;
        maxcountertimer = counterHittimer;//����L���ӽð�
        Maxslowspeed = slowspeed;//�ڱ� ���ο� ���ӽð�
        Maxmagicgage = magicgage;//���� ������ �ִ� ���� �ð�
    }

    void Start()
    {
        Weapontype = 0;

        GameManager.Instance.Player = this;
    }

    void Update()
    {
        move();//�̵�
        Anim();//�̵��ִϸ��̼�
        WeaponChange();//���� ü����

        bowattack();//���Ÿ� ����

        countergage();//�������� ������
        fullhit();//���� ����
        counterHit();//����2��ų

        magichit();//���� ����

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
        magiccounter();//���� ī���� �ڵ�
    }//�̵��ڵ�
    private void Anim()//�̵� �ִϸ��̼� �ڵ�
    {
        animator.SetFloat("Horizontal", (int)horizontals);
        animator.SetFloat("Vertical", (int)verticals);

        if (horizontals != 0)
        {
            transform.localScale = new Vector3(horizontals, 1, 1);
        }
    }

    public void bowattack()//1�� �ϴ� �Ϲ� ���� �ڵ�
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
                return;
            }
            Weaponcheck weaponcheck = go.GetComponent<Weaponcheck>();
            weaponcheck.Attackdamage(Weapontype, eulercheck);
        }//�Ϲ� ����

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
                Hitgauge = 1;
                countertimercheck = true;

                return;
                //count = Instantiate(sword);
            }
            else if (Weapontype == 2)//���������� ���
            {
                count = Instantiate(ctMagic);//������ �鿩��
                count.transform.eulerAngles = new Vector3(0, 0, CtCheckchange);//���⿡ ���� ����
                count.transform.position = transform.position + new Vector3(CtHorposition, CtVerposition, 0);//�ڱ⺸�� �տ��� �߻�
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
    }//���� ��ȯ �ڵ�

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
                eulercheck = 1;//�ٶ󺸴� ���� üũ -> 0�� ����
            }//Ȱ
            else if(Weapontype == 1)//Į�� ���
            {
                Yeulerchange = 0;
                Checkchange = 90;
                Verposition = 0.15f;
                Verposition = 0.1f;
                eulercheck = 1;//1�� ���ʹ���
            }
            else if(Weapontype == 2)
            {
                Yeulerchange = 0;//�ݴ� üũ �ݴ�� �����Ÿ� -180
                Checkchange = 90;//ȸ�� ��
                Verposition = 1f;//�� �Ʒ� üũ
                Horposition = 0;//�¿� üũ
                eulercheck = 0;//�ٶ󺸴� ���� üũ -> 0�� ����
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
                eulercheck = 4;//�ٶ󺸴� ���� üũ -> 0�� ����
            }//Ȱ
            else if(Weapontype == 1)
            {
                Yeulerchange = -180;
                Checkchange = -90;
                Verposition = 0.15f;
                Verposition = 0.1f;
                eulercheck = 4;//4�� �Ʒ�����
            }
            else if(Weapontype == 2)
            {
                Yeulerchange = 0;
                Checkchange = 270;
                Verposition = -1f;
                Horposition = 0;
                eulercheck = 0;
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
                eulercheck = 3;//�ٶ󺸴� ���� üũ -> 0�� ����
            }//Ȱ
            else if (Weapontype == 1)//���� ������ ���
            {
                Yeulerchange = 0;
                Checkchange = 0;
                Horposition = 0.15f;
                Verposition = -0.1f;
                eulercheck = 3;//3�� ������ ����
            }
            else if(Weapontype == 2)
            {
                Yeulerchange = 0;
                Checkchange = 0;
                Horposition = 0.9f;
                Verposition = 0;
                eulercheck = 0;
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
                eulercheck = 2;//�ٶ󺸴� ���� üũ -> 0�� ����
            }//Ȱ
            else if (Weapontype == 1)//���� ������ ���
            {
                Yeulerchange = -180;
                Checkchange = -90;
                Horposition = -0.15f;
                Verposition = -0.1f;
                eulercheck = 2;//2�� ���� ����
            }
            else if(Weapontype == 2)
            {
                Yeulerchange = 0;
                Checkchange = 180;
                Horposition = -0.9f;
                Verposition = 0;
                eulercheck = 0;
            }
        }
    }

    private void countergage()//�Ϲ� �����Ҷ� ������
    {
        if (countertimercheck != true)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                countergagecheck = true;
            }
            if (countergagecheck == true)
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
    }

    private void fullhit()//���������ϰ�� �ߵ��ϴ� �ڵ�
    {
        if (Input.GetKeyUp(KeyCode.K))//���������ϰ�쿡 �ߵ��ϴ� �ڵ�
        {
            GameObject go = null;
            if (Weapontype == 1)//������ ���
            {
                if (Hitgauge >= 1)
                {
                    if (countertimercheck != true)
                    {
                        Hitgauge = 0;//ī���� �������� �ʱ�ȭ
                    }
                    go = Instantiate(ctSword);//ī���� ���⸦ ��ȯ
                    go.transform.eulerAngles = new Vector3(0, Yeulerchange, Checkchange);//�ٶ󺸰��ִ� �������� ����
                    go.transform.position = transform.position + new Vector3(Horposition, Verposition, 0);//�ڱ���ġ���� �տ��� ��ȯ
                    Debug.Log("Ǯ��¡");

                    Weaponcheck weaponcheck_2 = go.GetComponent<Weaponcheck>();
                    weaponcheck_2.Counterdamage(Weapontype, eulercheck);
                    return;
                }
                else if (Hitgauge != 0)
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

    private void magichit()//���������� ���ϴ� �ڵ�
    {
        if(Weapontype == 2)
        {
            if (Input.GetKeyDown(KeyCode.K))//�������� ���������� �����
            {
                GameObject go = null;
                magiccheck = true;

                go = Instantiate(magic);
                go.transform.eulerAngles = new Vector3(0, 0, Checkchange);//���⿡ ���� �߻�
                go.transform.position = transform.position + new Vector3(Horposition, Verposition, 0);//�ڱ⺸�� �տ��� �߻�

                Weaponcheck weaponcheck = go.GetComponent<Weaponcheck>();
                weaponcheck.Attackdamage(Weapontype, eulercheck);
            }
            if (Input.GetKeyUp(KeyCode.K))//Ű�� ������ ���������� �׸���
            {
                magiccheck = false;
            }

            if (magiccheck == true)
            {
                magicgage -= 1 * Time.deltaTime;
                if (magicgage <= 0)
                {
                    magiccheck = false;
                }
            }
            else if (magiccheck == false)
            {
                magicgage += 1 * Time.deltaTime;
                if (magicgage >= Maxmagicgage)
                {
                    magicgage = Maxmagicgage;
                }
            }
        }
    }

    private void counterHit()//���� 2��ų �ð� �ڵ�
    {
        if (Hitgauge == 1 && countertimercheck == true)
        {
            maxcountertimer -= 1 * Time.deltaTime;
            if (maxcountertimer <= 0)
            {
                Hitgauge = 0;
                playerspeed = playerspeed - 3;
                maxcountertimer = counterHittimer;
                slowcheck = true;
                countertimercheck = false;
            }
        }
        else if(slowcheck == true)
        {
            Maxslowspeed -= 1 * Time.deltaTime;
            if (Maxslowspeed <= 0)
            {
                playerspeed = playerspeed + 3;
                Maxslowspeed = slowspeed;
                slowcheck = false;
            }
        }
    }

    private void magiccounter()
    {
        if (verticals == 1)//������ ����ų��
        {
            CtCheckchange = 0;//ȸ�� ��
            CtVerposition = 0.5f;//�� �Ʒ� üũ
            CtHorposition = 0;//�¿� üũ

        }
        else if (verticals == -1)//�Ʒ����� ����ų��
        {
            CtCheckchange = 180;
            CtVerposition = -0.5f;
            CtHorposition = 0;
        }

        if (horizontals == 1)//�������� ����ų��
        {
            CtCheckchange = 270;
            CtVerposition = 0f;
            CtHorposition = 0.5f;
        }
        else if (horizontals == -1)//������ ����ų��
        {
            CtCheckchange = 90;
            CtVerposition = 0f;
            CtHorposition = -0.5f;
        }
    }//���� 2��ų ���� �ڵ�
}
