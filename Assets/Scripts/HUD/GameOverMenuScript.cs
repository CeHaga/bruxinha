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

    public void Restart(){
		AudioManager.Instance.Play("SFX_ButtonClick");
        Time.timeScale = 1f;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void GoToMenu(){
		AudioManager.Instance.Play("SFX_ButtonClick");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Menu");
    }
}
