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

    private void Awake()
    {
        MenuButton.onClick.AddListener(() =>//�޴��� �������� ��ư
        {
            MenuScene.SetActive(true);//�޴�â�� ����
        });

        GameRePlay.onClick.AddListener(() =>//������ �̾��ϱ� ��ư
        {
            MenuScene.SetActive(false);//�޴�â�� �ݴ´�
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
