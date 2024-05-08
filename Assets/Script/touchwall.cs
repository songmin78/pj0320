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
        if ( horizontalcheck == true && collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            horizontalwallcheck = true;
        }
        else if(verticalcheck == true && collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            verticalwallcheck = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (horizontalcheck == true && collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            horizontalwallcheck = false;
        }
        else if (verticalcheck == true && collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            verticalwallcheck = false;
        }
    }


    void Update()
    {
        horizontalwall();
        vericalwall();
    }

    private void horizontalwall()
    {
        if(horizontalcheck == true)
        {
            HitboxMonster hitboxmonster = GetComponent<HitboxMonster>();
            hitboxmonster.walltest();
        }
    }

    private void vericalwall()
    {
        if(verticalcheck == true)
        {
            HitboxMonster hitboxmonster = GetComponent<HitboxMonster>();
            hitboxmonster.walltest2();
        }
    }
}
