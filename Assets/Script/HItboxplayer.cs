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
            //Debug.Log("��Ҵ�");
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Debug.Log("������");
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
