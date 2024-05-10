using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchwall : MonoBehaviour
{
    [SerializeField] bool checkboxtool;
    [Header("�¿� üũ")]
    [SerializeField] bool horizontalcheck;
    [Header("�� �Ʒ� üũ")]
    [SerializeField] bool verticalcheck;
    [Header("Ȯ��")]
    [SerializeField] bool horizontalwallcheck = false;
    [SerializeField]bool verticalwallcheck = false;
    bool checkwallbox = false;
    //��Ÿ
    float timerd = 0.1f;
    float Maxtimerd;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject find1 = GameObject.Find("Hitbox");
        if(checkboxtool == true && collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            HitboxMonster hitboxmonster = find1.GetComponent<HitboxMonster>();
            checkwallbox = true;
            hitboxmonster.wallcheckfind(checkwallbox);
        }
        if ( horizontalcheck == true && collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            HitboxMonster hitboxmonster = find1.GetComponent<HitboxMonster>();
            horizontalwallcheck = true;
            hitboxmonster.walltest(horizontalwallcheck);
        }
        else if(verticalcheck == true && collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            if(Maxtimerd != timerd)
            {
                return;
            }
            HitboxMonster hitboxmonster = find1.GetComponent<HitboxMonster>();
            verticalwallcheck = true;
            hitboxmonster.walltest2(verticalwallcheck);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (checkboxtool == true && collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            GameObject find1 = GameObject.Find("Hitbox");
            HitboxMonster hitboxmonster = find1.GetComponent<HitboxMonster>();
            checkwallbox = false;
            hitboxmonster.wallcheckfind(checkwallbox);
        }
        if (horizontalcheck == true && collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            if(verticalcheck == true)
            {
                return;
            }
            GameObject find1 = GameObject.Find("Hitbox");
            HitboxMonster hitboxmonster = find1.GetComponent<HitboxMonster>();
            horizontalwallcheck = false;
            hitboxmonster.walltest(horizontalwallcheck);
        }
        else if (verticalcheck == true && collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            if(horizontalwallcheck == true )
            {
                return;
            }
            GameObject find1 = GameObject.Find("Hitbox");
            HitboxMonster hitboxmonster = find1.GetComponent<HitboxMonster>();
            verticalwallcheck = false;
            hitboxmonster.walltest2(verticalwallcheck);
            timer();
        }
    }

    private void Awake()
    {
        Maxtimerd = timerd;
    }


    void Update()
    {
        //horizontalwall();
        //vericalwall();
        //timer();
    }

    //private void horizontalwall()
    //{

    //    if(horizontalwallcheck == true)
    //    {
    //        HitboxMonster hitboxmonster = transform.GetComponent<HitboxMonster>();
    //        hitboxmonster.walltest();
    //    }
    //}

    //private void vericalwall()
    //{
    //    if(verticalwallcheck == true)
    //    {
    //        Debug.Log("����");
    //    }
    //}

    private void timer()//�ٷ� �ٲ��� �ʰ� ����
    {
        if (verticalwallcheck == false)
        {
            if (Maxtimerd < 0)
            {
                Maxtimerd = timerd;
            }
            else
            {
                Maxtimerd -= Time.deltaTime;
            }
        }
        //if(Maxtimerd < 0)
        //{
        //    Maxtimerd = timerd;
        //}
        //else
        //{
        //    Maxtimerd -= Time.deltaTime;
        //}
    }
}
