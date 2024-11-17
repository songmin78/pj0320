using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    float Maxtime = 5f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            hpcontroll();
            Destroy(gameObject);
        }
    }

    private void hpcontroll()
    {
        GameManager.Instance.Player.HpUp();
    }

    private void Update()
    {
        Maxtime -= Time.deltaTime;
        if(Maxtime < 0)//5초가 지난 다음 삭제를 한다
        {
            Destroy(gameObject);
        }
    }
}
