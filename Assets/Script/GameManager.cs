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

    [SerializeField] public GameObject PlayerUI;
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
    }

    private void Update()
    {
        
    }


    public void recallfirstmonster()
    {
        for(int number = 0; number <= 5; number++)
        {
            Vector3 spawnPos = new Vector3(33.5f, -22.5f, 0);
            GameObject obj = Instantiate(monster, spawnPos, Quaternion.identity);
        }
    }
}
