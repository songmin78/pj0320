using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
