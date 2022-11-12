using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject gameOverPanel;
    // Start is called before the first frame update
    void GameOver()
    {
        gameOverPanel.SetActive(true);
    }
    
    
}
