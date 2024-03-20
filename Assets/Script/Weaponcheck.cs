using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaponcheck : MonoBehaviour
{
    [Header("기타 관리")]
    [SerializeField] float speed = 1.0f;

    [Header("무기관리")]
    [SerializeField] bool Counter = false;//카운터 여부
    [SerializeField] float AttackdamageMax = 1f;//무기 데미지
    [SerializeField] bool arrow = false;//화살인것을 확인
    [SerializeField] bool punch = false;//근접무기것을 확인
    [SerializeField] bool magic = false;//마법인것을 확인
     

    void Start()
    {
    }

    void Update()
    {
        shotarrow();
    }

    public void Attackdamage(int _weaponType)
    {
        if(_weaponType == 0)//원거리 무기
        {
            AttackdamageMax = 5;
            Debug.Log("원거리");
        }
        else if(_weaponType == 1)//근접 무기
        {
            Debug.Log("근거리");
        }
        else if(_weaponType == 2)//마법무기
        {
            Debug.Log("마법");
        }
    }

    public void Counterdamage(int _weaponType)
    {
        if (_weaponType == 0)//원거리 무기
        {
            Debug.Log("원거리 카운터");
        }
        else if (_weaponType == 1)//근접 무기
        {
            Debug.Log("근거리 카운터");
        }
        else if (_weaponType == 2)//마법무기
        {
            Debug.Log("마법 카운터");
        }
    }

    private void shotarrow()
    {
        if(arrow == true)
        {
            transform.position += transform.up * Time.deltaTime * speed;
        }
    }

}
