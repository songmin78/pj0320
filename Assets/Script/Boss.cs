using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    Animator animator;

    [Header("���� �ɷ�ġ")]
    [SerializeField] float bossHp;//���� HP
    float MaxbossHP;

    [SerializeField] GameObject pattern1;//��������1
    [SerializeField] GameObject pattern2;//��������2
    [SerializeField] GameObject Cosspattern3;//���� ����3-1
    [SerializeField] GameObject Cosspattern4;//���� ����3-2
    [SerializeField]int patterncount;//������ ������ �ִ´�
    [SerializeField]int lastpatterncount;//������ ���ڸ� �־� ������ ���ڸ� �����°� ������
    [SerializeField]float returnpattrn;
    [SerializeField]float Maxattacktimer;//���� ��Ÿ��
    float attacktimer = 2;
    float waittime = 1f;//1�ʰ� ���
    [SerializeField]float Maxwaittime;//1�ʰ� ���
    bool waittimer;// 1�ʰ� ���
    bool attackwait;//�����ϴ���
    [SerializeField] float hitendwait = 4f;//������ �� ��� �ð�
    float Maxhitendwait;
    bool hitwaitcheck;//������ ���ð� Ȯ��
    float rushtime;
    float Maxrushtime = 0.8f;
    bool rushrush = false;
    bool rushattackcheck;
    bool rushwait;
    bool afterattackcheck;
    float afterattack = 2;//���� ���Ŀ� ���Ÿ�� �ð�
    [SerializeField]float Maxafterattack;
    //[SerializeField, Range(0,1)]float randomtimer = 0.5f;//���� ���� ������ ��� �ð�
    //[SerializeField]float Maxrandomtimer;
    [Header("ī���� �κ�")]
    [SerializeField]bool counterwait = false;//���ݴ����� �̶� ī���� ������ ������ ������
    bool counterfaint = false;//ī���� ���ݿ� �¾����� true�� ��ȯ
    [SerializeField]float countertimer = 5;//ī���Ͱ��ݿ� �¾����� ����� ����ȭ �ð�
    float Maxcountertimer;
    bool rushcountertimecheck;//�з����� ī���ͱ��� �ð��� �� �帣�� ����
    [Header("1,2��ų ���� �κ�")]
    [SerializeField] GameObject rightattack;
    [SerializeField] GameObject leftattack;
    float skillattack = 0.25f;//0.25�ʸ�ŭ �����ϵ��� ����
    bool skillcheck;//������ �����Ҷ� true
    [Header("3��ų ���� �κ�")]
    [SerializeField] Image horizonalrush;
    [SerializeField] Image horizonalrush1;
    [SerializeField] Image verticalrush;
    [SerializeField] Image verticalrush1;
    [Header("���� ����")]
    bool beatendamage = false;
    bool magicchek;//���������� �������� �ƴ��� Ȯ��
    float retime = 0.5f;
    float Maxretime;
    //public bool Oncheckdamage = false;
    float weapondamage = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Weaponcheck weapon = collision.gameObject.GetComponent<Weaponcheck>();//���� gameobject�� ���� collision�� ������ 
        //Debug.Log(collision.name);
        Weaponcheck weapon = collision.gameObject.GetComponent<Weaponcheck>();

        if (weapon != null && counterwait == true && weapon.Counter == true)
        {
            counterfaint = true;
        }
        

        #region layer����ũ �κ��� ����
        //layer�� int������ �ۿ�  �� ������
        //layer���� �۷ε� layer�� ������ �÷��� LayerMask.NameToLayer�� Ȱ���� �̸��� �����´�
        //LayerMask�� �������� layer�� �����ü��ְ� ���ִ� �ڵ��̴�
        //if (collision.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        //{
        //    Debug.Log("1");
        //}
        #endregion

        #region 1��°�Ŷ� �����
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
        //������ �����ϴ� �κ�
        Randomcheck();
        Bosspattern();
        boosattacktimer();

        //���� HP������
        hpcommand();
        destrymagic();
        destroymonster();

        //����ī���� �κ�
        countertime();//�¿� ������  ī���� �ɶ� üũ
        rushcounter();//3�� ��ų�� ī���� �ɶ� üũ
        counterdamage();//ī���� ���� �ð�
    }

    private void Randomcheck()
    {
        if (attackwait == false && afterattackcheck == false)
        {
            #region
            if (lastpatterncount == patterncount)//���� 1�� ������ 2�� ������ �������
            {
                if (returnpattrn > 2)
                {
                    patterncount = 2;
                }
                else
                {
                    patterncount = Random.Range(0, 2);//2�� ������ ��ȭ��Ų��
                }
                Randomcheck();//�ٽ� ����
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
                    lastpatterncount = patterncount;//�ٸ� ������ ��� ���������ڸ�  �ٽ� ������
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
            #region �ݺ��� while�� ���� �׽�Ʈ�ܰ�
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
            //        //�ٸ� ������ ��� ���������ڸ�  �ٽ� ������
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
        if(counterfaint == false)//ī���� ���ݿ� �� �¾�����
        {
            if (patterncount == 0)//���� ����1
            {
                if (afterattackcheck == false)
                {
                    attackwait = true;//���� ���ڸ� ��� �ִ°��� ����
                    transform.position = new Vector3(10.5f, -5, 0);//������ �� �ڸ�(������ ��)
                    if (waittimer == false)//���ð��� 1���� ���ư����� ����
                    {
                        Maxwaittime -= Time.deltaTime;//�����ϸ� �ٷ� ������ �ƴ� 1�� ��ٸ���
                        if (Maxwaittime <= 0)
                        {
                            waittimer = true;//���ð��� �ٽ� �Ⱦ��̰� ����
                        }
                    }
                    else if (waittimer == true)
                    {
                        pattern1.SetActive(true);//���ݹ����� ������
                        counterwait = true;//ī������ ���ϱ� ���ؼ� true�� ��ȯ
                        if (Maxattacktimer <= 0)//���� ������� 0���� ������ ����
                        {
                            attackwait = false;//�������ڸ� �ִ��ڵ�
                            Maxwaittime = waittime;//���ð� �ٽ� ����
                            waittimer = false;//���ð� �ٽ� Ȱ��ȭ
                            Maxattacktimer = 2;//���� ���ð� ����
                            counterwait = false; //ī���Ͱ��� �����°��� ����
                            afterattackcheck = true;//�����Ŀ�  �ִ� ��� �ð�
                            skillcheck = true;
                            //hitwaitcheck = true;
                            //patterncount = Random.Range(0, 3);//�������� ������
                            pattern1.SetActive(false);//���� ������ ����
                        }
                        else if (Maxattacktimer > 0)//0�� �ƴҶ�
                        {
                            Maxattacktimer -= Time.deltaTime;//���ݸ���� ��� �ʰ� �� �Ǹ� ������ �ϱ����� ��
                            #region
                            //if (counterfaint == true)//ī���� 
                            //{
                            //    attackwait = false;//�������ڸ� �ִ��ڵ�
                            //    Maxwaittime = waittime;//���ð� �ٽ� ����
                            //    waittimer = false;//���ð� �ٽ� Ȱ��ȭ
                            //    counterwait = false;
                            //    Maxattacktimer = 2;
                            //    pattern1.SetActive(false);//���� ������ ����
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


            }//���� ���� 1 �����ʿ��� Į ����
            else if (patterncount == 1)//���� ���� 2
            {
                if (afterattackcheck == false)
                {
                    attackwait = true;//���� ���ڸ� ��� �ִ°��� ����
                    transform.localScale = new Vector3(-1, 1, 1);//�ݴ� �������� �ٲ��ڵ�
                    transform.position = new Vector3(-1.5f, -5, 0);//������ �� �ڸ�(���� ��)
                    if (waittimer == false)//���ð��� 1���� ���ư����� ����
                    {
                        Maxwaittime -= Time.deltaTime;//�����ϸ� �ٷ� ������ �ƴ� 1�� ��ٸ���
                        if (Maxwaittime <= 0)
                        {
                            waittimer = true;//���ð��� �ٽ� �Ⱦ��̰� ����
                        }
                    }
                    else if (waittimer == true)
                    {
                        pattern2.SetActive(true);//���ݹ����� ������
                        counterwait = true;//���� �����  true
                        if (Maxattacktimer <= 0)//���� ������� 0���� ������ ����
                        {
                            attackwait = false;//�������ڸ� �ִ��ڵ�
                            Maxwaittime = waittime;//���ð� �ٽ� ����
                            waittimer = false;//���ð� �ٽ� Ȱ��ȭ
                            Maxattacktimer = 2;//���� ���ð� ����
                            counterwait = false; //ī���Ͱ��� �����°��� ����
                            afterattackcheck = true;//�����Ŀ�  �ִ� ��� �ð�
                            skillcheck = true;
                            pattern2.SetActive(false);//���� ������ ����
                        }
                        else if (Maxattacktimer > 0)//0�� �ƴҶ�
                        {
                            Maxattacktimer -= Time.deltaTime;//���ݸ���� ��� �ʰ� �� �Ǹ� ����
                            #region
                            //if (counterfaint == true)//ī���� 
                            //{
                            //    attackwait = false;//�������ڸ� �ִ��ڵ�
                            //    Maxwaittime = waittime;//���ð� �ٽ� ����
                            //    waittimer = false;//���ð� �ٽ� Ȱ��ȭ
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
            }//���� ����2 ���ʿ��� Į ����
            else if (patterncount == 2)//���� ���� 3
            {
                if(afterattackcheck == false)
                {
                    attackwait = true;
                    if (waittimer == false && rushattackcheck == false)
                    {
                        transform.position = new Vector3(4.22f, 8.85f, 0);//ȭ�鿡�� �������
                        //Maxwaittime -= Time.deltaTime;//�����ϸ� �ٷ� ������ �ƴ� 1�� ��ٸ���
                        //Debug.Log(rushtime);
                        rushtime += Time.deltaTime;//���ݷ�Ʈ�� �˷��ִ� �ð� �ڵ�
                        Cosspattern3.SetActive(true);//���ݷ�Ʈ ������Ʈ�� �����ش�
                        horizonalrush.fillAmount = rushtime / Maxrushtime;//�ְ� ���ݷ�Ʈ
                        horizonalrush1.fillAmount = rushtime / Maxrushtime;//���� ������ ���ݷ�Ʈ
                        if (rushtime >= 0.79f)//1�� �ȴٸ�
                        {
                            waittimer = true;//1���� �����ϴ� �ڵ带 True
                            rushtime = 0;//�ð��ڵ� �ʱ�ȭ
                        }
                    }
                    else if (waittimer == true)
                    {
                        if (rushattackcheck == false)
                        {
                            Cosspattern3.SetActive(false);//���� ���ݷ�Ʈ ����
                            rushtime += Time.deltaTime;
                            Cosspattern4.SetActive(true);//���� ���� ��Ʈ�� �����ش�
                            verticalrush.fillAmount = rushtime / Maxrushtime;//�ְ�
                            verticalrush1.fillAmount = rushtime / Maxrushtime;//�߾�
                            if (rushtime >= 0.79f)//���ݷ�Ʈ�� �� �����شٸ�
                            {
                                rushattackcheck = true;
                                Maxattacktimer = 1;//������ �ٳ����� ���� ���ð����� Ȱ��
                                Cosspattern4.SetActive(false);
                                //Bosspattern();
                                #region
                                //Cosspattern4.SetActive(false);
                                //if (rushrush == false)//�ѹ��� �����ϰ� ����
                                //{
                                //    Cosspattern4.SetActive(false); //���ݷ�Ʈ ����

                                //    Maxwaittime -= Time.deltaTime;//1�ʰ� ���
                                //    if (Maxwaittime <= 0)//��� �ð��� �� ���
                                //    {
                                //        transform.position = new Vector3(14, -5, 0);//���ϴ� ��ġ�� ����
                                //        transform.position = transform.up * Time.deltaTime * 10;//����
                                //        if (transform.position.x < -5)//x��ǥ�� -5�� �� ��� �� �ʵ带 �����
                                //        {
                                //            rushrush = true;
                                //        }
                                //    }
                                //}
                                #endregion
                            }
                        }
                        else if (rushattackcheck == true)//���� ����
                        {
                            //Cosspattern4.SetActive(false);
                            if (rushrush == false)
                            {
                                //Maxattacktimer = 1;//������ �ٳ����� ���� ���ð����� Ȱ��
                                Maxattacktimer -= Time.deltaTime;//1�ʰ� ���
                                if (Maxattacktimer <= 0)
                                {
                                    rushrush = true;
                                }
                            }
                            else if (rushrush == true)//������ ���� ���� �Ҷ�
                            {
                                counterwait = true;
                                if (rushwait == false)//���� ����
                                {
                                    float PosX = transform.position.x;
                                    if (PosX == 4.22f)
                                    {
                                        transform.position = new Vector3(14, -5, 0);//���ϴ� ��ġ�� ����
                                    }

                                    transform.position = transform.position + new Vector3(-1, 0, 0) * Time.deltaTime * 30;
                                    #region
                                    //if(counterfaint == true)//ī���� ������ �������
                                    //{
                                    //    attackwait = false;
                                    //    Maxattacktimer = 2;
                                    //    waittimer = false;
                                    //    counterwait = false;
                                    //    return;
                                    //}
                                    #endregion
                                    if (transform.position.x < -5)//x��ǥ�� -5�� �� ��� �� �ʵ带 �����
                                    {
                                        rushwait = true;
                                    }
                                }
                                else if (rushwait == true)//���� ���� 
                                {
                                    float PosY = transform.position.y;
                                    if (PosY == -5)
                                    {
                                        transform.position = new Vector3(4.5f, 2.5f, 0);//���ϴ� ��ġ ����
                                    }

                                    transform.position = transform.position + new Vector3(0, -1, 0) * Time.deltaTime * 30;
                                    #region
                                    //if (counterfaint == true)//ī���� ������ �������
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


            }//���� ����3 ��������
        }
    }

    //���� HP���� �ڵ� ¥��
    private void hpcommand()//�Ϲ����� ������ ���
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

    private void destrymagic()//���� ���ݿ� ����� ���
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

    private void destroymonster()//������ �״� ���
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


    private void counterdamage()//ī���� ���ݿ� �¾�����
    {
        if(counterfaint == true && rushcountertimecheck == false)//ī���� ���ݿ� ������
        {
            Maxcountertimer -= Time.deltaTime;
            if (Maxcountertimer <= 0)
            {
                counterfaint = false;
                attackwait = false;
                rushwait = false;
                Maxcountertimer = countertimer;
                if(patterncount == 1)//���ʿ��� �����ϴ� �ڵ�
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }

        }
    }


    private void rushcounter()//��ų 3������ ī���͸� �¾��� ���
    {
        if(patterncount == 2 && counterfaint == true)
        {
            rushcountertimecheck = true;
            //ī���� �ɶ� �ٽ� �ʱ�ȭ ��Ű�� �κ�
            //attackwait = false;
            Maxattacktimer = 2;
            waittimer = false;
            counterwait = false;
            rushtime = 0;
            rushattackcheck = false;
            rushrush = false;
            afterattackcheck = false;
            //=================================
            if (rushwait == false)//���� ���� �϶�
            {
                transform.position = transform.position + new Vector3(1, 0, 0) * Time.deltaTime * 40;
                if (transform.position.x >= 10.5)
                {
                    transform.position = new Vector3(10.5f, -5, 0);
                    rushcountertimecheck = false;
                    return;
                }
            }
            else if(rushwait == true)//���� �����϶�
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
            if (patterncount == 0)//������ 0�� �̸鼭 ī���Ͱ� Ȱ��ȭ �Ǿ�����
            {
                //ī���� �ɽ� �ʱ�ȭ ��Ű�� �κ�
                //attackwait = false;//�������ڸ� �ִ��ڵ� -> ī���� ������ ����
                Maxwaittime = waittime;//���ð� �ٽ� ����
                waittimer = false;//���ð� �ٽ� Ȱ��ȭ
                counterwait = false;
                Maxattacktimer = 2;
                pattern1.SetActive(false);//���� ������ ����
                //======================================================
            }
            else if (patterncount == 1)
            {
                Maxwaittime = waittime;//���ð� �ٽ� ����
                waittimer = false;//���ð� �ٽ� Ȱ��ȭ
                counterwait = false;
                Maxattacktimer = 2;
                pattern2.SetActive(false);
            }
        }
       
    }

    private void boosattacktimer()//������ 1��ų Ȥ�� 2��ų�� ���� ����� ������Ʈ�� �ٷ�
    {
        if(skillcheck == true)
        {
            if(patterncount == 0)//������ ����
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
