using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bossstagebutton : MonoBehaviour
{
    [Header("������")]
    [SerializeField] Button gameexit;//������ ������ ��ư


    private void Awake()
    {
        gameexit.onClick.AddListener(() =>
        {
            SceneManager.LoadSceneAsync(0);//Scene(0) �� �κ�ȭ������ �̵�
            //UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();//���� ����
        });
    }

}
