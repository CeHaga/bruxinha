using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private Button primaryButton;
    [Header("Credits")]
    [SerializeField] private Button creditsButton;
    [SerializeField] private GameObject CreditsMenu;
    void Start()
    {
        primaryButton.Select();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Settings()
    {
        Debug.Log("Ajustes");
    }

    public void Credits(){
        creditsButton.Select();
        CreditsMenu.SetActive(true);
        Debug.Log("Créditos");
    }

    public void CreditsToMenu(){
        primaryButton.Select();
        CreditsMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
