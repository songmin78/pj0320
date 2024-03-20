using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaponcheck : MonoBehaviour
{
    [Header("기타 관리")]
    [SerializeField] float speed = 1.0f;//화살 속도
    [SerializeField] float Xposition;//시작 x좌표
    [SerializeField] float Yposition;//시작 y좌표
    [SerializeField] float puchtime = 1.0f;// 근접공격 사라지는 시간

    [Header("무기 종류")]
    [SerializeField] GameObject Typeweapon;

    [Header("무기관리")]
    [SerializeField] bool Counter = false;//카운터 여부
    [SerializeField] float AttackdamageMax = 1f;//무기 데미지
    [SerializeField] bool arrow = false;//화살인것을 확인
    [SerializeField] bool punch = false;//근접무기것을 확인
    [SerializeField] bool magic = false;//마법인것을 확인
     

    void Start()
    {
        startattack();
    }

    void Update()
    {
        shotarrow();
        weapondestory();
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

    public void startattack()
    {
        Xposition = transform.position.x;
        Yposition = transform.position.y;

        new Vector3(Xposition, Yposition, 0);
    }

    private void weapondestory()
    {
        if (arrow == true)
        {
            float Xchange = transform.position.x;
            float Ychange = transform.position.y;

            transform.position = new Vector3(Xchange, Ychange, 0);

            if (Xchange >= Xposition + 5 || Xchange <= Xposition - 5)
            {
                Destroy(Typeweapon);
            }
            else if (Ychange >= Yposition + 5 || Ychange <= Yposition - 5)
            {
                Destroy(Typeweapon);
            }
        }
        else if(punch == true)
        {
            if(puchtime >= 0)
            {
                puchtime  = puchtime -Time.deltaTime; 
            }
            else
            {
                Destroy(Typeweapon);
            }
        }
    }

}
