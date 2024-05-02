using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMoving : MonoBehaviour
{
    Animator animator;

    [Header("�Ѿư������� ����")]
    [SerializeField] public bool ChasePlayer = false;
    public float ttest;
    [SerializeField] bool Bosscheck;//�������� Ȯ��
    //[SerializeField] private bool ChaseX = false;
    //[SerializeField] private bool ChaseY = false;
    //[SerializeField] private float posX;//�÷��̾���ġ + ������ġ ��.X
    //[SerializeField] private float posY;//�÷��̾���ġ + ������ġ ��.Y

    //[SerializeField] Vector3 diffPos;

    [SerializeField] int horizontals;
    [SerializeField] int verticals;

    //[Header("���� ����")]
    //[SerializeField] float speed = 5f;//�����̵��ӵ�
    //float Maxspeed;

    //[Header(" �÷��̾� ��ġ")]
    //[SerializeField] float xplayer;
    //[SerializeField] float yplayer;
    //bool hitpush;

    private void Awake()
    {
        //Maxspeed = speed;
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if(player)
        {
            ChasePlayer = true;
            HitboxMonster hitboxmonster = transform.GetChild(0).GetComponent<HitboxMonster>();
            hitboxmonster.playerchasecheck();
        }
    }

    private void Start()
    {
        GameManager.Instance.MonsterMoving = this;
    }

    void Update()
    {
        //playerchase();
        PlayAnim();
        //slowspeed();
        //puchcheck();
        //test();
    }

    //private void playerchase()//�÷��̾ �Ѿư��� �ڵ�
    //{
    //    if(ChasePlayer == true)
    //    {
    //        //1.�÷��̾��� ��ġ�� �ǽð����� Ȯ��
    //        //2.�÷����� ��ġ - �ڽ��� ��ġ�� �Ͽ� �̵�
    //        //3.�밢�� ���� -> �켱����... -> horizon�̳� vertical�� ���ڰ� 0�� �� ����� ������ �̵�
    //        //����1: �÷��̾��� ��ġ�� �޴¹��� �𸥴�-�ذ�
    //        //����2:���Ͱ� �ڵ����� �̵��ϱ����� ��� �� (�ִϸ��̼� ������- �ذ�ȵ�) <- Player��ũ��Ʈ���� ������� ���� ����-�ذ�
    //        //����3: �������׷� �밢������ �̵��� -> �ذ�
    //        //����4: ������ �̼��� ���� ������

    //        Vector3 pos = GameManager.Instance.Player.transform.position;//GameManager���� �÷��̾��� ��ġ�� ���� ���� �ڵ�

    //        //�÷��̾� + ������ġ x��
    //        //�÷��̾� + ������ġ y��
    //        posX = pos.x - transform.position.x;
    //        posY = pos.y - transform.position.y;

    //        diffPos = pos - transform.position;

    //        bool ChaseHorizontal = false;
    //        // x,y��ǥ�� ���밪���� ���
    //        if (Mathf.Abs(posX) > 0.02)//x��ǥ�� �� Ŭ���,Mathf.Abs() <-���밪���� ��ȯ�ϴ� �ڵ�
    //        {
    //            ChaseHorizontal = true;
    //        }
    //        else if (Mathf.Abs(posX) <= 0.02)//x��ǥ�� 0�� �� ��� �� �¿�� ���� �̵� �� ���η� �̵�
    //        {
    //            ChaseHorizontal = false;
    //        }

    //        //bool ChaseHorizontal = Mathf.Abs(diffPos.x) < Mathf.Abs(diffPos.y);

    //        Vector3 dir = Vector3.zero;
    //        if (ChaseHorizontal == true)//�� ���
    //        {
    //            dir.x = diffPos.x > 0 ? 1 : -1;
    //        }
    //        else
    //        {
    //            dir.y = diffPos.y > 0 ? 1 : -1; 
    //        }

    //        transform.position += Maxspeed * Time.deltaTime * dir;

    //        #region �����ڵ�
    //        //x,y��ǥ�� ���밪���� ���
    //        //if (Mathf.Abs(posX) > 0.02)//x��ǥ�� �� Ŭ���,Mathf.Abs() <-���밪���� ��ȯ�ϴ� �ڵ�
    //        //{
    //        //    ChaseX = true;
    //        //    ChaseY = false;
    //        //}
    //        //else if(Mathf.Abs(posX) <= 0.02)//x��ǥ�� 0�� �� ��� �� �¿�� ���� �̵� �� ���η� �̵�
    //        //{
    //        //    ChaseX = false;
    //        //    ChaseY = true;
    //        //}

    //        //if (pos.x < transform.position.x && ChaseX == true)//���Ͱ� �÷��̾��� �����ʿ� ������� �׸��� ChaseX�� true�� ���(�밢�� ����)
    //        //{
    //        //    ChaseY = false;
    //        //    transform.position += new Vector3(-transform.position.x + 1 * speed, 0, 0) * Time.deltaTime;//������ �ڱ� ��ġ���� Vector3�� x ���� -1��ŭ �̵���Ű�� �ڵ�
    //        //}
    //        //else if(pos.x > transform.position.x && ChaseX == true)
    //        //{
    //        //    ChaseY = false;
    //        //    transform.position += new Vector3(transform.position.x + 1 * speed, 0, 0) * Time.deltaTime;
    //        //}

    //        //if(pos.y < transform.position.y && ChaseY == true)
    //        //{
    //        //    ChaseX = false;
    //        //    transform.position += new Vector3(0, transform.position.y - 1 * speed, 0) * Time.deltaTime;
    //        //}
    //        //else if(pos.y > transform.position.y && ChaseY == true)
    //        //{
    //        //    ChaseX = false;
    //        //    transform.position += new Vector3(0, transform.position.x + 1 * speed, 0) * Time.deltaTime;
    //        //}
    //        #endregion
    //    }
    //}


    public void Anim(int _horizontals, int _verticals)//�̵� �ִϸ��̼� �ڵ�
    {
        horizontals = _horizontals;
        verticals = _verticals;
    }

    public void PlayAnim()//�̵� �ִϸ��̼� �ڵ�
    {
        if(Bosscheck == false)
        {
            #region �ڵ� �Ⱥ��̰� ����
            //if (horizontals == 0 && verticals == 0)
            //{
            //    return;
            //}
            //if (horizontals == 0 && verticals == 0)
            //{
            //    animator.SetInteger("Horizontal", (int)horizontals);
            //    animator.SetInteger("Vertical", (int)verticals);
            //}
            #endregion
            if (horizontals < 0 )
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            animator.SetInteger("Horizontal", (int)horizontals);
            animator.SetInteger("Vertical", (int)verticals);
        }

        }

    //private void slowspeed()
    //{
    //    if(GameManager.Instance.HitboxMonster.magicchek == true)//���Ͱ� �������ݿ� ������
    //    {
    //        //Maxspeed -= 1;//�⺻ �̵��ӵ��� ���δ�
    //        //if(Maxspeed < speed - 1)
    //        //{
    //        //    Maxspeed = speed - 1;
    //        //}
    //        Maxspeed = 0;
    //    }
    //    else
    //    {
    //        //Maxspeed += 1;
    //        //if(Maxspeed > speed)
    //        //{
    //        //    Maxspeed = speed;
    //        //}

    //        Maxspeed = speed;
    //    }
    //}

    //private void puchcheck()
    //{
    //    if (GameManager.Instance.HitboxMonster.pushed == true)
    //    {
    //        Maxspeed = 0;
    //    }
    //    else if (GameManager.Instance.HitboxMonster.magicchek == false && GameManager.Instance.HitboxMonster.pushed == false)
    //    {
    //        Maxspeed = speed;
    //    }
    //}

    private void test()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
