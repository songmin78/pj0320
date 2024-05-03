using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HItboxplayer : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = gameObject.GetComponentInParent<Player>();
        if (collision.CompareTag("Monster"))
        {
            player.Monsterattackcheck = true;
            //Debug.Log("닿았다");
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Debug.Log("접근함");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
