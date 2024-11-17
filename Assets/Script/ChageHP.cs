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
    bool HpCheck = false;


    void Start()
    {
        GameManager.Instance.ChageHP = this;
        startHP();
    }

    void Update()
    {
        if (HpCheck == false)
        {
            DiechangeHp();
            HpCheck = true;
        }
        //DiechangeHp();
        //LiveChangeHp();
    }

    public void DiechangeHp()//HP가 줄어들었을때 코드
    {
        playHP = GameManager.Instance.Player.MaxHP;
        if (playHP == 2)
        {
            ChangePlayerHp(2, 5);
            //transform.GetChild(2).gameObject.SetActive(false);
            //transform.GetChild(5).gameObject.SetActive(true);
        }
        else if (playHP == 1)
        {
            ChangePlayerHp(1, 4);
            //transform.GetChild(1).gameObject.SetActive(false);
            //transform.GetChild(4).gameObject.SetActive(true);
        }
        else if (playHP == 0)
        {
            ChangePlayerHp(0, 3);
            //transform.GetChild(0).gameObject.SetActive(false);
            //transform.GetChild(3).gameObject.SetActive(true);
        }
        else{}

    }

    public void LiveChangeHp()
    {
        playHP = GameManager.Instance.Player.MaxHP;
        if (playHP == 3)
        {
            ChangePlayerHpUp(2, 5);
        }
        else if (playHP == 2)
        {
            ChangePlayerHpUp(1, 4);
        }
    }

    private void ChangePlayerHpUp(int LiveHp, int DieHp)
    {
        transform.GetChild(LiveHp).gameObject.SetActive(true);
        transform.GetChild(DieHp).gameObject.SetActive(false);
    }

    private void ChangePlayerHp(int LiveHp, int DieHp)
    {
        transform.GetChild(LiveHp).gameObject.SetActive(false);
        transform.GetChild(DieHp).gameObject.SetActive(true);
    }

    public void startHP()
    {
        int des = 0;
        while (des < 6)
        {
            if(des < 6)
            {
                transform.GetChild(des).gameObject.SetActive(false);
                des++;
            }
        }//모든 HP모양을 삭제
        for (int i = 0; i < 3; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        for (int n = 3; n < 6; n++)
        {
            transform.GetChild(n).gameObject.SetActive(false);
        }

    }
}
