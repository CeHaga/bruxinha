using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreController : MonoBehaviour
{
    [SerializeField]
    private GameObject background;

    [SerializeField]
    private UpdateHighscore currentScoreCounter;

    [SerializeField]
    private UpdateHighscore highscoreCounter;

    public void SaveHighscore(int score)
    {
        Debug.Log("Highscore: " + score);
        int highscore = PlayerPrefs.GetInt("highscore", 0);
        if (score > highscore)
        {
            PlayerPrefs.SetInt("highscore", score);
            highscore = score;
        }

        background.SetActive(true);
        currentScoreCounter.gameObject.SetActive(true);
        highscoreCounter.gameObject.SetActive(true);

        currentScoreCounter.UpdateText(score);
        highscoreCounter.UpdateText(highscore);
    }
}
