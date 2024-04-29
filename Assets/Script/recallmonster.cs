using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recallmonster : MonoBehaviour
{

    [Header("몬스터 소환 위치")]
    [SerializeField] bool firsttrap;
    [SerializeField] bool secondtrap;
    int trapnumber = 0;
    int spawnnumber;//소환될 몬스터 수
    int Maxspawnnumber;//다른 구역마다 다르게 소환될 몬스터 수
    bool spawncheck;
    [Header("기타")]
    [SerializeField] float Maxspawncount;
    float spawncount = 1f;

    bool onrecalltrap;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")//만약에 닿은 오브젝트가 플레이어일 경우
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

    private void firstrecall()//트랩에 닿았을 경우
    {
        if (Maxspawncount != spawncount)
        {
            return;
        }
        if (firsttrap == true && onrecalltrap == true)//첫번째트랩에 닿을 경우
        {
            Maxspawnnumber = 5;
            trapnumber = 1;//첫번째 트랩일 경우
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
            trapnumber = 2;//두번째 트랩일 경우
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
