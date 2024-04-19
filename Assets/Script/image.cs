using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magicscript : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 pos = GameManager.Instance.Player.transform.position;//GameManager에서 플레이어의 위치를 전달 받은 코드
        pos.x += 0.34f;
        transform.position = pos;
    }

    public void magicattack()
    {

    }
}
