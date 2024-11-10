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

        curBound = boxcoll.bounds;//백그라운드부터 바운드 크기를 복사해옴, 콜바이 벨류

        float minX = curBound.min.x + width;//x를 카메라크기만큼 우측으로
        float maX = curBound.max.x - width;//x를 카메라  크기만큼 좌측으로

        float minY = curBound.min.y + height;//y를 카메라크기만큼 위로
        float maY = curBound.max.y - height;//y를 카메라크기만큼 아래로

        curBound.SetMinMax(new Vector3(minX, minY), new Vector3(maX, maY));//바운즈를 계산한 값으로 수정
    }
}
