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
        Vector3 pos = GameManager.Instance.Player.transform.position;//GameManager���� �÷��̾��� ��ġ�� ���� ���� �ڵ�
        pos.x += 0.34f;
        transform.position = pos;
    }

    public void magicattack()
    {

    }
}
