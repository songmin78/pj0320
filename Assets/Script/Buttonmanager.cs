using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttonmanager : MonoBehaviour
{
    [SerializeField] Button MenuButton;//플레이어의 메뉴 버튼
    [SerializeField] Button GameRePlay;//게임을 이어하는 버튼
    [SerializeField] Button LobbyButton;//게임을 종료하는 버튼
    [SerializeField] Button CheatButton;//무적(치트) 버튼
    [SerializeField] Button BackMenu;//메뉴창으로 돌아가는 버튼
    [SerializeField] Button ExitLobbyButton;//로비창(종료)버튼

    [SerializeField] GameObject MenuScene;//메뉴오브젝트를 보여준다
    [SerializeField] GameObject LobbyScene;//로비로 갈지 다시 물어보는 오브젝트를 보여준다
    [SerializeField] GameObject CheatcheckScene;//치트를 썼는지 안썼는지 보여주는 오브젝트

    [SerializeField] public bool Cheatcheck;
    [SerializeField] public GameObject test;

    [Header("팁 창")]
    [SerializeField] Button nextbutton;
    [SerializeField] Button returnbutton;
    [SerializeField] Button exitbutton;

    [SerializeField] GameObject tutorial;
    [SerializeField] GameObject bow;
    [SerializeField] GameObject sword;
    [SerializeField] GameObject magic;
    [SerializeField] GameObject reurnbuttons;
    [SerializeField] GameObject bossstage;

    float passcheck = 0;
    [SerializeField] bool tapon;
    [SerializeField] GameObject aaa;

    [Header("보스전")]
    //[SerializeField] Button gameexit;//게임을 끝내는 버튼

    [Header("게임 종료창")]
    //[SerializeField] GameObject endtab;
    [SerializeField] Button endbutton;

    private bool menucheck;//메뉴창이 올라올때 확인하는 코드

    private void Awake()
    {
        MenuButton.onClick.AddListener(() =>//메뉴를 눌렀을때 버튼
        {
            MenuScene.SetActive(true);//메뉴창을 띄운다
            menucheck = true;
        });

        GameRePlay.onClick.AddListener(() =>//게임을 이어하기 버튼
        {
            MenuScene.SetActive(false);//메뉴창을 닫는다
            menucheck = false;
        });

        LobbyButton.onClick.AddListener(() =>//게임을 종료냐고 물어보는 버튼
        {
            LobbyScene.SetActive(true);//게임  종료할지 물어보는 창을 띄움
        });

        CheatButton.onClick.AddListener(() =>//무적(치트)를 체크하는 버튼
        {
            if(Cheatcheck == false)//치트가 안켜져을때
            {
                Cheatcheck = true;//치트확인을 킨다
                CheatcheckScene.SetActive(true);//치트를 켰는지 눈으로 보여주는 화면을 true시킨다
            }
            else if(Cheatcheck == true)//치트가 켜져있을때
            {
                Cheatcheck = false;//치트확인을 끈다
                CheatcheckScene.SetActive(false);//치트확인 화면을 없앤다
            }
        });

        BackMenu.onClick.AddListener(() =>//메뉴로 돌아가는 버튼(로비로 돌아갈지 물어보는 창에 뜸)
        {
            LobbyScene.SetActive(false);//메뉴로 다시 돌아가는 코드
        });

        ExitLobbyButton.onClick.AddListener(() =>//게임 종료 버튼
        {
            SceneManager.LoadSceneAsync(0);//Scene(0) 즉 로비화면으로 이동
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();//게임 종료
        });

        nextbutton.onClick.AddListener(() =>//설명집에서 다음으로 넘어 갈때
        {
            if (passcheck == 0)
            {
                passcheck = 1;//칼 설명집
                bow.SetActive(false);
                sword.SetActive(true);
                reurnbuttons.SetActive(true);
            }
            else if (passcheck == 1)
            {
                passcheck = 2;
                sword.SetActive(false);
                magic.SetActive(true);
            }
            else if (passcheck == 2)
            {
                passcheck = 3;
                magic.SetActive(false);
                bossstage.SetActive(true);
            }

        });

        returnbutton.onClick.AddListener(() =>//설명집에서 이전으로 돌아갈때
        {
            if (passcheck == 1)
            {
                passcheck = 0;
                sword.SetActive(false);
                bow.SetActive(true);
                reurnbuttons.SetActive(false);
            }
            else if (passcheck == 2)
            {
                passcheck = 1;
                magic.SetActive(false);
                sword.SetActive(true);
            }
            else if(passcheck == 3)
            {
                passcheck = 2;
                bossstage.SetActive(false);
                magic.SetActive(true);
            }
        });

        exitbutton.onClick.AddListener(() =>
        {
            menucheck = false;
            tutorial.SetActive(false);
        });

        //gameexit.onClick.AddListener(() =>
        //{
        //    SceneManager.LoadSceneAsync(0);//Scene(0) 즉 로비화면으로 이동
        //    UnityEditor.EditorApplication.isPlaying = false;
        //    Application.Quit();//게임 종료
        //});

        endbutton.onClick.AddListener(() =>//게임 종료 버튼
        {
            SceneManager.LoadSceneAsync(0);//Scene(0) 즉 로비화면으로 이동
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();//게임 종료
        });

    }

    void Start()
    {
        GameManager.Instance.Buttonmanager = this;
    }

    // Update is called once per frame
    void Update()
    {
        onenInventory();

        tutorialttons();
    }

    private void onenInventory()
    {
        if (menucheck == true)
        {
            Time.timeScale = 0f;//Time.timeScale -> 시간의 빠르기를 조절하는 코드
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void tutorialttons()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            tapon = true;
            //tutorial.SetActive(true);
        }
        if (tapon == true)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                menucheck = true;
                tutorial.SetActive(true);
            }
        }
    }
}
