using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MonsterMoving : MonoBehaviour
{
    Animator animator;

    [Header("쫓아가기위한 정보")]
    [SerializeField] private bool ChasePlayer = false;

    [SerializeField] float horizontals;
    [SerializeField] float verticals;

    [Header("몬스터 스팩")]
    [SerializeField] float speed = 5f;//몬스터이동속도

    [Header(" 몬스터 위치")]
    [SerializeField] float MonsterpositionX;
    [SerializeField] float MonsterpositionY;
    [Header(" 플레이어 위치")]
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
        Debug.Log("추격");
    }

    void Start()
    {
        
    }


    void Update()
    {
        playerchase();
        Anim();
    }

    private void playerchase()//플레이어를 쫓아가는 코드
    {
        if(ChasePlayer == true)
        {
            //1.플레이어의 위치를 실시간으로 확인
            //2.플레이의 위치 - 자신의 위치를 하여 이동
            //3.대각선 방지 -> 우선순위... -> horizon이나 vertical중 숫자가 0에 더 가까운 쪽으로 이동
            //문제1: 플레이어의 위치를 받는법을 모른다
            //문제2:몬스터가 자동으로 이동하기위한 방법 및 애니메이션 적용방법<- Player스크립트에서 어느정도 착안 가능

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

    private void Anim()//이동 애니메이션 코드
    {
        animator.SetFloat("Horizontal", (float)horizontals);
        animator.SetFloat("Vertical", (float)verticals);

        if (horizontals < 0)
        {
            transform.localScale = new Vector3(horizontals, 1, 1);
        }
    }
}
