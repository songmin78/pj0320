using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSc : MonoBehaviour
{
    [SerializeField] Button btnstart;//���� ���� ��ư
    [SerializeField] Button btnExit;//���� ���� ��ư(����â �����)
    [SerializeField] Button btntutorial;//Ʃ�丮�� ��ư(Ʃ�丮�� ���� �ѹ��� �����)
    [SerializeField] Button gameExit;//����â���� ���� ������ ������ ��ư
    [SerializeField] Button gametutorial;//Ʃ�丮��� �̵��ϴ� ��ư
    [SerializeField] Button LobbyScene;//�κ�� ���ư��� ��ư(����â)
    [SerializeField] Button LobbyScene2;//�κ�� ���ư��� ��ư(Ʃ�丮�� â)

    [SerializeField] GameObject nexitScene;//����â�� �翩�ִ� �뵵
    [SerializeField] GameObject tutorialScene;//Ʃ�丮���� ���� �ѹ��� Ȯ���� â

    //[SerializeField] Button settingbutton;//���� ���� ��ư
    //[SerializeField] Button settingExit;//�������� ������ ��ư


    private void Awake()
    {
        btnstart.onClick.AddListener(() =>//���� ���� ��ư�� ������
        {
            //���� �����ϴ� �ڵ�
            //���ξ� -> �÷��� ��

            //���¹�: file -> Build Settings -> Scene�� ����ٰ� �ֱ�
            SceneManager.LoadSceneAsync(2); //�÷���ȭ������ �Ѿ��
        });

        btnExit.onClick.AddListener(() =>//���� �����ư�� ������
        {
            //���� ���Ḧ Ȯ���ϴ� â�� ���

            nexitScene.SetActive(true);
        });

        btntutorial.onClick.AddListener(() =>//Ʃ�丮���ư�� ������
        {
            //Ʃ�丮�� �� â�� ���

            tutorialScene.SetActive(true);
        });

        gameExit.onClick.AddListener(() =>//���� ����â���� ������ ����
        {
            //������ ������ ���¿����� ������ ������
            //�����Ϳ��� ���ÿ��� �����͸� stop���� ����
            //��ó��

            //UnityEditor.EditorApplication.isPlaying = false;

            Application.Quit();//������ ����
        });

        gametutorial.onClick.AddListener(() =>//Ʃ�丮��â���� Ʃ�丮�� �ʵ�� ���� �ڵ�
        {
            SceneManager.LoadSceneAsync(1);//Ʃ�丮�� ������ �Ѿ�� �ڵ�
        });

        LobbyScene.onClick.AddListener(() =>//����â���� �κ�� ���ư� �ڵ�
        {
            nexitScene.SetActive(false);//������Ʈ �ݱ�
        });

        LobbyScene2.onClick.AddListener(() =>//����â���� �κ�� ���ư� �ڵ�
        {
            tutorialScene.SetActive(false);//������Ʈ �ݱ�
        });
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
