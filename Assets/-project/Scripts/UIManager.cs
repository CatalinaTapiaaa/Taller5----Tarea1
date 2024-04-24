using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject panelPause;
    public int actualLevel;

    void Update()
    {
        if (!GameManager.stopPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
        }        
    }
    void Pause()
    {
        Time.timeScale = 0;
        panelPause.SetActive(true);
    }
    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(actualLevel);
    }
    public void Continue()
    {
        Time.timeScale = 1;
        panelPause.SetActive(false);
    }
}
