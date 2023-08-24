using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateLife : MonoBehaviour
{
    [SerializeField] private Image[] digits;

    public void UpdateText(int count)
    {
        int number1 = count / 10;
        int number2 = count % 10;
        Debug.Log("Life number1: " + count / 10 + " number2: " + count % 10);
        digits[0].sprite = GlobalValues.instance.digits[number1].sprite;
        digits[1].sprite = GlobalValues.instance.digits[number2].sprite;
    }
}
