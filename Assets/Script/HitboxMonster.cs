using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class HitboxMonster : MonoBehaviour
{
    [SerializeField]GameObject parents;

    [Header("���� ����")]
    [SerializeField] public float attackdamage;//������ ������
    [SerializeField] float GameHp = 1;//������ HP
    private float MaxHp;
    

    [Header("���� ����")]
    bool beatendamage = false;
    bool Oncheckdamage = false;
    float weapondamage = 0;

    [Header("��Ÿ")]
    [SerializeField] bool magicchek = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Weaponcheck weapon = collision.gameObject.GetComponent<Weaponcheck>();//���� gameobject�� ���� collision�� ������ 

        if(weapon)
        {
            beatendamage = true;
            if(GameManager.Instance.Weaponcheck.magic == true)
            {
                magicchek = true;
            }
        }
    }


    //���࿡ magic�� true�϶� ������ ����� true ���Ŀ� update���� ����� true�϶� �������� �԰� 0.?�ʰ� ���� ���Ŀ� �� ���ظ� �Դ´�.


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
