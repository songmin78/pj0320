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

    [SerializeField] public GameObject PlayerUI;
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
}
