using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recallmonster : MonoBehaviour
{
    [Header("몬스터 소환 위치")]
    [SerializeField] bool firsttrap;
    [SerializeField] bool secondtrap;

    bool onrecalltrap;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")//만약에 닿은 오브젝트가 플레이어일 경우
        {
            onrecalltrap = true;
        }
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void firstrecall()//첫번째 트랩에 닿았을 경우
    {
        if(firsttrap == true && onrecalltrap == true)//첫번째트랩에 닿을 경우
        {

            Vector3 inpos = new Vector3(33.5f, -22.5f, 0);//적이 생성될 위치
        }
    }
}
