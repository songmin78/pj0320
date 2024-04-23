using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [Header("보스 능력치")]
    [SerializeField] float bossHp;//보스 HP
    float MaxbossHP;

    [SerializeField] GameObject pattern1;//공격패턴1
    [SerializeField] GameObject pattern2;//공격패턴2
    [SerializeField] GameObject Cosspattern3;//공격 패턴3-1
    [SerializeField] GameObject Cosspattern4;//공격 패턴3-2
    [SerializeField]int patterncount;//랜덤한 패턴을 넣는다
    int lastpatterncount;//마지막 숫자를 넣어 동일한 숫자를 나오는거 방지함
    [SerializeField]float Maxattacktimer;//공격 쿨타임
    float attacktimer = 2;
    float waittime = 1;//1초간 대기
    [SerializeField]float Maxwaittime;//1초간 대기
    bool waittimer;// 1초간 대기
    bool attackwait;//공격하는중
    [SerializeField] float hitendwait = 4f;//공격한 후 대기 시간
    float Maxhitendwait;
    bool hitwaitcheck;//끝나고 대기시간 확인
    float rushtime;
    float Maxrushtime = 1;
    [Header("카운터 부분")]
    bool counterwait = false;//공격대기상태 이때 카운터 공격을 맞으면 기절함
    bool counterfaint = false;//카운터 공격에 맞았을때 true로 변환
    [Header("공격 부분")]
    [SerializeField] Image horizonalrush;
    [SerializeField] Image horizonalrush1;
    [SerializeField] Image verticalrush;
    [SerializeField] Image verticalrush1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Weaponcheck weapon = collision.gameObject.GetComponent<Weaponcheck>();//무기 gameobject가 몬스터 collision에 닿을때 

        if(counterwait == true && weapon)
        {
            counterfaint = true;
        }
    }

    private void Awake()
    {
        Maxattacktimer = attacktimer;
        MaxbossHP = bossHp;
        Maxwaittime = waittime;
        Maxhitendwait = hitendwait;
    }

    void Start()
    {
        patterncount = Random.Range(0, 3);
        pattern1.SetActive(false);
        pattern2.SetActive(false);
        Cosspattern3.SetActive(false);
        Cosspattern4.SetActive(false);
    }

    void Update()
    {
        Randomcheck();
        Bosspattern();
    }

    private void Randomcheck()
    {
        if (attackwait == false)
        {
            if (lastpatterncount == patterncount)//만약 1의 변수랑 2의 변수가 같을경우
            {
                {
                    patterncount = Random.Range(0, 3);//2의 변수를 변화시킨다
                    Randomcheck();//다시 적용
                }
            }
            else
            {
                lastpatterncount = patterncount;//다른 숫자일 경우 마지막숫자르  다시 변경함
            }
        }
    }

    private void Bosspattern()
    {
        if(counterfaint == false)
        {
            if (patterncount == 0)//보스 패턴1
            {
                attackwait = true;//랜덤 숫자를 계속 넣는것을 방지
                transform.position = new Vector3(10.5f, -5, 0);//공격을 할 자리(오른쪽 끝)
                if(waittimer == false)//대기시간이 1번만 돌아가도록 만듬
                {
                    Maxwaittime -= Time.deltaTime;//도착하면 바로 공격이 아닌 1초 기다린다
                    if(Maxwaittime <= 0)
                    {
                        waittimer = true;//대기시간이 다시 안쓰이게 설정
                    }
                }
                else if(waittimer == true)
                {
                    pattern1.SetActive(true);//공격범위가 켜진다
                    counterwait = true;//공격 모션을  true
                    if (Maxattacktimer <= 0)//공격 대기모션이 0보다 작을때 공격
                    {
                        attackwait = false;//랜덤숫자를 넣는코드
                        pattern1.SetActive(false);//공격 범위를 삭제
                        Maxwaittime = waittime;//대기시간 다시 설정
                        waittimer = false;//대기시간 다시 활성화
                        Maxattacktimer = 2;//공격 대기시간 설정
                        counterwait = false; //카운터공격 들어오는것을 방지
                        //hitwaitcheck = true;
                        patterncount = Random.Range(0, 3);//랜덤숫자 돌리기
                    }
                    else if (Maxattacktimer > 0)//0이 아닐때
                    {
                        Maxattacktimer -= Time.deltaTime;//공격모션을 대기 초가 다 되면 공격
                        if (counterfaint == true)//카운터 
                        {
                            pattern1.SetActive(false);
                            counterwait = false;
                            Maxattacktimer = 2;
                            return;
                        }
                    }
                }
                

            }//보스 패턴 1 오른쪽에서 칼 공격
            else if (patterncount == 1)//보스 패턴 2
            {
                attackwait = true;
                transform.localScale = new Vector3(-1, 1, 1);//반대 방향으로 바꾼코드
                transform.position = new Vector3(-1.5f, -5, 0);//공격을 할 자리(오른쪽 끝)
                if (waittimer == false)
                {
                    Maxwaittime -= Time.deltaTime;//도착하면 바로 공격이 아닌 1초 기다린다
                    if (Maxwaittime <= 0)
                    {
                        waittimer = true;
                    }
                }
                else if(waittimer == true)
                {
                    pattern2.SetActive(true);//공격범위가 켜진다
                    counterwait = true;//공격 모션을  true
                    if (Maxattacktimer <= 0)//공격 대기모션이 0보다 작을때
                    {
                        attackwait = false;
                        pattern2.SetActive(false);
                        Maxwaittime = waittime;
                        waittimer = false;
                        Maxattacktimer = 2;
                        counterwait = false;
                        //hitwaitcheck = true;
                        transform.localScale = new Vector3(1, 1, 1);
                        patterncount = Random.Range(0, 3);
                    }
                    else if (Maxattacktimer > 0)//0이 아닐때
                    {
                        Maxattacktimer -= Time.deltaTime;//공격모션을 대기 초가 다 되면 공격
                        if (counterfaint == true)
                        {
                            pattern2.SetActive(false);
                            counterwait = false;
                            Maxattacktimer = 2;
                            return;
                        }
                    }
                }

            }//보스 패턴2 왼쪽에서 칼 공격
            else if (patterncount == 2)
            {
                attackwait = true;
                if (waittimer == false)
                {
                    //Maxwaittime -= Time.deltaTime;//도착하면 바로 공격이 아닌 1초 기다린다
                    rushtime += Time.deltaTime;
                    //if(rushtime >= 0.7f)
                    //{
                    //    rushtime = 0.7f;
                    //}
                    Cosspattern3.SetActive(true);
                    horizonalrush.fillAmount = rushtime / Maxrushtime;
                    horizonalrush1.fillAmount = rushtime / Maxrushtime;
                    if (rushtime >= 1)
                    {
                        waittimer = true;
                        rushtime = 0;
                    }
                }
                else if (waittimer == true)
                {
                    Cosspattern3.SetActive(false);
                    rushtime += Time.deltaTime;
                    Cosspattern4.SetActive(true);
                    verticalrush.fillAmount = rushtime / Maxrushtime;
                    verticalrush1.fillAmount = rushtime / Maxrushtime;

                }
            }//보스 패턴3 돌진공격
            else if(patterncount == 3)
            {
                return;
            }




        }
    }
}
