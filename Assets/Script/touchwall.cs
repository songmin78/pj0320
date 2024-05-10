using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchwall : MonoBehaviour
{
    [SerializeField] bool checkboxtool;
    [Header("좌우 체크")]
    [SerializeField] bool horizontalcheck;
    [Header("위 아래 체크")]
    [SerializeField] bool verticalcheck;
    [Header("확인")]
    [SerializeField] bool horizontalwallcheck = false;
    [SerializeField]bool verticalwallcheck = false;
    bool checkwallbox = false;
    //기타
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
    //        Debug.Log("상하");
    //    }
    //}

    private void timer()//바로 바뀌지 않게 조절
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
