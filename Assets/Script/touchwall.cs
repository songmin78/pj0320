using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchwall : MonoBehaviour
{
    [Header("좌우 체크")]
    [SerializeField] bool horizontalcheck;
    [Header("위 아래 체크")]
    [SerializeField] bool verticalcheck;
    bool horizontalwallcheck = false;
    bool verticalwallcheck = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject find1 = GameObject.Find("Hitbox");
        if ( horizontalcheck == true && collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            HitboxMonster hitboxmonster = find1.GetComponent<HitboxMonster>();
            horizontalwallcheck = true;
            hitboxmonster.walltest(horizontalwallcheck);
        }
        else if(verticalcheck == true && collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            HitboxMonster hitboxmonster = find1.GetComponent<HitboxMonster>();
            verticalwallcheck = true;
            hitboxmonster.walltest2(verticalwallcheck);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (horizontalcheck == true && collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            GameObject find1 = GameObject.Find("Hitbox");
            HitboxMonster hitboxmonster = find1.GetComponent<HitboxMonster>();
            horizontalwallcheck = false;
            hitboxmonster.walltest(horizontalwallcheck);
        }
        else if (verticalcheck == true && collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            GameObject find1 = GameObject.Find("Hitbox");
            HitboxMonster hitboxmonster = find1.GetComponent<HitboxMonster>();
            verticalwallcheck = false;
            hitboxmonster.walltest2(verticalwallcheck);
        }
    }


    void Update()
    {
        //horizontalwall();
        //vericalwall();
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
}
