using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monsterattack : MonoBehaviour
{
    [SerializeField] public float attackdamage;//������ ������

    [Header("���� ����")]
    bool beatendamage = false;
    public bool Oncheckdamage = false;
    float weapondamage = 0;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Weaponcheck weapon = collision.gameObject.GetComponent<Weaponcheck>();//���� gameobject�� ���� collision�� ������ 
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
