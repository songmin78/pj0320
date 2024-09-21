using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FildUI : MonoBehaviour
{
    [Header("�������� üũ")]
    [SerializeField] bool tutorialstage;
    [SerializeField] bool startstage;
    [SerializeField] bool fildstage;
    [SerializeField] int stagetype;
    [Header("���Ʈ�� ��Ҵ��� üũ")]
    [SerializeField] public bool passcheck = false;
    [Header("�������� �Ѿ�� ì�� ������Ʈ")]
    [SerializeField] GameObject aaa;
    [SerializeField] GameObject gagecheck;
    [SerializeField] GameObject gamemanager;
    [SerializeField] GameObject playerui;
    [SerializeField] GameObject menutest;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            transform.GetChild(0).gameObject.SetActive(true);
            passcheck = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            passcheck = false;
        }
    }


    void Update()
    {
        nextStage();
    }

    /// <summary>
    /// �׽�Ʈ�� �ڵ�
    /// </summary>
    //private void test()
    //{
        
    //}

    private void nextStage()
    {
        if(passcheck == true && Input.GetKeyDown(KeyCode.F))
        {
            DontDestroyOnLoad(aaa);
            DontDestroyOnLoad(gagecheck);
            DontDestroyOnLoad(gamemanager);
            DontDestroyOnLoad(GameManager.Instance.Player);
            DontDestroyOnLoad(GameManager.Instance.Buttonmanager);
            DontDestroyOnLoad(GameManager.Instance.PlayerUI);
            DontDestroyOnLoad(menutest);

            if (tutorialstage == true)
            {
                SceneManager.LoadSceneAsync(2);
                stagetype = 1;
            }
            else if(startstage == true)
            {
                SceneManager.LoadSceneAsync(3);
                stagetype = 2;
            }
            else if(fildstage == true)
            {
                SceneManager.LoadSceneAsync(4);
                stagetype = 3;
            }

            Player player = GameManager.Instance.Player;
            player.stage(stagetype);
        }
    }
}
