using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MonsterMoving : MonoBehaviour
{
    Animator animator;

    [Header("�Ѿư������� ����")]
    [SerializeField] private bool ChasePlayer = false;
    [SerializeField] private bool ChaseX = false;
    [SerializeField] private bool ChaseY = false;
    [SerializeField] private float posX;//�÷��̾���ġ - ������ġ ��.X
    [SerializeField] private float posY;//�÷��̾���ġ - ������ġ ��.Y

    [SerializeField] float horizontals;
    [SerializeField] float verticals;

    [Header("���� ����")]
    [SerializeField] float speed = 5f;//�����̵��ӵ�

    [Header(" �÷��̾� ��ġ")]
    [SerializeField] float xplayer;
    [SerializeField] float yplayer;

    private void Awake()
    {
        animator = GetComponent<Animator>(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if(player)
        {
            ChasePlayer = true;
        }
        Debug.Log("�߰�");
    }

    void Start()
    {
        
    }


    void Update()
    {
        playerchase();
        Anim();
    }

    private void playerchase()//�÷��̾ �Ѿư��� �ڵ�
    {
        if(ChasePlayer == true)
        {
            //1.�÷��̾��� ��ġ�� �ǽð����� Ȯ��
            //2.�÷����� ��ġ - �ڽ��� ��ġ�� �Ͽ� �̵�
            //3.�밢�� ���� -> �켱����... -> horizon�̳� vertical�� ���ڰ� 0�� �� ����� ������ �̵�
            //����1: �÷��̾��� ��ġ�� �޴¹��� �𸥴�
            //����2:���Ͱ� �ڵ����� �̵��ϱ����� ��� �� �ִϸ��̼� ������<- Player��ũ��Ʈ���� ������� ���� ����

            Vector3 pos = GameManager.Instance.Player.transform.position;//GameManager���� �÷��̾��� ��ġ�� ���� ���� �ڵ�

            if (pos.x < transform.position.x && ChaseX == true)//���Ͱ� �÷��̾��� �����ʿ� ������� �׸��� ChaseX�� true�� ���(�밢�� ����)
            {
                ChaseY = false;
                transform.position += new Vector3(-transform.position.x + 1 * speed, 0, 0) * Time.deltaTime;//������ �ڱ� ��ġ���� Vector3�� x ���� -1��ŭ �̵���Ű�� �ڵ�
            }
            else if(pos.x > transform.position.x && ChaseX == true)
            {
                ChaseY = false;
                transform.position += new Vector3(transform.position.x + 1 * speed, 0, 0) * Time.deltaTime;
            }

            if(pos.y < transform.position.y && ChaseY == true)
            {
                ChaseX = false;
                transform.position += new Vector3(0, transform.position.y - 1 * speed, 0) * Time.deltaTime;
            }
            else if(pos.y > transform.position.y && ChaseY == true)
            {
                ChaseX = false;
                transform.position += new Vector3(0, transform.position.x + 1 * speed, 0) * Time.deltaTime;
            }

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
}
