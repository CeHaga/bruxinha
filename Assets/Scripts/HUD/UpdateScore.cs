using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScore : MonoBehaviour
{
    [SerializeField] private Image[] digits;

    public void UpdateText(int score)
    {
        for (int i = 9; i >= 0; i--)
        {
            int digit = score % 10;
            score /= 10;
            digits[i].sprite = GlobalValues.instance.digits[digit].sprite;
        }
    }

}
