using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class HitboxMonster : MonoBehaviour
{
    [SerializeField]GameObject parents;

    [Header("몬스터 스펙")]
    //[SerializeField] public float attackdamage;//몬스터의 데미지
    [SerializeField] float MsGameHp = 1;//몬스터의 HP
    private float MsMaxHp;
    [SerializeField] bool pushdamage;
    public bool pushed;
    float pushway;
    float pushroad = 0.2f;
    public bool waycheck;

    [Header("보스 몬스터 체크")]
    [SerializeField] bool Bosscheck;


    [Header("공격 여부")]
    bool beatendamage = false;
    //public bool Oncheckdamage = false;
    float weapondamage = 0;

    [Header("기타")]
    [SerializeField] public bool magicchek = false;
    float retime = 0.5f;
    float Maxretime;

    [Header("몬스터 이동 스크립트 모음")]

    Animator animator;

    [Header("쫓아가기위한 정보")]
    //[SerializeField] private bool ChasePlayer = false;
    [SerializeField] private bool ChaseX = false;
    [SerializeField] private bool ChaseY = false;
    [SerializeField] private float posX;//플레이어위치 + 몬스터위치 값.X
    [SerializeField] private float posY;//플레이어위치 + 몬스터위치 값.Y

    [SerializeField] Vector3 diffPos;

    [SerializeField] float horizontals;
    [SerializeField] float verticals;

    [Header("몬스터 스팩")]
    [SerializeField] float speed = 5f;//몬스터이동속도
    float Maxspeed;

    [Header(" 플레이어 위치")]
    [SerializeField] float xplayer;
    [SerializeField] float yplayer;
    bool hitpush;

    //여기까지


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Weaponcheck weapon = collision.gameObject.GetComponent<Weaponcheck>();//무기 gameobject가 몬스터 collision에 닿을때 
        Player player = collision.gameObject.GetComponent<Player>();

        if (weapon)
        {
            beatendamage = true;
            if(GameManager.Instance.Weaponcheck.magic == true)//만약에 마법에 닿았을경우
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


    //만약에 magic이 true일때 만나면 어떤값을 true 이후에 update에서 어떤값이 true일때 데미지를 입고 0.?초간 무적 이후에 또 피해를 입는다.


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
        //몬스터 이동 스크립트
        playerchase();
        Anim();
        slowspeed();
        puchcheck();

        //몬스터 히트 스크립트
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

    private void destroymagic()//마법 공격이 닿았을 경우
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
            if (pushway == 1 && waycheck == false)//위쪽 날리기
            {
                waycheck = true;
                pushed = true;
                vec3.y += 1.3f;
                parents.transform.position = vec3;
            }
            else if(pushway == 2 && waycheck == false)//왼쪽으로 날리기
            {
                waycheck = true;
                pushed = true;
                vec3.x -= 1.3f;
                parents.transform.position = vec3;
            }
            else if(pushway == 3 && waycheck == false)//오른쪽으로 날리기
            {
                waycheck = true;
                pushed = true;
                vec3.x += 1.3f;
                parents.transform.position = vec3;
            }
            else if (pushway == 4 && waycheck == false)//아래로 날리기
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


    private void playerchase()//플레이어를 쫓아가는 코드
    {
        if ( GameManager.Instance.MonsterMoving.ChasePlayer == true)
        {
            #region 메커니즘 설명
            //1.플레이어의 위치를 실시간으로 확인
            //2.플레이의 위치 - 자신의 위치를 하여 이동
            //3.대각선 방지 -> 우선순위... -> horizon이나 vertical중 숫자가 0에 더 가까운 쪽으로 이동
            //문제1: 플레이어의 위치를 받는법을 모른다-해결
            //문제2:몬스터가 자동으로 이동하기위한 방법 및 (애니메이션 적용방법- 해결안됨) <- Player스크립트에서 어느정도 착안 가능-해결
            //문제3: 지그제그로 대각선으로 이동함 -> 해결
            //문제4: 몬스터의 이속이 점점 빨라짐
            #endregion

            Vector3 pos = GameManager.Instance.Player.transform.position;//GameManager에서 플레이어의 위치를 전달 받은 코드

            //플레이어 + 몬스터위치 x값
            //플레이어 + 몬스터위치 y값
            posX = pos.x - transform.position.x;
            posY = pos.y - transform.position.y;

            diffPos = pos - transform.position;

            bool ChaseHorizontal = false;
            // x,y좌표가 절대값으로 계산
            if (Mathf.Abs(posX) > 0.02)//x좌표가 더 클경우,Mathf.Abs() <-절대값으로 변환하는 코드
            {
                ChaseHorizontal = true;
            }
            else if (Mathf.Abs(posX) <= 0.02)//x좌표가 0이 될 경우 즉 좌우로 맞춰 이동 후 세로로 이동
            {
                ChaseHorizontal = false;
            }

            //bool ChaseHorizontal = Mathf.Abs(diffPos.x) < Mathf.Abs(diffPos.y);

            Vector3 dir = Vector3.zero;
            if (ChaseHorizontal == true)//좌 우로
            {
                dir.x = diffPos.x > 0 ? 1 : -1;
            }
            else
            {
                dir.y = diffPos.y > 0 ? 1 : -1;
            }

            parents.transform.position += Maxspeed * Time.deltaTime * dir;

            #region 이전코드
            //x,y좌표가 절대값으로 계산
            //if (Mathf.Abs(posX) > 0.02)//x좌표가 더 클경우,Mathf.Abs() <-절대값으로 변환하는 코드
            //{
            //    ChaseX = true;
            //    ChaseY = false;
            //}
            //else if(Mathf.Abs(posX) <= 0.02)//x좌표가 0이 될 경우 즉 좌우로 맞춰 이동 후 세로로 이동
            //{
            //    ChaseX = false;
            //    ChaseY = true;
            //}

            //if (pos.x < transform.position.x && ChaseX == true)//몬스터가 플레이어의 오른쪽에 있을경우 그리고 ChaseX가 true일 경우(대각선 방지)
            //{
            //    ChaseY = false;
            //    transform.position += new Vector3(-transform.position.x + 1 * speed, 0, 0) * Time.deltaTime;//몬스터의 자기 위치에서 Vector3의 x 값을 -1만큼 이동시키는 코드
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

    private void Anim()//이동 애니메이션 코드
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
        if (magicchek == true)//몬스터가 마법공격에 닿을때
        {
            #region 이전코드
            //Maxspeed -= 1;//기본 이동속도를 줄인다
            //if(Maxspeed < speed - 1)
            //{
            //    Maxspeed = speed - 1;
            //}
            #endregion

            Maxspeed = 0;
        }
        else
        {
            #region 이전코드 쓸일없음
            //Maxspeed += 1;
            //if(Maxspeed > speed)
            //{
            //    Maxspeed = speed;
            //}
            #endregion

            Maxspeed = speed;
        }
    }

    private void puchcheck()//밀려 났을때
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
