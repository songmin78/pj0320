using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialButton : MonoBehaviour
{
    [SerializeField] Button nextbutton;
    [SerializeField] Button returnbutton;


    [SerializeField] GameObject tutorial;
    [SerializeField] GameObject bow;
    [SerializeField] GameObject sword;
    [SerializeField] GameObject magic;
    [SerializeField] GameObject reurnbuttons;

    float passcheck = 0;
    [SerializeField]bool tapon;

    private void Awake()
    {
        nextbutton.onClick.AddListener(() =>
        {
            if(passcheck == 0)
            {
                passcheck = 1;
                bow.SetActive(false);
                sword.SetActive(true);
                reurnbuttons.SetActive(true);
            }
            else if(passcheck == 1)
            {
                passcheck = 2;
                sword.SetActive(false);
                magic.SetActive(true);
            }
        });

        returnbutton.onClick.AddListener(() =>
        {
            if(passcheck == 1)
            {
                passcheck = 0;
                sword.SetActive(false);
                bow.SetActive(true);
                reurnbuttons.SetActive(false);
            }
            else if(passcheck == 2)
            {
                passcheck = 1;
                magic.SetActive(false);
                sword.SetActive(true);
            }
        });
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            tapon = true;
            //tutorial.SetActive(true);
        }
        if (tapon == true)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                tutorial.SetActive(true);
            }
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            tapon = false;
        }
    }
}
