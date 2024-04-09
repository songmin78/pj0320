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

    private void Awake()
    {
        MenuButton.onClick.AddListener(() =>//메뉴를 눌렀을때 버튼
        {
            MenuScene.SetActive(true);//메뉴창을 띄운다
        });

        GameRePlay.onClick.AddListener(() =>//게임을 이어하기 버튼
        {
            MenuScene.SetActive(false);//메뉴창을 닫는다
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
        });
    }

    void Start()
    {
        GameManager.Instance.Buttonmanager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
