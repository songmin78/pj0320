using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bossstagebutton : MonoBehaviour
{
    [Header("보스전")]
    [SerializeField] Button gameexit;//게임을 끝내는 버튼


    private void Awake()
    {
        gameexit.onClick.AddListener(() =>
        {
            SceneManager.LoadSceneAsync(0);//Scene(0) 즉 로비화면으로 이동
            //UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();//게임 종료
        });
    }

}
