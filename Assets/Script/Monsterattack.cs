using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monsterattack : MonoBehaviour
{
    [SerializeField] public float attackdamage;//몬스터의 데미지

    [Header("공격 여부")]
    bool beatendamage = false;
    public bool Oncheckdamage = false;
    float weapondamage = 0;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Weaponcheck weapon = collision.gameObject.GetComponent<Weaponcheck>();//무기 gameobject가 몬스터 collision에 닿을때 
        Player player = collision.gameObject.GetComponent<Player>();

        if (collision.gameObject.tag == "Player")
        {
            Oncheckdamage = true;
        }
        else
        {
            Oncheckdamage = false;
        }
    }


    void Start()
    {
        GameManager.Instance.Monsterattack = this;
    }
}
