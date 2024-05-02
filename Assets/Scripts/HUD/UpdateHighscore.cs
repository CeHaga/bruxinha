using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHighscore : MonoBehaviour
{
    [SerializeField]
    private Image[] digits;

    public void UpdateText(int score)
    {
        int newScore = score;
        for (int i = 9; i >= 0; i--)
        {
            int digit = newScore % 10;
            newScore /= 10;
            digits[i].sprite = GlobalValues.instance.highscoreDigits[digit].sprite;
        }
    }
}
