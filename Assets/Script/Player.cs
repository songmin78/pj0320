using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    Transform Attackbox;

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
    private float MaxHP;
    [SerializeField] float Checkchange = 0;//무기 회전 방향
    [SerializeField] float Yeulerchange = 0;//무기 방향을 조정하기 0 <-> -180
    [SerializeField] private float Horposition = 0f;//무기 공격위치
    [SerializeField] private float Verposition = 0f;//무기 공격위치
    public int eulercheck =0;
    [SerializeField] bool slowcheck = false;

    [Header("카운터 기준")]
    [SerializeField] bool arrowcheck = false;
    [SerializeField] float Hitgauge = 0f;//근접 카운터 게이지 1이되면 카운터
    [SerializeField] private bool countergagecheck = false;//게이지가 올라가는지 안가는지 확인
    [SerializeField] bool countertimercheck = false;//근접 L스킬을 쓸때 무한 카운터 공격
    [SerializeField] float counterHittimer = 3;//근접 L스킬을 쓰는 지속시간
    private float maxcountertimer;
    [SerializeField] float slowspeed = 3;//근접L스킬이 끝나고 생기는 슬로우 지속 시간
    private float Maxslowspeed;
    

    [Header("플레이어 정보")]
    [SerializeField] float playerspeed = 5f;//플레이어가 이동하는 속도
    [SerializeField] public int Weapontype;//무기 리스트

    [Header("플레이어의 능력치 설정")]
    [SerializeField,Range(1,5)] int GameHP;//게임내 플레이어 체력

    [Header("무기 공격 관련 정보(마법)")]
    [SerializeField] bool magiccheck = false;//마법게이지가 닳는위한 확인
    public bool MagicCheck => magiccheck;

    [SerializeField] float magicgage = 5f;//공격 게이지 0이되면 사용 불가
    private float Maxmagicgage;
    private float CtHorposition = 0f;
    private float CtVerposition = 0f;
    private float CtCheckchange = 0f;

    Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        MaxHP = GameHP;
        maxcountertimer = counterHittimer;//근접L지속시간
        Maxslowspeed = slowspeed;//자기 슬로우 지속시간
        Maxmagicgage = magicgage;//마법 게이지 최대 지속 시간
    }

    void Start()
    {
        Weapontype = 0;

        GameManager.Instance.Player = this;
    }

    void Update()
    {
        move();//이동
        Anim();//이동애니메이션
        WeaponChange();//무기 체인지

        bowattack();//원거리 공격

        countergage();//근접공격 게이지
        fullhit();//근접 공격
        counterHit();//근접2스킬

        magichit();//마법 공격

    }

    public void move()
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
        Changecheck();
        magiccounter();//마법 카운터 코드
    }//이동코드
    private void Anim()//이동 애니메이션 코드
    {
        animator.SetFloat("Horizontal", (int)horizontals);
        animator.SetFloat("Vertical", (int)verticals);

        if (horizontals != 0)
        {
            transform.localScale = new Vector3(horizontals, 1, 1);
        }
    }

    public void bowattack()//1차 일단 일반 공격 코드
    {
        if(Input.GetKeyDown(KeyCode.K))//일반 공격
        {
            GameObject go = null;

            if (Weapontype == 0)//원거리(활)일경우
            {
                if(arrowcheck == false)
                {
                    go = Instantiate(arrow);//화살을 소환
                    go.transform.eulerAngles = new Vector3(0, 0, Checkchange);//방향에 맞춰 발사
                    go.transform.position = transform.position + new Vector3(Horposition, Verposition, 0);//자기보다 앞에서 발사
                }
                else if( arrowcheck == true)
                {
                    go = Instantiate(ctArrow);//화살을 소환
                    go.transform.eulerAngles = new Vector3(0, 0, Checkchange);//방향에 맞춰 발사
                    go.transform.position = transform.position + new Vector3(Horposition, Verposition, 0);//자기보다 앞에서 발사

                    Weaponcheck weaponcheck_2 = go.GetComponent<Weaponcheck>();
                    weaponcheck_2.Counterdamage(Weapontype, eulercheck);
                    return;
                    
                }

            }
            else if(Weapontype == 1)
            {
                return;
            }
            else if(Weapontype == 2)//마법공격일 경우
            {
                return;
            }
            Weaponcheck weaponcheck = go.GetComponent<Weaponcheck>();
            weaponcheck.Attackdamage(Weapontype, eulercheck);
        }//일반 공격

        if(Input.GetKeyDown(KeyCode.L))//카운터 공격
        {
            GameObject count = null;
            if (Weapontype == 0)//원거리(활)일경우
            {
                if (arrowcheck == false)
                {
                    arrowcheck = true;
                }
                else if(arrowcheck == true)
                {
                    arrowcheck = false;
                }
                return;
            }
            else if (Weapontype == 1)//근접일 경우
            {
                Hitgauge = 1;
                countertimercheck = true;

                return;
                //count = Instantiate(sword);
            }
            else if (Weapontype == 2)//마법공격일 경우
            {
                count = Instantiate(ctMagic);//마법을 들여옴
                count.transform.eulerAngles = new Vector3(0, 0, CtCheckchange);//방향에 맞춰 공격
                count.transform.position = transform.position + new Vector3(CtHorposition, CtVerposition, 0);//자기보다 앞에서 발사
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
                Hitgauge = 0;
                Weapontype = 1;
            }
            else if(Weapontype == 1)//근거리 무기 => 1
            {
                Weapontype = 2;
            }
            else if(Weapontype == 2)//원거리 마법무기 => 2
            {
                Weapontype = 0;
            }
            else
            {
                Weapontype = 0;
            }
        }
    }//무기 교환 코드

    public void Changecheck()//공격방향을 알려주는 코드
    {
        if(verticals == 1)//위쪽을 가르킬때
        {
            if(Weapontype == 0)//활일 경우
            {
                Yeulerchange = 0;//반대 체크 반대로 돌릴거면 -180
                Checkchange = 0;//회전 값
                Verposition = 0.5f;//위 아래 체크
                Horposition = 0;//좌우 체크
                eulercheck = 1;//바라보는 방향 체크 -> 0은 없음
            }//활
            else if(Weapontype == 1)//칼일 경우
            {
                Yeulerchange = 0;
                Checkchange = 90;
                Verposition = 0.15f;
                Verposition = 0.1f;
                eulercheck = 1;//1은 위쪽방향
            }
            else if(Weapontype == 2)
            {
                Yeulerchange = 0;//반대 체크 반대로 돌릴거면 -180
                Checkchange = 90;//회전 값
                Verposition = 1f;//위 아래 체크
                Horposition = 0;//좌우 체크
                eulercheck = 0;//바라보는 방향 체크 -> 0은 없음
            }

        }
        else if(verticals == -1)//아래쪽을 가르킬때
        {
            if(Weapontype == 0)
            {
                Yeulerchange = 0;
                Checkchange = 180;
                Verposition = -0.5f;
                Horposition = 0;
                eulercheck = 4;//바라보는 방향 체크 -> 0은 없음
            }//활
            else if(Weapontype == 1)
            {
                Yeulerchange = -180;
                Checkchange = -90;
                Verposition = 0.15f;
                Verposition = 0.1f;
                eulercheck = 4;//4는 아랫방향
            }
            else if(Weapontype == 2)
            {
                Yeulerchange = 0;
                Checkchange = 270;
                Verposition = -1f;
                Horposition = 0;
                eulercheck = 0;
            }
        }

        if (horizontals == 1)//오른쪽을 가르킬때
        {
            if(Weapontype == 0)
            {
                Yeulerchange = 0;
                Checkchange = 270;
                Horposition = 0.5f;
                Verposition = 0;
                eulercheck = 3;//바라보는 방향 체크 -> 0은 없음
            }//활
            else if (Weapontype == 1)//근접 무기일 경우
            {
                Yeulerchange = 0;
                Checkchange = 0;
                Horposition = 0.15f;
                Verposition = -0.1f;
                eulercheck = 3;//3은 오른쪽 방향
            }
            else if(Weapontype == 2)
            {
                Yeulerchange = 0;
                Checkchange = 0;
                Horposition = 0.9f;
                Verposition = 0;
                eulercheck = 0;
            }
        }
        else if(horizontals == -1)//왼쪽을 가르킬때
        {
            if(Weapontype == 0)
            {
                Yeulerchange = 0;
                Checkchange = 90;
                Horposition = -0.5f;
                Verposition = 0;
                eulercheck = 2;//바라보는 방향 체크 -> 0은 없음
            }//활
            else if (Weapontype == 1)//근접 무기일 경우
            {
                Yeulerchange = -180;
                Checkchange = -90;
                Horposition = -0.15f;
                Verposition = -0.1f;
                eulercheck = 2;//2는 왼쪽 방향
            }
            else if(Weapontype == 2)
            {
                Yeulerchange = 0;
                Checkchange = 180;
                Horposition = -0.9f;
                Verposition = 0;
                eulercheck = 0;
            }
        }
    }

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
                if (Hitgauge >= 1)
                {
                    Hitgauge = 1;
                }
            }
            if (Input.GetKeyUp(KeyCode.K))
            {
                countergagecheck = false;
            }
        }
    }

    private void fullhit()//근접무기일경우 발동하는 코드
    {
        if (Input.GetKeyUp(KeyCode.K))//근접무기일경우에 발동하는 코드
        {
            GameObject go = null;
            if (Weapontype == 1)//근접일 경우
            {
                if (Hitgauge >= 1)
                {
                    if (countertimercheck != true)
                    {
                        Hitgauge = 0;//카운터 게이지를 초기화
                    }
                    go = Instantiate(ctSword);//카운터 무기를 소환
                    go.transform.eulerAngles = new Vector3(0, Yeulerchange, Checkchange);//바라보고있는 방향으로 공격
                    go.transform.position = transform.position + new Vector3(Horposition, Verposition, 0);//자기위치보다 앞에서 소환
                    Debug.Log("풀차징");

                    Weaponcheck weaponcheck_2 = go.GetComponent<Weaponcheck>();
                    weaponcheck_2.Counterdamage(Weapontype, eulercheck);
                    return;
                }
                else if (Hitgauge != 0)
                {
                    Hitgauge = 0;
                    go = Instantiate(sword);
                    go.transform.eulerAngles = new Vector3(0, Yeulerchange, Checkchange);
                    go.transform.position = transform.position + new Vector3(Horposition, Verposition, 0);
                    Debug.Log("기본공격");

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
                magiccheck = true;

                go = Instantiate(magic);
                go.transform.eulerAngles = new Vector3(0, 0, Checkchange);//방향에 맞춰 발사
                go.transform.position = transform.position + new Vector3(Horposition, Verposition, 0);//자기보다 앞에서 발사

                Weaponcheck weaponcheck = go.GetComponent<Weaponcheck>();
                weaponcheck.Attackdamage(Weapontype, eulercheck);
            }
            if (Input.GetKeyUp(KeyCode.K))//키를 땠을때 마법공격을 그만함
            {
                magiccheck = false;
            }

            if (magiccheck == true)
            {
                magicgage -= 1 * Time.deltaTime;
                if (magicgage <= 0)
                {
                    magiccheck = false;
                }
            }
            else if (magiccheck == false)
            {
                magicgage += 1 * Time.deltaTime;
                if (magicgage >= Maxmagicgage)
                {
                    magicgage = Maxmagicgage;
                }
            }
        }
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
}
