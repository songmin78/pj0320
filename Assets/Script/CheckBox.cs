using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBox : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);

            transform.GetChild(0).gameObject.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);

            transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);

            transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);

            transform.GetChild(2).gameObject.SetActive(true);
        }
    }
}
