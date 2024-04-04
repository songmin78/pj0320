using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    

    private Player player;
    private Weaponcheck weaponcheck;
    public Player Player
    {
        get { return player; }
        set { player = value; }
    }

    public Weaponcheck Weaponcheck
    {
        get { return weaponcheck; }
        set { weaponcheck = value; }
    }



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
