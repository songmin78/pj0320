using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class SceneMgr : MonoBehaviour
{
    eSCENE Scene = eSCENE.eSCENE_TITLE;

    public void ChangeScene(eSCENE _e, bool _Loading = false)
    {
        if (Scene == _e)
            return;

        Scene = _e;

        switch (_e)
        {
            case eSCENE.eSCENE_TITLE:
                SceneManager.LoadScene("Title");
                break;
            case eSCENE.eSCENE_LOGIN:
                SceneManager.LoadScene("Login");
                break;
            case eSCENE.eSCENE_LOBBY:
                SceneManager.LoadScene("Lobby");
                break;
            case eSCENE.eSCENE_BATTLE:
                SceneManager.LoadScene("Battle");
                break;
        }
    }

    public void SetPlayerPrefsIntKey(string _key, int _Value)//계정을 만들고 저장할때 쓰이는 부분
    {
        PlayerPrefs.SetInt(_key, _Value);
        PlayerPrefs.Save();
    }
    public void SetPlayerPrefsFloatKey(string _key, float _float)//계정을 만들고 저장할때 쓰이는 부분
    {
        PlayerPrefs.SetFloat(_key, _float);
        PlayerPrefs.Save();
    }
    public void SetPlayerPrefsStringKey(string _key, string _string)//계정을 만들고 저장할때 쓰이는 부분
    {
        PlayerPrefs.SetString(_key, _string);
        PlayerPrefs.Save();
    }

    public int GetPlayerPrefsIntKey(string _key)
    {
        return PlayerPrefs.GetInt(_key);
    }
    public float GetPlayerPrefsFloatKey(string _key)
    {
        return PlayerPrefs.GetFloat(_key);
    }
    public string GetPlayerPrefsStringKey(string _key)
    {
        return PlayerPrefs.GetString(_key);
    }
}
