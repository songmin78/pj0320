using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaponcheck : MonoBehaviour
{
    [Header("기타 관리")]
    [SerializeField] float speed = 1.0f;//무기 공격 속도
    [SerializeField] float Xposition;//시작 x좌표
    [SerializeField] float Yposition;//시작 y좌표
    [SerializeField] float Zposition;//시작 z좌표
    [SerializeField] float eulercheck;//바라보고있는 방향

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

    public void Attackdamage(int _weaponType,int _eulercheck)
    {
        if(_weaponType == 0)//원거리 무기
        {
            AttackdamageMax = 5;
            Debug.Log("원거리");
        }
        else if(_weaponType == 1)//근접 무기
        {
            eulercheck = _eulercheck; 
            Debug.Log(eulercheck);
        }
        else if(_weaponType == 2)//마법무기
        {
            Debug.Log("마법");
        }
    }

    public void Counterdamage(int _weaponType, int _eulercheck)
    {
        if (_weaponType == 0)//원거리 무기
        {
            Debug.Log("원거리 카운터");
        }
        else if (_weaponType == 1)//근접 무기
        {
            eulercheck = _eulercheck;
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
        else if(punch == true)
        {
            //1.오브젝트가 생성하면 그 자리의 값을 저장 ->끝
            //2.rotation의 z값을 +180 혹은 -180 을 더한다
            //3.2번에서 더한 값을 Time.deltatime * punchtime 초로 회전을 실행 시킨다 
            eulerchange();
        }
    }

    public void startattack()
    {
        Xposition = transform.position.x;
        Yposition = transform.position.y;
        Zposition = transform.position.z;

        new Vector3(Xposition, Yposition, Zposition);
    }

    private void weapondestory()
    {
        float Xchange = transform.position.x;
        float Ychange = transform.position.y;
        float Zchange = transform.position.z;

        float Zrotation = transform.eulerAngles.z;

        if (arrow == true)
        {
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
            
            transform.eulerAngles = new Vector3(0, 0, Zrotation);

            if (Zrotation >= Zposition +170 || Zrotation <= Zposition - 170)
            {
                Destroy(Typeweapon);
            }
        }
    }

    /// <summary>
    /// eluercheck-> 1=> 위쪽,2 => 왼쪽, 3=> 오른쪽, 4=> 아래쪽 
    /// </summary>
    private void eulerchange()//공격하는 방향에 맞춰 회전하는 코드
    {
        if(eulercheck == 1)
        {
            transform.eulerAngles += new Vector3(0, 0, -90 * Time.deltaTime * speed);
        }
        else if(eulercheck == 2)
        {
            transform.eulerAngles += new Vector3(0, 0, -180 * Time.deltaTime * speed);
        }
        else if(eulercheck == 3)
        {
            transform.eulerAngles += new Vector3(0, 0, -180 * Time.deltaTime * speed);
        }
        else if(eulercheck == 4)
        {
            transform.eulerAngles += new Vector3(0, 0, -270 * Time.deltaTime * speed);
        }
    }


}
