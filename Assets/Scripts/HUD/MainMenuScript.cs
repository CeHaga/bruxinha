using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour{
    [SerializeField] private Button primaryButton;
    void Start(){
        primaryButton.Select();
    }
    public void PlayGame(){
        SceneManager.LoadScene("SampleScene");
    }

    public void Settings(){
        Debug.Log("Ajustes");
    }

    public void QuitGame(){
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
