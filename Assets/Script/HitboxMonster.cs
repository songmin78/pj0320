using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxMonster : MonoBehaviour
{
    GameObject parents;

    [Header("���� ����")]
    [SerializeField] float attackdamage;//������ ������
    [SerializeField] float GameHp = 1;//������ HP
    private float MaxHp;
    

    [Header("���� ����")]
    [SerializeField] bool beatendamage = false;
    [SerializeField] bool Oncheckdamage = false;
    [SerializeField] float weapondamage = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Weaponcheck weapon = collision.gameObject.GetComponent<Weaponcheck>();//���� gameobject�� ���� collision�� ������ 

        if(weapon)
        {
            beatendamage = true;
        }
    }

    private void Awake()
    {
        MaxHp = GameHp;
        parents = GetComponent<GameObject>();
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
            Destroy(gameObject.transform.parent);
        }
    }
}
