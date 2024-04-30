using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Collider2D collision;
    [SerializeField]Camera mainCam;

    [Header("일반공격")]
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject sword;
    [SerializeField] GameObject magic;

    [Header("카운터 공격")]
    [SerializeField] GameObject ctArrow;
    [SerializeField] GameObject ctSword;
    [SerializeField] GameObject ctMagic;

    [Header("기타")]
    [SerializeField] float horizontals;
    [SerializeField] float verticals;
    public float MaxHP;
    float Checkchange = 0;//무기 회전 방향
    float Yeulerchange = 0;//무기 방향을 조정하기 0 <-> -180
    public float Horposition = 0f;//무기 공격위치
    public float Verposition = 0f;//무기 공격위치
    public int eulercheck =0;
    [SerializeField] bool slowcheck = false;
    public bool Monsterattackcheck;//몬스터가 때렸는지 안때렸는지 확인
    bool Bossattackcheck;//보스몬스터가 공격했는지 확인하는 코드
    public float Monsterdamage;//몬스터가 플레이어를 공격할때 데미지
    public float invincibilitytime = 1;//몬스터한테 공격받고 생기는 무적시간
    private float Maxinvincibilitytime;
    [SerializeField] public bool attackstandard;//공격을 할때 똑같은 무기가 안나오는걸 확해주는 것
    [SerializeField] bool ctattackstandard;//카운터무기 체크
    [SerializeField] float attacktimer;//무기 재 사용시간 설정
    public float Maxattacktimer;
    [SerializeField] float ctattacktimer;//카운터무기 재 사용시간 설정
    public float Maxctattacktimer;
    bool timerattack;
    bool cttimerattack;
    private float wayattack;//바라보고있는 방향 체크
    bool waytest;
    public bool movecheck;
    [SerializeField] GameObject bowweaponbox;
    [SerializeField] GameObject swordweaponbox;
    [SerializeField] GameObject magicweaponbox;
    [SerializeField] GameObject bowweapon;
    [SerializeField] GameObject ctounterbow;
    [SerializeField] GameObject gagecanvas;
    [SerializeField] BoxCollider2D roadmap;
    public bool destroyplayer;//플레이어가 죽을 경우

    Rigidbody2D rigid2D;

    [Header("카운터 기준")]
    [SerializeField] bool arrowcheck = false;
    [SerializeField] float Hitgauge = 0f;//근접 카운터 게이지 1이되면 카운터
    float Maxhitgage;
    [SerializeField] private bool countergagecheck = false;//게이지가 올라가는지 안가는지 확인
    [SerializeField] bool countertimercheck = false;//근접 L스킬을 쓸때 무한 카운터 공격
    [SerializeField] float counterHittimer = 3;//근접 L스킬을 쓰는 지속시간
    private float maxcountertimer;
    [SerializeField] float slowspeed = 3;//근접L스킬이 끝나고 생기는 슬로우 지속 시간
    private float Maxslowspeed;


    [Header("플레이어 정보")]
    [SerializeField] GameObject play;//플레이어블 캐릭터
    [SerializeField] float playerspeed = 5f;//플레이어가 이동하는 속도
    [SerializeField] public int Weapontype;//무기 리스트

    [Header("플레이어의 능력치 설정")]
    [SerializeField,Range(1,3)] int GameHP;//게임내 플레이어 체력

    [Header("무기 공격 관련 정보(마법)")]
    [SerializeField] bool magiccheck = false;//마법게이지가 닳는위한 확인
    public bool MagicCheck => magiccheck;

    [SerializeField] float magicgage = 5f;//공격 게이지 0이되면 사용 불가
    private float Maxmagicgage;
    private float CtHorposition = 0f;
    private float CtVerposition = 0f;
    private float CtCheckchange = 0f;

    Animator animator;

    [Header("좌표확인 관련")] 
    [SerializeField] public float Xplayer;//x좌표 확인
    [SerializeField] public float Yplayer;//y좌표 확인

    [SerializeField] Transform trsHands;

    [Header("플레리어UI")]
    [SerializeField] Image bowui;
    [SerializeField] Image bowui2;
    [SerializeField] Image swordui;
    [SerializeField] Image magicui;

    [SerializeField] Image ctbowui;
    [SerializeField] Image ctswordui;
    [SerializeField] Image ctmagicui;

    [SerializeField] Image hitgage;


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    HitboxMonster hitboxmonster = collision.gameObject.GetComponent<HitboxMonster>();

    //    if(hitboxmonster)
    //    {
    //        Monsterattackcheck = true;
    //    }
    //}

    private void OnTriggerStay2D(Collider2D collision)
    {

        HitboxMonster hitboxmonster = collision.gameObject.GetComponent<HitboxMonster>();

        if (hitboxmonster && GameManager.Instance.Monsterattack.Oncheckdamage == true)
        {
            Monsterattackcheck = true;
        }

        if(collision.gameObject.tag == "bossdamage")
        {
            Bossattackcheck = true;
        }
    }


    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        mainCam = GetComponent<Camera>();
        collision = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        MaxHP = GameHP;
        maxcountertimer = counterHittimer;//근접L지속시간
        Maxslowspeed = slowspeed;//자기 슬로우 지속시간
        Maxmagicgage = magicgage;//마법 게이지 최대 지속 시간
        Maxinvincibilitytime = invincibilitytime;//무적 지속 시간
    }

    void Start()
    {
        Weapontype = 0;
        gagecanvas.SetActive(false);

        GameManager.Instance.Player = this;

        movecheck = false;
    }

    void Update()
    {
        //checkMoveposition();//카메라에 맞춰 플레이어가 못 나가게하는 코드
        turnway();//무기 공격 방향 체크
        move();//이동
        playerposition();//플레이어위치를 실시간으로 확인
        WeaponChange();//무기 체인지

        bowattack();//원거리 공격

        countergage();//근접공격 게이지
        fullhit();//근접 공격
        counterHit();//근접2스킬

        magichit();//마법 공격

        weaponattacktimer();//각 무기 쿨타임 관리

        playerhpcheck();//플레이어 HP 관리
        bossdamage();//보스전 HP관리

        Anim();//애니메이션 관리
    }

    public void move()
    {
        if(movecheck == false)
        {
            horizontals = Input.GetAxisRaw("Horizontal");//좌우
            verticals = Input.GetAxisRaw("Vertical");//상하


            if (horizontals != 0)
            {
                verticals = 0;
            }
            else if (verticals != 0)
            {
                horizontals = 0;
            }
            transform.position += new Vector3(horizontals * playerspeed, verticals * playerspeed, 0) * Time.deltaTime;
            //Changecheck();
            magiccounter();//마법 카운터 코드
        }
    }//이동코드
    private void Anim()//애니메이션관리 코드
    {
        if(movecheck == false)
        {
            animator.SetFloat("Horizontal", (int)horizontals);
            animator.SetFloat("Vertical", (int)verticals);

            if (horizontals != 0)
            {
                transform.localScale = new Vector3(horizontals, 1, 1);
            }
        }
    }

    public void bowattack()//1차 일단 일반 공격 코드
    {
        if(Input.GetKeyDown(KeyCode.K))//일반 공격
        {
            GameObject go = null;

            if (Weapontype == 0)//원거리(활)일경우
            {
                if(arrowcheck == false && attackstandard == false)
                {
                    go = Instantiate(arrow);//화살을 소환
                    go.transform.eulerAngles = new Vector3(0, 0, Checkchange);//방향에 맞춰 발사
                    go.transform.position = transform.position + new Vector3(Horposition, Verposition, 0);//자기보다 앞에서 발사
                    attackstandard = true;
                    
                }
                else if( arrowcheck == true && ctattackstandard == false)
                {
                    go = Instantiate(ctArrow);//화살을 소환
                    go.transform.eulerAngles = new Vector3(0, 0, Checkchange);//방향에 맞춰 발사
                    go.transform.position = transform.position + new Vector3(Horposition, Verposition, 0);//자기보다 앞에서 발사
                    ctattackstandard = true;

                    if(ctattackstandard == true)
                    {
                        return;
                    }
                    Weaponcheck weaponcheck_2 = go.GetComponent<Weaponcheck>();
                    weaponcheck_2.Counterdamage(Weapontype, eulercheck);
                    return;
                }
                else if (Weapontype == 1)
                {
                    return;
                }
                else if (Weapontype == 2)//마법공격일 경우
                {
                    return;
                }
                if(attackstandard == true)
                {
                    return;
                }
                if(ctattackstandard == true)
                {
                    return;
                }
                Weaponcheck weaponcheck = go.GetComponent<Weaponcheck>();
                weaponcheck.Attackdamage(Weapontype, eulercheck);
            }
        }//일반 공격

        if(Input.GetKeyDown(KeyCode.L))//카운터 공격
        {
            GameObject count = null;
            if (Weapontype == 0)//원거리(활)일경우
            {
                if (arrowcheck == false)
                {
                    bowweapon.SetActive(false);
                    ctounterbow.SetActive(true);

                    arrowcheck = true;
                }
                else if(arrowcheck == true)
                {
                    bowweapon.SetActive(true);
                    ctounterbow.SetActive(false);
                    arrowcheck = false;
                }
                return;
            }
            else if (Weapontype == 1 && ctattackstandard == false)//근접일 경우
            {
                Hitgauge = 1;
                countertimercheck = true;
                ctattackstandard = true;

                return;
                //count = Instantiate(sword);
            }
            else if (Weapontype == 2 && ctattackstandard == false && magiccheck == false)//마법공격일 경우
            {
                ctattackstandard = true;
                count = Instantiate(ctMagic);//마법을 들여옴
                count.transform.eulerAngles = new Vector3(0, 0, CtCheckchange);//방향에 맞춰 공격
                count.transform.position = transform.position + new Vector3(CtHorposition, CtVerposition, 0);//자기보다 앞에서 발사
            }
            if(ctattackstandard == true || magiccheck == true)
            {
                return;
            }
            Weaponcheck weaponcheck = count.GetComponent<Weaponcheck>();
            weaponcheck.Counterdamage(Weapontype, eulercheck);
        }
    }

    private void WeaponChange()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if(Weapontype == 0)//원거리 물리 무기 => 0
            {
                bowweaponbox.SetActive(false);
                swordweaponbox.SetActive(true);
                gagecanvas.SetActive(true);
                Hitgauge = 0;
                Weapontype = 1;
            }
            else if(Weapontype == 1)//근거리 무기 => 1
            {
                swordweaponbox.SetActive(false);
                magicweaponbox.SetActive(true);
                gagecanvas.SetActive(false);
                Weapontype = 2;
            }
            else if(Weapontype == 2)//원거리 마법무기 => 2
            {
                magicweaponbox.SetActive(false);
                bowweaponbox.SetActive(true);
                Weapontype = 0;
            }
            else
            {
                Weapontype = 0;
            }
        }
    }//무기 교환 코드

    #region
    //public void Changecheck()//공격방향을 알려주는 코드
    //{
    //    if(verticals == 1)//위쪽을 가르킬때
    //    {
    //        if(Weapontype == 0)//활일 경우
    //        {
    //            Yeulerchange = 0;//반대 체크 반대로 돌릴거면 -180
    //            Checkchange = 0;//회전 값
    //            Verposition = 0.5f;//위 아래 체크
    //            Horposition = 0;//좌우 체크
    //            eulercheck = 1;//바라보는 방향 체크 -> 0은 없음
    //        }//활
    //        else if(Weapontype == 1)//칼일 경우
    //        {
    //            Yeulerchange = 0;
    //            Checkchange = 90;
    //            Verposition = 0.15f;
    //            Verposition = 0.1f;
    //            eulercheck = 1;//1은 위쪽방향
    //        }
    //        else if(Weapontype == 2)
    //        {
    //            Yeulerchange = 0;//반대 체크 반대로 돌릴거면 -180
    //            Checkchange = 270;//회전 값
    //            Verposition = 1f;//위 아래 체크
    //            Horposition = 0;//좌우 체크
    //            eulercheck = 0;//바라보는 방향 체크 -> 0은 없음
    //        }

    //    }
    //    else if(verticals == -1)//아래쪽을 가르킬때
    //    {
    //        if(Weapontype == 0)
    //        {
    //            Yeulerchange = 0;
    //            Checkchange = 180;
    //            Verposition = -0.5f;
    //            Horposition = 0;
    //            eulercheck = 4;//바라보는 방향 체크 -> 0은 없음
    //        }//활
    //        else if(Weapontype == 1)
    //        {
    //            Yeulerchange = -180;
    //            Checkchange = -90;
    //            Verposition = 0.15f;
    //            Verposition = 0.1f;
    //            eulercheck = 4;//4는 아랫방향
    //        }
    //        else if(Weapontype == 2)
    //        {
    //            Yeulerchange = 0;
    //            Checkchange = 90;
    //            Verposition = -1f;
    //            Horposition = 0;
    //            eulercheck = 0;
    //        }
    //    }

    //    if (horizontals == 1)//오른쪽을 가르킬때
    //    {
    //        if(Weapontype == 0)
    //        {
    //            Yeulerchange = 0;
    //            Checkchange = 270;
    //            Horposition = 0.5f;
    //            Verposition = 0;
    //            eulercheck = 3;//바라보는 방향 체크 -> 0은 없음
    //        }//활
    //        else if (Weapontype == 1)//근접 무기일 경우
    //        {
    //            Yeulerchange = 0;
    //            Checkchange = 0;
    //            Horposition = 0.15f;
    //            Verposition = -0.1f;
    //            eulercheck = 3;//3은 오른쪽 방향
    //        }
    //        else if(Weapontype == 2)
    //        {
    //            Yeulerchange = 0;
    //            Checkchange = 0;
    //            Horposition = 0.9f;
    //            Verposition = 0;
    //            eulercheck = 0;
    //        }
    //    }
    //    else if(horizontals == -1)//왼쪽을 가르킬때
    //    {
    //        if(Weapontype == 0)
    //        {
    //            Yeulerchange = 0;
    //            Checkchange = 90;
    //            Horposition = -0.5f;
    //            Verposition = 0;
    //            eulercheck = 2;//바라보는 방향 체크 -> 0은 없음
    //        }//활
    //        else if (Weapontype == 1)//근접 무기일 경우
    //        {
    //            Yeulerchange = -180;
    //            Checkchange = -90;
    //            Horposition = -0.15f;
    //            Verposition = -0.1f;
    //            eulercheck = 2;//2는 왼쪽 방향
    //        }
    //        else if(Weapontype == 2)
    //        {
    //            Yeulerchange = 0;
    //            Checkchange = 0;
    //            Horposition = -0.9f;
    //            Verposition = 0;
    //            eulercheck = 0;
    //        }
    //    }
    //}
    #endregion

    private void countergage()//일반 공격할때 게이지
    {
        if (countertimercheck != true)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                countergagecheck = true;
            }
            if (countergagecheck == true)
            {
                Hitgauge += Time.deltaTime;
                hitgage.fillAmount = Hitgauge;
                if (Hitgauge >= 1)
                {
                    Hitgauge = 1;
                }
            }
            if (Input.GetKeyUp(KeyCode.K))
            {
                countergagecheck = false;
                hitgage.fillAmount = 0;
            }
        }
    }

    private void fullhit()//근접무기일경우 발동하는 코드
    {
        if (Input.GetKeyUp(KeyCode.K))//근접무기일경우에 발동하는 코드
        {
            GameObject go = null;
            if (Weapontype == 1 && attackstandard == false)//근접일 경우
            {
                attackstandard = true;
                if (Hitgauge >= 1)
                {
                    if (countertimercheck != true)
                    {
                        Hitgauge = 0;//카운터 게이지를 초기화
                    }
                    movecheck = true;
                    go = Instantiate(ctSword, trsHands);//카운터 무기를 소환
                    go.transform.eulerAngles = new Vector3(0, Yeulerchange, Checkchange);//바라보고있는 방향으로 공격
                    go.transform.position = transform.position + new Vector3(Horposition, Verposition, 0);//자기위치보다 앞에서 소환
                    //Debug.Log("풀차징");
                    Weaponcheck weaponcheck_2 = go.GetComponent<Weaponcheck>();
                    weaponcheck_2.Counterdamage(Weapontype, eulercheck);
                    return;
                }
                else if (Hitgauge != 0)//게이지를 다 못채운채로 공격 할 경우
                {
                    attackstandard = true;
                    Hitgauge = 0;
                    movecheck = true;
                    go = Instantiate(sword, trsHands);
                    go.transform.eulerAngles = new Vector3(0, Yeulerchange, Checkchange);
                    go.transform.position = transform.position + new Vector3(Horposition, Verposition, 0);
                    //Debug.Log("기본공격");

                    Weaponcheck weaponcheck = go.GetComponent<Weaponcheck>();
                    weaponcheck.Attackdamage(Weapontype, eulercheck);
                }
            }
            else
            {
                return;
            }
        }
    }

    private void magichit()//마법공격이 통하는 코드
    {
        if(Weapontype == 2)
        {
            if (Input.GetKeyDown(KeyCode.K))//눌렀을때 마법공격을 계속함
            {
                GameObject go = null;
                if(magicgage <= 1)
                {
                    return;
                }
                else
                {
                    magiccheck = true;
                    magicgage -= 1;
                }


                go = Instantiate(magic,trsHands);
                go.transform.eulerAngles = new Vector3(0, 0, Checkchange);//방향에 맞춰 발사
                go.transform.position = transform.position + new Vector3(Horposition, Verposition, 0);//자기보다 앞에서 발사
                //위 아래로 발사될때 안바뀜-> 

                //Magicscript magicscript = go.GetComponent<Magicscript>();
                //magicscript.magicattack();

                Weaponcheck weaponcheck = go.GetComponent<Weaponcheck>();
                weaponcheck.Attackdamage(Weapontype, eulercheck);
            }
            if (Input.GetKeyUp(KeyCode.K))//키를 땠을때 마법공격을 그만함
            {
                magiccheck = false;
            }

            if (magiccheck == true)//마법 공겨키를 누를때
            {
                magicgage -= 1 * Time.deltaTime;//게이지를 초만큼 뺀다
                magicui.fillAmount = magicgage / Maxmagicgage;
                if (magicgage <= 0)
                {
                    magiccheck = false;
                }
            }
            else if (magiccheck == false)
            {
                magicgage += 1 * Time.deltaTime;
                magicui.fillAmount = magicgage / Maxmagicgage;
                if (magicgage >= Maxmagicgage)
                {
                    magicgage = Maxmagicgage;
                }
            }
        }

        //플레이어를 쫓아가면서 공격을 하게 해야됨->플레이어에 손을 만들어 넣고 그 손에 오브젝트를 넣으면 쫓아가도록 할수있나? -> 된다면 방향을 어떻게 처리... <- 근접무기에도 포함
        //애니메이션을 반복하는 구간을 만들어야함 /끝
    }

    private void counterHit()//근접 2스킬 시간 코드
    {
        if (Hitgauge == 1 && countertimercheck == true)
        {
            maxcountertimer -= 1 * Time.deltaTime;
            if (maxcountertimer <= 0)
            {
                Hitgauge = 0;
                playerspeed = playerspeed - 3;
                maxcountertimer = counterHittimer;
                slowcheck = true;
                countertimercheck = false;
            }
        }
        else if(slowcheck == true)
        {
            Maxslowspeed -= 1 * Time.deltaTime;
            if (Maxslowspeed <= 0)
            {
                playerspeed = playerspeed + 3;
                Maxslowspeed = slowspeed;
                slowcheck = false;
            }
        }
    }

    private void magiccounter()
    {
        if (verticals == 1)//위쪽을 가르킬때
        {
            CtCheckchange = 0;//회전 값
            CtVerposition = 0.5f;//위 아래 체크
            CtHorposition = 0;//좌우 체크

        }
        else if (verticals == -1)//아래쪽을 가르킬때
        {
            CtCheckchange = 180;
            CtVerposition = -0.5f;
            CtHorposition = 0;
        }

        if (horizontals == 1)//오른쪽을 가르킬때
        {
            CtCheckchange = 270;
            CtVerposition = 0f;
            CtHorposition = 0.5f;
        }
        else if (horizontals == -1)//왼쪽을 가르킬때
        {
            CtCheckchange = 90;
            CtVerposition = 0f;
            CtHorposition = -0.5f;
        }
    }//마법 2스킬 방향 코드

    public void playerposition()
    {
        Xplayer = transform.position.x;
        Yplayer = transform.position.y;
    }//플레이어좌표를 확인하는 코드(몬스터가 추적할때 쓰임)

    public void playerhpcheck()//몬스터가 플레이어를 공격할때 플레이어가 맞는 코드
    {
        if (Monsterattackcheck == true)//플레이어에 몬스터가 접촉할때
        {
            if (GameManager.Instance.Buttonmanager.Cheatcheck == true)
            {
                MaxHP -= 0;
                Monsterattackcheck = false;
            }
            else
            {
                Monsterdamage = GameManager.Instance.Monsterattack.attackdamage;//몬스터 데미지를 받는다
                if (Maxinvincibilitytime == 1)
                {
                    MaxHP -= Monsterdamage;
                }

                if (MaxHP <= 0)
                {
                    destroyplayer = true;
                    Destroy(play);
                }
                else if (MaxHP > 0)
                {
                    //만약 플레이어 HP가 남으면 1초간 무적

                    Maxinvincibilitytime -= 1 * Time.deltaTime;

                    if (Maxinvincibilitytime <= 0)
                    {
                        Maxinvincibilitytime = invincibilitytime;
                        Monsterattackcheck = false;
                    }
                }
            }
        }
    }

    private void weaponattacktimer()//각각 무기마다 쿨타임을 부여
    {
        //공격을 할때 attackstandard를 true로 만든 다음 true가 될시 weaponcheck에 등록된 것에 따라 attacktimer에 부여 이후  attacktimer에서 바로 뺀다
        if (attackstandard == true)//무기공격키를 누르면 true가 됨
        {
            if (Weapontype == 0)//원거리 1스킬일 경우
            {
                if (timerattack == false)//들어올때 한번만 실행하도록 만듬
                {
                    attacktimer = 0.15f;
                    Maxattacktimer = attacktimer;
                    timerattack = true;
                }
                Maxattacktimer -= Time.deltaTime;
                bowui.fillAmount = attacktimer / Maxattacktimer;
                if (Maxattacktimer <= 0)//쿨타임이 다 돌면 실행
                {
                    timerattack = false;
                    attackstandard = false;
                }
            }
            else if(Weapontype == 1)
            {
                if (timerattack == false && attacktimer <= 0)//무기를 체인지 할때 바로 공격 못하도록 만듬
                {
                    attacktimer = 0.3f;
                    Maxattacktimer = attacktimer;
                    timerattack = true;
                }
                attacktimer -= Time.deltaTime;
                swordui.fillAmount = attacktimer / Maxattacktimer;
                if (attacktimer <= 0)//쿨타임이 다 돌면 실행
                {
                    timerattack = false;
                    attackstandard = false;
                    //Debug.Log("초기화");
                }
            }//근접
        }

        if(ctattackstandard == true)//카운터일 경우
        {
            if(Weapontype == 0)//활
            {
                if (cttimerattack == false && ctattacktimer <= 0)//무기를 체인지 할때 바로 공격 못하도록 만듬
                {
                    attacktimer = 0.5f;
                    Maxattacktimer = attacktimer;
                    cttimerattack = true;
                }
                attacktimer -= Time.deltaTime;
                bowui2.fillAmount = attacktimer / Maxattacktimer;
                if (attacktimer <= 0)//쿨타임이 다 돌면 실행
                {
                    cttimerattack = false;
                    ctattackstandard = false;
                    //Debug.Log("카운터 초기화");
                }
            }
            else if(Weapontype == 1)//검
            {
                if (cttimerattack == false && ctattacktimer <= 0)//무기를 체인지 할때 바로 공격 못하도록 만듬
                {
                    ctattacktimer = 10f;//L스킬이후에 쿨타임
                    Maxctattacktimer = ctattacktimer;
                    cttimerattack = true;
                }
                ctattacktimer -= Time.deltaTime;
                ctswordui.fillAmount = ctattacktimer / Maxctattacktimer;
                if (ctattacktimer <= 0)//쿨타임이 다 돌면 실행
                {
                    cttimerattack = false;
                    ctattackstandard = false;
                    Debug.Log("카운터 초기화");
                }
            }
            else if(Weapontype == 2)//마법
            {
                if (cttimerattack == false && ctattacktimer <= 0)//무기를 체인지 할때 바로 공격 못하도록 만듬
                {
                    ctattacktimer = 0.5f;
                    Maxctattacktimer = ctattacktimer;
                    cttimerattack = true;
                }
                ctattacktimer -= Time.deltaTime;
                ctmagicui.fillAmount = ctattacktimer / Maxctattacktimer; 
                if (ctattacktimer <= 0)//쿨타임이 다 돌면 실행
                {
                    cttimerattack = false;
                    ctattackstandard = false;
                    //Debug.Log("카운터 초기화");
                }
            }
        }
    }


    //동생에 있는 움직일때마다 바뀌는 방향을 기준으로 공격방향을 설정
    /// <summary>
    /// 0 => 위쪽, 1=> 오른쪽, 2=> 아래쪽, 3=> 왼쪽
    /// </summary>
    private void turnway()
    {
        wayattack = GameManager.Instance.CheckBox.waycheck;
        if(wayattack == 0)//위쪽
        {
            if (Weapontype == 0)//활일 경우
            {
                Yeulerchange = 0;//반대 체크 반대로 돌릴거면 -180
                Checkchange = 0;//회전 값
                Verposition = 0.5f;//위 아래 체크
                Horposition = 0;//좌우 체크
                eulercheck = 1;//바라보는 방향 체크 -> 0은 없음
            }//활
            else if (Weapontype == 1)
            {
                Yeulerchange = 0;
                Checkchange = 0;
                Verposition = 0.5f;
                Horposition = 0;
                eulercheck = 1;
            }
            //else if (Weapontype == 2)//마법일경우
            //{
            //    Yeulerchange = 0;//반대 체크 반대로 돌릴거면 -180
            //    Checkchange = 90;//회전 값
            //    Verposition = 1f;//위 아래 체크
            //    Horposition = 0;//좌우 체크
            //    eulercheck = 0;//바라보는 방향 체크 -> 0은 없음
            //}
        }
        else if(wayattack == 1)//오른쪽
        {
            if (Weapontype == 0)
            {
                Yeulerchange = 0;
                Checkchange = 270;
                Horposition = 0.5f;
                Verposition = 0;
                eulercheck = 3;//바라보는 방향 체크 -> 0은 없음
            }//활
            else if(Weapontype == 1)
            {
                Yeulerchange = 0;
                Checkchange = 270;
                Horposition = 0.5f;
                Verposition = 0;
                eulercheck = 3;
            }
            //else if (Weapontype == 2)
            //{
            //    Yeulerchange = 0;
            //    Checkchange = 0;
            //    Horposition = 0.9f;
            //    Verposition = 0;
            //    eulercheck = 0;
            //}
        }
        else if(wayattack == 2)//아래쪽
        {
            if (Weapontype == 0)
            {
                Yeulerchange = 0;
                Checkchange = 180;
                Verposition = -0.5f;
                Horposition = 0;
                eulercheck = 4;//바라보는 방향 체크 -> 0은 없음
            }//활
            else if(Weapontype == 1)
            {
                Yeulerchange = 0;
                Checkchange = 180;
                Verposition = -0.5f;
                Horposition = 0;
                eulercheck = 4;
            }
            //else if (Weapontype == 2)
            //{
            //    Yeulerchange = 0;
            //    Checkchange = 270;
            //    Verposition = -1f;
            //    Horposition = 0;
            //    eulercheck = 0;
            //}
        }
        else if (wayattack == 3)//왼쪽
        {
            if (Weapontype == 0)
            {
                Yeulerchange = 0;
                Checkchange = 90;
                Horposition = -0.5f;
                Verposition = 0;
                eulercheck = 2;//바라보는 방향 체크 -> 0은 없음
            }//활
            else if(Weapontype == 1)
            {
                Yeulerchange = 0;
                Checkchange = 90;
                Horposition = -0.5f;
                Verposition = 0;
                eulercheck = 2;
            }
            //else if (Weapontype == 2)
            //{
            //    Yeulerchange = 0;
            //    Checkchange = 0;
            //    Horposition = -0.9f;
            //    Verposition = 0;
            //    eulercheck = 0;
            //}
        }

    }


    private void checkMoveposition()
    {
        //이전에 한 코딩 수업에서 필요한 부분 찾기 벽에 닿았을때 물리가 아닌 코드로만 막기
        #region
        //Vector3 currentPlayerPos = mainCam.WorldToScreenPoint(transform.position);

        //if(currentPlayerPos.x < 0.1f)
        //{
        //    currentPlayerPos.x = 0.1f;
        //}
        //else if(currentPlayerPos.x > 0.9f)
        //{
        //    currentPlayerPos.x = 0.9f;
        //}

        //if(currentPlayerPos.y < 0.1f)
        //{
        //    currentPlayerPos.y = 0.1f;
        //}
        //else if(currentPlayerPos.y > 0.9f)
        //{
        //    currentPlayerPos.y = 0.9f;
        //}

        //Vector3 fixedPlayerPos = mainCam.ViewportToWorldPoint(currentPlayerPos);
        //transform.position = fixedPlayerPos;
        #endregion
        //슈팅 게임 응용

        

    }

    public void stage(int _stagetype)
    {
        if(_stagetype == 1)//튜토리얼 > 시작 스테이지
        {
            transform.position = new Vector3(4, -10, 0);
        }
        else if(_stagetype == 2)//시작 스테이지 -> 스테이지 1
        {
            transform.position = new Vector3(1, -9, 0);
        }
        else if(_stagetype == 3)//스테이지1 -> 보스 스테이지
        {
            transform.position = new Vector3(0.12f, -21, 0);
        }
    }

    private void bossdamage()//보스 공격 받을때 생기는 코드
    {
        if(Bossattackcheck == true)//보스 공격에 맞았을때
        {
            if (GameManager.Instance.Buttonmanager.Cheatcheck == true)
            {
                MaxHP -= 0;
                Bossattackcheck = false;
            }
            else
            {
                //Monsterdamage = GameManager.Instance.Monsterattack.attackdamage;//몬스터 데미지를 받는다
                Monsterdamage = 1;
                if (Maxinvincibilitytime == 1)
                {
                    MaxHP -= Monsterdamage;
                }

                if (MaxHP <= 0)
                {
                    Destroy(play);
                }
                else if (MaxHP > 0)
                {
                    //만약 플레이어 HP가 남으면 1초간 무적

                    Maxinvincibilitytime -= 1 * Time.deltaTime;

                    if (Maxinvincibilitytime <= 0)
                    {
                        Maxinvincibilitytime = invincibilitytime;
                        Bossattackcheck = false;
                    }
                }
            }
        }
    }


    
}
