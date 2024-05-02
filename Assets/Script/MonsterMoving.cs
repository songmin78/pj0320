using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMoving : MonoBehaviour
{
    Animator animator;

    [Header("쫓아가기위한 정보")]
    [SerializeField] public bool ChasePlayer = false;
    public float ttest;
    [SerializeField] bool Bosscheck;//보스인지 확인
    //[SerializeField] private bool ChaseX = false;
    //[SerializeField] private bool ChaseY = false;
    //[SerializeField] private float posX;//플레이어위치 + 몬스터위치 값.X
    //[SerializeField] private float posY;//플레이어위치 + 몬스터위치 값.Y

    //[SerializeField] Vector3 diffPos;

    [SerializeField] int horizontals;
    [SerializeField] int verticals;

    //[Header("몬스터 스팩")]
    //[SerializeField] float speed = 5f;//몬스터이동속도
    //float Maxspeed;

    //[Header(" 플레이어 위치")]
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

    //private void playerchase()//플레이어를 쫓아가는 코드
    //{
    //    if(ChasePlayer == true)
    //    {
    //        //1.플레이어의 위치를 실시간으로 확인
    //        //2.플레이의 위치 - 자신의 위치를 하여 이동
    //        //3.대각선 방지 -> 우선순위... -> horizon이나 vertical중 숫자가 0에 더 가까운 쪽으로 이동
    //        //문제1: 플레이어의 위치를 받는법을 모른다-해결
    //        //문제2:몬스터가 자동으로 이동하기위한 방법 및 (애니메이션 적용방법- 해결안됨) <- Player스크립트에서 어느정도 착안 가능-해결
    //        //문제3: 지그제그로 대각선으로 이동함 -> 해결
    //        //문제4: 몬스터의 이속이 점점 빨라짐

    //        Vector3 pos = GameManager.Instance.Player.transform.position;//GameManager에서 플레이어의 위치를 전달 받은 코드

    //        //플레이어 + 몬스터위치 x값
    //        //플레이어 + 몬스터위치 y값
    //        posX = pos.x - transform.position.x;
    //        posY = pos.y - transform.position.y;

    //        diffPos = pos - transform.position;

    //        bool ChaseHorizontal = false;
    //        // x,y좌표가 절대값으로 계산
    //        if (Mathf.Abs(posX) > 0.02)//x좌표가 더 클경우,Mathf.Abs() <-절대값으로 변환하는 코드
    //        {
    //            ChaseHorizontal = true;
    //        }
    //        else if (Mathf.Abs(posX) <= 0.02)//x좌표가 0이 될 경우 즉 좌우로 맞춰 이동 후 세로로 이동
    //        {
    //            ChaseHorizontal = false;
    //        }

    //        //bool ChaseHorizontal = Mathf.Abs(diffPos.x) < Mathf.Abs(diffPos.y);

    //        Vector3 dir = Vector3.zero;
    //        if (ChaseHorizontal == true)//좌 우로
    //        {
    //            dir.x = diffPos.x > 0 ? 1 : -1;
    //        }
    //        else
    //        {
    //            dir.y = diffPos.y > 0 ? 1 : -1; 
    //        }

    //        transform.position += Maxspeed * Time.deltaTime * dir;

    //        #region 이전코드
    //        //x,y좌표가 절대값으로 계산
    //        //if (Mathf.Abs(posX) > 0.02)//x좌표가 더 클경우,Mathf.Abs() <-절대값으로 변환하는 코드
    //        //{
    //        //    ChaseX = true;
    //        //    ChaseY = false;
    //        //}
    //        //else if(Mathf.Abs(posX) <= 0.02)//x좌표가 0이 될 경우 즉 좌우로 맞춰 이동 후 세로로 이동
    //        //{
    //        //    ChaseX = false;
    //        //    ChaseY = true;
    //        //}

    //        //if (pos.x < transform.position.x && ChaseX == true)//몬스터가 플레이어의 오른쪽에 있을경우 그리고 ChaseX가 true일 경우(대각선 방지)
    //        //{
    //        //    ChaseY = false;
    //        //    transform.position += new Vector3(-transform.position.x + 1 * speed, 0, 0) * Time.deltaTime;//몬스터의 자기 위치에서 Vector3의 x 값을 -1만큼 이동시키는 코드
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


    public void Anim(int _horizontals, int _verticals)//이동 애니메이션 코드
    {
        horizontals = _horizontals;
        verticals = _verticals;
    }

    public void PlayAnim()//이동 애니메이션 코드
    {
        if(Bosscheck == false)
        {
            #region 코드 안보이게 정리
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
    //    if(GameManager.Instance.HitboxMonster.magicchek == true)//몬스터가 마법공격에 닿을때
    //    {
    //        //Maxspeed -= 1;//기본 이동속도를 줄인다
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
