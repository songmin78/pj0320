using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class HitboxMonster : MonoBehaviour
{
    [SerializeField]GameObject parents;

    [Header("몬스터 스펙")]
    [SerializeField] public float attackdamage;//몬스터의 데미지
    [SerializeField] float MsGameHp = 1;//몬스터의 HP
    private float MsMaxHp;
    

    [Header("공격 여부")]
    bool beatendamage = false;
    public bool Oncheckdamage = false;
    float weapondamage = 0;

    [Header("기타")]
    [SerializeField] public bool magicchek = false;
    float retime = 0.5f;
    float Maxretime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Weaponcheck weapon = collision.gameObject.GetComponent<Weaponcheck>();//무기 gameobject가 몬스터 collision에 닿을때 
        Player player = collision.gameObject.GetComponent<Player>();

        if(weapon)
        {
            beatendamage = true;
            if(GameManager.Instance.Weaponcheck.magic == true)//만약에 마법에 닿았을경우
            {
                magicchek = true;
            }
        }

        if(collision.gameObject.tag == "Player")
        {
            Oncheckdamage = true;
        }
        else
        {
            Oncheckdamage = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(magicchek == true)
        {
            magicchek = false;
        }
    }


    //만약에 magic이 true일때 만나면 어떤값을 true 이후에 update에서 어떤값이 true일때 데미지를 입고 0.?초간 무적 이후에 또 피해를 입는다.


    private void Awake()
    {
        Maxretime = retime;
        MsMaxHp = MsGameHp;
    }
    private void Start()
    {
        GameManager.Instance.HitboxMonster = this;
    }

    void Update()
    {
        Hitboxmaonster();
        destroymonster();
        destroymagic();
    }

    private void Hitboxmaonster()
    {
        if(beatendamage == true && magicchek == false)
        {
            weapondamage = GameManager.Instance.Weaponcheck.AttackdamageMax;
            MsMaxHp -= weapondamage;
        }
    }

    private void destroymonster()
    {
        if(MsMaxHp <= 0)
        {
            Destroy(parents);
        }
        else
        {
            beatendamage = false;
        }
    }

    private void destroymagic()//마법 공격이 닿았을 경우
    {
        //if(magicchek == true)
        //{
        //    weapondamage = GameManager.Instance.Weaponcheck.AttackdamageMax;
        //    MaxHp -= weapondamage;
        //    //retime -= Time.deltaTime;
        //}

        if(magicchek == true)
        {
            if(Maxretime <= 0)
            {
                Maxretime = retime;
            }
            while (Maxretime == retime)
            {
                weapondamage = GameManager.Instance.Weaponcheck.AttackdamageMax;
                MsMaxHp -= weapondamage;
                break;
            }
            Maxretime -= Time.deltaTime;
            
        }

    }


}
