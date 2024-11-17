using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSc : MonoBehaviour
{
    [SerializeField] Button btnstart;//게임 시작 버튼
    [SerializeField] Button btnExit;//게임 종료 버튼(종료창 띄우기용)
    [SerializeField] Button btntutorial;//튜토리얼 버튼(튜토리얼에 갈지 한번더 물어보기)
    [SerializeField] Button gameExit;//종료창에서 직접 게임을 나가는 버튼
    [SerializeField] Button gametutorial;//튜토리얼로 이동하는 버튼
    [SerializeField] Button LobbyScene;//로비로 돌아가는 버튼(종료창)
    [SerializeField] Button LobbyScene2;//로비로 돌아가는 버튼(튜토리얼 창)

    [SerializeField] GameObject nexitScene;//종료창을 띄여주는 용도
    [SerializeField] GameObject tutorialScene;//튜토리얼을 갈지 한번더 확인할 창

    //[SerializeField] Button settingbutton;//설정 들어가는 버튼
    //[SerializeField] Button settingExit;//설정에서 나가는 버튼


    private void Awake()
    {
        btnstart.onClick.AddListener(() =>//게임 시작 버튼을 누를때
        {
            //씬을 변경하는 코드
            //메인씬 -> 플레이 씬

            //가는법: file -> Build Settings -> Scene을 끌어다가 넣기
            SceneManager.LoadSceneAsync(2); //플레이화면으로 넘어가기
        });

        btnExit.onClick.AddListener(() =>//게임 종료버튼을 누를때
        {
            //게임 종료를 확인하는 창을 띄움

            nexitScene.SetActive(true);
        });

        btntutorial.onClick.AddListener(() =>//튜토리얼버튼을 누를때
        {
            //튜토리얼에 갈 창을 띄움

            tutorialScene.SetActive(true);
        });

        gameExit.onClick.AddListener(() =>//게임 종료창에서 게임을 종료
        {
            //게임을 빌드한 상태에서는 게임을 종료함
            //에디터에서 사용시에는 에디터를 stop으로 변경
            //전처리

            //UnityEditor.EditorApplication.isPlaying = false;

            Application.Quit();//어플을 종료
        });

        gametutorial.onClick.AddListener(() =>//튜토리얼창에서 튜토리얼 필드로 가는 코드
        {
            SceneManager.LoadSceneAsync(1);//튜토리얼 씬으로 넘어가는 코드
        });

        LobbyScene.onClick.AddListener(() =>//종료창에서 로비로 돌아갈 코드
        {
            nexitScene.SetActive(false);//오브젝트 닫기
        });

        LobbyScene2.onClick.AddListener(() =>//종료창에서 로비로 돌아갈 코드
        {
            tutorialScene.SetActive(false);//오브젝트 닫기
        });
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
