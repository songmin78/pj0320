using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMoving : MonoBehaviour
{
    [Header("�Ѿư������� ����")]
    [SerializeField] private bool ChasePlayer = false;

    [SerializeField] float horizontals;
    [SerializeField] float verticals;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if(player)
        {
            ChasePlayer = true;
        }
        Debug.Log("�߰�");
    }

    void Start()
    {
        
    }


    void Update()
    {
        playerchase();
    }

    private void playerchase()//�÷��̾ �Ѿư��� �ڵ�
    {
        if(ChasePlayer == true)
        {
            //1.�÷��̾��� ��ġ�� �ǽð����� Ȯ��
            //2.�÷����� ��ġ - �ڽ��� ��ġ�� �Ͽ� �̵�
            //3.�밢�� ���� -> �켱����... -> horizon�̳� vertical�� ���ڰ� 0�� �� ����� ������ �̵�
            //����1: �÷��̾��� ��ġ�� �޴¹��� �𸥴�
            //����2:���Ͱ� �ڵ����� �̵��ϱ����� ��� �� �ִϸ��̼� ������<- Player��ũ��Ʈ���� ������� ���� ����

            //GameManager.Instance.Player track


        }
    }
}
