using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChageHP : MonoBehaviour
{
    GameObject obj;
    [SerializeField] GameObject playerhp1;
    [SerializeField] GameObject playerhp2;
    float playHP;
    void Start()
    {
        startHP();
    }

    void Update()
    {
        changeHp();
    }

    private void changeHp()
    {
        playHP = GameManager.Instance.Player.MaxHP;
        if (playHP == 2)
        {
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(5).gameObject.SetActive(true);
        }
        else if(playHP == 1)
        {
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(4).gameObject.SetActive(true);
        }
        else if (playHP == 0)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    private void startHP()
    {
        for(int i = 0; i < 3; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        for (int n = 3; n < 6; n++)
        {
            transform.GetChild(n).gameObject.SetActive(false);
        }
    }
}
