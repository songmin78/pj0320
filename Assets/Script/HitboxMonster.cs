using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class HitboxMonster : MonoBehaviour
{
    [SerializeField]GameObject parents;

    [Header("몬스터 스펙")]
    [SerializeField] public float attackdamage;//몬스터의 데미지
    [SerializeField] float GameHp = 1;//몬스터의 HP
    private float MaxHp;
    

    [Header("공격 여부")]
    bool beatendamage = false;
    bool Oncheckdamage = false;
    float weapondamage = 0;

    [Header("기타")]
    [SerializeField] bool magicchek = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Weaponcheck weapon = collision.gameObject.GetComponent<Weaponcheck>();//무기 gameobject가 몬스터 collision에 닿을때 

        if(weapon)
        {
            beatendamage = true;
            if(GameManager.Instance.Weaponcheck.magic == true)
            {
                magicchek = true;
            }
        }
    }


    //만약에 magic이 true일때 만나면 어떤값을 true 이후에 update에서 어떤값이 true일때 데미지를 입고 0.?초간 무적 이후에 또 피해를 입는다.


    private void Awake()
    {
        MaxHp = GameHp;
    }
    private void Start()
    {
        GameManager.Instance.HitboxMonster = this;
    }

    void Update()
    {
        Hitboxmaonster();
        destroymonster();
    }

    private void Hitboxmaonster()
    {
        if(beatendamage == true)
        {
            weapondamage = GameManager.Instance.Weaponcheck.AttackdamageMax;
            MaxHp -= weapondamage;
        }
    }

    private void destroymonster()
    {
        if(MaxHp <= 0)
        {
            Destroy(parents);
        }
        else
        {
            beatendamage = false;
        }
    }
}
