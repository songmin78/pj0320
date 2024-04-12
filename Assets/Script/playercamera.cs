using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercamera : MonoBehaviour
{
    Camera camMain;

    [SerializeField] Bounds curBound;
    [SerializeField] BoxCollider2D boxcoll;

    [SerializeField] Transform trsPlayer;

    private void Start()
    {
        camMain = Camera.main;
        checkBound();

    }

    private void Update()
    {
        if (trsPlayer == null)
        {
            return;
        }

        camMain.transform.position = new Vector3(
            Mathf.Clamp(trsPlayer.transform.position.x, curBound.min.x, curBound.max.x),
            Mathf.Clamp(trsPlayer.transform.position.y, curBound.min.y, curBound.max.y),
            camMain.transform.position.z);
    }

    private void checkBound()
    {
        float height = camMain.orthographicSize;
        float width = height * camMain.aspect;

        curBound = boxcoll.bounds;//��׶������ �ٿ�� ũ�⸦ �����ؿ�, �ݹ��� ����

        float minX = curBound.min.x + width;//x�� ī�޶�ũ�⸸ŭ ��������
        float maX = curBound.max.x - width;//x�� ī�޶�  ũ�⸸ŭ ��������

        float minY = curBound.min.y + height;//y�� ī�޶�ũ�⸸ŭ ����
        float maY = curBound.max.y - height;//y�� ī�޶�ũ�⸸ŭ �Ʒ���

        curBound.SetMinMax(new Vector3(minX, minY), new Vector3(maX, maY));//�ٿ�� ����� ������ ����
    }
}
