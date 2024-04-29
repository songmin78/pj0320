using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static GameManager Instance2;
    

    private Player player;
    private Weaponcheck weaponcheck;
    private HitboxMonster hitboxMonster;
    private Buttonmanager buttonmanager;
    private CheckBox checkbox;
    private Monsterattack monsterattack;
    private MonsterMoving monstermoving;
    private Boss boss;

    //bool spawncheck;
    [SerializeField]int spawnnumber;
    float spawncount = 1f;
    [SerializeField]float Maxspawncount;
    bool spawmonster;
    bool hitmonstercheck;

    [SerializeField]  public GameObject PlayerUI;
    [SerializeField] int trapnumber;
    [Header("소환될 몬스터")]
    [SerializeField] public GameObject monster;
    public Player Player
    {
        get { return player; }
        set { player = value; }
    }

    public Weaponcheck Weaponcheck
    {
        get { return weaponcheck; }
        set { weaponcheck = value; }
    }

    public HitboxMonster HitboxMonster
    {
        get { return hitboxMonster; }
        set { hitboxMonster = value; }
    }

    public Buttonmanager Buttonmanager
    {
        get { return buttonmanager; }
        set { buttonmanager = value; }
    }

    public CheckBox CheckBox
    {
        get { return checkbox; }
        set { checkbox = value; }
    }

    public Monsterattack Monsterattack
    {
        get { return monsterattack; }
        set { monsterattack = value; }
    }

    public MonsterMoving MonsterMoving
    {
        get { return monstermoving; }
        set { monstermoving = value; }
    }

    public Boss Boss
    {
        get { return boss; }
        set { boss = value; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Maxspawncount = spawncount;
    }

    private void Update()
    {
        //MonsterTimer();
        //spawnnumbercheck();
        //recallfirstmonster();
    }

    public void traprecall(int _trapnumber)
    {
        if(_trapnumber == 1)
        {
            //spawmonster = true;
            recallfirstmonster();
        }
        else if(_trapnumber == 2)
        {
            //spawmonster = true;
            recallsecoundmonster();
        }
    }


    public void recallfirstmonster()//첫번째 트랩일 경우
    {

        #region
        //for (int number = 0; number <= 5; number++)
        //{
        //    spawncheck = true;
        //    hitmonstercheck = true;
        //    if (Maxspawncount != spawncount)
        //    {
        //        return;
        //    }
        //    Vector3 spawnPos = new Vector3(33.5f, -22.5f, 0);
        //    Instantiate(monster, spawnPos, Quaternion.identity);
        //    MonsterTimer();

        //    //recallmonster recallmonster = GetComponent<recallmonster>();
        //    //recallmonster.Onofftrapswich();
        //}
        ////if (spawnnumber >= 5)
        ////{
        ////    spawnnumber = 0;
        ////    traprecall(0);
        ////}

        ////recallmonster recallmonster = GetComponent<recallmonster>();
        ////recallmonster.Onofftrapswich();
        #endregion

        //spawncheck = true;
        //if (Maxspawncount != spawncount)
        //{
        //    return;
        //}

        hitmonstercheck = true;
        Vector3 spawnPos = new Vector3(33.5f, -22.5f, 0);
        Instantiate(monster, spawnPos, Quaternion.identity);
        //MonsterTimer();
        //recallmonster recallmonster = obj.GetComponent<recallmonster>();
        //recallmonster.Onofftrapswich(spawnnumber);


    }

    public void recallsecoundmonster()//두번째 트랩일 경우
    {
        #region
        //for (int number = 0; number <= 5; number++)
        //{
        //    Vector3 spawnPos = new Vector3(49, -4.5f, 0);
        //    Instantiate(monster, spawnPos, Quaternion.identity);
        //}
        //recallmonster recallmonster = GetComponent<recallmonster>();
        //recallmonster.Onofftrapswich(spawnnumber);
        #endregion

        hitmonstercheck = true;
        Vector3 spawnPos = new Vector3(49, -4.5f, 0);
        Instantiate(monster, spawnPos, Quaternion.identity);
    }

    //public void MonsterTimer()
    //{
    //    if(spawncheck == true)
    //    {
    //        Maxspawncount -= Time.deltaTime;
    //        if (Maxspawncount <= 0)
    //        {
    //            Maxspawncount = spawncount;
    //            spawnnumber += 1;
    //            //spawncheck = false;
    //        }
    //    }
        
    //}

    //private void spawnnumbercheck()
    //{
    //    if (spawnnumber >= 5)
    //    {
    //        spawmonster = false;
    //    }
    //}

}