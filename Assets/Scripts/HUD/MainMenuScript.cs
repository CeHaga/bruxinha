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
    void Start(){
        primaryButton.Select();
    }
    public void PlayGame(){
		AudioManager.Instance.Play("SFX_ButtonClick");
        SceneManager.LoadScene("GameScene");
    }

    public void Settings(){
		AudioManager.Instance.Play("SFX_ButtonClick");
        Debug.Log("Ajustes");
    }

    public void Credits(){
		AudioManager.Instance.Play("SFX_ButtonClick");
        creditsButton.Select();
        CreditsMenu.SetActive(true);
        Debug.Log("Créditos");
    }

    public void CreditsToMenu(){
		AudioManager.Instance.Play("SFX_ButtonClick");
        primaryButton.Select();
        CreditsMenu.SetActive(false);
    }

    public void QuitGame(){
		AudioManager.Instance.Play("SFX_ButtonClick");
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
