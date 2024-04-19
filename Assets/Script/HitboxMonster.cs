using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class HitboxMonster : MonoBehaviour
{
    [SerializeField]GameObject parents;

    [Header("���� ����")]
    //[SerializeField] public float attackdamage;//������ ������
    [SerializeField] float MsGameHp = 1;//������ HP
    private float MsMaxHp;
    [SerializeField] bool pushdamage;
    public bool pushed;
    float pushway;
    float pushroad = 0.2f;
    public bool waycheck;

    [Header("���� ���� üũ")]
    [SerializeField] bool Bosscheck;


    [Header("���� ����")]
    bool beatendamage = false;
    //public bool Oncheckdamage = false;
    float weapondamage = 0;

    [Header("��Ÿ")]
    [SerializeField] public bool magicchek = false;
    float retime = 0.5f;
    float Maxretime;

    [Header("���� �̵� ��ũ��Ʈ ����")]

    Animator animator;

    [Header("�Ѿư������� ����")]
    //[SerializeField] private bool ChasePlayer = false;
    [SerializeField] private bool ChaseX = false;
    [SerializeField] private bool ChaseY = false;
    [SerializeField] private float posX;//�÷��̾���ġ + ������ġ ��.X
    [SerializeField] private float posY;//�÷��̾���ġ + ������ġ ��.Y

    [SerializeField] Vector3 diffPos;

    [SerializeField] float horizontals;
    [SerializeField] float verticals;

    [Header("���� ����")]
    [SerializeField] float speed = 5f;//�����̵��ӵ�
    float Maxspeed;

    [Header(" �÷��̾� ��ġ")]
    [SerializeField] float xplayer;
    [SerializeField] float yplayer;
    bool hitpush;

    //�������


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Weaponcheck weapon = collision.gameObject.GetComponent<Weaponcheck>();//���� gameobject�� ���� collision�� ������ 
        Player player = collision.gameObject.GetComponent<Player>();

        if (weapon)
        {
            beatendamage = true;
            if(GameManager.Instance.Weaponcheck.magic == true)//���࿡ ������ ��������
            {
                magicchek = true;
            }
            else if(GameManager.Instance.Weaponcheck.Counter == true && GameManager.Instance.Weaponcheck.punch == true)
            {
                pushdamage = true;
            }
        }

        //if(collision.gameObject.tag == "Player")
        //{
        //    Oncheckdamage = true;
        //}
        //else
        //{
        //    Oncheckdamage = false;
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(magicchek == true)
        {
            magicchek = false;
        }
        else if(pushdamage == true)
        {
            pushdamage = false;
        }
    }


    //���࿡ magic�� true�϶� ������ ����� true ���Ŀ� update���� ����� true�϶� �������� �԰� 0.?�ʰ� ���� ���Ŀ� �� ���ظ� �Դ´�.


    private void Awake()
    {
        Maxretime = retime;
        MsMaxHp = MsGameHp;
        Maxspeed = speed;
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        GameManager.Instance.HitboxMonster = this;
    }

    void Update()
    {
        //���� �̵� ��ũ��Ʈ
        playerchase();
        Anim();
        slowspeed();
        puchcheck();

        //���� ��Ʈ ��ũ��Ʈ
        Hitboxmaonster();
        destroymonster();
        destroymagic();

        pushmonster();
        Timer();

    }

    private void Hitboxmaonster()
    {
        if(beatendamage == true && magicchek == false)
        {
            weapondamage = GameManager.Instance.Weaponcheck.AttackdamageMax;
            MsMaxHp -= weapondamage;
        }
    }

    private void destroymonster()
    {
        if(MsMaxHp <= 0)
        {
            Destroy(parents);
        }
        else
        {
            beatendamage = false;
        }
    }

    private void destroymagic()//���� ������ ����� ���
    {
        //if(magicchek == true)
        //{
        //    weapondamage = GameManager.Instance.Weaponcheck.AttackdamageMax;
        //    MaxHp -= weapondamage;
        //    //retime -= Time.deltaTime;
        //}

        if(magicchek == true)
        {
            if(Maxretime <= 0)
            {
                Maxretime = retime;
            }
            while (Maxretime == retime)
            {
                weapondamage = GameManager.Instance.Weaponcheck.AttackdamageMax;
                MsMaxHp -= weapondamage;
                break;
            }
            Maxretime -= Time.deltaTime;
            
        }

    }

    private void pushmonster()
    {
        //Vector3 vec = parents.transform.position;
        Vector3 vec3 = parents.transform.position;
        if (pushdamage == true && Bosscheck == false)
        {
            pushway = GameManager.Instance.Weaponcheck.eulercheck;
            if (pushway == 1 && waycheck == false)//���� ������
            {
                waycheck = true;
                pushed = true;
                vec3.y += 1.3f;
                parents.transform.position = vec3;
            }
            else if(pushway == 2 && waycheck == false)//�������� ������
            {
                waycheck = true;
                pushed = true;
                vec3.x -= 1.3f;
                parents.transform.position = vec3;
            }
            else if(pushway == 3 && waycheck == false)//���������� ������
            {
                waycheck = true;
                pushed = true;
                vec3.x += 1.3f;
                parents.transform.position = vec3;
            }
            else if (pushway == 4 && waycheck == false)//�Ʒ��� ������
            {
                waycheck = true;
                pushed = true;
                vec3.y -= 1.3f;
                parents.transform.position = vec3;
            }

        }
        


    }

    private void Timer()
    {
        if(pushed == true)
        {
            pushroad -= Time.deltaTime;
            if (pushroad <= 0)
            {
                pushroad = 0.2f;
                pushed = false;
                waycheck = false;
            }
        }
    }




    //=======================================


    private void playerchase()//�÷��̾ �Ѿư��� �ڵ�
    {
        if ( GameManager.Instance.MonsterMoving.ChasePlayer == true)
        {
            #region ��Ŀ���� ����
            //1.�÷��̾��� ��ġ�� �ǽð����� Ȯ��
            //2.�÷����� ��ġ - �ڽ��� ��ġ�� �Ͽ� �̵�
            //3.�밢�� ���� -> �켱����... -> horizon�̳� vertical�� ���ڰ� 0�� �� ����� ������ �̵�
            //����1: �÷��̾��� ��ġ�� �޴¹��� �𸥴�-�ذ�
            //����2:���Ͱ� �ڵ����� �̵��ϱ����� ��� �� (�ִϸ��̼� ������- �ذ�ȵ�) <- Player��ũ��Ʈ���� ������� ���� ����-�ذ�
            //����3: �������׷� �밢������ �̵��� -> �ذ�
            //����4: ������ �̼��� ���� ������
            #endregion

            Vector3 pos = GameManager.Instance.Player.transform.position;//GameManager���� �÷��̾��� ��ġ�� ���� ���� �ڵ�

            //�÷��̾� + ������ġ x��
            //�÷��̾� + ������ġ y��
            posX = pos.x - transform.position.x;
            posY = pos.y - transform.position.y;

            diffPos = pos - transform.position;

            bool ChaseHorizontal = false;
            // x,y��ǥ�� ���밪���� ���
            if (Mathf.Abs(posX) > 0.02)//x��ǥ�� �� Ŭ���,Mathf.Abs() <-���밪���� ��ȯ�ϴ� �ڵ�
            {
                ChaseHorizontal = true;
            }
            else if (Mathf.Abs(posX) <= 0.02)//x��ǥ�� 0�� �� ��� �� �¿�� ���� �̵� �� ���η� �̵�
            {
                ChaseHorizontal = false;
            }

            //bool ChaseHorizontal = Mathf.Abs(diffPos.x) < Mathf.Abs(diffPos.y);

            Vector3 dir = Vector3.zero;
            if (ChaseHorizontal == true)//�� ���
            {
                dir.x = diffPos.x > 0 ? 1 : -1;
            }
            else
            {
                dir.y = diffPos.y > 0 ? 1 : -1;
            }

            parents.transform.position += Maxspeed * Time.deltaTime * dir;

            #region �����ڵ�
            //x,y��ǥ�� ���밪���� ���
            //if (Mathf.Abs(posX) > 0.02)//x��ǥ�� �� Ŭ���,Mathf.Abs() <-���밪���� ��ȯ�ϴ� �ڵ�
            //{
            //    ChaseX = true;
            //    ChaseY = false;
            //}
            //else if(Mathf.Abs(posX) <= 0.02)//x��ǥ�� 0�� �� ��� �� �¿�� ���� �̵� �� ���η� �̵�
            //{
            //    ChaseX = false;
            //    ChaseY = true;
            //}

            //if (pos.x < transform.position.x && ChaseX == true)//���Ͱ� �÷��̾��� �����ʿ� ������� �׸��� ChaseX�� true�� ���(�밢�� ����)
            //{
            //    ChaseY = false;
            //    transform.position += new Vector3(-transform.position.x + 1 * speed, 0, 0) * Time.deltaTime;//������ �ڱ� ��ġ���� Vector3�� x ���� -1��ŭ �̵���Ű�� �ڵ�
            //}
            //else if(pos.x > transform.position.x && ChaseX == true)
            //{
            //    ChaseY = false;
            //    transform.position += new Vector3(transform.position.x + 1 * speed, 0, 0) * Time.deltaTime;
            //}

            //if(pos.y < transform.position.y && ChaseY == true)
            //{
            //    ChaseX = false;
            //    transform.position += new Vector3(0, transform.position.y - 1 * speed, 0) * Time.deltaTime;
            //}
            //else if(pos.y > transform.position.y && ChaseY == true)
            //{
            //    ChaseX = false;
            //    transform.position += new Vector3(0, transform.position.x + 1 * speed, 0) * Time.deltaTime;
            //}
            #endregion
        }
    }

    private void Anim()//�̵� �ִϸ��̼� �ڵ�
    {
        //animator.SetFloat("Horizontal", (float)horizontals);
        //animator.SetFloat("Vertical", (float)verticals);

        //if (horizontals < 0)
        //{
        //    transform.localScale = new Vector3(horizontals, 1, 1);
        //}
    }


    private void slowspeed()
    {
        if (magicchek == true)//���Ͱ� �������ݿ� ������
        {
            #region �����ڵ�
            //Maxspeed -= 1;//�⺻ �̵��ӵ��� ���δ�
            //if(Maxspeed < speed - 1)
            //{
            //    Maxspeed = speed - 1;
            //}
            #endregion

            Maxspeed = 0;
        }
        else
        {
            #region �����ڵ� ���Ͼ���
            //Maxspeed += 1;
            //if(Maxspeed > speed)
            //{
            //    Maxspeed = speed;
            //}
            #endregion

            Maxspeed = speed;
        }
    }

    private void puchcheck()//�з� ������
    {
        if (pushed == true)
        {
            Maxspeed = 0;
        }
        else if (magicchek == false && pushed == false)
        {
            Maxspeed = speed;
        }
    }
}
