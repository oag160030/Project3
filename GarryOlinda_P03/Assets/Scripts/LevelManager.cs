using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    void Update()
    {
        // 'Escape' exits the program
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Exit Program");
            Application.Quit();
        }
    }
}
