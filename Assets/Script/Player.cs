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

    [Header("카운터 기준")]
    [SerializeField] bool arrowcheck = false;
    [SerializeField] float Hitgauge = 0f;//근접 카운터 게이지 1이되면 카운터
    [SerializeField] private bool countergagecheck = false;//게이지가 올라가는지 안가는지 확인


    [Header("플레이어 정보")]
    [SerializeField] float playerspeed = 5f;//플레이어가 이동하는 속도
    [SerializeField] public int Weapontype;//무기 리스트

    [Header("플레이어의 능력치 설정")]
    [SerializeField,Range(1,5)] int GameHP;//게임내 플레이어 체력

    Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        MaxHP = GameHP;
    }

    void Start()
    {
        Weapontype = 0;
    }

    void Update()
    {
        move();
        Anim();
        WeaponChange();
        countergage();
        bowattack();
        fullhit();
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
    }
    private void Anim()
    {
        animator.SetFloat("Horizontal", (int)horizontals);
        animator.SetFloat("Vertical", (int)verticals);

        if (horizontals != 0)
        {
            transform.localScale = new Vector3(horizontals, 1, 1);
        }
    }

    public void bowattack()
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
                go = Instantiate(magic);
            }
            Weaponcheck weaponcheck = go.GetComponent<Weaponcheck>();
            weaponcheck.Attackdamage(Weapontype, eulercheck);
        }

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
                //count = Instantiate(sword);
            }
            else if (Weapontype == 2)//마법공격일 경우
            {
                //count  = Instantiate(magic);
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
    }

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
                eulercheck = 0;//바라보는 방향 체크 -> 0은 없음
            }//활
            else if(Weapontype == 1)//칼일 경우
            {
                Yeulerchange = 0;
                Checkchange = 90;
                Verposition = 0.15f;
                Verposition = 0.1f;
                eulercheck = 1;//1은 위쪽방향
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
                eulercheck = 0;//바라보는 방향 체크 -> 0은 없음
            }//활
            else if(Weapontype == 1)
            {
                Yeulerchange = -180;
                Checkchange = -90;
                Verposition = 0.15f;
                Verposition = 0.1f;
                eulercheck = 2;//2는 왼쪽 방향
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
                eulercheck = 0;//바라보는 방향 체크 -> 0은 없음
            }//활
            else if (Weapontype == 1)//근접 무기일 경우
            {
                Yeulerchange = 0;
                Checkchange = 0;
                Horposition = 0.15f;
                Verposition = -0.1f;
                eulercheck = 3;//3은 오른쪽 방향
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
                eulercheck = 0;//바라보는 방향 체크 -> 0은 없음
            }//활
            else if (Weapontype == 1)//근접 무기일 경우
            {
                Yeulerchange = -180;
                Checkchange = -90;
                Horposition = -0.15f;
                Verposition = -0.1f;
                eulercheck = 4;//4는 아랫방향
            }
        }
    }

    private void countergage()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            countergagecheck = true;
        }
        if(countergagecheck == true)
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

    private void fullhit()
    {
        if(Input.GetKeyUp(KeyCode.K))//근접무기일경우에 발동하는 코드
        {
            GameObject go = null;
            if (Weapontype == 1)//근접일 경우
            {
                if (Hitgauge >= 1)
                {
                    Hitgauge = 0;//카운터 게이지를 초기화
                    go = Instantiate(ctSword);//카운터 무기를 소환
                    go.transform.eulerAngles = new Vector3(0, Yeulerchange, Checkchange);//바라보고있는 방향으로 공격
                    go.transform.position = transform.position + new Vector3(Horposition, Verposition, 0);//자기위치보다 앞에서 소환
                    Debug.Log("풀차징");

                    Weaponcheck weaponcheck_2 = go.GetComponent<Weaponcheck>();
                    weaponcheck_2.Counterdamage(Weapontype,eulercheck);
                    return;
                }
                else if (Weapontype != 0)
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
}
