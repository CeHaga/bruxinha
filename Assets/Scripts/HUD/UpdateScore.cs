using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpdateScore : MonoBehaviour
{
    [SerializeField]
    private int score;

    [SerializeField]
    private Image[] digits;

    [SerializeField]
    private UpdateScoreCountEvent onHighscoreSave;

    public void AddScore(int value)
    {
        score += value;
        score = Mathf.Clamp(score, 0, 999999999);
        UpdateText();
    }

    public void UpdateText()
    {
        int newScore = score;
        for (int i = 9; i >= 0; i--)
        {
            int digit = newScore % 10;
            newScore /= 10;
            digits[i].sprite = GlobalValues.instance.digits[digit].sprite;
        }
    }

    public void SaveHighscore()
    {
        Debug.Log("Death: " + score);
        onHighscoreSave?.Invoke(score);
    }
}
