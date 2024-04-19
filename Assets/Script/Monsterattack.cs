using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monsterattack : MonoBehaviour
{
    //������ ���Ͱ� �÷��̾ ���� �Ҷ��� �ߵ��ϴ� �ڵ�

    [Header("���� ����")]
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
