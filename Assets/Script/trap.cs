using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour
{
    [SerializeField] GameObject trapp;
    [SerializeField] GameObject fildtrap;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if(player)
        {
            trapp.SetActive(false);
            fildtrap.SetActive(true);
        }
    }

    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
