using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Collider2D collision;
    [SerializeField]Camera mainCam;

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
    public float MaxHP;
    float Checkchange = 0;//���� ȸ�� ����
    float Yeulerchange = 0;//���� ������ �����ϱ� 0 <-> -180
    public float Horposition = 0f;//���� ������ġ
    public float Verposition = 0f;//���� ������ġ
    public int eulercheck =0;
    [SerializeField] bool slowcheck = false;
    public bool Monsterattackcheck;//���Ͱ� ���ȴ��� �ȶ��ȴ��� Ȯ��
    bool Bossattackcheck;//�������Ͱ� �����ߴ��� Ȯ���ϴ� �ڵ�
    public float Monsterdamage;//���Ͱ� �÷��̾ �����Ҷ� ������
    public float invincibilitytime = 1;//�������� ���ݹް� ����� �����ð�
    private float Maxinvincibilitytime;
    [SerializeField] public bool attackstandard;//������ �Ҷ� �Ȱ��� ���Ⱑ �ȳ����°� Ȯ���ִ� ��
    [SerializeField] bool ctattackstandard;//ī���͹��� üũ
    [SerializeField] float attacktimer;//���� �� ���ð� ����
    public float Maxattacktimer;
    [SerializeField] float ctattacktimer;//ī���͹��� �� ���ð� ����
    public float Maxctattacktimer;
    bool timerattack;
    bool cttimerattack;
    private float wayattack;//�ٶ󺸰��ִ� ���� üũ
    bool waytest;
    public bool movecheck;
    [SerializeField] GameObject bowweaponbox;
    [SerializeField] GameObject swordweaponbox;
    [SerializeField] GameObject magicweaponbox;
    [SerializeField] GameObject bowweapon;
    [SerializeField] GameObject ctounterbow;
    [SerializeField] GameObject gagecanvas;
    [SerializeField] BoxCollider2D roadmap;
    public bool destroyplayer;//�÷��̾ ���� ���

    Rigidbody2D rigid2D;

    [Header("ī���� ����")]
    [SerializeField] bool arrowcheck = false;
    [SerializeField] float Hitgauge = 0f;//���� ī���� ������ 1�̵Ǹ� ī����
    float Maxhitgage;
    [SerializeField] private bool countergagecheck = false;//�������� �ö󰡴��� �Ȱ����� Ȯ��
    [SerializeField] bool countertimercheck = false;//���� L��ų�� ���� ���� ī���� ����
    [SerializeField] float counterHittimer = 3;//���� L��ų�� ���� ���ӽð�
    private float maxcountertimer;
    [SerializeField] float slowspeed = 3;//����L��ų�� ������ ����� ���ο� ���� �ð�
    private float Maxslowspeed;


    [Header("�÷��̾� ����")]
    [SerializeField] GameObject play;//�÷��̾�� ĳ����
    [SerializeField] float playerspeed = 5f;//�÷��̾ �̵��ϴ� �ӵ�
    [SerializeField] public int Weapontype;//���� ����Ʈ

    [Header("�÷��̾��� �ɷ�ġ ����")]
    [SerializeField,Range(1,3)] int GameHP;//���ӳ� �÷��̾� ü��

    [Header("���� ���� ���� ����(����)")]
    [SerializeField] bool magiccheck = false;//������������ ������� Ȯ��
    public bool MagicCheck => magiccheck;

    [SerializeField] float magicgage = 5f;//���� ������ 0�̵Ǹ� ��� �Ұ�
    private float Maxmagicgage;
    private float CtHorposition = 0f;
    private float CtVerposition = 0f;
    private float CtCheckchange = 0f;

    Animator animator;

    [Header("��ǥȮ�� ����")] 
    [SerializeField] public float Xplayer;//x��ǥ Ȯ��
    [SerializeField] public float Yplayer;//y��ǥ Ȯ��

    [SerializeField] Transform trsHands;

    [Header("�÷�����UI")]
    [SerializeField] Image bowui;
    [SerializeField] Image bowui2;
    [SerializeField] Image swordui;
    [SerializeField] Image magicui;

    [SerializeField] Image ctbowui;
    [SerializeField] Image ctswordui;
    [SerializeField] Image ctmagicui;

    [SerializeField] Image hitgage;


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    HitboxMonster hitboxmonster = collision.gameObject.GetComponent<HitboxMonster>();

    //    if(hitboxmonster)
    //    {
    //        Monsterattackcheck = true;
    //    }
    //}

    private void OnTriggerStay2D(Collider2D collision)
    {

        HitboxMonster hitboxmonster = collision.gameObject.GetComponent<HitboxMonster>();

        if (hitboxmonster && GameManager.Instance.Monsterattack.Oncheckdamage == true)
        {
            Monsterattackcheck = true;
        }

        if(collision.gameObject.tag == "bossdamage")
        {
            Bossattackcheck = true;
        }
    }


    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        mainCam = GetComponent<Camera>();
        collision = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        MaxHP = GameHP;
        maxcountertimer = counterHittimer;//����L���ӽð�
        Maxslowspeed = slowspeed;//�ڱ� ���ο� ���ӽð�
        Maxmagicgage = magicgage;//���� ������ �ִ� ���� �ð�
        Maxinvincibilitytime = invincibilitytime;//���� ���� �ð�
    }

    void Start()
    {
        Weapontype = 0;
        gagecanvas.SetActive(false);

        GameManager.Instance.Player = this;

        movecheck = false;
    }

    void Update()
    {
        //checkMoveposition();//ī�޶� ���� �÷��̾ �� �������ϴ� �ڵ�
        turnway();//���� ���� ���� üũ
        move();//�̵�
        playerposition();//�÷��̾���ġ�� �ǽð����� Ȯ��
        WeaponChange();//���� ü����

        bowattack();//���Ÿ� ����

        countergage();//�������� ������
        fullhit();//���� ����
        counterHit();//����2��ų

        magichit();//���� ����

        weaponattacktimer();//�� ���� ��Ÿ�� ����

        playerhpcheck();//�÷��̾� HP ����
        bossdamage();//������ HP����

        Anim();//�ִϸ��̼� ����
    }

    public void move()
    {
        if(movecheck == false)
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
            //Changecheck();
            magiccounter();//���� ī���� �ڵ�
        }
    }//�̵��ڵ�
    private void Anim()//�ִϸ��̼ǰ��� �ڵ�
    {
        if(movecheck == false)
        {
            animator.SetFloat("Horizontal", (int)horizontals);
            animator.SetFloat("Vertical", (int)verticals);

            if (horizontals != 0)
            {
                transform.localScale = new Vector3(horizontals, 1, 1);
            }
        }
    }

    public void bowattack()//1�� �ϴ� �Ϲ� ���� �ڵ�
    {
        if(Input.GetKeyDown(KeyCode.K))//�Ϲ� ����
        {
            GameObject go = null;

            if (Weapontype == 0)//���Ÿ�(Ȱ)�ϰ��
            {
                if(arrowcheck == false && attackstandard == false)
                {
                    go = Instantiate(arrow);//ȭ���� ��ȯ
                    go.transform.eulerAngles = new Vector3(0, 0, Checkchange);//���⿡ ���� �߻�
                    go.transform.position = transform.position + new Vector3(Horposition, Verposition, 0);//�ڱ⺸�� �տ��� �߻�
                    attackstandard = true;
                    
                }
                else if( arrowcheck == true && ctattackstandard == false)
                {
                    go = Instantiate(ctArrow);//ȭ���� ��ȯ
                    go.transform.eulerAngles = new Vector3(0, 0, Checkchange);//���⿡ ���� �߻�
                    go.transform.position = transform.position + new Vector3(Horposition, Verposition, 0);//�ڱ⺸�� �տ��� �߻�
                    ctattackstandard = true;

                    if(ctattackstandard == true)
                    {
                        return;
                    }
                    Weaponcheck weaponcheck_2 = go.GetComponent<Weaponcheck>();
                    weaponcheck_2.Counterdamage(Weapontype, eulercheck);
                    return;
                }
                else if (Weapontype == 1)
                {
                    return;
                }
                else if (Weapontype == 2)//���������� ���
                {
                    return;
                }
                if(attackstandard == true)
                {
                    return;
                }
                if(ctattackstandard == true)
                {
                    return;
                }
                Weaponcheck weaponcheck = go.GetComponent<Weaponcheck>();
                weaponcheck.Attackdamage(Weapontype, eulercheck);
            }
        }//�Ϲ� ����

        if(Input.GetKeyDown(KeyCode.L))//ī���� ����
        {
            GameObject count = null;
            if (Weapontype == 0)//���Ÿ�(Ȱ)�ϰ��
            {
                if (arrowcheck == false)
                {
                    bowweapon.SetActive(false);
                    ctounterbow.SetActive(true);

                    arrowcheck = true;
                }
                else if(arrowcheck == true)
                {
                    bowweapon.SetActive(true);
                    ctounterbow.SetActive(false);
                    arrowcheck = false;
                }
                return;
            }
            else if (Weapontype == 1 && ctattackstandard == false)//������ ���
            {
                Hitgauge = 1;
                countertimercheck = true;
                ctattackstandard = true;

                return;
                //count = Instantiate(sword);
            }
            else if (Weapontype == 2 && ctattackstandard == false && magiccheck == false)//���������� ���
            {
                ctattackstandard = true;
                count = Instantiate(ctMagic);//������ �鿩��
                count.transform.eulerAngles = new Vector3(0, 0, CtCheckchange);//���⿡ ���� ����
                count.transform.position = transform.position + new Vector3(CtHorposition, CtVerposition, 0);//�ڱ⺸�� �տ��� �߻�
            }
            if(ctattackstandard == true || magiccheck == true)
            {
                return;
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
                bowweaponbox.SetActive(false);
                swordweaponbox.SetActive(true);
                gagecanvas.SetActive(true);
                Hitgauge = 0;
                Weapontype = 1;
            }
            else if(Weapontype == 1)//�ٰŸ� ���� => 1
            {
                swordweaponbox.SetActive(false);
                magicweaponbox.SetActive(true);
                gagecanvas.SetActive(false);
                Weapontype = 2;
            }
            else if(Weapontype == 2)//���Ÿ� �������� => 2
            {
                magicweaponbox.SetActive(false);
                bowweaponbox.SetActive(true);
                Weapontype = 0;
            }
            else
            {
                Weapontype = 0;
            }
        }
    }//���� ��ȯ �ڵ�

    #region
    //public void Changecheck()//���ݹ����� �˷��ִ� �ڵ�
    //{
    //    if(verticals == 1)//������ ����ų��
    //    {
    //        if(Weapontype == 0)//Ȱ�� ���
    //        {
    //            Yeulerchange = 0;//�ݴ� üũ �ݴ�� �����Ÿ� -180
    //            Checkchange = 0;//ȸ�� ��
    //            Verposition = 0.5f;//�� �Ʒ� üũ
    //            Horposition = 0;//�¿� üũ
    //            eulercheck = 1;//�ٶ󺸴� ���� üũ -> 0�� ����
    //        }//Ȱ
    //        else if(Weapontype == 1)//Į�� ���
    //        {
    //            Yeulerchange = 0;
    //            Checkchange = 90;
    //            Verposition = 0.15f;
    //            Verposition = 0.1f;
    //            eulercheck = 1;//1�� ���ʹ���
    //        }
    //        else if(Weapontype == 2)
    //        {
    //            Yeulerchange = 0;//�ݴ� üũ �ݴ�� �����Ÿ� -180
    //            Checkchange = 270;//ȸ�� ��
    //            Verposition = 1f;//�� �Ʒ� üũ
    //            Horposition = 0;//�¿� üũ
    //            eulercheck = 0;//�ٶ󺸴� ���� üũ -> 0�� ����
    //        }

    //    }
    //    else if(verticals == -1)//�Ʒ����� ����ų��
    //    {
    //        if(Weapontype == 0)
    //        {
    //            Yeulerchange = 0;
    //            Checkchange = 180;
    //            Verposition = -0.5f;
    //            Horposition = 0;
    //            eulercheck = 4;//�ٶ󺸴� ���� üũ -> 0�� ����
    //        }//Ȱ
    //        else if(Weapontype == 1)
    //        {
    //            Yeulerchange = -180;
    //            Checkchange = -90;
    //            Verposition = 0.15f;
    //            Verposition = 0.1f;
    //            eulercheck = 4;//4�� �Ʒ�����
    //        }
    //        else if(Weapontype == 2)
    //        {
    //            Yeulerchange = 0;
    //            Checkchange = 90;
    //            Verposition = -1f;
    //            Horposition = 0;
    //            eulercheck = 0;
    //        }
    //    }

    //    if (horizontals == 1)//�������� ����ų��
    //    {
    //        if(Weapontype == 0)
    //        {
    //            Yeulerchange = 0;
    //            Checkchange = 270;
    //            Horposition = 0.5f;
    //            Verposition = 0;
    //            eulercheck = 3;//�ٶ󺸴� ���� üũ -> 0�� ����
    //        }//Ȱ
    //        else if (Weapontype == 1)//���� ������ ���
    //        {
    //            Yeulerchange = 0;
    //            Checkchange = 0;
    //            Horposition = 0.15f;
    //            Verposition = -0.1f;
    //            eulercheck = 3;//3�� ������ ����
    //        }
    //        else if(Weapontype == 2)
    //        {
    //            Yeulerchange = 0;
    //            Checkchange = 0;
    //            Horposition = 0.9f;
    //            Verposition = 0;
    //            eulercheck = 0;
    //        }
    //    }
    //    else if(horizontals == -1)//������ ����ų��
    //    {
    //        if(Weapontype == 0)
    //        {
    //            Yeulerchange = 0;
    //            Checkchange = 90;
    //            Horposition = -0.5f;
    //            Verposition = 0;
    //            eulercheck = 2;//�ٶ󺸴� ���� üũ -> 0�� ����
    //        }//Ȱ
    //        else if (Weapontype == 1)//���� ������ ���
    //        {
    //            Yeulerchange = -180;
    //            Checkchange = -90;
    //            Horposition = -0.15f;
    //            Verposition = -0.1f;
    //            eulercheck = 2;//2�� ���� ����
    //        }
    //        else if(Weapontype == 2)
    //        {
    //            Yeulerchange = 0;
    //            Checkchange = 0;
    //            Horposition = -0.9f;
    //            Verposition = 0;
    //            eulercheck = 0;
    //        }
    //    }
    //}
    #endregion

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
                hitgage.fillAmount = Hitgauge;
                if (Hitgauge >= 1)
                {
                    Hitgauge = 1;
                }
            }
            if (Input.GetKeyUp(KeyCode.K))
            {
                countergagecheck = false;
                hitgage.fillAmount = 0;
            }
        }
    }

    private void fullhit()//���������ϰ�� �ߵ��ϴ� �ڵ�
    {
        if (Input.GetKeyUp(KeyCode.K))//���������ϰ�쿡 �ߵ��ϴ� �ڵ�
        {
            GameObject go = null;
            if (Weapontype == 1 && attackstandard == false)//������ ���
            {
                attackstandard = true;
                if (Hitgauge >= 1)
                {
                    if (countertimercheck != true)
                    {
                        Hitgauge = 0;//ī���� �������� �ʱ�ȭ
                    }
                    movecheck = true;
                    go = Instantiate(ctSword, trsHands);//ī���� ���⸦ ��ȯ
                    go.transform.eulerAngles = new Vector3(0, Yeulerchange, Checkchange);//�ٶ󺸰��ִ� �������� ����
                    go.transform.position = transform.position + new Vector3(Horposition, Verposition, 0);//�ڱ���ġ���� �տ��� ��ȯ
                    //Debug.Log("Ǯ��¡");
                    Weaponcheck weaponcheck_2 = go.GetComponent<Weaponcheck>();
                    weaponcheck_2.Counterdamage(Weapontype, eulercheck);
                    return;
                }
                else if (Hitgauge != 0)//�������� �� ��ä��ä�� ���� �� ���
                {
                    attackstandard = true;
                    Hitgauge = 0;
                    movecheck = true;
                    go = Instantiate(sword, trsHands);
                    go.transform.eulerAngles = new Vector3(0, Yeulerchange, Checkchange);
                    go.transform.position = transform.position + new Vector3(Horposition, Verposition, 0);
                    //Debug.Log("�⺻����");

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
                if(magicgage <= 1)
                {
                    return;
                }
                else
                {
                    magiccheck = true;
                    magicgage -= 1;
                }


                go = Instantiate(magic,trsHands);
                go.transform.eulerAngles = new Vector3(0, 0, Checkchange);//���⿡ ���� �߻�
                go.transform.position = transform.position + new Vector3(Horposition, Verposition, 0);//�ڱ⺸�� �տ��� �߻�
                //�� �Ʒ��� �߻�ɶ� �ȹٲ�-> 

                //Magicscript magicscript = go.GetComponent<Magicscript>();
                //magicscript.magicattack();

                Weaponcheck weaponcheck = go.GetComponent<Weaponcheck>();
                weaponcheck.Attackdamage(Weapontype, eulercheck);
            }
            if (Input.GetKeyUp(KeyCode.K))//Ű�� ������ ���������� �׸���
            {
                magiccheck = false;
            }

            if (magiccheck == true)//���� ����Ű�� ������
            {
                magicgage -= 1 * Time.deltaTime;//�������� �ʸ�ŭ ����
                magicui.fillAmount = magicgage / Maxmagicgage;
                if (magicgage <= 0)
                {
                    magiccheck = false;
                }
            }
            else if (magiccheck == false)
            {
                magicgage += 1 * Time.deltaTime;
                magicui.fillAmount = magicgage / Maxmagicgage;
                if (magicgage >= Maxmagicgage)
                {
                    magicgage = Maxmagicgage;
                }
            }
        }

        //�÷��̾ �Ѿư��鼭 ������ �ϰ� �ؾߵ�->�÷��̾ ���� ����� �ְ� �� �տ� ������Ʈ�� ������ �Ѿư����� �Ҽ��ֳ�? -> �ȴٸ� ������ ��� ó��... <- �������⿡�� ����
        //�ִϸ��̼��� �ݺ��ϴ� ������ �������� /��
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

    public void playerposition()
    {
        Xplayer = transform.position.x;
        Yplayer = transform.position.y;
    }//�÷��̾���ǥ�� Ȯ���ϴ� �ڵ�(���Ͱ� �����Ҷ� ����)

    public void playerhpcheck()//���Ͱ� �÷��̾ �����Ҷ� �÷��̾ �´� �ڵ�
    {
        if (Monsterattackcheck == true)//�÷��̾ ���Ͱ� �����Ҷ�
        {
            if (GameManager.Instance.Buttonmanager.Cheatcheck == true)
            {
                MaxHP -= 0;
                Monsterattackcheck = false;
            }
            else
            {
                Monsterdamage = GameManager.Instance.Monsterattack.attackdamage;//���� �������� �޴´�
                if (Maxinvincibilitytime == 1)
                {
                    MaxHP -= Monsterdamage;
                }

                if (MaxHP <= 0)
                {
                    destroyplayer = true;
                    Destroy(play);
                }
                else if (MaxHP > 0)
                {
                    //���� �÷��̾� HP�� ������ 1�ʰ� ����

                    Maxinvincibilitytime -= 1 * Time.deltaTime;

                    if (Maxinvincibilitytime <= 0)
                    {
                        Maxinvincibilitytime = invincibilitytime;
                        Monsterattackcheck = false;
                    }
                }
            }
        }
    }

    private void weaponattacktimer()//���� ���⸶�� ��Ÿ���� �ο�
    {
        //������ �Ҷ� attackstandard�� true�� ���� ���� true�� �ɽ� weaponcheck�� ��ϵ� �Ϳ� ���� attacktimer�� �ο� ����  attacktimer���� �ٷ� ����
        if (attackstandard == true)//�������Ű�� ������ true�� ��
        {
            if (Weapontype == 0)//���Ÿ� 1��ų�� ���
            {
                if (timerattack == false)//���ö� �ѹ��� �����ϵ��� ����
                {
                    attacktimer = 0.15f;
                    Maxattacktimer = attacktimer;
                    timerattack = true;
                }
                Maxattacktimer -= Time.deltaTime;
                bowui.fillAmount = attacktimer / Maxattacktimer;
                if (Maxattacktimer <= 0)//��Ÿ���� �� ���� ����
                {
                    timerattack = false;
                    attackstandard = false;
                }
            }
            else if(Weapontype == 1)
            {
                if (timerattack == false && attacktimer <= 0)//���⸦ ü���� �Ҷ� �ٷ� ���� ���ϵ��� ����
                {
                    attacktimer = 0.3f;
                    Maxattacktimer = attacktimer;
                    timerattack = true;
                }
                attacktimer -= Time.deltaTime;
                swordui.fillAmount = attacktimer / Maxattacktimer;
                if (attacktimer <= 0)//��Ÿ���� �� ���� ����
                {
                    timerattack = false;
                    attackstandard = false;
                    //Debug.Log("�ʱ�ȭ");
                }
            }//����
        }

        if(ctattackstandard == true)//ī������ ���
        {
            if(Weapontype == 0)//Ȱ
            {
                if (cttimerattack == false && ctattacktimer <= 0)//���⸦ ü���� �Ҷ� �ٷ� ���� ���ϵ��� ����
                {
                    attacktimer = 0.5f;
                    Maxattacktimer = attacktimer;
                    cttimerattack = true;
                }
                attacktimer -= Time.deltaTime;
                bowui2.fillAmount = attacktimer / Maxattacktimer;
                if (attacktimer <= 0)//��Ÿ���� �� ���� ����
                {
                    cttimerattack = false;
                    ctattackstandard = false;
                    //Debug.Log("ī���� �ʱ�ȭ");
                }
            }
            else if(Weapontype == 1)//��
            {
                if (cttimerattack == false && ctattacktimer <= 0)//���⸦ ü���� �Ҷ� �ٷ� ���� ���ϵ��� ����
                {
                    ctattacktimer = 10f;//L��ų���Ŀ� ��Ÿ��
                    Maxctattacktimer = ctattacktimer;
                    cttimerattack = true;
                }
                ctattacktimer -= Time.deltaTime;
                ctswordui.fillAmount = ctattacktimer / Maxctattacktimer;
                if (ctattacktimer <= 0)//��Ÿ���� �� ���� ����
                {
                    cttimerattack = false;
                    ctattackstandard = false;
                    Debug.Log("ī���� �ʱ�ȭ");
                }
            }
            else if(Weapontype == 2)//����
            {
                if (cttimerattack == false && ctattacktimer <= 0)//���⸦ ü���� �Ҷ� �ٷ� ���� ���ϵ��� ����
                {
                    ctattacktimer = 0.5f;
                    Maxctattacktimer = ctattacktimer;
                    cttimerattack = true;
                }
                ctattacktimer -= Time.deltaTime;
                ctmagicui.fillAmount = ctattacktimer / Maxctattacktimer; 
                if (ctattacktimer <= 0)//��Ÿ���� �� ���� ����
                {
                    cttimerattack = false;
                    ctattackstandard = false;
                    //Debug.Log("ī���� �ʱ�ȭ");
                }
            }
        }
    }


    //������ �ִ� �����϶����� �ٲ�� ������ �������� ���ݹ����� ����
    /// <summary>
    /// 0 => ����, 1=> ������, 2=> �Ʒ���, 3=> ����
    /// </summary>
    private void turnway()
    {
        wayattack = GameManager.Instance.CheckBox.waycheck;
        if(wayattack == 0)//����
        {
            if (Weapontype == 0)//Ȱ�� ���
            {
                Yeulerchange = 0;//�ݴ� üũ �ݴ�� �����Ÿ� -180
                Checkchange = 0;//ȸ�� ��
                Verposition = 0.5f;//�� �Ʒ� üũ
                Horposition = 0;//�¿� üũ
                eulercheck = 1;//�ٶ󺸴� ���� üũ -> 0�� ����
            }//Ȱ
            else if (Weapontype == 1)
            {
                Yeulerchange = 0;
                Checkchange = 0;
                Verposition = 0.5f;
                Horposition = 0;
                eulercheck = 1;
            }
            //else if (Weapontype == 2)//�����ϰ��
            //{
            //    Yeulerchange = 0;//�ݴ� üũ �ݴ�� �����Ÿ� -180
            //    Checkchange = 90;//ȸ�� ��
            //    Verposition = 1f;//�� �Ʒ� üũ
            //    Horposition = 0;//�¿� üũ
            //    eulercheck = 0;//�ٶ󺸴� ���� üũ -> 0�� ����
            //}
        }
        else if(wayattack == 1)//������
        {
            if (Weapontype == 0)
            {
                Yeulerchange = 0;
                Checkchange = 270;
                Horposition = 0.5f;
                Verposition = 0;
                eulercheck = 3;//�ٶ󺸴� ���� üũ -> 0�� ����
            }//Ȱ
            else if(Weapontype == 1)
            {
                Yeulerchange = 0;
                Checkchange = 270;
                Horposition = 0.5f;
                Verposition = 0;
                eulercheck = 3;
            }
            //else if (Weapontype == 2)
            //{
            //    Yeulerchange = 0;
            //    Checkchange = 0;
            //    Horposition = 0.9f;
            //    Verposition = 0;
            //    eulercheck = 0;
            //}
        }
        else if(wayattack == 2)//�Ʒ���
        {
            if (Weapontype == 0)
            {
                Yeulerchange = 0;
                Checkchange = 180;
                Verposition = -0.5f;
                Horposition = 0;
                eulercheck = 4;//�ٶ󺸴� ���� üũ -> 0�� ����
            }//Ȱ
            else if(Weapontype == 1)
            {
                Yeulerchange = 0;
                Checkchange = 180;
                Verposition = -0.5f;
                Horposition = 0;
                eulercheck = 4;
            }
            //else if (Weapontype == 2)
            //{
            //    Yeulerchange = 0;
            //    Checkchange = 270;
            //    Verposition = -1f;
            //    Horposition = 0;
            //    eulercheck = 0;
            //}
        }
        else if (wayattack == 3)//����
        {
            if (Weapontype == 0)
            {
                Yeulerchange = 0;
                Checkchange = 90;
                Horposition = -0.5f;
                Verposition = 0;
                eulercheck = 2;//�ٶ󺸴� ���� üũ -> 0�� ����
            }//Ȱ
            else if(Weapontype == 1)
            {
                Yeulerchange = 0;
                Checkchange = 90;
                Horposition = -0.5f;
                Verposition = 0;
                eulercheck = 2;
            }
            //else if (Weapontype == 2)
            //{
            //    Yeulerchange = 0;
            //    Checkchange = 0;
            //    Horposition = -0.9f;
            //    Verposition = 0;
            //    eulercheck = 0;
            //}
        }

    }


    private void checkMoveposition()
    {
        //������ �� �ڵ� �������� �ʿ��� �κ� ã�� ���� ������� ������ �ƴ� �ڵ�θ� ����
        #region
        //Vector3 currentPlayerPos = mainCam.WorldToScreenPoint(transform.position);

        //if(currentPlayerPos.x < 0.1f)
        //{
        //    currentPlayerPos.x = 0.1f;
        //}
        //else if(currentPlayerPos.x > 0.9f)
        //{
        //    currentPlayerPos.x = 0.9f;
        //}

        //if(currentPlayerPos.y < 0.1f)
        //{
        //    currentPlayerPos.y = 0.1f;
        //}
        //else if(currentPlayerPos.y > 0.9f)
        //{
        //    currentPlayerPos.y = 0.9f;
        //}

        //Vector3 fixedPlayerPos = mainCam.ViewportToWorldPoint(currentPlayerPos);
        //transform.position = fixedPlayerPos;
        #endregion
        //���� ���� ����

        

    }

    public void stage(int _stagetype)
    {
        if(_stagetype == 1)//Ʃ�丮�� > ���� ��������
        {
            transform.position = new Vector3(4, -10, 0);
        }
        else if(_stagetype == 2)//���� �������� -> �������� 1
        {
            transform.position = new Vector3(1, -9, 0);
        }
        else if(_stagetype == 3)//��������1 -> ���� ��������
        {
            transform.position = new Vector3(0.12f, -21, 0);
        }
    }

    private void bossdamage()//���� ���� ������ ����� �ڵ�
    {
        if(Bossattackcheck == true)//���� ���ݿ� �¾�����
        {
            if (GameManager.Instance.Buttonmanager.Cheatcheck == true)
            {
                MaxHP -= 0;
                Bossattackcheck = false;
            }
            else
            {
                //Monsterdamage = GameManager.Instance.Monsterattack.attackdamage;//���� �������� �޴´�
                Monsterdamage = 1;
                if (Maxinvincibilitytime == 1)
                {
                    MaxHP -= Monsterdamage;
                }

                if (MaxHP <= 0)
                {
                    Destroy(play);
                }
                else if (MaxHP > 0)
                {
                    //���� �÷��̾� HP�� ������ 1�ʰ� ����

                    Maxinvincibilitytime -= 1 * Time.deltaTime;

                    if (Maxinvincibilitytime <= 0)
                    {
                        Maxinvincibilitytime = invincibilitytime;
                        Bossattackcheck = false;
                    }
                }
            }
        }
    }


    
}
