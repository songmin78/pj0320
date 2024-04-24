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
            if (lastpatterncount == patterncount)//���� 1�� ������ 2�� ������ �������
            {
                if(returnpattrn > 2)
                {
                    patterncount = 2;
                }
                else
                {
                    patterncount = Random.Range(0, 2);//2�� ������ ��ȭ��Ų��
                }
                Randomcheck();//�ٽ� ����
            }
            else
            {
                lastpatterncount = patterncount;//�ٸ� ������ ��� ���������ڸ�  �ٽ� ������
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
                        counterwait = true;//���� �����  true
                        if (Maxattacktimer <= 0)//���� ������� 0���� ������ ����
                        {
                            attackwait = false;//�������ڸ� �ִ��ڵ�
                            Maxwaittime = waittime;//���ð� �ٽ� ����
                            waittimer = false;//���ð� �ٽ� Ȱ��ȭ
                            Maxattacktimer = 2;//���� ���ð� ����
                            counterwait = false; //ī���Ͱ��� �����°��� ����
                            afterattackcheck = true;//�����Ŀ�  �ִ� ��� �ð�
                            //hitwaitcheck = true;
                            //patterncount = Random.Range(0, 3);//�������� ������
                            pattern1.SetActive(false);//���� ������ ����
                        }
                        else if (Maxattacktimer > 0)//0�� �ƴҶ�
                        {
                            Maxattacktimer -= Time.deltaTime;//���ݸ���� ��� �ʰ� �� �Ǹ� ����
                            if (counterfaint == true)//ī���� 
                            {
                                counterwait = false;
                                Maxattacktimer = 2;
                                pattern1.SetActive(false);//���� ������ ����
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
                            pattern2.SetActive(false);//���� ������ ����
                        }
                        else if (Maxattacktimer > 0)//0�� �ƴҶ�
                        {
                            Maxattacktimer -= Time.deltaTime;//���ݸ���� ��� �ʰ� �� �Ǹ� ����
                            if (counterfaint == true)//ī���� 
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
                        Debug.Log(rushtime);
                        rushtime += Time.deltaTime;//���ݷ�Ʈ�� �˷��ִ� �ð� �ڵ�
                        Cosspattern3.SetActive(true);//���ݷ�Ʈ ������Ʈ�� �����ش�
                        horizonalrush.fillAmount = rushtime / Maxrushtime;//�ְ� ���ݷ�Ʈ
                        horizonalrush1.fillAmount = rushtime / Maxrushtime;//���� ������ ���ݷ�Ʈ
                        if (rushtime >= 0.78f)//1�� �ȴٸ�
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
                            if (rushtime >= 0.78f)//���ݷ�Ʈ�� �� �����شٸ�
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
                        else if (rushattackcheck == true)
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
                                if (counterwait == false)//ī���� ���ݿ� �¾��� ���
                                {
                                    return;
                                }
                                if (rushwait == false)//���� ����
                                {
                                    float PosX = transform.position.x;
                                    if (PosX == 4.22f)
                                    {
                                        transform.position = new Vector3(14, -5, 0);//���ϴ� ��ġ�� ����
                                    }

                                    transform.position = transform.position + new Vector3(-1, 0, 0) * Time.deltaTime * 30;
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


            }//���� ����3 ��������
        }
    }
}
