using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitboxMonster : MonoBehaviour
{
    [SerializeField] GameObject parents;
    [SerializeField] Image monsterHpbar;

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
    [SerializeField] public bool hitmagicchek = false;
    //[SerializeField] bool stopnmagiccheck = false;
    float retime = 0.5f;
    float Maxretime;
    [SerializeField] bool ChaseHorizontal = false;

    [Header("���� �̵� ��ũ��Ʈ ����")]

    Animator animator;

    [Header("�Ѿư������� ����")]
    [SerializeField] bool fildmonster;//�ʵ忡 �ִ� ���ʹ� �ڵ����� �� �Ѿư��� ���� �ڵ�
    [SerializeField] public bool ChasePlayered = false;
    [SerializeField] public bool autoChasePlayered = false;
    //[SerializeField] private bool ChaseX = false;
    //[SerializeField] private bool ChaseY = false;
    [SerializeField] private float posX;//�÷��̾���ġ + ������ġ ��.X
    [SerializeField] private float posY;//�÷��̾���ġ + ������ġ ��.Y
    [SerializeField]bool wallcheck;//���� ��Ҵ��� �� ��Ҵ��� Ȯ���ϴ� �ڵ�
    [SerializeField] bool debug = false;
    bool wallhorizontal;
    bool wallcheck1;
    bool wallcheck2;
    bool wallvertical;
    float wallmove;//���� �ѹ������θ� �����̵��� ����

    [SerializeField] Vector3 diffPos;

    [SerializeField] public int horizontals;
    [SerializeField] public int verticals;

    [Header("���� ����")]
    [SerializeField] float speed = 5f;//�����̵��ӵ�
    float Maxspeed;

    [Header(" �÷��̾� ��ġ")]
    [SerializeField] float xplayer;
    [SerializeField] float yplayer;
    bool hitpush;

    //�� �浹 ���� ����
    [SerializeField]bool horizontalwall;
    [SerializeField]bool verticalwall;
    bool onetouchpos;//�ѹ��� ��ġ Ȯ��
    [SerializeField]float updowncheck;//�� �Ʒ�üũ
    [SerializeField]float rightleftcheck;//�¿�üũ
    
    //�������


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Weaponcheck weapon = collision.gameObject.GetComponent<Weaponcheck>();//���� gameobject�� ���� collision�� ������ 
        //Player player = collision.gameObject.GetComponent<Player>();

        if (collision.CompareTag("weapon"))//null ���¿����� ������ �ȵ�
        {
            beatendamage = true;
            //if(GameManager.Instance.Weaponcheck.magic == true)//���࿡ ������ ��������
            //{
            //    magicchek = true;
            //}
            //else if(GameManager.Instance.Weaponcheck.Counter == true && GameManager.Instance.Weaponcheck.punch == true)
            //{
            //    pushdamage = true;
            //}

            if(weapon.magic == true)
            {
                hitmagicchek = true;
                //stopnmagiccheck = true;
            }
            else if(weapon.Counter == true && weapon.punch == true)
            {
                pushdamage = true;
            }
        }

        #region
        //if(collision.gameObject.tag == "Player")
        //{
        //    Oncheckdamage = true;
        //}
        //else
        //{
        //    Oncheckdamage = false;
        //}
        #endregion

        //if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        //{
        //    if (debug == true)
        //    {
        //    }

        //    wallcheck = true;
        //}
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        //if(hitmagicchek == true)//���� �Ѹ����� ���̸� ��� �������� ������ ������ ����
        //{
        //    //if(stopnmagiccheck == true && hitmagicchek == false)
        //    //{
        //    //    return;
        //    //}
        //    //else
        //    //{
        //    //    hitmagicchek = false;
        //    //    stopnmagiccheck = false;
        //    //}
        //    hitmagicchek = false;
        //}
        //else if(pushdamage == true)
        //{
        //    pushdamage = false;
        //}

        if(collision.CompareTag("weapon"))
        {
            hitmagicchek = false;
        }
        else if (pushdamage == true)
        {
            pushdamage = false;
        }

        //if(collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        //{
        //    wallcheck = false;
        //}
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
        slowspeed();
        puchcheck();
        spawnmonstercheck();

        updownChaseWallplayer();
        rightleftChaseWallplayer();
        exitwall();
        //movetest();
        //wallroad();
        //Anim();

        //���� ��Ʈ ��ũ��Ʈ
        Hitboxmonster();
        destroymonster();
        destroymagic();

        pushmonster();
        Timer();

    }

    private void Hitboxmonster()//�Ϲ����� ������ ���
    {
        if(beatendamage == true && hitmagicchek == false)
        {
            weapondamage = GameManager.Instance.Weaponcheck.AttackdamageMax;
            MsMaxHp -= weapondamage;
        }
        monsterHpbar.fillAmount = MsMaxHp / MsGameHp;
    }

    private void destroymonster()
    {
        if(MsMaxHp <= 0)
        {
            if(Random.Range(0,3) > 1)//���Ͱ� ��������� ����Ȯ���� ������ ����Ʈ����
            {

            }
            //stopnmagiccheck = false;
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

        if(hitmagicchek == true)
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
            monsterHpbar.fillAmount = MsMaxHp / MsGameHp;
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

    public void playerchasecheck()
    {
        ChasePlayered = true;
    }

    private void spawnmonstercheck()
    {
        if (GameManager.Instance.HitboxMonster == true && fildmonster == false)
        {
            autoChasePlayered = true;
        }
    }


    public void playerchase()//�÷��̾ �Ѿư��� �ڵ�
    {
        if (ChasePlayered == true || autoChasePlayered == true)
        {
            if (GameManager.Instance.Player.destroyplayer == true || wallcheck == true)
            {
                return;
            }
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

            //bool ChaseHorizontal = false;
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
                if (diffPos.x > 0)//������
                {
                    dir.x = 1;
                    horizontals = 1;
                    verticals = 0;
                }
                else//����
                {
                    dir.x = -1;
                    horizontals = -1;
                    verticals = 0;
                }
                #region 
                //if(dir.x >  0)
                //{
                //    horizontals = 1;
                //    verticals = 0;
                //}
                //else if(dir.x < 0)
                //{
                //    horizontals = -1f;
                //    verticals = 0;
                //}
                #endregion

            }
            else if(ChaseHorizontal == false)//x��ǥ�� ���η� �� ���
            {
                dir.y = diffPos.y > 0 ? 1 : -1;
                if (dir.y > 0)//����
                {
                    dir.y = 1;
                    verticals = 1;
                    horizontals = 0;
                }
                else if (dir.y < 0)//�Ʒ���
                {
                    dir.y = -1;
                    verticals = -1;
                    horizontals = 0;
                }
                #region
                //dir.y = diffPos.y > 0 ? 1 : -1;
                //if (dir.y > 0)//����
                //{
                //    verticals = 1;
                //    horizontals = 0;
                //}
                //else if (dir.y < 0)//�Ʒ���
                //{
                //    verticals = -1;
                //    horizontals = 0;
                //}
                #endregion
            }
            MonsterMoving monstermoving = parents.GetComponent<MonsterMoving>();//�θ��ִ� ���� ������Ʈ���ִ� MonsterMoving�� �����´�
            monstermoving.Anim(horizontals, verticals);
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

    #region
    private void Anim()//�̵� �ִϸ��̼� �ڵ�
    {
        //if(horizontals == 0 && verticals == 0)
        //{
        //    return;
        //}
        //animator.SetFloat("Horizontal",(float)horizontals);
        //animator.SetFloat("Vertical",(float) verticals);
        
    }
    #endregion


    private void slowspeed()
    {
        if (hitmagicchek == true&& Bosscheck == false)//���Ͱ� �������ݿ� ������
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
        else if (hitmagicchek == false && pushed == false)
        {
            Maxspeed = speed;
        }
    }

    private void movetest()
    {
        if(wallcheck == true)
        {
            Vector3 pos = GameManager.Instance.Player.transform.position;

            if (wallcheck == true)//���࿡ ���� ����� ���
            {
                if (transform.position.x < pos.x)//�÷��̾ ������ �����ʿ� �ִ� ���
                {
                    wallhorizontal = true;
                }
                else if (transform.position.x > pos.x)
                {
                    wallhorizontal = true;
                }

                if (transform.position.y < pos.y)//�÷��̾ ���ͺ��� �����ִ°��
                {
                    wallvertical = true;
                }
                else if (transform.position.y > pos.y)
                {
                    wallvertical = true;
                }


            }
        }

    }

    private void wallroad()
    {
        if (wallcheck == false)
        {
            wallcheck1 = false;
            wallcheck2 = false;
            return;
        }
        #region
        //Vector3 pos = GameManager.Instance.Player.transform.position;
        //Vector3 dir = Vector3.zero;
        //if (wallhorizontal == true && wallcheck1 == false && wallcheck2 == false)//�� �Ǵ� �Ʒ��� �̵�
        //{
        //    if (pos.y < transform.position.y)//�÷��̾� y��ǥ�� ���� ���� ���ٸ�
        //    {
        //        dir.y = -1;
        //        verticals = -1;
        //        horizontals = 0;
        //        wallcheck1 = true;
        //        wallcheck2 = false;
        //    }
        //    else if(pos.y > transform.position.y)//�÷��̾� y��ǥ�� ���� ���� ���ٸ�
        //    {
        //        dir.y = 1;
        //        verticals = 1;
        //        horizontals = 0;
        //        wallcheck2 = true;
        //        wallcheck1 = false;
        //    }
        //}
        //else if(wallvertical == true && wallcheck1 == false && wallcheck2 == false)
        //{
        //    if(pos.x < transform.position.x)
        //    {
        //        dir.x = -1;
        //        horizontals = -1;
        //        verticals = 0;
        //        wallcheck1 = true;
        //        wallcheck2 = false;
        //    }
        //    else if(pos.x > transform.position.x)
        //    {
        //        dir.x = 1;
        //        horizontals = 1;
        //        verticals = 0;
        //        wallcheck2 = true;
        //        wallcheck1 = false;
        //    }
        //}
        //if(wallcheck1 == true)
        //{
        //    if(wallhorizontal == true)
        //    {
        //        dir.y = -1;
        //    }
        //    else if(wallvertical == true)
        //    {
        //        dir.y = 1;//
        //    }
        //}
        //else if(wallcheck2 == true)
        //{
        //    if (wallhorizontal == true)
        //    {
        //        dir.x = -1;
        //    }
        //    else if (wallvertical == true)
        //    {
        //        dir.x = 1;//
        //    }
        //}
#endregion

        //MonsterMoving monstermoving = parents.GetComponent<MonsterMoving>();//�θ��ִ� ���� ������Ʈ���ִ� MonsterMoving�� �����´�
        //monstermoving.Anim(horizontals, verticals);
        //parents.transform.position += Maxspeed * Time.deltaTime * dir;
        
    }

    public void walltest(bool _horizontalwallcheck)
    {
        horizontalwall = _horizontalwallcheck;//�¿� �κ��� ������� �� �Ѿư��� ���ؼ� �� �Ʒ��� �̵�
        onetouchpos = true;
        Debug.Log("1");
    }

    public void walltest2(bool _verticalwallcheck)
    {
        verticalwall = _verticalwallcheck;//���� �κ��� ������� �� �¿�� �Ѿ� �������� ����
        onetouchpos = true;
        Debug.Log("2");
    }

    public void wallcheckfind(bool _wallcheckfind)
    {
        wallcheck = _wallcheckfind;
    }

    private void updownChaseWallplayer()//���� ���� �÷��̾ �̵� ��Ű�� �κ�
    {
        //if(horizontalwall == false) { return; }
        if(horizontalwall == true && verticalwall == false && wallcheck == true)
        {
            Vector3 dir = Vector3.zero;
            if (onetouchpos == true)
            {
                Vector3 pos = GameManager.Instance.Player.transform.position;
                //Vector3 dir = Vector3.zero;
                if (pos.y < transform.position.y)//�÷��̾� y��ǥ�� ���� ���� ���ٸ�
                {
                    updowncheck = 1;//�Ʒ��� ���Ҷ�
                    dir.y = -1;
                    verticals = -1;
                    horizontals = 0;
                }
                else if (pos.y > transform.position.y)//�÷��̾� y��ǥ�� ���� ���� ���ٸ�
                {
                    updowncheck = 2;//���� ���Ҷ�
                    dir.y = 1;
                    verticals = 1;
                    horizontals = 0;
                }
                onetouchpos = false;
            }
            if(updowncheck == 1)//�Ʒ��� �����ϴ� ���
            {
                //Vector3 dir = Vector3.zero;
                dir.y = -1;
                verticals = -1;
                horizontals = 0;
            }
            else if(updowncheck == 2)//���� �����ϴ� ���
            {
                //Vector3 dir = Vector3.zero;
                dir.y = 1;
                verticals = 1;
                horizontals = 0;
            }


            MonsterMoving monstermoving = parents.GetComponent<MonsterMoving>();//�θ��ִ� ���� ������Ʈ���ִ� MonsterMoving�� �����´�
            monstermoving.Anim(horizontals, verticals);
            parents.transform.position += Maxspeed * Time.deltaTime * dir;
        }
    }

    private void rightleftChaseWallplayer()//���� ���� �÷��̾ �̵� ��Ű�� �κ�
    {
        //if(horizontalwall == false) { return; }
        if (horizontalwall == false && verticalwall == true && wallcheck == true)
        {
            Vector3 dir = Vector3.zero;
            if (onetouchpos == true)
            {
                Vector3 pos = GameManager.Instance.Player.transform.position;
                //Vector3 dir = Vector3.zero;
                if (pos.x < transform.position.x)
                {
                    rightleftcheck = 1;//�������� �� ���
                    dir.x = -1;
                    horizontals = -1;
                    verticals = 0;
                }
                else if (pos.x > transform.position.x)
                {
                    rightleftcheck = 2;//���������� �� ���
                    dir.x = 1;
                    horizontals = 1;
                    verticals = 0;
                }
                onetouchpos = false;
            }
            if (rightleftcheck == 1)//�Ʒ��� �����ϴ� ���
            {
                //Vector3 dir = Vector3.zero;
                dir.x = -1;
                horizontals = -1;
                verticals = 0;
            }
            else if (rightleftcheck == 2)//���� �����ϴ� ���
            {
                //Vector3 dir = Vector3.zero;
                dir.x = 1;
                horizontals = 1;
                verticals = 0;
            }
            MonsterMoving monstermoving = parents.GetComponent<MonsterMoving>();//�θ��ִ� ���� ������Ʈ���ִ� MonsterMoving�� �����´�
            monstermoving.Anim(horizontals, verticals);
            parents.transform.position += Maxspeed * Time.deltaTime * dir;
        }
    }


    private void exitwall()
    {
        if (horizontalwall == false && verticalwall == false && wallcheck == true)
        {
            
            Vector3 dir = Vector3.zero;

            if (rightleftcheck == 1)//�Ʒ��� �����ϴ� ���
            {
                //Vector3 dir = Vector3.zero;
                dir.x = -1;
                horizontals = -1;
                verticals = 0;
                updowncheck = 0;
            }
            else if (rightleftcheck == 2)//���� �����ϴ� ���
            {
                //Vector3 dir = Vector3.zero;
                dir.x = 1;
                horizontals = 1;
                verticals = 0;
                updowncheck = 0;
            }
            if (updowncheck == 1)//�Ʒ��� �����ϴ� ���
            {
                //Vector3 dir = Vector3.zero;
                dir.y = -1;
                verticals = -1;
                horizontals = 0;
                rightleftcheck = 0;
            }
            else if (updowncheck == 2)//���� �����ϴ� ���
            {
                //Vector3 dir = Vector3.zero;
                dir.y = 1;
                verticals = 1;
                horizontals = 0;
                rightleftcheck = 0;
            }

            MonsterMoving monstermoving = parents.GetComponent<MonsterMoving>();//�θ��ִ� ���� ������Ʈ���ִ� MonsterMoving�� �����´�
            monstermoving.Anim(horizontals, verticals);
            parents.transform.position += Maxspeed * Time.deltaTime * dir;
        }
    }
}
