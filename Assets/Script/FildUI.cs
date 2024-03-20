using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FildUI : MonoBehaviour
{
    [SerializeField] public bool passcheck = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        passcheck = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.GetChild(0).gameObject.SetActive(false);
        passcheck = false;
    }

    void Start()
    {
        
    }


    void Update()
    {
        nextStage();




        test();
    }

    /// <summary>
    /// 테스트용 코드
    /// </summary>
    private void test()
    {
        
    }

    private void nextStage()
    {
        if(passcheck == true || Input.GetKeyDown(KeyCode.F))
        {

        }
    }
}
