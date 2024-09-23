using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour
{
    [SerializeField] GameObject trapp;
    [SerializeField] GameObject fildtrap;
    [SerializeField] Canvas BossHP;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if(collision.gameObject.tag == "Player")
        {
            trapp.SetActive(false);
            fildtrap.SetActive(true);
            GameManager.Instance.Boss.bossstart = true;
            BossHP.gameObject.SetActive(true);
        }
    }

}
