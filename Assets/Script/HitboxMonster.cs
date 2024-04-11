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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Weaponcheck weapon = collision.gameObject.GetComponent<Weaponcheck>();//무기 gameobject가 몬스터 collision에 닿을때 

        if(weapon)
        {
            beatendamage = true;
        }
    }

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
