using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMoving : MonoBehaviour
{
    [Header("쫓아가기위한 정보")]
    [SerializeField] private bool ChasePlayer = false;

    [SerializeField] float horizontals;
    [SerializeField] float verticals;

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

            //GameManager.Instance.Player track


        }
    }
}
