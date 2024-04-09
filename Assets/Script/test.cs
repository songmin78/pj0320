using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
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
        if(trsPlayer == null)
        {
            return;
        }

        camMain.transform.position = new Vector3(
            Mathf.Clamp(trsPlayer.transform.position.x, curBound.min.x, curBound.max.x),
            Mathf.Clamp(trsPlayer.transform.position.y, curBound.min.y, curBound.max.y),
            camMain.transform.position.z);
    }

    /// <summary>
    /// ���带 ©�� ��ǻ�Ͱ� �� ������ �����ִ� �ڵ� �˾Ƶθ� ����
    /// </summary>
    private void setResolution()
    {
        float targetRatio = 9f / 16f;//fhd
        float ratio = (float)Screen.width / (float)Screen.height;
        float scaleHeight = ratio / targetRatio;
        float fixedWidth = (float)Screen.width / scaleHeight;

        Screen.SetResolution((int)fixedWidth, Screen.height, true);
    }

    /// <summary>
    /// ī�޶� ���� ������ ���� ������ �� �������ϴ� �ڵ�
    /// </summary>
    private void checkBound()
    {
        float height = camMain.orthographicSize;
        float width = height * camMain.aspect;

        curBound = boxcoll.bounds;//��׶������ �ٿ�� ũ�⸦ �����ؿ�, �ݹ��� ����

        float minX = curBound.min.x + width;//x�� ī�޶�ũ�⸸ŭ ��������
        float maX = curBound.extents.x - width;//x�� ī�޶�  ũ�⸸ŭ ��������

        float minY = curBound.min.y + height;//y�� ī�޶�ũ�⸸ŭ ����
        float maY = curBound.extents.y - height;//y�� ī�޶�ũ�⸸ŭ �Ʒ���

        curBound.SetMinMax(new Vector3(minX, minY), new Vector3(maX, maY));//�ٿ�� ����� ������ ����
    }



}
