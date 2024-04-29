using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recallmonster : MonoBehaviour
{

    [Header("���� ��ȯ ��ġ")]
    [SerializeField] bool firsttrap;
    [SerializeField] bool secondtrap;
    int trapnumber = 0;
    int spawnnumber;//��ȯ�� ���� ��
    int Maxspawnnumber;//�ٸ� �������� �ٸ��� ��ȯ�� ���� ��
    bool spawncheck;
    [Header("��Ÿ")]
    [SerializeField] float Maxspawncount;
    float spawncount = 1f;

    bool onrecalltrap;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")//���࿡ ���� ������Ʈ�� �÷��̾��� ���
        {
            onrecalltrap = true;
        }
    }

    private void Awake()
    {
        Maxspawncount = spawncount;
    }

    void Start()
    {
        
    }

    void Update()
    {
        firstrecall();
        secoundrecall();
        MonsterTimer();
        spawnnumbercheck();
    }

    private void firstrecall()//Ʈ���� ����� ���
    {
        if (Maxspawncount != spawncount)
        {
            return;
        }
        if (firsttrap == true && onrecalltrap == true)//ù��°Ʈ���� ���� ���
        {
            Maxspawnnumber = 5;
            trapnumber = 1;//ù��° Ʈ���� ���
            spawncheck = true;
            //spawnnumber += 1;
            //GameManager gamemanager = GetComponent<GameManager>();
            //gamemanager.traprecall(trapnumber);
            //firsttrap = false;
            GameManager.Instance.traprecall(trapnumber);
            MonsterTimer();
        }
    }

    private void secoundrecall()
    {
        if(Maxspawncount != spawncount)
        {
            return;
        }
        if(secondtrap == true && onrecalltrap == true)
        {
            Maxspawnnumber = 6;
            trapnumber = 2;//�ι�° Ʈ���� ���
            spawncheck = true;
            GameManager.Instance.traprecall(trapnumber);
            MonsterTimer();
        }
    }

    //public void Onofftrapswich(int _spawnnumber)
    //{
    //    if(_spawnnumber >= 5)
    //    {
    //        onrecalltrap = false;
    //    }
    //}

    public void MonsterTimer()
    {
        if (spawncheck == true)
        {
            Maxspawncount -= Time.deltaTime;
            if (Maxspawncount <= 0)
            {
                Maxspawncount = spawncount;
                spawnnumber += 1;
                //spawncheck = false;
            }
        }
    }

    private void spawnnumbercheck()
    {
        if (spawnnumber >= Maxspawnnumber)
        {
            onrecalltrap = false;
        }
    }
}
