using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBox : MonoBehaviour
{
    public float waycheck = 0;


    private void Start()
    {
        GameManager.Instance.CheckBox = this;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);

            transform.GetChild(0).gameObject.SetActive(true);

            waycheck = 0;//���� �ٶ󺼶�
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);

            transform.GetChild(1).gameObject.SetActive(true);

            waycheck = 3;//����
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);

            transform.GetChild(1).gameObject.SetActive(true);

            waycheck = 1;//������
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);

            transform.GetChild(2).gameObject.SetActive(true);

            waycheck = 2;//�Ʒ���
        }
    }
}
