using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttonmanager : MonoBehaviour
{
    [SerializeField] Button MenuButton;//�÷��̾��� �޴� ��ư
    [SerializeField] Button GameRePlay;//������ �̾��ϴ� ��ư
    [SerializeField] Button LobbyButton;//������ �����ϴ� ��ư
    [SerializeField] Button CheatButton;//����(ġƮ) ��ư
    [SerializeField] Button BackMenu;//�޴�â���� ���ư��� ��ư
    [SerializeField] Button ExitLobbyButton;//�κ�â(����)��ư

    [SerializeField] GameObject MenuScene;//�޴�������Ʈ�� �����ش�
    [SerializeField] GameObject LobbyScene;//�κ�� ���� �ٽ� ����� ������Ʈ�� �����ش�
    [SerializeField] GameObject CheatcheckScene;//ġƮ�� ����� �Ƚ���� �����ִ� ������Ʈ

    [SerializeField] public bool Cheatcheck;
    [SerializeField] public GameObject test;

    [Header("�� â")]
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

    [Header("������")]
    //[SerializeField] Button gameexit;//������ ������ ��ư

    [Header("���� ����â")]
    //[SerializeField] GameObject endtab;
    [SerializeField] Button endbutton;

    private bool menucheck;//�޴�â�� �ö�ö� Ȯ���ϴ� �ڵ�

    private void Awake()
    {
        MenuButton.onClick.AddListener(() =>//�޴��� �������� ��ư
        {
            MenuScene.SetActive(true);//�޴�â�� ����
            menucheck = true;
        });

        GameRePlay.onClick.AddListener(() =>//������ �̾��ϱ� ��ư
        {
            MenuScene.SetActive(false);//�޴�â�� �ݴ´�
            menucheck = false;
        });

        LobbyButton.onClick.AddListener(() =>//������ ����İ� ����� ��ư
        {
            LobbyScene.SetActive(true);//����  �������� ����� â�� ���
        });

        CheatButton.onClick.AddListener(() =>//����(ġƮ)�� üũ�ϴ� ��ư
        {
            if(Cheatcheck == false)//ġƮ�� ����������
            {
                Cheatcheck = true;//ġƮȮ���� Ų��
                CheatcheckScene.SetActive(true);//ġƮ�� �״��� ������ �����ִ� ȭ���� true��Ų��
            }
            else if(Cheatcheck == true)//ġƮ�� ����������
            {
                Cheatcheck = false;//ġƮȮ���� ����
                CheatcheckScene.SetActive(false);//ġƮȮ�� ȭ���� ���ش�
            }
        });

        BackMenu.onClick.AddListener(() =>//�޴��� ���ư��� ��ư(�κ�� ���ư��� ����� â�� ��)
        {
            LobbyScene.SetActive(false);//�޴��� �ٽ� ���ư��� �ڵ�
        });

        ExitLobbyButton.onClick.AddListener(() =>//���� ���� ��ư
        {
            SceneManager.LoadSceneAsync(0);//Scene(0) �� �κ�ȭ������ �̵�
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();//���� ����
        });

        nextbutton.onClick.AddListener(() =>//���������� �������� �Ѿ� ����
        {
            if (passcheck == 0)
            {
                passcheck = 1;//Į ������
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

        returnbutton.onClick.AddListener(() =>//���������� �������� ���ư���
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
        //    SceneManager.LoadSceneAsync(0);//Scene(0) �� �κ�ȭ������ �̵�
        //    UnityEditor.EditorApplication.isPlaying = false;
        //    Application.Quit();//���� ����
        //});

        endbutton.onClick.AddListener(() =>//���� ���� ��ư
        {
            SceneManager.LoadSceneAsync(0);//Scene(0) �� �κ�ȭ������ �̵�
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();//���� ����
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
            Time.timeScale = 0f;//Time.timeScale -> �ð��� �����⸦ �����ϴ� �ڵ�
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
