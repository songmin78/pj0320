using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    Animator animator;

    [Header("보스 능력치")]
    [SerializeField] float bossHp;//보스 HP
    float MaxbossHP;

    [SerializeField] GameObject pattern1;//공격패턴1
    [SerializeField] GameObject pattern2;//공격패턴2
    [SerializeField] GameObject Cosspattern3;//공격 패턴3-1
    [SerializeField] GameObject Cosspattern4;//공격 패턴3-2
    [SerializeField]int patterncount;//랜덤한 패턴을 넣는다
    [SerializeField]int lastpatterncount;//마지막 숫자를 넣어 동일한 숫자를 나오는거 방지함
    [SerializeField]float returnpattrn;
    [SerializeField]float Maxattacktimer;//공격 쿨타임
    float attacktimer = 2;
    float waittime = 1f;//1초간 대기
    [SerializeField]float Maxwaittime;//1초간 대기
    bool waittimer;// 1초간 대기
    bool attackwait;//공격하는중
    [SerializeField] float hitendwait = 4f;//공격한 후 대기 시간
    float Maxhitendwait;
    bool hitwaitcheck;//끝나고 대기시간 확인
    float rushtime;
    float Maxrushtime = 0.8f;
    bool rushrush = false;
    bool rushattackcheck;
    bool rushwait;
    bool afterattackcheck;
    float afterattack = 2;//공격 이후에 대기타는 시간
    [SerializeField]float Maxafterattack;
    //[SerializeField, Range(0,1)]float randomtimer = 0.5f;//랜덤 숫자 돌릴때 대기 시간
    //[SerializeField]float Maxrandomtimer;
    [Header("카운터 부분")]
    [SerializeField]bool counterwait = false;//공격대기상태 이때 카운터 공격을 맞으면 기절함
    bool counterfaint = false;//카운터 공격에 맞았을때 true로 변환
    [SerializeField]float countertimer = 5;//카운터공격에 맞았을때 생기는 무력화 시간
    float Maxcountertimer;
    bool rushcountertimecheck;//밀려날때 카운터기절 시간이 안 흐르게 설정
    [Header("1,2스킬 공격 부분")]
    [SerializeField] GameObject rightattack;
    [SerializeField] GameObject leftattack;
    float skillattack = 0.25f;//0.25초만큼 공격하도록 설정
    bool skillcheck;//보스가 공격할때 true
    [Header("3스킬 공격 부분")]
    [SerializeField] Image horizonalrush;
    [SerializeField] Image horizonalrush1;
    [SerializeField] Image verticalrush;
    [SerializeField] Image verticalrush1;
    [Header("공격 여부")]
    bool beatendamage = false;
    bool magicchek;//마법공격이 들어오는지 아닌지 확인
    float retime = 0.5f;
    float Maxretime;
    //public bool Oncheckdamage = false;
    float weapondamage = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Weaponcheck weapon = collision.gameObject.GetComponent<Weaponcheck>();//무기 gameobject가 몬스터 collision에 닿을때 
        //Debug.Log(collision.name);
        Weaponcheck weapon = collision.gameObject.GetComponent<Weaponcheck>();

        if (weapon != null && counterwait == true && weapon.Counter == true)
        {
            counterfaint = true;
        }
        

        #region layer마스크 부분을 적음
        //layer는 int형으로 밖에  못 가져옴
        //layer에서 글로된 layer를 가지고 올려면 LayerMask.NameToLayer를 활용해 이름을 가져온다
        //LayerMask는 여러가지 layer를 가져올수있게 해주는 코드이다
        //if (collision.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        //{
        //    Debug.Log("1");
        //}
        #endregion

        #region 1번째거랑 비슷함
        //Weaponcheck weapon = collision.gameObject.GetComponent<Weaponcheck>();
        //Debug.Log(weapon.name);
        //if(weapon.Counter == true && counterwait == true)
        //{
        //    counterfaint = true;
        //}
        #endregion

        if(weapon)
        {
            beatendamage = true;
            if (weapon.magic == true)
            {
                magicchek = true;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (magicchek == true)
        {
            magicchek = false;
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Maxattacktimer = attacktimer;
        MaxbossHP = bossHp;
        Maxwaittime = waittime;
        Maxhitendwait = hitendwait;
        Maxafterattack = afterattack;
        Maxcountertimer = countertimer;
        //Maxrandomtimer = randomtimer;
    }

    void Start()
    {
        pattern1.SetActive(false);
        pattern2.SetActive(false);
        Cosspattern3.SetActive(false);
        Cosspattern4.SetActive(false);
    }

    void Update()
    {
        //보스가 공격하는 부분
        Randomcheck();
        Bosspattern();
        boosattacktimer();

        //보스 HP관리쪽
        hpcommand();
        destrymagic();
        destroymonster();

        //보스카운터 부분
        countertime();//좌우 공격이  카운터 될때 체크
        rushcounter();//3번 스킬이 카운터 될때 체크
        counterdamage();//카운터 기절 시간
    }

    private void Randomcheck()
    {
        if (attackwait == false && afterattackcheck == false)
        {
            #region
            if (lastpatterncount == patterncount)//만약 1의 변수랑 2의 변수가 같을경우
            {
                if (returnpattrn > 2)
                {
                    patterncount = 2;
                }
                else
                {
                    patterncount = Random.Range(0, 2);//2의 변수를 변화시킨다
                }
                Randomcheck();//다시 적용
            }
            else if (lastpatterncount != patterncount)
            {
                if(returnpattrn > 2)
                {
                    patterncount = 2;
                    lastpatterncount = patterncount;
                    returnpattrn = 0;
                }
                else
                {
                    lastpatterncount = patterncount;//다른 숫자일 경우 마지막숫자르  다시 변경함
                    if (lastpatterncount == 0 || lastpatterncount == 1)
                    {
                        returnpattrn += 1;
                    }
                    else if (lastpatterncount == 2)
                    {
                        returnpattrn = 0;
                    }
                }
            }
            #endregion
            #region 반복식 while문 아직 테스트단계
            //while (lastpatterncount == patterncount)
            //{
            //    if(returnpattrn > 2)
            //    {
            //        patterncount = 2;
            //    }
            //    else
            //    {
            //        patterncount = Random.Range(0, 2);
            //    }
            //    if(lastpatterncount != patterncount)
            //    {
            //        lastpatterncount = patterncount;
            //        //다른 숫자일 경우 마지막숫자르  다시 변경함
            //        if (lastpatterncount == 0 || lastpatterncount == 1) 
            //        {
            //            returnpattrn += 1;
            //        }
            //        else if (lastpatterncount == 2)
            //        {
            //            returnpattrn = 0;
            //        }
            //        return;
            //    }
            //}
            #endregion
        }
    }

    private void Bosspattern()
    {
        if(counterfaint == false)//카운터 공격에 안 맞았을때
        {
            if (patterncount == 0)//보스 패턴1
            {
                if (afterattackcheck == false)
                {
                    attackwait = true;//랜덤 숫자를 계속 넣는것을 방지
                    transform.position = new Vector3(10.5f, -5, 0);//공격을 할 자리(오른쪽 끝)
                    if (waittimer == false)//대기시간이 1번만 돌아가도록 만듬
                    {
                        Maxwaittime -= Time.deltaTime;//도착하면 바로 공격이 아닌 1초 기다린다
                        if (Maxwaittime <= 0)
                        {
                            waittimer = true;//대기시간이 다시 안쓰이게 설정
                        }
                    }
                    else if (waittimer == true)
                    {
                        pattern1.SetActive(true);//공격범위가 켜진다
                        counterwait = true;//카운터을 당하기 위해서 true로 전환
                        if (Maxattacktimer <= 0)//공격 대기모션이 0보다 작을때 공격
                        {
                            attackwait = false;//랜덤숫자를 넣는코드
                            Maxwaittime = waittime;//대기시간 다시 설정
                            waittimer = false;//대기시간 다시 활성화
                            Maxattacktimer = 2;//공격 대기시간 설정
                            counterwait = false; //카운터공격 들어오는것을 방지
                            afterattackcheck = true;//공격후에  있는 대기 시간
                            skillcheck = true;
                            //hitwaitcheck = true;
                            //patterncount = Random.Range(0, 3);//랜덤숫자 돌리기
                            pattern1.SetActive(false);//공격 범위를 삭제
                        }
                        else if (Maxattacktimer > 0)//0이 아닐때
                        {
                            Maxattacktimer -= Time.deltaTime;//공격모션을 대기 초가 다 되면 공격을 하기위한 초
                            #region
                            //if (counterfaint == true)//카운터 
                            //{
                            //    attackwait = false;//랜덤숫자를 넣는코드
                            //    Maxwaittime = waittime;//대기시간 다시 설정
                            //    waittimer = false;//대기시간 다시 활성화
                            //    counterwait = false;
                            //    Maxattacktimer = 2;
                            //    pattern1.SetActive(false);//공격 범위를 삭제
                            //    return;
                            //}
                            #endregion
                        }
                    }
                }
                else if (afterattackcheck == true)
                {
                    Maxafterattack -= Time.deltaTime;
                    if (Maxafterattack <= 0)
                    {
                        Maxafterattack = afterattack;
                        afterattackcheck = false;
                        patterncount = Random.Range(0, 2);
                    }
                }


            }//보스 패턴 1 오른쪽에서 칼 공격
            else if (patterncount == 1)//보스 패턴 2
            {
                if (afterattackcheck == false)
                {
                    attackwait = true;//랜덤 숫자를 계속 넣는것을 방지
                    transform.localScale = new Vector3(-1, 1, 1);//반대 방향으로 바꾼코드
                    transform.position = new Vector3(-1.5f, -5, 0);//공격을 할 자리(왼쪽 끝)
                    if (waittimer == false)//대기시간이 1번만 돌아가도록 만듬
                    {
                        Maxwaittime -= Time.deltaTime;//도착하면 바로 공격이 아닌 1초 기다린다
                        if (Maxwaittime <= 0)
                        {
                            waittimer = true;//대기시간이 다시 안쓰이게 설정
                        }
                    }
                    else if (waittimer == true)
                    {
                        pattern2.SetActive(true);//공격범위가 켜진다
                        counterwait = true;//공격 모션을  true
                        if (Maxattacktimer <= 0)//공격 대기모션이 0보다 작을때 공격
                        {
                            attackwait = false;//랜덤숫자를 넣는코드
                            Maxwaittime = waittime;//대기시간 다시 설정
                            waittimer = false;//대기시간 다시 활성화
                            Maxattacktimer = 2;//공격 대기시간 설정
                            counterwait = false; //카운터공격 들어오는것을 방지
                            afterattackcheck = true;//공격후에  있는 대기 시간
                            skillcheck = true;
                            pattern2.SetActive(false);//공격 범위를 삭제
                        }
                        else if (Maxattacktimer > 0)//0이 아닐때
                        {
                            Maxattacktimer -= Time.deltaTime;//공격모션을 대기 초가 다 되면 공격
                            #region
                            //if (counterfaint == true)//카운터 
                            //{
                            //    attackwait = false;//랜덤숫자를 넣는코드
                            //    Maxwaittime = waittime;//대기시간 다시 설정
                            //    waittimer = false;//대기시간 다시 활성화
                            //    counterwait = false;
                            //    Maxattacktimer = 2;
                            //    pattern2.SetActive(false);
                            //    return;
                            //}
                            #endregion
                        }
                    }
                }
                else if (afterattackcheck == true)
                {
                    Maxafterattack -= Time.deltaTime;
                    if (Maxafterattack <= 0)
                    {
                        Maxafterattack = afterattack;
                        afterattackcheck = false;
                        transform.localScale = new Vector3(1, 1, 1);
                        patterncount = Random.Range(0,2);
                    }
                }
            }//보스 패턴2 왼쪽에서 칼 공격
            else if (patterncount == 2)//보스 패턴 3
            {
                if(afterattackcheck == false)
                {
                    attackwait = true;
                    if (waittimer == false && rushattackcheck == false)
                    {
                        transform.position = new Vector3(4.22f, 8.85f, 0);//화면에서 사라진다
                        //Maxwaittime -= Time.deltaTime;//도착하면 바로 공격이 아닌 1초 기다린다
                        //Debug.Log(rushtime);
                        rushtime += Time.deltaTime;//공격루트를 알려주는 시간 코드
                        Cosspattern3.SetActive(true);//공격루트 오브젝트를 보여준다
                        horizonalrush.fillAmount = rushtime / Maxrushtime;//왜각 공격루트
                        horizonalrush1.fillAmount = rushtime / Maxrushtime;//직접 공격할 공격루트
                        if (rushtime >= 0.79f)//1이 된다면
                        {
                            waittimer = true;//1번만 실행하는 코드를 True
                            rushtime = 0;//시간코드 초기화
                        }
                    }
                    else if (waittimer == true)
                    {
                        if (rushattackcheck == false)
                        {
                            Cosspattern3.SetActive(false);//원래 공격루트 삭제
                            rushtime += Time.deltaTime;
                            Cosspattern4.SetActive(true);//세로 공격 루트를 보여준다
                            verticalrush.fillAmount = rushtime / Maxrushtime;//왜각
                            verticalrush1.fillAmount = rushtime / Maxrushtime;//중앙
                            if (rushtime >= 0.79f)//공격루트를 다 보여준다면
                            {
                                rushattackcheck = true;
                                Maxattacktimer = 1;//공격이 다끝나고 남은 대기시간으로 활용
                                Cosspattern4.SetActive(false);
                                //Bosspattern();
                                #region
                                //Cosspattern4.SetActive(false);
                                //if (rushrush == false)//한번만 공격하게 설정
                                //{
                                //    Cosspattern4.SetActive(false); //공격루트 삭제

                                //    Maxwaittime -= Time.deltaTime;//1초간 대기
                                //    if (Maxwaittime <= 0)//대기 시간이 될 경우
                                //    {
                                //        transform.position = new Vector3(14, -5, 0);//원하는 위치에 등장
                                //        transform.position = transform.up * Time.deltaTime * 10;//돌진
                                //        if (transform.position.x < -5)//x좌표가 -5가 될 경우 즉 필드를 벗어날때
                                //        {
                                //            rushrush = true;
                                //        }
                                //    }
                                //}
                                #endregion
                            }
                        }
                        else if (rushattackcheck == true)//직접 공격
                        {
                            //Cosspattern4.SetActive(false);
                            if (rushrush == false)
                            {
                                //Maxattacktimer = 1;//공격이 다끝나고 남은 대기시간으로 활용
                                Maxattacktimer -= Time.deltaTime;//1초간 대기
                                if (Maxattacktimer <= 0)
                                {
                                    rushrush = true;
                                }
                            }
                            else if (rushrush == true)//보스가 직접 공격 할때
                            {
                                counterwait = true;
                                if (rushwait == false)//가로 공격
                                {
                                    float PosX = transform.position.x;
                                    if (PosX == 4.22f)
                                    {
                                        transform.position = new Vector3(14, -5, 0);//원하는 위치에 등장
                                    }

                                    transform.position = transform.position + new Vector3(-1, 0, 0) * Time.deltaTime * 30;
                                    #region
                                    //if(counterfaint == true)//카운터 공격이 닿을경우
                                    //{
                                    //    attackwait = false;
                                    //    Maxattacktimer = 2;
                                    //    waittimer = false;
                                    //    counterwait = false;
                                    //    return;
                                    //}
                                    #endregion
                                    if (transform.position.x < -5)//x좌표가 -5가 될 경우 즉 필드를 벗어날때
                                    {
                                        rushwait = true;
                                    }
                                }
                                else if (rushwait == true)//새로 공격 
                                {
                                    float PosY = transform.position.y;
                                    if (PosY == -5)
                                    {
                                        transform.position = new Vector3(4.5f, 2.5f, 0);//원하는 위치 세로
                                    }

                                    transform.position = transform.position + new Vector3(0, -1, 0) * Time.deltaTime * 30;
                                    #region
                                    //if (counterfaint == true)//카운터 공격이 닿을경우
                                    //{
                                    //    attackwait = false;
                                    //    Maxattacktimer = 2;
                                    //    waittimer = false;
                                    //    counterwait = false;
                                    //    rushtime = 0;
                                    //    rushattackcheck = false;
                                    //    rushwait = false;
                                    //    rushrush = false;
                                    //    afterattackcheck = false;
                                    //    return;
                                    //}
                                    #endregion
                                    if (transform.position.y < -13)
                                    {
                                        attackwait = false;
                                        Maxattacktimer = 2;
                                        waittimer = false;
                                        counterwait = false;
                                        afterattackcheck = true;
                                        transform.position = new Vector3(4.4f, -5, 0);
                                        //patterncount = Random.Range(0, 3);
                                    }
                                }
                            }
                        }
                    }
                }
                else if (afterattackcheck == true)
                {
                    Maxafterattack -= Time.deltaTime;
                    if (Maxafterattack <= 0)
                    {
                        rushtime = 0;
                        rushattackcheck = false;
                        rushwait = false;
                        rushrush = false;
                        Maxafterattack = afterattack;
                        afterattackcheck = false;
                        patterncount = Random.Range(0, 1);
                    }
                }


            }//보스 패턴3 돌진공격
        }
    }

    //보스 HP관리 코드 짜기
    private void hpcommand()//일반적인 공격일 경우
    {
        if (beatendamage == true && magicchek == false)
        {
            weapondamage = GameManager.Instance.Weaponcheck.AttackdamageMax;
            if (counterfaint == true)
            {
                MaxbossHP -= weapondamage * 3;
            }
            else
            {
                MaxbossHP -= weapondamage;
            }
            Debug.Log(MaxbossHP);
        }

    }

    private void destrymagic()//마법 공격에 닿았을 경우
    {
        if(magicchek == true)
        {
            if (Maxretime <= 0)
            {
                Maxretime = retime;
            }
            while (Maxretime == retime)
            {
                weapondamage = GameManager.Instance.Weaponcheck.AttackdamageMax;
                if(counterfaint == true)
                {
                    MaxbossHP -= weapondamage * 3;
                }
                else
                {
                    MaxbossHP -= weapondamage;
                }
                break;
            }
            Maxretime -= Time.deltaTime;
        }
        
    }

    private void destroymonster()//보스가 죽는 경우
    {
        if (MaxbossHP <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            beatendamage = false;
        }
    }


    private void counterdamage()//카운터 공격에 맞았을시
    {
        if(counterfaint == true && rushcountertimecheck == false)//카운터 공격에 맞을때
        {
            Maxcountertimer -= Time.deltaTime;
            if (Maxcountertimer <= 0)
            {
                counterfaint = false;
                attackwait = false;
                rushwait = false;
                Maxcountertimer = countertimer;
                if(patterncount == 1)//왼쪽에서 공격하는 코드
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }

        }
    }


    private void rushcounter()//스킬 3번에서 카운터를 맞았을 경우
    {
        if(patterncount == 2 && counterfaint == true)
        {
            rushcountertimecheck = true;
            //카운터 될때 다시 초기화 시키는 부분
            //attackwait = false;
            Maxattacktimer = 2;
            waittimer = false;
            counterwait = false;
            rushtime = 0;
            rushattackcheck = false;
            rushrush = false;
            afterattackcheck = false;
            //=================================
            if (rushwait == false)//가로 공격 일때
            {
                transform.position = transform.position + new Vector3(1, 0, 0) * Time.deltaTime * 40;
                if (transform.position.x >= 10.5)
                {
                    transform.position = new Vector3(10.5f, -5, 0);
                    rushcountertimecheck = false;
                    return;
                }
            }
            else if(rushwait == true)//세로 공격일때
            {
                transform.position = transform.position + new Vector3(0, +1, 0) * Time.deltaTime * 30;
                if(transform.position.y >= 0.2f)
                {
                    transform.position = new Vector3(4.5f, 0.2f, 0);
                    rushcountertimecheck = false;
                    return;
                }
            }

        }
    }

    private void countertime()
    {
        if(counterfaint == true)
        {
            if (patterncount == 0)//패턴이 0번 이면서 카운터가 활성화 되었을때
            {
                //카운터 될시 초기화 시키는 부분
                //attackwait = false;//랜덤숫자를 넣는코드 -> 카운터 끝나고 실행
                Maxwaittime = waittime;//대기시간 다시 설정
                waittimer = false;//대기시간 다시 활성화
                counterwait = false;
                Maxattacktimer = 2;
                pattern1.SetActive(false);//공격 범위를 삭제
                //======================================================
            }
            else if (patterncount == 1)
            {
                Maxwaittime = waittime;//대기시간 다시 설정
                waittimer = false;//대기시간 다시 활성화
                counterwait = false;
                Maxattacktimer = 2;
                pattern2.SetActive(false);
            }
        }
       
    }

    private void boosattacktimer()//보스가 1스킬 혹은 2스킬을 쓸때 생기는 오브젝트를 다룸
    {
        if(skillcheck == true)
        {
            if(patterncount == 0)//오른쪽 공격
            {
                if(skillattack <= 0)
                {
                    skillcheck = false;
                    skillattack = 0.25f;
                    rightattack.SetActive(false);
                }
                else
                {
                    rightattack.SetActive(true);
                    skillattack -= Time.deltaTime;
                }
            }
            else if(patterncount == 1)
            {
                if (skillattack <= 0)
                {
                    skillcheck = false;
                    skillattack = 0.25f;
                    leftattack.SetActive(false);
                }
                else
                {
                    leftattack.SetActive(true);
                    skillattack -= Time.deltaTime;
                }
            }
        }
    }
}
