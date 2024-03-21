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
    [SerializeField] private float Horposition = 0f;//무기 공격위치
    [SerializeField] private float Verposition = 0f;//무기 공격위치


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
        bowattack();
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
            if(Weapontype == 0)//원거리(활)일경우
            {
                go = Instantiate(arrow);
                go.transform.eulerAngles = new Vector3(0, 0, Checkchange);
                go.transform.position = transform.position + new Vector3(Horposition, Verposition, 0);

            }
            else if(Weapontype == 1)//근접일 경우
            {
                go = Instantiate(sword);
                go.transform.eulerAngles = new Vector3(0, 0, Checkchange -90 );
                go.transform.position = transform.position + new Vector3(Horposition, Verposition, 0);
            }
            else if(Weapontype == 2)//마법공격일 경우
            {
                go = Instantiate(magic);
            }
            Weaponcheck weaponcheck = go.GetComponent<Weaponcheck>();
            weaponcheck.Attackdamage(Weapontype);
        }

        if(Input.GetKeyDown(KeyCode.L))//카운터 공격
        {
            GameObject count = null;
            if (Weapontype == 0)//원거리(활)일경우
            {
                count = Instantiate(arrow);
            }
            else if (Weapontype == 1)//근접일 경우
            {
                count = Instantiate(sword);
            }
            else if (Weapontype == 2)//마법공격일 경우
            {
                count  = Instantiate(magic);
            }
            Weaponcheck weaponcheck = count.GetComponent<Weaponcheck>();
            weaponcheck.Counterdamage(Weapontype);
        }
    }

    private void WeaponChange()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if(Weapontype == 0)//원거리 물리 무기 => 0
            {
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

    public void Changecheck()
    {
        if(verticals == 1)
        {
            Checkchange = 0;
            Verposition = 0.5f;
            Horposition = 0;

        }
        else if(verticals == -1)
        {
            Checkchange = 180;
            Verposition = -0.5f;
            Horposition = 0;
        }

        if (horizontals == 1)
        {
            Checkchange = 270;
            if (Weapontype == 1)
            {
                Checkchange = 90;
            }
            Horposition = 0.5f;
            Verposition = 0;
        }
        else if(horizontals == -1)
        {
            Checkchange = 90;
            Horposition = -0.5f;
            Verposition = 0;
        }
    }

}
