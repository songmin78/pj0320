using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recallmonster : MonoBehaviour
{
    [Header("���� ��ȯ ��ġ")]
    [SerializeField] bool firsttrap;
    [SerializeField] bool secondtrap;

    bool onrecalltrap;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")//���࿡ ���� ������Ʈ�� �÷��̾��� ���
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

    private void firstrecall()//ù��° Ʈ���� ����� ���
    {
        if(firsttrap == true && onrecalltrap == true)//ù��°Ʈ���� ���� ���
        {

            Vector3 inpos = new Vector3(33.5f, -22.5f, 0);//���� ������ ��ġ
        }
    }
}
