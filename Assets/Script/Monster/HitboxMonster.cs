using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitboxMonster : MonoBehaviour
{
    [SerializeField] GameObject parents;
    [SerializeField] Image monsterHpbar;

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
    [SerializeField] public bool hitmagicchek = false;
    //[SerializeField] bool stopnmagiccheck = false;
    float retime = 0.5f;
    float Maxretime;
    [SerializeField] bool ChaseHorizontal = false;

    [Header("몬스터 이동 스크립트 모음")]

    Animator animator;

    [Header("쫓아가기위한 정보")]
    [SerializeField] bool fildmonster;//필드에 있는 몬스터는 자동으로 못 쫓아가게 만든 코드
    [SerializeField] public bool ChasePlayered = false;
    [SerializeField] public bool autoChasePlayered = false;
    //[SerializeField] private bool ChaseX = false;
    //[SerializeField] private bool ChaseY = false;
    [SerializeField] private float posX;//플레이어위치 + 몬스터위치 값.X
    [SerializeField] private float posY;//플레이어위치 + 몬스터위치 값.Y
    [SerializeField]bool wallcheck;//벽에 닿았는지 안 닿았는지 확인하는 코드
    [SerializeField] bool debug = false;
    bool wallhorizontal;
    bool wallcheck1;
    bool wallcheck2;
    bool wallvertical;
    float wallmove;//벽에 한방향으로만 움직이도록 제작

    [SerializeField] Vector3 diffPos;

    [SerializeField] public int horizontals;
    [SerializeField] public int verticals;

    [Header("몬스터 스팩")]
    [SerializeField] float speed = 5f;//몬스터이동속도
    float Maxspeed;

    [Header(" 플레이어 위치")]
    [SerializeField] float xplayer;
    [SerializeField] float yplayer;
    bool hitpush;

    //벽 충돌 관련 모음
    [SerializeField]bool horizontalwall;
    [SerializeField]bool verticalwall;
    bool onetouchpos;//한번만 위치 확인
    [SerializeField]float updowncheck;//위 아래체크
    [SerializeField]float rightleftcheck;//좌우체크
    
    //여기까지


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Weaponcheck weapon = collision.gameObject.GetComponent<Weaponcheck>();//무기 gameobject가 몬스터 collision에 닿을때 
        //Player player = collision.gameObject.GetComponent<Player>();

        if (collision.CompareTag("weapon"))//null 상태에서는 적용이 안됨
        {
            beatendamage = true;
            //if(GameManager.Instance.Weaponcheck.magic == true)//만약에 마법에 닿았을경우
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
        //if(hitmagicchek == true)//현재 한마리를 죽이면 모든 몬스터한테 꺼지는 현상이 있음
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
        slowspeed();
        puchcheck();
        spawnmonstercheck();

        updownChaseWallplayer();
        rightleftChaseWallplayer();
        exitwall();
        //movetest();
        //wallroad();
        //Anim();

        //몬스터 히트 스크립트
        Hitboxmonster();
        destroymonster();
        destroymagic();

        pushmonster();
        Timer();

    }

    private void Hitboxmonster()//일반적인 공격일 경우
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
            if(Random.Range(0,3) > 1)//몬스터가 사망했을때 일정확률로 아이템 떨어트리기
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

    private void destroymagic()//마법 공격이 닿았을 경우
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


    public void playerchase()//플레이어를 쫓아가는 코드
    {
        if (ChasePlayered == true || autoChasePlayered == true)
        {
            if (GameManager.Instance.Player.destroyplayer == true || wallcheck == true)
            {
                return;
            }
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

            //bool ChaseHorizontal = false;
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
                if (diffPos.x > 0)//오른쪽
                {
                    dir.x = 1;
                    horizontals = 1;
                    verticals = 0;
                }
                else//왼쪽
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
            else if(ChaseHorizontal == false)//x좌표가 세로로 될 경우
            {
                dir.y = diffPos.y > 0 ? 1 : -1;
                if (dir.y > 0)//위로
                {
                    dir.y = 1;
                    verticals = 1;
                    horizontals = 0;
                }
                else if (dir.y < 0)//아래로
                {
                    dir.y = -1;
                    verticals = -1;
                    horizontals = 0;
                }
                #region
                //dir.y = diffPos.y > 0 ? 1 : -1;
                //if (dir.y > 0)//위로
                //{
                //    verticals = 1;
                //    horizontals = 0;
                //}
                //else if (dir.y < 0)//아래로
                //{
                //    verticals = -1;
                //    horizontals = 0;
                //}
                #endregion
            }
            MonsterMoving monstermoving = parents.GetComponent<MonsterMoving>();//부모에있느 게임 오브젝트에있는 MonsterMoving을 가져온다
            monstermoving.Anim(horizontals, verticals);
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

    #region
    private void Anim()//이동 애니메이션 코드
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
        if (hitmagicchek == true&& Bosscheck == false)//몬스터가 마법공격에 닿을때
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

            if (wallcheck == true)//만약에 벽에 닿았을 경우
            {
                if (transform.position.x < pos.x)//플레이어가 몬스터의 오른쪽에 있는 경우
                {
                    wallhorizontal = true;
                }
                else if (transform.position.x > pos.x)
                {
                    wallhorizontal = true;
                }

                if (transform.position.y < pos.y)//플레이어가 몬스터보다 위에있는경우
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
        //if (wallhorizontal == true && wallcheck1 == false && wallcheck2 == false)//위 또는 아래로 이동
        //{
        //    if (pos.y < transform.position.y)//플레이어 y좌표가 몬스터 보다 낮다면
        //    {
        //        dir.y = -1;
        //        verticals = -1;
        //        horizontals = 0;
        //        wallcheck1 = true;
        //        wallcheck2 = false;
        //    }
        //    else if(pos.y > transform.position.y)//플레이어 y좌표가 몬스터 보다 높다면
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

        //MonsterMoving monstermoving = parents.GetComponent<MonsterMoving>();//부모에있느 게임 오브젝트에있는 MonsterMoving을 가져온다
        //monstermoving.Anim(horizontals, verticals);
        //parents.transform.position += Maxspeed * Time.deltaTime * dir;
        
    }

    public void walltest(bool _horizontalwallcheck)
    {
        horizontalwall = _horizontalwallcheck;//좌우 부분이 닿았을때 즉 쫓아가기 위해서 위 아래로 이동
        onetouchpos = true;
        Debug.Log("1");
    }

    public void walltest2(bool _verticalwallcheck)
    {
        verticalwall = _verticalwallcheck;//상하 부분이 닿았을때 즉 좌우로 쫓아 가기위해 제작
        onetouchpos = true;
        Debug.Log("2");
    }

    public void wallcheckfind(bool _wallcheckfind)
    {
        wallcheck = _wallcheckfind;
    }

    private void updownChaseWallplayer()//벽에 따라 플레이어를 이동 시키는 부분
    {
        //if(horizontalwall == false) { return; }
        if(horizontalwall == true && verticalwall == false && wallcheck == true)
        {
            Vector3 dir = Vector3.zero;
            if (onetouchpos == true)
            {
                Vector3 pos = GameManager.Instance.Player.transform.position;
                //Vector3 dir = Vector3.zero;
                if (pos.y < transform.position.y)//플레이어 y좌표가 몬스터 보다 낮다면
                {
                    updowncheck = 1;//아래로 향할때
                    dir.y = -1;
                    verticals = -1;
                    horizontals = 0;
                }
                else if (pos.y > transform.position.y)//플레이어 y좌표가 몬스터 보다 높다면
                {
                    updowncheck = 2;//위로 향할때
                    dir.y = 1;
                    verticals = 1;
                    horizontals = 0;
                }
                onetouchpos = false;
            }
            if(updowncheck == 1)//아래로 추적하는 경우
            {
                //Vector3 dir = Vector3.zero;
                dir.y = -1;
                verticals = -1;
                horizontals = 0;
            }
            else if(updowncheck == 2)//위로 추적하는 경우
            {
                //Vector3 dir = Vector3.zero;
                dir.y = 1;
                verticals = 1;
                horizontals = 0;
            }


            MonsterMoving monstermoving = parents.GetComponent<MonsterMoving>();//부모에있느 게임 오브젝트에있는 MonsterMoving을 가져온다
            monstermoving.Anim(horizontals, verticals);
            parents.transform.position += Maxspeed * Time.deltaTime * dir;
        }
    }

    private void rightleftChaseWallplayer()//벽에 따라 플레이어를 이동 시키는 부분
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
                    rightleftcheck = 1;//왼쪽으로 갈 경우
                    dir.x = -1;
                    horizontals = -1;
                    verticals = 0;
                }
                else if (pos.x > transform.position.x)
                {
                    rightleftcheck = 2;//오른쪽으로 갈 경우
                    dir.x = 1;
                    horizontals = 1;
                    verticals = 0;
                }
                onetouchpos = false;
            }
            if (rightleftcheck == 1)//아래로 추적하는 경우
            {
                //Vector3 dir = Vector3.zero;
                dir.x = -1;
                horizontals = -1;
                verticals = 0;
            }
            else if (rightleftcheck == 2)//위로 추적하는 경우
            {
                //Vector3 dir = Vector3.zero;
                dir.x = 1;
                horizontals = 1;
                verticals = 0;
            }
            MonsterMoving monstermoving = parents.GetComponent<MonsterMoving>();//부모에있느 게임 오브젝트에있는 MonsterMoving을 가져온다
            monstermoving.Anim(horizontals, verticals);
            parents.transform.position += Maxspeed * Time.deltaTime * dir;
        }
    }


    private void exitwall()
    {
        if (horizontalwall == false && verticalwall == false && wallcheck == true)
        {
            
            Vector3 dir = Vector3.zero;

            if (rightleftcheck == 1)//아래로 추적하는 경우
            {
                //Vector3 dir = Vector3.zero;
                dir.x = -1;
                horizontals = -1;
                verticals = 0;
                updowncheck = 0;
            }
            else if (rightleftcheck == 2)//위로 추적하는 경우
            {
                //Vector3 dir = Vector3.zero;
                dir.x = 1;
                horizontals = 1;
                verticals = 0;
                updowncheck = 0;
            }
            if (updowncheck == 1)//아래로 추적하는 경우
            {
                //Vector3 dir = Vector3.zero;
                dir.y = -1;
                verticals = -1;
                horizontals = 0;
                rightleftcheck = 0;
            }
            else if (updowncheck == 2)//위로 추적하는 경우
            {
                //Vector3 dir = Vector3.zero;
                dir.y = 1;
                verticals = 1;
                horizontals = 0;
                rightleftcheck = 0;
            }

            MonsterMoving monstermoving = parents.GetComponent<MonsterMoving>();//부모에있느 게임 오브젝트에있는 MonsterMoving을 가져온다
            monstermoving.Anim(horizontals, verticals);
            parents.transform.position += Maxspeed * Time.deltaTime * dir;
        }
    }
}
