using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour{
    public static bool GameIsPaused = false;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private Button primaryButton;
    [SerializeField] private PauseEvent pauseEvent;

    public void Resume(){
        pauseEvent.Invoke(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause(){
        pauseEvent.Invoke(true);
        pauseMenuUI.SetActive(true);
        primaryButton.Select();
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void GoToMenu(){
        GameIsPaused = false;
        //Time.timeScale = 1f;
        //SceneManager.LoadScene("MainMenu");
        Debug.Log("Menu");
    }

    public void OnMenuStartOpen(InputAction.CallbackContext context){
        if(!GameIsPaused){
            if(context.performed){
                Pause();
            }
        }
    }

}
