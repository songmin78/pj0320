using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class HitboxMonster : MonoBehaviour
{
    [SerializeField]GameObject parents;

    [Header("���� ����")]
    //[SerializeField] public float attackdamage;//������ ������
    [SerializeField] float MsGameHp = 1;//������ HP
    private float MsMaxHp;
    [SerializeField] bool pushdamage;
    public bool pushed;
    float pushway;
    float pushroad = 0.2f;
    public bool waycheck;

    [Header("���� ���� üũ")]
    [SerializeField] bool Bosscheck;


    [Header("���� ����")]
    bool beatendamage = false;
    //public bool Oncheckdamage = false;
    float weapondamage = 0;

    [Header("��Ÿ")]
    [SerializeField] public bool magicchek = false;
    float retime = 0.5f;
    float Maxretime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Weaponcheck weapon = collision.gameObject.GetComponent<Weaponcheck>();//���� gameobject�� ���� collision�� ������ 
        Player player = collision.gameObject.GetComponent<Player>();

        if(weapon)
        {
            beatendamage = true;
            if(GameManager.Instance.Weaponcheck.magic == true)//���࿡ ������ ��������
            {
                magicchek = true;
            }
            else if(GameManager.Instance.Weaponcheck.Counter == true && GameManager.Instance.Weaponcheck.punch == true)
            {
                pushdamage = true;
            }
        }

        //if(collision.gameObject.tag == "Player")
        //{
        //    Oncheckdamage = true;
        //}
        //else
        //{
        //    Oncheckdamage = false;
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(magicchek == true)
        {
            magicchek = false;
        }
        else if(pushdamage == true)
        {
            pushdamage = false;
        }
    }


    //���࿡ magic�� true�϶� ������ ����� true ���Ŀ� update���� ����� true�϶� �������� �԰� 0.?�ʰ� ���� ���Ŀ� �� ���ظ� �Դ´�.


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

        pushmonster();
        Timer();
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

    private void destroymagic()//���� ������ ����� ���
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

    private void pushmonster()
    {
        Vector3 vec = parents.transform.position;
        Vector3 vec3 = parents.transform.position;
        if (pushdamage == true && Bosscheck == false)
        {
            pushway = GameManager.Instance.Weaponcheck.eulercheck;
            if (pushway == 1 && waycheck == false)//���� ������
            {
                waycheck = true;
                pushed = true;
                vec3.y += 1.3f;
                parents.transform.position = vec3;
            }
            else if(pushway == 2 && waycheck == false)//�������� ������
            {
                waycheck = true;
                pushed = true;
                vec3.x -= 1.3f;
                parents.transform.position = vec3;
            }
            else if(pushway == 3 && waycheck == false)//���������� ������
            {
                waycheck = true;
                pushed = true;
                vec3.x += 1.3f;
                parents.transform.position = vec3;
            }
            else if (pushway == 4 && waycheck == false)//�Ʒ��� ������
            {
                waycheck = true;
                pushed = true;
                vec3.y -= 1.3f;
                parents.transform.position = vec3;
            }

        }
        


    }

    private void Timer()
    {
        if(pushed == true)
        {
            pushroad -= Time.deltaTime;
            if (pushroad <= 0)
            {
                pushroad = 0.2f;
                pushed = false;
            }
        }
    }

}
