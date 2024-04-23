using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [Header("���� �ɷ�ġ")]
    [SerializeField] float bossHp;//���� HP
    float MaxbossHP;

    [SerializeField] GameObject pattern1;//��������1
    [SerializeField] GameObject pattern2;//��������2
    [SerializeField] GameObject Cosspattern3;//���� ����3-1
    [SerializeField] GameObject Cosspattern4;//���� ����3-2
    [SerializeField]int patterncount;//������ ������ �ִ´�
    int lastpatterncount;//������ ���ڸ� �־� ������ ���ڸ� �����°� ������
    [SerializeField]float Maxattacktimer;//���� ��Ÿ��
    float attacktimer = 2;
    float waittime = 1;//1�ʰ� ���
    [SerializeField]float Maxwaittime;//1�ʰ� ���
    bool waittimer;// 1�ʰ� ���
    bool attackwait;//�����ϴ���
    [SerializeField] float hitendwait = 4f;//������ �� ��� �ð�
    float Maxhitendwait;
    bool hitwaitcheck;//������ ���ð� Ȯ��
    float rushtime;
    float Maxrushtime = 1;
    [Header("ī���� �κ�")]
    bool counterwait = false;//���ݴ����� �̶� ī���� ������ ������ ������
    bool counterfaint = false;//ī���� ���ݿ� �¾����� true�� ��ȯ
    [Header("���� �κ�")]
    [SerializeField] Image horizonalrush;
    [SerializeField] Image horizonalrush1;
    [SerializeField] Image verticalrush;
    [SerializeField] Image verticalrush1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Weaponcheck weapon = collision.gameObject.GetComponent<Weaponcheck>();//���� gameobject�� ���� collision�� ������ 

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
            if (lastpatterncount == patterncount)//���� 1�� ������ 2�� ������ �������
            {
                {
                    patterncount = Random.Range(0, 3);//2�� ������ ��ȭ��Ų��
                    Randomcheck();//�ٽ� ����
                }
            }
            else
            {
                lastpatterncount = patterncount;//�ٸ� ������ ��� ���������ڸ�  �ٽ� ������
            }
        }
    }

    private void Bosspattern()
    {
        if(counterfaint == false)
        {
            if (patterncount == 0)//���� ����1
            {
                attackwait = true;//���� ���ڸ� ��� �ִ°��� ����
                transform.position = new Vector3(10.5f, -5, 0);//������ �� �ڸ�(������ ��)
                if(waittimer == false)//���ð��� 1���� ���ư����� ����
                {
                    Maxwaittime -= Time.deltaTime;//�����ϸ� �ٷ� ������ �ƴ� 1�� ��ٸ���
                    if(Maxwaittime <= 0)
                    {
                        waittimer = true;//���ð��� �ٽ� �Ⱦ��̰� ����
                    }
                }
                else if(waittimer == true)
                {
                    pattern1.SetActive(true);//���ݹ����� ������
                    counterwait = true;//���� �����  true
                    if (Maxattacktimer <= 0)//���� ������� 0���� ������ ����
                    {
                        attackwait = false;//�������ڸ� �ִ��ڵ�
                        pattern1.SetActive(false);//���� ������ ����
                        Maxwaittime = waittime;//���ð� �ٽ� ����
                        waittimer = false;//���ð� �ٽ� Ȱ��ȭ
                        Maxattacktimer = 2;//���� ���ð� ����
                        counterwait = false; //ī���Ͱ��� �����°��� ����
                        //hitwaitcheck = true;
                        patterncount = Random.Range(0, 3);//�������� ������
                    }
                    else if (Maxattacktimer > 0)//0�� �ƴҶ�
                    {
                        Maxattacktimer -= Time.deltaTime;//���ݸ���� ��� �ʰ� �� �Ǹ� ����
                        if (counterfaint == true)//ī���� 
                        {
                            pattern1.SetActive(false);
                            counterwait = false;
                            Maxattacktimer = 2;
                            return;
                        }
                    }
                }
                

            }//���� ���� 1 �����ʿ��� Į ����
            else if (patterncount == 1)//���� ���� 2
            {
                attackwait = true;
                transform.localScale = new Vector3(-1, 1, 1);//�ݴ� �������� �ٲ��ڵ�
                transform.position = new Vector3(-1.5f, -5, 0);//������ �� �ڸ�(������ ��)
                if (waittimer == false)
                {
                    Maxwaittime -= Time.deltaTime;//�����ϸ� �ٷ� ������ �ƴ� 1�� ��ٸ���
                    if (Maxwaittime <= 0)
                    {
                        waittimer = true;
                    }
                }
                else if(waittimer == true)
                {
                    pattern2.SetActive(true);//���ݹ����� ������
                    counterwait = true;//���� �����  true
                    if (Maxattacktimer <= 0)//���� ������� 0���� ������
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
                    else if (Maxattacktimer > 0)//0�� �ƴҶ�
                    {
                        Maxattacktimer -= Time.deltaTime;//���ݸ���� ��� �ʰ� �� �Ǹ� ����
                        if (counterfaint == true)
                        {
                            pattern2.SetActive(false);
                            counterwait = false;
                            Maxattacktimer = 2;
                            return;
                        }
                    }
                }

            }//���� ����2 ���ʿ��� Į ����
            else if (patterncount == 2)
            {
                attackwait = true;
                if (waittimer == false)
                {
                    //Maxwaittime -= Time.deltaTime;//�����ϸ� �ٷ� ������ �ƴ� 1�� ��ٸ���
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
            }//���� ����3 ��������
            else if(patterncount == 3)
            {
                return;
            }




        }
    }
}
