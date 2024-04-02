using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MonsterMoving : MonoBehaviour
{
    Animator animator;

    [Header("�Ѿư������� ����")]
    [SerializeField] private bool ChasePlayer = false;

    [SerializeField] float horizontals;
    [SerializeField] float verticals;

    [Header("���� ����")]
    [SerializeField] float speed = 5f;//�����̵��ӵ�

    [Header(" ���� ��ġ")]
    [SerializeField] float MonsterpositionX;
    [SerializeField] float MonsterpositionY;
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

            Vector3 pos = GameManager.Instance.Player.transform.position;
            Monsterposition();

            if (xplayer < MonsterpositionX)
            {
                horizontals = -1;
                MonsterpositionX -= 1;
            }
            else if(xplayer > MonsterpositionX)
            {
                horizontals = +1;
                MonsterpositionX += 1;
            }

            if(yplayer < MonsterpositionY)
            {
                verticals = -1;
                MonsterpositionY -= 1;
            }
            else if(yplayer > MonsterpositionY)
            {
                verticals = +1;
                MonsterpositionX += 1;
            }

        }
    }

    public void Monsterposition()
    {
        MonsterpositionX = transform.position.x;
        MonsterpositionY = transform.position.y;

        new Vector3(MonsterpositionX, MonsterpositionY, 0);
    }

    private void Anim()//�̵� �ִϸ��̼� �ڵ�
    {
        animator.SetFloat("Horizontal", (float)horizontals);
        animator.SetFloat("Vertical", (float)verticals);

        if (horizontals < 0)
        {
            transform.localScale = new Vector3(horizontals, 1, 1);
        }
    }
}
