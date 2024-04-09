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
    /// 빌드를 짤때 컴퓨터가 안 맞을시 맞춰주는 코드 알아두면 편리함
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
    /// 카메라가 내가 설정한 범위 밖으로 못 나가게하는 코드
    /// </summary>
    private void checkBound()
    {
        float height = camMain.orthographicSize;
        float width = height * camMain.aspect;

        curBound = boxcoll.bounds;//백그라운드부터 바운드 크기를 복사해옴, 콜바이 벨류

        float minX = curBound.min.x + width;//x를 카메라크기만큼 우측으로
        float maX = curBound.extents.x - width;//x를 카메라  크기만큼 좌측으로

        float minY = curBound.min.y + height;//y를 카메라크기만큼 위로
        float maY = curBound.extents.y - height;//y를 카메라크기만큼 아래로

        curBound.SetMinMax(new Vector3(minX, minY), new Vector3(maX, maY));//바운즈를 계산한 값으로 수정
    }



}
