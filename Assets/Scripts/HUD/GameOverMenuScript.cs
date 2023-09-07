using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenuScript : MonoBehaviour{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Button primaryButton;
    [SerializeField] private PauseEvent pauseEvent;

    public void Resume(){
        pauseEvent.Invoke(false);
        gameOverUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OpenRestartScreen(){
        pauseEvent.Invoke(true);
        gameOverUI.SetActive(true);
        primaryButton.Select();
        Time.timeScale = 0f;
    }

    public void GoToMenu(){
        //Time.timeScale = 1f;
        //SceneManager.LoadScene("MainMenu");
        Debug.Log("Menu");
    }
}
