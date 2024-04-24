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
        Maxafterattack = afterattack;
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
        Randomcheck();
        Bosspattern();
    }

    private void Randomcheck()
    {
        if (attackwait == false && afterattackcheck == false)
        {
            if (lastpatterncount == patterncount)//만약 1의 변수랑 2의 변수가 같을경우
            {
                if(returnpattrn > 2)
                {
                    patterncount = 2;
                }
                else
                {
                    patterncount = Random.Range(0, 2);//2의 변수를 변화시킨다
                }
                Randomcheck();//다시 적용
            }
            else
            {
                lastpatterncount = patterncount;//다른 숫자일 경우 마지막숫자르  다시 변경함
                if(lastpatterncount == 0 || lastpatterncount == 1)
                {
                    returnpattrn += 1;
                }
                else if(lastpatterncount == 2)
                {
                    returnpattrn = 0;
                }
            }
        }
    }

    private void Bosspattern()
    {
        if(counterfaint == false)
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
                        counterwait = true;//공격 모션을  true
                        if (Maxattacktimer <= 0)//공격 대기모션이 0보다 작을때 공격
                        {
                            attackwait = false;//랜덤숫자를 넣는코드
                            Maxwaittime = waittime;//대기시간 다시 설정
                            waittimer = false;//대기시간 다시 활성화
                            Maxattacktimer = 2;//공격 대기시간 설정
                            counterwait = false; //카운터공격 들어오는것을 방지
                            afterattackcheck = true;//공격후에  있는 대기 시간
                            //hitwaitcheck = true;
                            //patterncount = Random.Range(0, 3);//랜덤숫자 돌리기
                            pattern1.SetActive(false);//공격 범위를 삭제
                        }
                        else if (Maxattacktimer > 0)//0이 아닐때
                        {
                            Maxattacktimer -= Time.deltaTime;//공격모션을 대기 초가 다 되면 공격
                            if (counterfaint == true)//카운터 
                            {
                                counterwait = false;
                                Maxattacktimer = 2;
                                pattern1.SetActive(false);//공격 범위를 삭제
                                return;
                            }
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
                        patterncount = Random.Range(0, 3);
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
                            pattern2.SetActive(false);//공격 범위를 삭제
                        }
                        else if (Maxattacktimer > 0)//0이 아닐때
                        {
                            Maxattacktimer -= Time.deltaTime;//공격모션을 대기 초가 다 되면 공격
                            if (counterfaint == true)//카운터 
                            {
                                counterwait = false;
                                Maxattacktimer = 2;
                                pattern2.SetActive(false);
                                return;
                            }
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
                        patterncount = Random.Range(0,3);
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
                        Debug.Log(rushtime);
                        rushtime += Time.deltaTime;//공격루트를 알려주는 시간 코드
                        Cosspattern3.SetActive(true);//공격루트 오브젝트를 보여준다
                        horizonalrush.fillAmount = rushtime / Maxrushtime;//왜각 공격루트
                        horizonalrush1.fillAmount = rushtime / Maxrushtime;//직접 공격할 공격루트
                        if (rushtime >= 0.78f)//1이 된다면
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
                            if (rushtime >= 0.78f)//공격루트를 다 보여준다면
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
                        else if (rushattackcheck == true)
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
                                if (counterwait == false)//카운터 공격에 맞았을 경우
                                {
                                    return;
                                }
                                if (rushwait == false)//가로 공격
                                {
                                    float PosX = transform.position.x;
                                    if (PosX == 4.22f)
                                    {
                                        transform.position = new Vector3(14, -5, 0);//원하는 위치에 등장
                                    }

                                    transform.position = transform.position + new Vector3(-1, 0, 0) * Time.deltaTime * 30;
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
}
