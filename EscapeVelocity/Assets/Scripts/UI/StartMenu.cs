using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartMenu : MonoBehaviour
{
    [SerializeField] private string _mainScene;
    private void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene(_mainScene);
        }
    }
}

