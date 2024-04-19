using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monsterattack : MonoBehaviour
{
    //오로지 몬스터가 플레이어를 공격 할때만 발동하는 코드

    [Header("공격 여부")]
    public float attackdamage;
    public bool Oncheckdamage = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
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
